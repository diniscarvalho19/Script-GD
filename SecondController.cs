using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SecondController : MonoBehaviour
{

    public GameObject CanvasTimer;
    public GameObject CanvasScore;
    public GameObject FOV;

    private void Start() {
        IgnoreCollisionsWithTag();
    }




    public void toggleLevel () {
        
        GameObject wallObject = GameObject.FindGameObjectWithTag("Wall");
        Destroy(wallObject);

        
        FOV.GetComponent<Rigidbody2D>().isKinematic = true;
        FOV.GetComponent<Collider2D>().enabled = false;

        FOV.GetComponent<MoveRight>().enabled = true;


   

        CanvasTimer.SetActive(false);
        CanvasScore.SetActive(false);

        CameraFollow myScript = Camera.main.GetComponent<CameraFollow>();

        // Check if the script component exists
        if (myScript != null)
        {
            // Call a public method in the script component to activate it
            myScript.Activate();
        }
	}


    void IgnoreCollisionsWithTag() {  

        GameObject objectPlayer = GameObject.FindGameObjectWithTag("Hat");
        GameObject objectFov = GameObject.FindGameObjectWithTag("Fov");
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Fascist");

        for (int i = 0; i < objectsWithTag.Length; i++) {
            Collider2D collider1 = objectsWithTag[i].GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(objectPlayer.GetComponent<Collider2D>(), collider1, true);
            Physics2D.IgnoreCollision(objectFov.GetComponent<Collider2D>(), collider1, true);

            for (int j = i + 1; j < objectsWithTag.Length; j++) {
                Collider2D collider2 = objectsWithTag[j].GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(collider1, collider2, true);
            }
        }
    }


}
