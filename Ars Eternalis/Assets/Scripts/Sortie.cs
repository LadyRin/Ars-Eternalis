using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortie : MonoBehaviour
{
    public static GameObject[] prefabs1x1;
    public static GameObject[] prefabs1x2;
    public static GameObject wallPrefab;

    public void CreateRoom(GameObject prefab) {
        GameObject room = Instantiate(prefab, transform.position, transform.rotation);
    }

    public void CreateWall() {
        Instantiate(wallPrefab, transform.position, transform.rotation);
    }

    public Room CreateRoom1x1() {
        GameObject go = Instantiate(prefabs1x1[Random.Range(0, prefabs1x1.Length)], transform.position, transform.rotation);
        return go.GetComponent<Room>();
    }

    public Room CreateRoom1x2() {
        GameObject go = Instantiate(prefabs1x2[Random.Range(0, prefabs1x2.Length)], transform.position, transform.rotation);
        return go.GetComponent<Room>();
    }


    //Returns 0 if no space, 1 if there is a 30x30 space in front, 2 if there is a 30x60 space in front
    //Use physics to check for space
    public int CheckSpace() {
        Vector3 pos = transform.position + Vector3.up * 2f;
        Vector3 dir = transform.forward + Vector3.down * 0.3f;
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, 20)) {
            return 0;
        } else {
            pos += transform.forward * 30;
            if (Physics.Raycast(pos, dir, out hit, 20)) {
                return 1;
            } else {
                return 2;
            }
        }
        
    }

}
