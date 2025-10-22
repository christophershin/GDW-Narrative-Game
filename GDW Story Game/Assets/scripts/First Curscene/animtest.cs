using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class animtest : MonoBehaviour
{
    public GameObject trs;
    
    public bool canWalk;
    
    public bool canHit;
    private Animator anim;


    private float time;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        
        anim.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                trs.transform.position,
                10f * Time.deltaTime
            );;
        }
        else
        {
            time += Time.deltaTime;
            if (time > .4f)
            {
                var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.normalizedTime >= 1f && !anim.IsInTransition(0))
                {
                    SceneManager.LoadScene("model scene");
                }
                
            }
            
            // agent.destination = transform.position;
        }
        
        
        anim.SetBool("Walk", canWalk);
        anim.SetBool("Hit",  canHit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            canWalk = false;
            canHit = true;
        }
    }
}
