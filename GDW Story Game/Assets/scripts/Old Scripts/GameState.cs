using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator _gameStates;
    public UnityEngine.UI.Image ImageCanvas;

    public GameObject player;



    public Sprite dreamImage;

    public HashSet<string> Dialogue;

    


    void Start()
    {
        _gameStates = GetComponent<Animator>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo name = _gameStates.GetCurrentAnimatorStateInfo(0);

        if(name.IsName("dream state"))
        {
            Debug.Log("dream");

            ImageCanvas.sprite = dreamImage;
            ImageCanvas.color = new Vector4(1, 1, 1, 1);

        }




    }


    public void DrawText()
    {

    }
}
