using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortie : MonoBehaviour
{
    public void CreateRoom(GameObject prefab) {
        Debug.Log("Function called");
        GameObject room = Instantiate(prefab, transform.position, transform.rotation);
        //room.transform.parent = 
    }

}
