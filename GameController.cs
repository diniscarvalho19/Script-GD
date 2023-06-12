using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public Text timerText;
	public GameObject startScreen;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject startButton;
	public HatController hat_Controller;
	public SecondController second_Controller;
	public AudioPlayer audioController;
	

	private float maxWidth;
	private bool playing;
	private int ballCount;
	private bool setActive = true;

	// Use this for initialization
	void Start () {

		timerText.color = UnityEngine.Color.black;

		if (cam == null) {
			cam = Camera.main;
		}
		playing = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x-ballWidth;
		//StartCoroutine (Spawn ());
		UpdateText ();
	}

	void FixedUpdate () {
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			UpdateText ();
		}
	}

	public void StartGame () {	
		if(!ReadFileContents()){
			audioController.Music_1(true);
		}else{
			audioController.Music_2(true);
			audioController.Crickets(true);
			audioController.SetVolumeCrickets(0);
			audioController.WalkPlanet(true);
			audioController.SetVolumeWalkPlanet(0);
		}
		startButton.SetActive (false);
		startScreen.SetActive (false);
		playing = true;
		hat_Controller.toggledControl (true);
		StartCoroutine (Spawn ());
	}

	public void BallCountUpdate () {
		ballCount--;
	}

	IEnumerator Spawn () {

		//yield return new WaitForSeconds (2.0f);
		while (timeLeft > 0) {
			int rand = Random.Range (1, 6);
			while(rand>0){
				GameObject ball = balls[Random.Range (0, balls.Length)];
				Vector3 spawnPosition = new Vector3 (
					Random.Range (2.0f, 38.0f), 
					-2.0f, 
					0.0f
				);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (ball, spawnPosition, spawnRotation);
				ballCount++;
				rand--;
				yield return new WaitForSeconds (Random.Range (0.3f, 0.5f));
			}
			yield return new WaitForSeconds (Random.Range (0.0f, 2.0f)); //Wait for 1 or 2 seconds & go for the loop again
		}
		//yield return new WaitForSeconds(2.0f);
			

		
		if(!ReadFileContents()){
			gameOverText.SetActive (true);
			yield return new WaitForSeconds(0.5f);
			restartButton.SetActive (true);
			startScreen.SetActive (true);
			writeFile("true");
		}else{
			second_Controller.toggleLevel();
			writeFile("true");
			while (setActive) {
			int rand = Random.Range (15, 30);
			while(rand>0){
				GameObject ball = balls[Random.Range (0, balls.Length)];
				Vector3 spawnPosition = new Vector3 (
					Random.Range (4.0f, 121.0f), 
					-2.0f, 
					0.0f
				);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (ball, spawnPosition, spawnRotation);
				ballCount++;
				rand--;
				yield return new WaitForSeconds (Random.Range (0.2f, 0.3f));
			}
			yield return new WaitForSeconds (Random.Range (0.0f, 2.0f)); //Wait for 1 or 2 seconds & go for the loop again
		}
	}
		
		
	} 

	void UpdateText () {
		timerText.text = "TIME LEFT: " + Mathf.RoundToInt (timeLeft);
	}

void writeFile(string data)
{
    // Create a new file named "data.txt" in the project's root directory if it doesn't exist
    string filePath = Application.dataPath + "/data.txt";
    if (!File.Exists(filePath))
    {
        File.Create(filePath).Dispose();
    }

    // Clear the file contents
    File.WriteAllText(filePath, "");

    // Write the input data to the file
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        writer.WriteLine(data);
    }
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

	public void Stop(bool key){
		setActive = !key;
	}




}

 