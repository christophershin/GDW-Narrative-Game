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
    void Start()
    {
        
    }
    
    void Update()
    {
        
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

    public void SetDialoguePath(string dialogue, string option1, string option2,  string option3)
    {
        dialogueText.text = dialogue;

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
