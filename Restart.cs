using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Restart : MonoBehaviour {
	public void RestartGame() {
		Application.LoadLevel (Application.loadedLevel);
	}
}
