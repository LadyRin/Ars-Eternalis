using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] GameObject endRoomPrefab;
    void Start()
    {
        AddEndRoom();
    }

    void AddEndRoom() {
        GameObject go = Instantiate(endRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        go.GetComponent<Room>().Expand(3);
    }




}
