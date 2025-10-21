using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SleepManager : MonoBehaviour
{
    private string weapon;
    private bool guardSupport;
    private DialogueManager dialogueManager;

    public string dialogue, option1, option2, option3;
    
    
    // Killer
    private string _killerState = "idle";
    public Animator anim;
    
    // Ending
    public GameObject endScreen;
    public TextMeshProUGUI ending, endReason;
    
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
                endReason.text = "You received a life sentence taking someone's life. Your defense wasn't accepted for the sole reason of having a gun in prison illegally.";

                isEnd = "kill";
                break;
            case "*stab the killer*":
                dialogueManager.DeactivateNarration(false);
                
                ending.text = "You lived!";
                endReason.text = "You received a life sentence taking someone's life. Your defense wasn't accepted for the sole reason of having a gun in prison illegally.";

                isEnd = "kill";
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
}
