using System;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Dialogue Manager
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private TextMeshProUGUI prompt;
    
    // Data
    private string state = "Intro";
    private string dialogueOption = "";
    public string personTalking = "";
    
    // Items
    private bool guardSupport = true;
    private string weapon = "none";
    
    // Options
    private string dialogue, option1,  option2, option3;
    
    // Guard
    private string guardState = "wait";
    [SerializeField] private Transform grd, barPos, backPos;
    
    // Garry
    private bool garryGive = false;
    public Animator garryAnimator;
    public GameObject dagger, gun, photograph;

    void Start()
    {
        StartIntro();
        personTalking = "Player";
        PlayerPrefs.SetString("Weapon", "none");
        PlayerPrefs.SetString("GuardSupport", "true");
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (state)
            {
                case "Guard":
                    personTalking = "Guard";
                    StartCoroutine(StartGuard());
                    break;
                case "Friend":
                    personTalking = "Friend";
                    StartFriend();
                    break;
                case "Sleep":
                    SceneManager.LoadScene("sleep scene");
                    break;
            }
        }

        if (guardState == "Bar")
        {
            grd.position = Vector3.MoveTowards(
                grd.position,
                barPos.position,
                6f * Time.deltaTime
            );;
        }

        if (guardState == "Away")
        {
            grd.position = Vector3.MoveTowards(
                grd.position,
                backPos.position,
                6f * Time.deltaTime
            );;
        }
        
        garryAnimator.SetBool("Pick", garryGive);
        gun.SetActive(garryGive);
        dagger.SetActive(garryGive);
        
        if (weapon != "none")
        {
            garryGive = false;

            if (weapon == "Photograph")
            {
                PlayerPrefs.SetString("Weapon", "Photograph");
                PlayerPrefs.Save();
                photograph.SetActive(false);
            }
        }
    }
    
    public void PickDialogueOption(TextMeshProUGUI t)
    {
        dialogueOption = t.text;

        switch (state)
        {
            case "Intro":
                ContinueIntro();
                //SetDialogue();
                StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
                break;
            case "Guard":
                ContinueGuard();
                //SetDialogue();
                StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
                break;
            case "Friend":
                ContinueFriend();
                //SetDialogue();
                StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
                break;
        }
    }

    private void StartIntro()
    {
        // Activate Box
        StartDialogue();

        // Dialogue
        dialogue = "Was that just a dream, it must have been! After all this time I spent locked up here I must be going crazy.";
        
        // Options
        option1 = "It was probably just a dream";
        option2 = "Scary... I should tell a guard";
        option3 = "Lets tell Garry, he knows what to do";
        
        // Set Dialogue
        //SetDialogue();
        StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
    }

    private IEnumerator StartGuard()
    {
        // Activate Box
        prompt.gameObject.SetActive(false);
        
        // Lock Player
        dialogueManager.LockPlayer();
        
        // Move Guard
        guardState = "Bar";
        
        yield return new  WaitForSeconds(1.5f);
        
        StartDialogue();
        
        // Dialogue
        dialogue = "Why did you call me for?";
        
        // Options
        option1 = "I think someone is going to kill me";
        option2 = "Never mind";
        option3 = "";
        
        // Set Dialogue
        //SetDialogue();
        StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
    }
    
    private void StartFriend()
    {
        // Activate Box
        prompt.gameObject.SetActive(false);
        StartDialogue();
        
        // Dialogue
        dialogue = "Yo!";
        
        // Options
        option1 = "Dude, I have to tell you something";
        option2 = "Never mind";
        option3 = "";
        
        // Set Dialogue
        //SetDialogue();
        StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1,  option2,  option3));
    }

    private void ContinueIntro()
    {
        switch (dialogueOption)
        {
            // Layer 1
            case "It was probably just a dream":
                
                // Dialogue
                dialogue = "Lets go back to sleep...";
        
                // Options
                option1 = "...";
                option2 = "";
                option3 = "";
                
                break;
            case "Scary... I should tell a guard":
                
                // Consequences
                state = "Guard";
                EndDialogue();
                break;
            case "Lets tell Garry, he knows what to do":
                // Consequences
                state = "Friend";
                EndDialogue();
                break;
            
            //Layer 2
            case "...":
                state = "Sleep";
                PlayerPrefs.SetString("State", "Sleep");
                PlayerPrefs.Save();
                EndDialogue();
                break;
        }
    }
    
    private void ContinueGuard()
    {
        switch (dialogueOption)
        {
            // Stage 1
            case "Never mind":
                // Consequences
                guardState = "Away";
                
                // Dialogue
                dialogue = "You just wasted my time man. I had to quit my game because of you.";
        
                // Options
                option1 = "Sorry...";
                option2 = "";
                option3 = "";
                
                break;
            case "I think someone is going to kill me":
                // Consequences
                
                // Dialogue
                dialogue = "What are you talking about?";
        
                // Options
                option1 = "So I had this dream";
                option2 = "Never mind";
                option3 = "";
                
                break;
            
            case "So I had this dream":
                // Consequences
                guardSupport = false;
                PlayerPrefs.SetString("GuardSupport", "false");
                PlayerPrefs.Save();

                guardState = "Away";
                
                // Dialogue
                dialogue = "Don't call me for useless things again...";
        
                // Options
                option1 = "NO WAIT!";
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 2
            case "NO WAIT!" or "Sorry...":

                personTalking = "Player";
                
                if (weapon == "none")
                {
                    // Dialogue
                    dialogue = "He's gone... so what should I do now...";
        
                    // Options
                    option1 = "Maybe it was just a dream";
                    option2 = "Lets tell Garry, he knows what to do";
                }
                else
                {
                    if (guardSupport == true)
                    {
                        // Dialogue
                        dialogue = "Yep not telling was the right call";
        
                        // Options
                        option1 = "At least I told garry";
                        option2 = ""; 
                    }
                    else
                    {
                        // Dialogue
                        dialogue = "Bruh he didn't even listen to me...";
        
                        // Options
                        option1 = "At least Garry listened";
                        option2 = ""; 
                    }
                    
                }
                option3 = "";
                
                break;
            
            // Stage 3
            case "Maybe it was just a dream" or "At least Garry listened" or "At least I told garry":
                
                // Dialogue
                dialogue = "Lets go to sleep now...";
        
                // Options
                option1 = "...";
                option2 = "";
                option3 = "";
                
                break;
            case "Lets tell Garry, he knows what to do":
                
                // Consequences
                state = "Friend";
                EndDialogue();
                
                break;
            
            // Stage 4
            case "...":
                
                // Consequences
                state = "Sleep";
                EndDialogue();
                
                break;
        }
    }
    
    private void ContinueFriend()
    {
        switch (dialogueOption)
        {
            // Stage 1
            case "Dude, I have to tell you something":
                // Consequences
                
                // Dialogue
                dialogue = "Im all ears :)";
        
                // Options
                option1 = "So I had this weird dream"; //
                option2 = "Actually forget it";//
                option3 = "";
                
                break;
            case "Never mind":
                // Dialogue
                dialogue = "Ok man...";
        
                // Options
                option1 = "..."; //
                option2 = "Actually lemme tell you";//
                option3 = "";
                
                break;
            
            // Stage 2
            case "So I had this weird dream":
                // Dialogue
                dialogue = "I never knew you were the type to be scared about a silly little dream.";
        
                // Options
                option1 = "Someone killed me"; //
                option2 = "Nevermind gotta sleep"; //
                option3 = "";
                
                break;
            case "Actually forget it" or "Nevermind gotta sleep":
                // Dialogue
                dialogue = "Ok man...";
        
                // Options
                option1 = "...";//
                option2 = "Actually lemme tell you";//
                option3 = "";
                
                break;
            case "Actually lemme tell you":
                // Dialogue
                dialogue = "Go ahead.";
        
                // Options
                option1 = "So I had this weird dream";//
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 3
            case "Someone killed me":
                // Dialogue
                dialogue = "Hmm so it was that bad huh... tell me more";
        
                // Options
                option1 = "It was some psycho killer";//
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 4
            case "It was some psycho killer":
                // Dialogue
                dialogue = "It does sound scary. Now tell me, what did he look like?";
        
                // Options
                option1 = "He was wearing a mask.";//
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 5
            case "He was wearing a mask.":
                // Dialogue
                dialogue = "Hmm....";
        
                // Options
                option1 = "A super cheap looking mask";//
                option2 = "He then hit me with a crowbar, and I woke up panting.";
                option3 = "";
                
                break;
            
            // Stage 6
            case "A super cheap looking mask":
                // Dialogue
                dialogue = "This sounds funny rather than scary ngl...";
        
                // Options
                option1 = "What the hell man?";//
                option2 = "You're right";//
                option3 = "";
                
                break;
            
            // Stage 7
            case "What the hell man?" or "You're right":
                // Dialogue
                dialogue = "*Still Laughing* Sorry dude, you can continue";
        
                // Options
                option1 = "...."; // four dots not 3
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 8
            case "....": // four dots
                // Dialogue
                dialogue = "So what happened next?";
        
                // Options
                option1 = "He hit me with a crowbar, and I woke up panting."; //
                option2 = "";
                option3 = "";
                
                break;
            
            // Stage 9
            case "He hit me with a crowbar, and I woke up panting." or "He then hit me with a crowbar, and I woke up panting.": 
                // Dialogue
                dialogue = "Damn... tell you what. If you're still scared, I can offer you some defense measures.";
        
                // Options
                option1 = "Yes please"; //
                option2 = "Actually, I think it was just a dream"; //
                option3 = "";
                
                break;
            
            // Stage 10
            case "Yes please" or "Never mind, gimme the defense measures": 
                // Dialogue
                dialogue = "Ok pick one of these.";

                garryGive = true;
        
                // Options
                option1 = "Pick the dagger"; //
                option2 = "Pick the gun"; //
                option3 = "Pick the photograph"; //
                
                break;
            case "Actually, I think it was just a dream": 
                // Dialogue
                dialogue = "If you think so";
        
                // Options
                option1 = "Alright, I'm gonna sleep"; //
                option2 = "Never mind, gimme the defense measures"; //
                option3 = "";
                
                break;
            
            // Stage 11
            case "Alright, I'm gonna sleep": 
                // Dialogue
                dialogue = "Good night man";
        
                // Options
                option1 = "Good night"; //
                option2 = "Never mind, gimme the defense measures"; //
                option3 = "";
                
                break;
            
            // Pick weapon
            case "Pick the dagger": 
                // Consequences
                weapon = "Dagger";
                
                PlayerPrefs.SetString("Weapon", "Dagger");
                PlayerPrefs.Save();
                
                // Dialogue
                dialogue = "Great choice! You should probably go to sleep now.";
        
                // Options
                option1 = "Good night"; //
                option2 = "So how exactly did you get weapons in here"; //
                option3 = "";
                
                break;
            case "Pick the gun":
                // Consequences
                weapon = "Gun";
                
                PlayerPrefs.SetString("Weapon", "Gun");
                PlayerPrefs.Save();
                
                // Dialogue
                dialogue = "Careful though, it only has one bullet.";
        
                // Options
                option1 = "Alright, thanks man."; //
                option2 = "So how exactly did you get weapons in here"; //
                option3 = "";
                
                break;
            
            // Pick Photograph
            case "Pick the photograph": 
                // Dialogue
                dialogue = "What the hell man? That wasn't even an option...";
        
                // Options
                option1 = "You said to pick something!"; //
                option2 = "Ok, I'll pick something else"; //
                option3 = "";
                
                break;
            case "You said to pick something!": 
                // Dialogue
                dialogue = "Yeah... but not this...";
        
                // Options
                option1 = "Gimme the photograph!"; 
                option2 = "Ok, I'll pick something else"; //
                option3 = "";
                
                break;
            
            case "Gimme the photograph!": 
                // Dialogue
                dialogue = "WHY DO YOU WANT A PICTURE OF MY WIFE????!!!!";
        
                // Options
                option1 = "Gimme the picture like you promised!"; //
                option2 = "Ok, I'll pick something else"; //
                option3 = "";
                
                break;
            
            case "Gimme the picture like you promised!": 
                // Dialogue
                dialogue = "HELL NO!!!";
        
                // Options
                option1 = "I thought you don't go back on your promise..."; //
                option2 = "Fine, I'll pick something else"; //
                option3 = "";
                
                break;
            
            case "I thought you don't go back on your promise...": 
                // Dialogue
                dialogue = "Are you deadass?";
        
                // Options
                option1 = "Yes! Now gimme the picture."; //
                option2 = "I was joking, lemme pick something else"; //
                option3 = "";
                
                break;
            
            case "Yes! Now gimme the picture.": 
                // Dialogue
                dialogue = "Fine... you can have the picture...";
        
                // Options
                option1 = "Thank you"; //
                option2 = "I was joking, lemme pick something else"; //
                option3 = "";
                
                break;
            
            case "Thank you": 
                // Consequences
                weapon = "Photograph";
                
                PlayerPrefs.SetString("Weapon", "Photograph");
                PlayerPrefs.Save();
                photograph.SetActive(false);
                
                // Dialogue
                dialogue = "You should probably go to sleep now";
        
                // Options
                option1 = "Good night"; //
                option2 = "So how the hell did you get weapons in here"; //
                option3 = "";
                
                break;
            
            case "Ok, I'll pick something else" or "I was joking, lemme pick something else" or "Fine, I'll pick something else": 
                // Dialogue
                dialogue = "THANK YOU!!! Now pick one of these";
        
                // Options
                option1 = "Pick the dagger"; //
                option2 = "Pick the gun"; //
                option3 = "";
                
                break;
            
            // Questioning
            case "So how the hell did you get weapons in here" or "So how exactly did you get weapons in here": 
                // Dialogue
                dialogue = "I have my ways ;)";
        
                // Options
                option1 = "ok..."; //
                option2 = ""; //
                option3 = "";
                
                break;
            
            case "ok..." or "Alright, thanks man.": 
                // Dialogue
                dialogue = "You should probably go to sleep now";
        
                // Options
                option1 = "Good night"; //

                if (guardSupport == true)
                {
                    option2 = "I should probably tell the guards about the dream";//
                }
                else
                {
                    option2 = ""; 
                }
                
                option3 = "";
                
                break;
            
            case "I should probably tell the guards about the dream":
                // Dialogue
                dialogue = "They will probably call you crazy but tell them if you want";
                
                // Options
                option1 = "You're right. Gotta go sleep now";//
                option2 = "Nothing harmful in trying"; //
                option3 = "";
                
                break;
            
            case "You're right. Gotta go sleep now":
                // Dialogue
                dialogue = "Good night man";
                
                // Options
                option1 = "Good night";//
                option2 = "";
                option3 = "";
                break;
            
            // END
            case "Nothing harmful in trying":
                // Consequences
                state = "Guard";
                EndDialogue();
                break;
            case "..." or "Good night" or "Good night man":
                state = "Sleep";
                EndDialogue();
                break;
        }
    }

    private void StartDialogue()
    {
        dialogueManager.ActivateNarration();
    }

    private void EndDialogue()
    {
        dialogueManager.DeactivateNarration();
    }
    
    private void SetDialogue()
    {
        StartCoroutine(dialogueManager.SetDialoguePath(dialogue, option1, option2, option3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") && state == "Guard")
        {
            prompt.gameObject.SetActive(true);
            prompt.text = "Press E to call guard";
        }
        
        if (other.CompareTag("Friend") && state == "Friend")
        {
            prompt.gameObject.SetActive(true);
            prompt.text = "Press E to talk";
        }

        if (other.CompareTag("Sleep") && state == "Sleep")
        {
            prompt.gameObject.SetActive(true);
            prompt.text = "Press E to sleep";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door") && state == "Guard")
        {
            prompt.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("Friend")  && state == "Friend")
        {
            prompt.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("Sleep") && state == "Sleep")
        {
            prompt.gameObject.SetActive(false);
        }
    }

    IEnumerator DrawText(string drawnText)
    {
        

        for (int i = 0; i < drawnText.Length; i++)
        {

            dialogue += drawnText[i];

            yield return new WaitForSeconds(0.05f);


        }

    }



}
