using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Sprite gun;
    [SerializeField] private Sprite dagger;
    [SerializeField] private Sprite photo;
    [SerializeField] private GameObject playerManager;

    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.GetComponent<StateManager>().weapon != "none")
        {

            string tool = playerManager.GetComponent<StateManager>().weapon;
            GetComponent<Image>().enabled = true;

            if (tool == "Dagger")
            {
                GetComponent<Image>().sprite = dagger;
            }
            if (tool == "Gun")
            {
                GetComponent<Image>().sprite = gun;
            }
            if (tool == "Photograph")
            {
                GetComponent<Image>().sprite = photo;
            }

        }
    }
}
