using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TD_1 : MonoBehaviour
{
    public bool td_1 = true;
    public TimeDetector TimeDetector;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(td_1){
            TimeDetector.activateTD1();
        }else{
            TimeDetector.activateTD2();
            
        }   
    }
}
