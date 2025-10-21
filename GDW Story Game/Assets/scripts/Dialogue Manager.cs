using TMPro;
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

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

    public List<AudioClip> dialogueSounds;
    private AudioSource Source;
    private string charTalking;


    void Start()
    {
        Source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        charTalking = GetComponent<StateManager>().personTalking;
        // a state decides which sound to play per character
        if(charTalking == "Player")
        {
            Source.clip = dialogueSounds[0];

        }else if(charTalking == "Friend")
        {
            Source.clip = dialogueSounds[1];

        }
        else if(charTalking == "Guard")
        {
            Source.clip = dialogueSounds[2];

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

    public void DeactivateNarration()
    {
        player.FreePlayer();
        dialogueBox.SetActive(false);
    }

    public void SetDialoguePath(string dialogue, string option1, string option2,  string option3)
    {
        if (charTalking != "")
        {
            StartCoroutine(DrawText(dialogue));
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

    IEnumerator DrawText(string drawnText)
    {
        dialogueText.text = "";
        

        for (int i = 0; i < drawnText.Length; i++)
        {
            
            dialogueText.text += drawnText[i];
            Source.Play();

            yield return new WaitForSeconds(0.05f);


        }


    }
}
