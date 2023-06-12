using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDetector : MonoBehaviour
{
    public FourthController FourthController;

    private int days = 1;

    private bool td_1 = false;

    public void activateTD1(){
        td_1 = true;
    }

    public void activateTD2(){
        if(td_1 == true){
            days++;
            FourthController.changeDay();
            
        }
        td_1 = false;
    }

}
