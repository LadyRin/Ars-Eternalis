using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    public GameObject timeTravelPS;

    void Start()
    {
        timeTravelPS = transform.Find("TimeTravelPS").gameObject;
    }
    public void SendThroughTime(EnemyController enemy)
    {
        GameObject ps = Instantiate(timeTravelPS, enemy.transform.position, Quaternion.identity);
        ps.GetComponent<ParticleSystem>().Play();
        enemy.gameObject.SetActive(false);
        StartCoroutine(BringBack(enemy));
        Destroy(ps, 5f);
    }

    private IEnumerator BringBack(EnemyController enemy) {
        yield return new WaitForSeconds(5f);
        enemy.gameObject.SetActive(true);
        GameObject ps = Instantiate(timeTravelPS, enemy.transform.position, Quaternion.identity);
        ps.GetComponent<ParticleSystem>().Play();
        Destroy(ps, 5f);
    }


}
