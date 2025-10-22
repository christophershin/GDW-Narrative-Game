using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Dialogues
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    // Buttons
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private TextMeshProUGUI[] buttonTexts;
    
    // Player
    [SerializeField] private Player player;
    
    // State Manager
    public StateManager stateManager;
    
    // Audio
    public List<AudioClip> dialogueSounds;
    public AudioSource audioSource;
    private string charTalking;

    public bool isFinalArc;
    
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //stateManager = GetComponent<StateManager>();
    }
    
    void Update()
    {
        if (isFinalArc)
        {
            audioSource.clip = dialogueSounds[1];
        }
        else
        {
            charTalking = stateManager.personTalking;

            if(dialogueBox.activeSelf == true)
            {
                switch (charTalking)
                {
                    case "Player":
                        audioSource.clip = dialogueSounds[0];
                        break;
                    case "Friend":
                        audioSource.clip = dialogueSounds[1];
                        break;
                    case "Guard":
                        audioSource.clip = dialogueSounds[2];
                        break;
                }
            }
        }
    }

    public void LockPlayer()
    {
        player.LockPlayer();
    }

    public void ActivateNarration()
    {
        player.LockPlayer();
        dialogueBox.SetActive(true);

        dialogueText.text = "";
        
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);
    }

    public void DeactivateNarration(bool freePlayer = true)
    {
        if (freePlayer == true)
        {
            player.FreePlayer();
        }
        
        dialogueBox.SetActive(false);
    }

    public IEnumerator SetDialoguePath(string dialogue, string option1, string option2,  string option3)
    {
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        buttons[2].SetActive(false);
        
        dialogueText.text = "";

        if (dialogueBox.activeSelf == true)
        {
            foreach (char c in dialogue)
            {
                dialogueText.text += c;
                audioSource.Play();

                yield return new WaitForSecondsRealtime(0.025f);
            }
            
            if (option3 != "")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[2].SetActive(true);
            
                buttonTexts[0].text = option3;
                buttonTexts[1].text = option2;
                buttonTexts[2].text = option1;
            }
            else if (option2 != "")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[2].SetActive(false);
            
                buttonTexts[0].text = option2;
                buttonTexts[1].text = option1;
            }
            else
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(false);
                buttons[2].SetActive(false);
            
                buttonTexts[0].text = option1;
            }
        }
    }
}
