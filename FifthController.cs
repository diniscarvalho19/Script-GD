using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FifthController : MonoBehaviour
{


    public GameObject SaramagoPanel;
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody2D;
    public float delayInSeconds = 4f;
    public TextMeshPro txt;
    public GameObject curtain;
    public Attractor attractor;
    public Attractable attractable;
    private int ending;
    private bool[] npcArray = new bool[4];
    private int talkedCount = 0;
    //npc text
    public TextMeshPro npcTitle;
    public TextMeshPro npc_1;
    public TextMeshPro npc_2;
    public TextMeshPro npc_3;
    public TextMeshPro npc_4;
   

    public void PostEnding(int endingResult){
        ending = endingResult;
        Invoke("Mockups", delayInSeconds);  
    }

    private void Mockups(){
        SaramagoPanel.SetActive(false);
        curtain.SetActive(true);
        attractor.TheEnd();
        playerRigidbody2D.mass = 1f;
        Vector2 newPosition = new Vector2(-475, -16);
        playerTransform.position = newPosition;
        Vector3 newRotation = playerTransform.rotation.eulerAngles;
        newRotation.z = 0f;
        playerTransform.rotation = Quaternion.Euler(newRotation);
        attractable.enabled = false;
        playerRigidbody2D.gravityScale = 75f;
        txt.SetText("Ending #" + (ending - 2).ToString() + "/4");
        NpcTextList();
    }

    public void NpcCheck(int npc){
        npcArray[npc] = true;
        talkedCount++;
    }

    public void NpcTextList(){
        talkedCount = 3;
        npcTitle.SetText("You've talked with " + talkedCount.ToString() + "/4 NPCs:");
        if(!npcArray[0])
            npc_1.SetText("Old Guy: \u2713");
        if(!npcArray[1])
            npc_2.SetText("Eye Patch: \u2713");
        if(npcArray[2])
            npc_3.SetText("Marcelo: \u2713");
        if(!npcArray[3])
            npc_4.SetText("Doctor: \u2713");
    }


   

}
