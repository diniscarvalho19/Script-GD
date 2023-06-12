using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Explode : MonoBehaviour {


	
	public GameObject explosion;
	public ParticleSystem[] effects;
	public AudioClip catchSFX;
	public AudioClip breakSFX;

    private  AudioSource audioSource;
	private Collider2D fovCollider;
	private GameObject fovObject;
	private Collider2D playerCollider;
	//private GameObject fakeFovObject;
	private GameObject audioObject;
	private GameObject timerObject;

	private string timerString;

	//score
	private int ballValue = 20;
	private int score;
	private bool trans = true;
	private bool stopTrans = false;

	private int timeLeft;

	private Renderer renderer;

    private void Start()
    {
        // Get the renderer component
		fovObject = GameObject.Find("Fov");
		if(fovObject != null){
			fovCollider = fovObject.GetComponent<Collider2D>();  
		}
		playerCollider = GameObject.Find("Hat").GetComponent<Collider2D>();  
        renderer = GetComponent<Renderer>();
		audioObject = GameObject.Find("Hat");
        audioSource = audioObject.GetComponent<AudioSource>();
		timerObject = GameObject.Find("Timer");
		Text timerText = timerObject.GetComponentInChildren<Text>();
		timerString = timerText.text;
    }

	private void Update()
    {
		StopTransFunction();
		if(!stopTrans){
			SetTransparent(trans);
			trans = !trans;	
		}
			
    }
	

	void OnCollisionEnter2D (Collision2D collision) {
		if (timeLeft > 0)
		{
			if (collision.gameObject.tag == "Fov") {
				Physics2D.IgnoreCollision(fovCollider, GetComponent<Collider2D>());	
				SetTransparent(false);
				stopTrans = true;
			}else if (collision.gameObject.tag == "Hat") {
				audioSource.clip = catchSFX;
				audioSource.Play();
				UpdateScore (ballValue);
				fovObject.transform.localScale += new Vector3(1.0f, 1.0f, 0);	
				Destroy (gameObject);			
			}else if (collision.gameObject.tag == "Floor") {
				Instantiate (explosion, transform.position, transform.rotation);
				foreach (var effect in effects) {
					effect.transform.parent = null;
					effect.Stop ();
					Destroy (effect.gameObject, 1.0f);
				}
				audioSource.PlayOneShot(breakSFX);
				UpdateScore (-ballValue * 2);
				GameObject.Find("Fov").transform.localScale -= new Vector3(1.0f, 1.0f, 0.0f);
				fovObject.transform.localScale = Vector3.Max(fovObject.transform.localScale, new Vector3(8.0f, 8.0f, 0.0f));
				Destroy (gameObject);
			}	
		}else{
			if(collision.gameObject.tag == "Hat"){
				Physics2D.IgnoreCollision(playerCollider, GetComponent<Collider2D>());
			}else if (collision.gameObject.tag == "Fascist") {
				audioSource.clip = catchSFX;
				audioSource.Play();
				Destroy (gameObject);
			}else if (collision.gameObject.tag == "Floor") {
				Instantiate (explosion, transform.position, transform.rotation);
				foreach (var effect in effects) {
					effect.transform.parent = null;
					effect.Stop ();
					Destroy (effect.gameObject, 1.0f);
				}
				audioSource.PlayOneShot(breakSFX);
				Destroy (gameObject);
			}
		}
		



	}

	void UpdateScore (int value) {
		GameObject scoreObject = GameObject.Find("Score");
		string scoreString = scoreObject.GetComponent<Text>().text;
		int scoreIndex = scoreString.IndexOf(":") + 2; // Find the index of the newline character and add 1 to get the start of the score
		int scoreLength = scoreString.IndexOf("$") - scoreIndex; // Find the length of the score
		string scoreOnlyString = scoreString.Substring(scoreIndex, scoreLength); // Extract the score as a string
		int scoreOnly = int.Parse(scoreOnlyString); // Convert the score string to an integer
		score = scoreOnly + value;
		scoreObject.GetComponent<Text>().text = "TOTAL SCORE: " + score + "$";
	}

	private void SetTransparent(bool transparent)
    {
        // Get the current color
        Color color = renderer.material.color;

        // Set the alpha value based on the transparent parameter
        color.a = transparent ? 0f : 1f;

        // Set the new color
        renderer.material.color = color;
    }

	 bool ReadFileContents()
    {
        // Open the file named "example.txt" in the project's root directory
        string filePath = Application.dataPath + "/data.txt";
        StreamReader reader = new StreamReader(filePath);

        // Read the contents of the file
        string fileContents = reader.ReadToEnd();

        // Close the file
        reader.Close();

        // Check the file contents and return true or false
        bool result = (fileContents.Trim().ToLower() == "true");
        
        return result;
    }

	void StopTransFunction(){
				
		string[] strArr = timerString.Split(": ");
		timeLeft = int.Parse(strArr[1]);

		if(timeLeft == 0){
			stopTrans = true;
			IgnoreCollisionsWithTag();
		}
	}

	void IgnoreCollisionsWithTag() {  

        GameObject objectPlayer = GameObject.FindGameObjectWithTag("Hat");
        GameObject objectFov = GameObject.FindGameObjectWithTag("Fov");
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Coin");
        for (int i = 0; i < objectsWithTag.Length; i++) {
            Collider2D collider1 = objectsWithTag[i].GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(objectPlayer.GetComponent<Collider2D>(), collider1, true);
            Physics2D.IgnoreCollision(objectFov.GetComponent<Collider2D>(), collider1, true);
        }
    }

}
