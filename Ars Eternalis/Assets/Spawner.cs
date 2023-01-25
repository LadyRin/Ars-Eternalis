using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] float radius;

    [SerializeField] static GameObject[] prefabs;
    [SerializeField] float spawnTimeDiff;

    float spawnNumber = 0;

    public void setSpawnNumber(float number) {
        spawnNumber = number;
    }

    public void Activate() {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() {
        for (int i = 0; i < spawnNumber; i++) {
            Spawn();
            yield return new WaitForSeconds(spawnTimeDiff);
        }
    }

    void Spawn() {
        Vector2 r = Random.insideUnitCircle * radius;   
        Vector3 pos = new Vector3(r.x, 0, r.y);
        int prefabIndex = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[prefabIndex], transform.position + pos, Quaternion.identity);        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
        
}
