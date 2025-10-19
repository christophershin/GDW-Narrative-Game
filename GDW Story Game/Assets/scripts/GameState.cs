using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // referencing the animator 
    private Animator _gameStates;

    // the Canvas image that covers the screen
    public UnityEngine.UI.Image ImageCanvas;

    // player object
    public GameObject player;

    public Sprite dreamImage;


    // bool for playing the text
    bool playtext = false;
    public TextMeshProUGUI textCanvas;
    public GameObject BlackBar;
    public float timeBetweenText = 0.05f;
    // the dialogue in the game
    Dictionary<string, string> dialogue;


    [SerializeField]
    newDict newDict;


    void Start()
    {
        _gameStates = GetComponent<Animator>();

        dialogue = newDict.ToDictionary();
        playtext = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo name = _gameStates.GetCurrentAnimatorStateInfo(0);

        if (name.IsName("dream state"))
        {
            player.GetComponent<Player>().canMove = false;

            //ImageCanvas.sprite = dreamImage;
            //ImageCanvas.color = new Vector4(1, 1, 1, 1);

            if (playtext)
            {

                StartCoroutine(DrawText(timeBetweenText));
                
                playtext = false;
            }

        }




    }

    IEnumerator DrawText(float howlong)
    {
        BlackBar.SetActive(true);

        for (int i=0; i < dialogue["Dream text"].Length; i++)
        {

            textCanvas.text += dialogue["Dream text"][i];

            yield return new WaitForSeconds(howlong);


        }

    }


}


[Serializable]
public class newDict
{
    [SerializeField]
    NewDictItem[] thisDictItems;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> newDict = new Dictionary<string, string>();

        foreach(var item in thisDictItems)
        {
            newDict.Add(item.name, item.text);
        }

        return newDict;
    }

}

[Serializable]
public class NewDictItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public string text;
    
}










