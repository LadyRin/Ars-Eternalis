using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, IFreezable
{

    Animator animator;
    GameObject player;
    float moveSpeed =1.0f;
    float detectionRange =5.0f;
    float attackRange=0.5f;

    private bool isFreezed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        animator.SetBool("isWalking",true);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,player.transform.position);

        if(distanceToPlayer<=detectionRange && !isFreezed){
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking",false);
        }
        
    }

    public void Freeze()
    {
        StartCoroutine(FreezingDelay());
    }
    
    private IEnumerator FreezingDelay()
    {
        isFreezed = true;
        yield return new WaitForSeconds(5f);
        isFreezed = false;
    }
}
