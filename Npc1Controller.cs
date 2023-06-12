using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Npc1Controller : MonoBehaviour
{
    private Vector3 direction = Vector3.left;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject playerObject;
    public AudioPlayer audioController;
    

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
    public Sprite profilePic;
    public GameObject interactClue;
    public HatController hatController;
    public FifthController fifthController;

    //memorys
    //object_1
    public GameObject gameObjectReal_1;
    public FadeInEffect gameObjectMemory_1;
    private string wordToHighlight;
    private bool objectActive;
    private bool talking = false;



    void Start()
    {
        objectActive = false;
        wordToHighlight = "";
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindGameObjectWithTag("Hat");
    }


    void Update(){
        LookAtPlayer();
        Skip();
        DialogueFunction();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hat")){
            playerIsClose = true;
            interactClue.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Hat")){
            playerIsClose = false;
            zeroText();
            interactClue.SetActive(false);
            //audioController.FadeInMusic_2();
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
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose && !talking){
            wordSpeed = 0.03f;
            talking = true;
            EndingStats();
            hatController.canControl = false;
            //audioController.FadeOutMusic_2();
            interactClue.SetActive(false);
            dialogueText.color = UnityEngine.Color.black;
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

        if(dialogueText.text == dialogue[index]){
            HighlightWord();
        }
    }

    public void zeroText(){
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        hatController.canControl = true;
        //audioController.FadeInMusic_2();
    }

    IEnumerator Typing(){

        if(nameString == "Old Guy"){
            audioController.OldMan();
        }else if(nameString == "Eye Patch"){
            audioController.EyePatch();
        }else if(nameString == "Sunglasses Girl"){
            audioController.Marcelo();
        }else if(nameString == "Doctor"){
            audioController.Doctor();
        }


        StoryTeller();
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    private void HighlightWord(){
        if (dialogueText.text.Contains(wordToHighlight) && !string.IsNullOrEmpty(wordToHighlight)){
            string originalText = dialogueText.text;
            string highlightedText = $"<b><color=red>{wordToHighlight}</color></b>";
            string newText = originalText.Replace(wordToHighlight, highlightedText);
            dialogueText.SetText(newText);
            wordToHighlight = "";
        }
        contButton.SetActive(true);
    }



    public void NextLine(){
        wordSpeed = 0.03f;
        contButton.SetActive(false);

        if (index < dialogue.Length - 1){
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }else{
            zeroText();
        }
    }

    bool WordInPhrase(string word, string phrase){
        if (phrase.Contains(word)){
            return true;
        }
        return false;
    }

    void StoryTeller(){
        string tag = gameObject.tag;
        if(tag == "NPC_1"){
            if(WordInPhrase("trees", dialogue[index])){
                wordToHighlight = "trees";
                ActivateObject();
            }
        }else if(tag == "NPC_2"){
            if(WordInPhrase("mountains", dialogue[index])){
                wordToHighlight = "mountains";
                ActivateObject();
            }
        }else if(tag == "NPC_3"){
            if(WordInPhrase("house", dialogue[index])){
                wordToHighlight = "house";
                ActivateObject();
            }
        }else if(tag == "NPC_4"){
            if(WordInPhrase("deer", dialogue[index])){
                wordToHighlight = "mountdeerains";
                ActivateObject();
            }
        }
    }

    private void ActivateObject(){
        if(!objectActive){
            gameObjectMemory_1.Activate();
            gameObjectReal_1.SetActive(true);
            objectActive = true;
        }
    }

    private void Skip(){
        if(Input.GetKeyDown(KeyCode.E)){
            wordSpeed = 0;
        }
    }

    private void EndingStats(){
        if(nameString == "Old Guy"){
            fifthController.NpcCheck(0);
        }else if(nameString == "Eye Patch"){
            fifthController.NpcCheck(1);
        }else if(nameString == "Marcelo"){
            fifthController.NpcCheck(2);
        }else if(nameString == "Doctor"){
            fifthController.NpcCheck(3);
        }
    }

}
