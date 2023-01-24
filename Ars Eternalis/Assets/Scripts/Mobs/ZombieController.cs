using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    Animator animator;
    GameObject player;
    float moveSpeed =1.0f;
    float detectionRange =5.0f;
    float attackRange=0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        animator.SetBool("isWalking",true);
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,player.transform.position);

        if(distanceToPlayer<=detectionRange){
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking",false);
        }
        
    }

}
