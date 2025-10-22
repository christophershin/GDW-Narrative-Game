using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepManager : MonoBehaviour
{
    private string weapon;
    private bool guardSupport;
    private DialogueManager dialogueManager;

    public string dialogue, option1, option2, option3;

    public GameObject dagger, mask;
    
    
    // Killer
    private string _killerState = "idle";
    public Animator anim;
    
    // Ending
    public GameObject endScreen;
    public TextMeshProUGUI ending, endReason;

    public GameObject darkScreen;
    
    // Ending Stuff
    private string isEnd = "none";
    
    // Time
    private float time = 0;
    
    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
        weapon = PlayerPrefs.GetString("Weapon");
        if (PlayerPrefs.GetString("GuardSupport") == "true")
        {
            guardSupport = true;
        }
        else
        {
            guardSupport = false;
        }

        StartCoroutine(BlackFade());
    }

    private IEnumerator BlackFade()
    {
        yield return new WaitForSeconds(2f);
        darkScreen.SetActive(false);
        SlasherEvent("");
    }

    private void Update()
    {
        
        if (isEnd == "hit")
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.normalizedTime >= 1f && !anim.IsInTransition(0))
                {
                    endScreen.SetActive(true);
                }
            }
        }
        else if (isEnd == "kill")
        {
            endScreen.SetActive(true);
        }
        
    }

    public void PickOption(TextMeshProUGUI txt)
    {
        SlasherEvent(txt.text);
        SetDialogue();
    }

    private void SlasherEvent(string option = "")
    {
        switch (option)
        {
            // Start
            case "":
                // Activate Box
                StartDialogue();

                // Dialogue
                dialogue = "Hello there...";
        
                // Options
                option1 = "GUARDS HELP!!";
                option2 = "GARRY HELP ME!!";

                switch (weapon)
                {
                    case "none":
                        option3 = "";
                        break;
                    case "Gun":
                        option3 = "*shoot the killer*";
                        break;
                    case "Dagger":
                        option3 = "*stab the killer*";
                        break;
                    case "Photograph":
                        option3 = "*take out the photograph*";
                        break;
                }
                
                // Set Dialogue
                SetDialogue();
                break;
            
            // stage 1
            
            case "GUARDS HELP!!":
                // Dialogue
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                
                switch (guardSupport)
                {
                    case true:
                        ending.text = "You died!";
                        endReason.text = "You called the guards, but they were too late.";
                        break;
                    case false:
                        ending.text = "You died!";
                        endReason.text = "You called the guards, but because of your previous interaction, they thought you were just dreaming. You ended up dying because you decided to talk about your dream.";
                        break;
                }

                isEnd = "hit";
                
                break;
            
            case "GARRY HELP ME!!":
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                
                ending.text = "You died!";
                endReason.text = "For some reason, Garry wasn't there.";

                isEnd = "hit";
                break;
            
            // Use Weapons
            case "*shoot the killer*":
                dialogueManager.DeactivateNarration(false);
                
                ending.text = "You lived!";
                endReason.text = "You received a life sentence for taking someone's life. Your defense wasn't accepted for the sole reason of having a gun in prison illegally.";

                isEnd = "kill";
                break;
            case "*stab the killer*":
                
                dagger.SetActive(true);
                
                // Dialogue
                dialogue = "Ow... don't think... this...";
        
                // Options
                option1 = "GUARDS HELP!!!";//
                option2 = "Stop man, you don't have to do this...";//
                option3 = "*stab again*";//
                break;
            case "GUARDS HELP!!!":
                dialogueManager.DeactivateNarration(false);

                if (guardSupport)
                {
                    isEnd = "kill";
                    ending.text = "You lived!";
                    endReason.text = "The guards came in time, detaining the injured killer, yet for some reason, Garry was missing since that day.";
                }
                else
                {
                    isEnd = "hit";
                    ending.text = "You died!";
                    endReason.text = "The guards didn't come because they thought you were hallucinating. Maybe you shouldn't have talked about your dream.";
                }
                
                break;
            case "Stop man, you don't have to do this...":
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                isEnd = "hit";

                ending.text = "You died!";
                endReason.text = "Maybe you shouldn't try talking to a killer...";
                
                break;
            case "*stab again*":
                dialogueManager.DeactivateNarration(false);
                isEnd = "kill";

                ending.text = "You lived!";
                endReason.text = "You received a life sentence for taking someone's life. Your defense wasn't accepted for the sole reason of having a dagger in prison illegally.";
                
                break;
            
            // Photo
            case "*take out the photograph*":
                // Dialogue
                dialogue = "No... you... STOP STOP DONT... DOnT#$Y#Y#$*";
        
                // Options
                option1 = "What's going on?";//
                option2 = "";//
                option3 = "";//
                break;
            case "What's going on?":
                mask.SetActive(false);
                
                // Dialogue
                dialogue = "......yo";
        
                // Options
                option1 = "Garry?";//
                option2 = "";//
                option3 = "";//
                break;
            case "Garry?":
                // Dialogue
                dialogue = "Yeah... this happens sometimes...";
        
                // Options
                option1 = "Explain yourself man";//
                option2 = "";//
                option3 = "";//
                break;
            case "Explain yourself man":
                // Dialogue
                dialogue = "I have a split personality... one that randomly comes out.";
        
                // Options
                option1 = "Oh...";//
                option2 = "Are you serious";//
                option3 = "";//
                break;
            case "Oh..." or "Are you serious":
                // Dialogue
                dialogue = "Thats the reason why I ended up in prison... I randomly went crazy and killed someone in broad daylight...";
        
                // Options
                option1 = "Tough life...";//
                option2 = "";//
                option3 = "";//
                break;
            case "Tough life...":
                // Dialogue
                dialogue = "Yea if I was sane at that time, I would've done the job without getting caught...";
        
                // Options
                option1 = "huh...?";//
                option2 = "";//
                option3 = "";//
                break;
            case "huh...?":
                // Dialogue
                dialogue = "Never mind forget it...";
        
                // Options
                option1 = "ok...";//
                option2 = "No explain what you meant";//
                option3 = "";//
                break;
            
            case "ok...":
                // Dialogue
                dialogue = "Yeah, I guess I'll ask to be transferred to a cell alone so things like this don't happen again...";
        
                // Options
                option1 = "Yeah...";//
                option2 = "Please don't go";//
                option3 = "I guess you can have your wife's photo back";//
                break;
            
            // Endings for photo
            case "Yeah...":
                dialogueManager.DeactivateNarration(false);
                isEnd = "kill";

                ending.text = "You lived!";
                endReason.text = "Garry moved onto a different cell, but you felt lonely since that day.";
                
                break;
            
            case "I guess you can have your wife's photo back":
                // Dialogue
                dialogue = "Really? I'd be super grateful";
        
                // Options
                option1 = "Yep, you can have it";//
                option2 = "No, I was joking";//
                option3 = "";//
                break;
            
            case "No explain what you meant":
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                isEnd = "hit";

                ending.text = "You died!";
                endReason.text = "Maybe you shouldn't have asked Garry to explain what he meant...";
                
                break;
            case "Please don't go":
                mask.SetActive(true);
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                isEnd = "hit";

                ending.text = "You died!";
                endReason.text = "Garry's crazy split personality came back. Maybe you shouldn't have asked him to stay.";
                
                break;
            case "Yep, you can have it":
                dialogueManager.DeactivateNarration(false);
                isEnd = "kill";

                ending.text = "You lived!";
                endReason.text = "Garry moved onto a new cell but came back in a few days. He says he grew attached to you so his crazy personality won't be targeting you. ";
                
                break;
            case "No, I was joking":
                dialogueManager.DeactivateNarration(false);
                anim.SetBool("Hit", true);
                isEnd = "hit";

                ending.text = "You died!";
                endReason.text = "That was really your fault this time...";
                
                break;
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Title Screen");
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
}
