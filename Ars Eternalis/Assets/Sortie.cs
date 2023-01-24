using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortie : MonoBehaviour
{
    public void CreateRoom(GameObject prefab) {
        GameObject room = Instantiate(prefab, transform.position, transform.rotation);
    }

}
