using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Saramago : MonoBehaviour
{
    private Vector3 direction = Vector3.left;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject playerObject;

    public FifthController fifthController;
    private int ending;

    //dialogue
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public string nameString;
    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public Image NPCImage;
    public Sprite playerPic;
    public Sprite profilePic;
    public GameObject curtain;
    public GameObject curtain_2;
    public GameObject clue;

    //dialogue player
    public GameObject dialoguePanelPlayer_1;
    public GameObject dialoguePanelPlayer_2;
    private bool playerTalking; 
    private int indexPlayer_1;
    private int indexPlayer_2;
    public string[] dialoguePlayer_1;
    public string[] dialoguePlayer_2;
    public TextMeshProUGUI dialogueTextPlayer_1;
    public TextMeshProUGUI dialogueTextPlayer_2;
    public GameObject contButtonPlayer_1;
    public GameObject contButtonPlayer_2;
    private bool startBool;
    private int playerChoice_1;
    private int playerChoice_2;
    private int playerChoice_3;
    private bool endDialogue;
    



    void Start()
    {
        endDialogue = false;
        playerChoice_1 = 0;
        playerChoice_2 = 0;
        playerChoice_3 = 0;
        startBool = true;
        playerTalking = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindGameObjectWithTag("Hat");
    }


    void Update(){
        LookAtPlayer();
        DialogueFunction();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hat")){
            playerIsClose = true;
            clue.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Hat")){
            playerIsClose = false;
            clue.SetActive(false);
        }
    }


    void LookAtPlayer(){
        if (playerObject.transform.position.x < transform.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    void DialogueFunction(){
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose){
            clue.SetActive(false);
            playerObject.GetComponent<HatController>().FemoralNerve(true);
            dialogueText.color = UnityEngine.Color.white;
            nameText.text = nameString;
            NPCImage.sprite = profilePic; 

            if(dialoguePanel.activeInHierarchy){
                zeroText();
            }else{
                dialoguePanel.SetActive(true);
                contButton.SetActive(false);
                StartCoroutine(Typing());
            }
        }

        if(playerTalking){
            NPCImage.sprite = playerPic; 
            if(dialogueTextPlayer_1.text == dialoguePlayer_1[indexPlayer_1] && dialogueTextPlayer_2.text == dialoguePlayer_2[indexPlayer_2]){
                contButtonPlayer_1.SetActive(true);
                contButtonPlayer_2.SetActive(true);
                if(playerChoice_2 == 0){
                    indexPlayer_1 = 0;
                    indexPlayer_2 = 0;
                }else if(playerChoice_3 == 0){
                    indexPlayer_1 = 2;
                    indexPlayer_2 = 2;
                }
            }

        }else{
            NPCImage.sprite = profilePic; 
            if(dialogueText.text == dialogue[index]){
                contButton.SetActive(true);
                if(playerChoice_1 == 0){
                    index = 0;
                }else if(playerChoice_2 == 0){
                    index = 2;
                }else if (playerChoice_3 == 0){
                    ending = index;
                    index = 6;
                    fifthController.PostEnding(ending);
                    endDialogue = true;
                }
                  
            }
        }


    }

    public void zeroText(){
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

    }

    IEnumerator Typing(){
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }




    public void NextLine(){

        playerTalking = false;
        contButton.SetActive(false);
        contButtonPlayer_1.SetActive(false);
        contButtonPlayer_2.SetActive(false);

        if (index < dialogue.Length - 1){
            switch(index){
                case 0:
                    if(playerChoice_1 == 1){
                        index = 1;
                    }else if (playerChoice_1 == 2){
                        index = 2;
                    }
                    break;
                case 2:
                    if(playerChoice_1 == 1){
                        if(playerChoice_2 == 1){
                            index = 3;
                        }else if (playerChoice_2 == 2){
                            index = 4;
                        }
                    }else if (playerChoice_1 == 2){
                        if(playerChoice_2 == 1){
                            index = 5;
                        }else if (playerChoice_2 == 2){
                            index = 6;
                        }
                    }
                    break;    
            }   

            dialogueTextPlayer_1.text = "";
            dialogueTextPlayer_2.text = "";
            StartCoroutine(Typing());
        }else{
            zeroText();
        }
    }

    public void NextLinePlayer(){
        playerTalking = true;
        contButton.SetActive(false);
        contButtonPlayer_1.SetActive(false);
        contButtonPlayer_2.SetActive(false);
        if(startBool){
            dialogueText.text = "";
            StartCoroutine(TypingPlayer_1());
            StartCoroutine(TypingPlayer_2());
            startBool = false;
        }else{
            NextLinePlayer_1();
            NextLinePlayer_2();
        }        
    }

    public void NextLinePlayer_1(){
        if (indexPlayer_1 < dialoguePlayer_1.Length - 1 && !endDialogue){
            switch(indexPlayer_1){
                case 0:
                    if(playerChoice_1 == 1){
                        indexPlayer_1 = 1;
                    }else if (playerChoice_1 == 2){
                        indexPlayer_1 = 2;
                    }
                    break;
            }
            dialogueText.text = "";
            StartCoroutine(TypingPlayer_1());
        }else{
            zeroTextPlayer_1();
        }
    }

    public void NextLinePlayer_2(){
       
        if (indexPlayer_2 < dialoguePlayer_2.Length - 1 && !endDialogue){

            switch(indexPlayer_2){
                case 0:
                    if(playerChoice_1 == 1){
                        indexPlayer_2 = 1;
                    }else if (playerChoice_1 == 2){
                        indexPlayer_2 = 2;
                    }
                    break;
            }

            dialogueText.text = "";
            StartCoroutine(TypingPlayer_2());
        }else{
            zeroTextPlayer_2();
        }
    }

    //if true (1) || false (2)
    public void PlayerChoice(int choice){
        switch(index){
            case 0:
                playerChoice_1 = choice;
                break;
            case 2:
                playerChoice_2 = choice;
                break;
            case 6:
                playerChoice_3 = choice;
                break;   
        }
    }

    
    public void zeroTextPlayer_1(){
        if(endDialogue){
            curtain_2.SetActive(true);
        }else{
            dialogueTextPlayer_1.text = "";
            indexPlayer_1 = 0;
            dialoguePanelPlayer_1.SetActive(false);
        }
    }

    IEnumerator TypingPlayer_1(){
        foreach (char letter in dialoguePlayer_1[indexPlayer_1].ToCharArray())
        {
            dialogueTextPlayer_1.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void zeroTextPlayer_2(){
        if(endDialogue){
            curtain_2.SetActive(true);
        }else{
            dialogueTextPlayer_2.text = "";
            indexPlayer_2 = 0;
            dialoguePanelPlayer_2.SetActive(false);
        }
    }

    IEnumerator TypingPlayer_2(){
        foreach (char letter in dialoguePlayer_2[indexPlayer_2].ToCharArray())
        {
            dialogueTextPlayer_2.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

}
