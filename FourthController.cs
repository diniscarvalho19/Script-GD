using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FourthController : MonoBehaviour
{

    private int daysNumber = 1;


    public GameObject NPC_1;
    public GameObject NPC_2;
    public GameObject NPC_3;
    public GameObject NPC_4;
    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Q4;
    public GameObject Saramago;

    public TextMeshPro txt;

    private string newText;

    public void changeDay(){
        //daysNumber++;
        fadeText();
        daysNumber = 5;
        switch(daysNumber){
            case 2:
                NPC_1.SetActive(false);
                Q1.SetActive(false);
                NPC_2.SetActive(true);
                Q2.SetActive(true);
                break;
            case 3:
                NPC_2.SetActive(false);
                Q2.SetActive(false);
                NPC_3.SetActive(true);
                Q3.SetActive(true);
                break;
            case 4:
                NPC_3.SetActive(false);
                Q3.SetActive(false);
                NPC_4.SetActive(true);
                Q4.SetActive(true);
                break;         
            case 5:
                NPC_4.SetActive(false);
                Q4.SetActive(false);
                Saramago.SetActive(true);
                break;                    
        }
    }


    void fadeText(){

        switch(daysNumber)
        {
            case 2:
                newText = "2nd";
                break;
            case 3:
                newText = "3rd";
                break;
            case 4:
                newText = "4th";
                break;
            case 5:
                newText = "5th";
                break;
            case 6:
                newText = "6th";
                break;
            case 7:
                newText = "7th";
                break;                    
        }

        txt.SetText(newText);

        //image.Activate();

        //changeText.SetActive(false);
        //dontChangeText.SetActive(false);

        //changeText.SetActive(true);
        //dontChangeText.SetActive(true);
    }

}
