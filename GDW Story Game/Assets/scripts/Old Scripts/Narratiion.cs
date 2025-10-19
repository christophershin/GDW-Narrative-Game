using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Narratiion : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI text; // proximity prompt
    [SerializeField] private GameObject dialogues;
    
    // Dialogue
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private TextMeshProUGUI[] buttonTexts;
    
    // Button Events
    private string option1, option2, option3;

    private string weapon = "none";
    
    // Checks
    private string whichPrompt = "";
    private bool promptClicked = false;
    private bool canGetPhoto = true;
    private bool canTalkToFriend = true;
    private bool canTalkToGuard = true;
    
    // Data
    private bool guardSupport = true;
    
    
    void Start()
    {
        player.LockPlayer();
        IntroEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && whichPrompt != "" && promptClicked == false)
        {
            promptClicked = true;
            
            switch (whichPrompt)
            {
                case "Door":
                    GuardEvent();
                    break;
                case "Friend":
                    FriendEvent();
                    break;
            }
        }
    }

    private void IntroEvent()
    {
        // Dialogue
        dialogues.SetActive(true);
        dialogueText.text = "What was that?";
        
        // Setting buttons active
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[2].gameObject.SetActive(true);
        
        // Dialogues
        option1 = "Its just a dream";
        option2 = "Lets tell Garry";
        option3 = "Scary... I should tell a guard";
        
        // Buttons
        buttonTexts[0].text = option1;
        buttonTexts[1].text = option2;
        buttonTexts[2].text = option3;
    }
    
    private void GuardEvent()
    {
        text.gameObject.SetActive(false);
        canTalkToFriend = false;
        canTalkToGuard = false;
        // Dialogue
        player.LockPlayer();
        dialogues.SetActive(true);
        dialogueText.text = "Why the hell did you call me for?";
        
        // Setting buttons active
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[2].gameObject.SetActive(false);
        
        // Dialogues
        option1 = "Never mind...";
        option2 = "I think someone is going to kill me";
        
        // Buttons
        buttonTexts[0].text = option1;
        buttonTexts[1].text = option2;
        buttonTexts[2].text = option3;
    }

    private void FriendEvent()
    {
        text.gameObject.SetActive(false);
        canTalkToGuard = false;
        canTalkToFriend = false;
        // Dialogue
        player.LockPlayer();
        dialogues.SetActive(true);
        dialogueText.text = "Whats up?";
        
        // Setting buttons active
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[2].gameObject.SetActive(false);
        
        // Dialogues
        option1 = "Never mind...";
        option2 = "I had this weird dream";
        
        // Buttons
        buttonTexts[0].text = option1;
        buttonTexts[1].text = option2;
        buttonTexts[2].text = option3;
    }

    public void Option1Event()
    {
        switch (option1)
        {
            case "Lets tell Garry":
                canTalkToGuard = false;
                canTalkToFriend = true;
                promptClicked = false;
                dialogues.SetActive(false);
                player.FreePlayer();
                break;
            case "..." or "Good night" or "Thank you man":
                promptClicked = false;
                dialogues.SetActive(false);
                player.FreePlayer();
                break;
            case "Never mind...":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Inmates these days...";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "...";
                buttonTexts[0].text = option1;
                break;
            case "Its just a dream":
                
                // Consequences
                canTalkToGuard = false;
                canTalkToFriend = false;
                
                // Change Dialogue
                dialogueText.text = "Well I should probably go back to sleep";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "...";
                buttonTexts[0].text = option1;
                break;
            case "It was probably just a dream":
                // Consequences
                guardSupport = false;
                canTalkToFriend = false;
                
                // Change Dialogue
                dialogueText.text = "Yeah man... you should head to sleep now...";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "...";
                buttonTexts[0].text = option1;
                break;
            case "Wait listen to me...":
                
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "What should I do now?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Its just a dream";
                option2 = "Lets tell Garry";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "He wore a mask":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Interesting... so why didn't you run?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Well, I was stuck in place and couldn't move";
                buttonTexts[0].text = option1;
                break;
            case "Well, I was stuck in place and couldn't move":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "If you're still scared, I'll offer you some defense measures";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Actually, forget what I said";
                option2 = "I'd be super grateful";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "Actually, forget what I said":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Why?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "It was probably just a dream";
                option2 = "Actually, give me the defense measures";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "Pick the Gun":
                // Consequences
                weapon = "Gun";
                
                // Change Dialogue
                dialogueText.text = "Careful though, it only has one bullet!";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Thank you man";
                buttonTexts[0].text = option1;
                
                if (guardSupport == true)
                {
                    buttons[1].gameObject.SetActive(true);
                    option2 = "I should probably tell a guard too...";
                    buttonTexts[1].text = option2;
                }
                else
                {
                    buttons[1].gameObject.SetActive(false);
                }
                
                break;
            case "You said to pick something!":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Yeah, but WHY DO YOU WANT A PICTURE OF MY WIFE?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Give me the picture like you promised!";
                option2 = "Lemme pick something else";
                buttonTexts[0].text = option1;
                break;
            case "Give me the picture like you promised!":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "HELL NAH!!!";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "I thought you don't go back on your promises...";
                option2 = "Lemme pick something else";
                buttonTexts[0].text = option1;
                break;
            case "I thought you don't go back on your promises...":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Are you serious???";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Yeah, now gimme the picture!";
                option2 = "Lemme pick something else";
                buttonTexts[0].text = option1;
                break;
            case "Yeah, now gimme the picture!":
                // Consequences
                weapon = "Photograph";
                
                // Change Dialogue
                dialogueText.text = "Ok fine... you can have it...";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Thank you man";
                buttonTexts[0].text = option1;
                
                if (guardSupport == true)
                {
                    buttons[1].gameObject.SetActive(true);
                    option2 = "I should probably tell a guard too...";
                    buttonTexts[1].text = option2;
                }
                else
                {
                    buttons[1].gameObject.SetActive(false);
                }
                
                break;
            case "Nothing harmful in trying":
                // Consequences
                canTalkToGuard = true;
                
                // Change Dialogue
                dialogueText.text = "You do you man...";
                
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                option1 = "...";
                buttonTexts[0].text = option1;
                break;
            case "Yeah":
                // Consequences
                canTalkToGuard = true;
                
                // Change Dialogue
                dialogueText.text = "You do you man...";
                
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                option1 = "...";
                buttonTexts[0].text = option1;
                break;
        }
    }

    public void Option2Event()
    {
        switch (option2)
        {
            case "I had this weird dream":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "What dream?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Never mind...";
                option2 = "There was this guy trying to kill me";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "There was this guy trying to kill me":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Hmm... how did he look like?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "He wore a mask";
                buttonTexts[0].text = option1;
                break;
            case "Lets tell Garry":
                canTalkToFriend = true;
                promptClicked = false;
                dialogues.SetActive(false);
                player.FreePlayer();
                break;
            case "I think someone is going to kill me":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "What the hell are you talking about?";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Never mind...";
                option2 = "Yeah, I had this dream...";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "Yeah, I had this dream...":
                // Consequences
                guardSupport = false;
                
                // Change Dialogue
                dialogueText.text = "Don't ever call me about useless things again!";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Wait listen to me...";
                buttonTexts[0].text = option1;
                break;
            case "Lemme pick something else":
                canGetPhoto = false;
                
                // Change Dialogue
                dialogueText.text = "As you should! Now pick one of these... EXCEPT THE PHOTO!";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Pick the Gun";
                option2 = "Pick the Knife";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "Actually, give me the defense measures" or "I'd be super grateful":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Ok, pick something to defend yourself";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(true);
                
                // Change options
                option1 = "Pick the Gun";
                option2 = "Pick the Knife";
                option3 = "Pick the Photograph";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                buttonTexts[2].text = option3;
                break;
            case "Pick the Knife":
                // Consequences
                weapon = "Knife";
                
                // Change Dialogue
                dialogueText.text = "Great choice! You should probably go to sleep now.";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "Good night";
                buttonTexts[0].text = option1;

                if (guardSupport == true)
                {
                    buttons[1].gameObject.SetActive(true);
                    option2 = "I should probably tell a guard too...";
                    buttonTexts[1].text = option2;
                }
                else
                {
                    buttons[1].gameObject.SetActive(false);
                }
                break;
            case "I should probably tell a guard too...":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "They will probably think you're crazy";
                
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                option1 = "Nothing harmful in trying";
                option2 = "Never mind, going to sleep now";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
            case "Never mind, going to sleep now":
                // Consequences
                
                // Change Dialogue
                dialogueText.text = "Good night";
                
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);
                
                option1 = "Good night";
                buttonTexts[0].text = option1;
                break;
        }
    }

    public void Option3Event()
    {
        switch (option3)
        {
            case "Scary... I should tell a guard":
                canTalkToFriend = false;
                promptClicked = false;
                dialogues.SetActive(false);
                player.FreePlayer();
                break;
            case "Pick the Photograph":
                // Consequences
                weapon = "Photograph";
                
                // Change Dialogue
                dialogueText.text = "What the hell man? That wasn't even an option!";
                
                // Change button actives
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
                
                // Change options
                option1 = "You said to pick something!";
                option2 = "Lemme pick something else";
                buttonTexts[0].text = option1;
                buttonTexts[1].text = option2;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") && canTalkToGuard == true)
        {
            whichPrompt = "Door";
            text.gameObject.SetActive(true);
            text.text = "Press E to call guard";
        }
        
        if (other.CompareTag("Friend") && canTalkToFriend == true)
        {
            whichPrompt = "Friend";
            text.gameObject.SetActive(true);
            text.text = "Press E to talk to friend";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door") ||  other.CompareTag("Friend"))
        {
            whichPrompt = "";
            text.gameObject.SetActive(false);
        }
    }
}
