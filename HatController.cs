using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HatController : MonoBehaviour {

	public Camera cam;
	public GameObject fov;
	public bool canControl;
	public float speed = 25f; 
	public ThirdController thirdController;
	private Animator animator;
	private bool hasActivated = false;
	public LayerMask groundLayer;
	public LayerMask planetLayer;
	public float radius = 50f;
	private bool paralysed = false;
	public AudioPlayer audioController;
	
	// Use this for initialization
	void Start () {
	
		animator = GetComponent<Animator>();
		if (cam == null) {
			cam = Camera.main;
		}
		canControl = false;
		SetTransparent(true);
		GameObject fovObject = GameObject.FindGameObjectWithTag("Fov");
		SetTransparentFov(fovObject,true);
		
	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {

		

		if (canControl) {
			if(transform.position.y < -14.0f){
				audioController.SetVolumeWalkOffice(0);
				if(transform.position.y <= -129){
					audioController.SetVolumeMusic_2(audioController.GetVolumeMusic_2(transform.position.y));	
					audioController.SetVolumeCrickets(audioController.GetVolumeCrickets(transform.position.y));	
				}
				
				float horizontalInput = Input.GetAxisRaw("Horizontal");
				
				if (!paralysed && horizontalInput != 0)
				{
					audioController.SetVolumeWalkPlanet(0.2f);
					// Get the angle that the player is facing
					float angle = Mathf.Deg2Rad * transform.eulerAngles.z;

					// Calculate the movement direction based on the angle
					Vector2 movement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

					animator.SetFloat("Horizontal", horizontalInput);
					animator.SetFloat("Speed", movement.sqrMagnitude);

					// Apply the movement to the Rigidbody2D
					GetComponent<Rigidbody2D>().velocity = movement * speed * horizontalInput;
				}
				else
				{
					audioController.SetVolumeWalkPlanet(0);
					// Stop the player if no input is detected
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					animator.SetFloat("Speed", 0f);
				}


			}else{
				float horizontalInput = Input.GetAxisRaw("Horizontal");
				
				if (!paralysed && horizontalInput != 0)
				{
					audioController.SetVolumeWalkOffice(1);
					// Get the angle that the player is facing
					float angle = Mathf.Deg2Rad * transform.eulerAngles.z;

					// Calculate the movement direction based on the angle
					Vector2 movement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

					animator.SetFloat("Horizontal", horizontalInput);
					animator.SetFloat("Speed", movement.sqrMagnitude);

					// Apply the movement to the Rigidbody2D
					GetComponent<Rigidbody2D>().velocity = movement * speed * horizontalInput;
				}
				else
				{
					audioController.SetVolumeWalkOffice(0);
					// Stop the player if no input is detected
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					animator.SetFloat("Speed", 0f);
				}
			}


			if(transform.position.y < -20f){
				ActivateOnce();
			}
			
		}
	}


	public void toggledControl (bool toggle) {
		canControl = toggle;
		SetTransparent(false);
		GameObject fovObject = GameObject.FindGameObjectWithTag("Fov");
		SetTransparentFov(fovObject,false);
	}

	private void SetTransparent(bool transparent)
    {
        // Get the current color
        Color color = GetComponent<Renderer>().material.color;

        // Set the alpha value based on the transparent parameter
        color.a = transparent ? 0f : 1f;

        // Set the new color
        GetComponent<Renderer>().material.color = color;
    }


	void SetTransparentFov(GameObject obj, bool isTransparent)
	{
		// Get the material of the game object
		Material mat = obj.GetComponent<Renderer>().material;

		// Set the alpha value of the material to 0 if transparent, or 1 if opaque
		Color col = mat.color;
		col.a = isTransparent ? 0.0f : 1.0f;
		mat.color = col;

	}

	void ActivateOnce() {
		if (!hasActivated) {
			thirdController.StartThirdLevel();
			hasActivated = true;
		}
	}

	public void FemoralNerve(bool toggle) {
		paralysed = toggle;
	}

	public void SetSpeed(float speedInput) {
		speed = speedInput;
	}





}
