using System.Collections;
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
    private StateManager stateManager;
    
    // Audio
    private AudioSource audioSource;
    private string charTalking;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateManager = GetComponent<StateManager>();
    }
    
    void Update()
    {
        charTalking = stateManager.personTalking;
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
        
        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            // Source.Play();

            yield return new WaitForSeconds(0.025f);
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
