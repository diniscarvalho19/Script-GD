using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdController : MonoBehaviour
{
    public SecondController second_Controller;
    public GameController game_Controller;

    private bool startLevel = false;

    private bool fov = true;


    public void StartThirdLevel(){
        startLevel = true;            
    }



    void FixedUpdate() {
        if(startLevel){
            second_Controller.enabled = false;
            game_Controller.Stop(true);
            DisableScriptsOnTaggedObjects("Fascist");  
            DisableFOV();
        }
    }

    void DisableScriptsOnTaggedObjects(string tag) {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject taggedObject in taggedObjects) {
            taggedObject.GetComponent<RandomWalker>().Shush();
            taggedObject.GetComponent<RandomWalker>().enabled = false;
        }
    }

    void DisableFOV(){

        if(fov){
            fov = false;
            GameObject objectWithTag = GameObject.FindGameObjectWithTag("Fov");
            if (objectWithTag != null)
            {
                objectWithTag.SetActive(false);
            }
        }

    }


}
