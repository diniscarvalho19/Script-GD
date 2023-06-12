using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // The offset distance between the player and camera

    public float cameraHeight = 5.78f;

    private bool activeFollow = false;
    private bool activeRotate = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Get a reference to the main camera
    }
    
    public void Activate(){
        activeFollow = true;
    }

    public void ActivateRotate(){
        activeRotate = true;
    }


    void Update()
    {
        if(activeFollow && player.position.x > 20.5f && !activeRotate){
            transform.position = player.position + offset;
        }

        else if(activeRotate){
            
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z;  
            newPosition.y = player.position.y + cameraHeight * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
            newPosition.x = player.position.x - cameraHeight * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
            transform.position = newPosition;
            Vector3 playerForward = player.transform.right;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(playerForward.y, playerForward.x) * Mathf.Rad2Deg);
            mainCamera.orthographicSize = 11.0f;
    
        }
        
    }
}