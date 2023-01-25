using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] GameObject endRoomPrefab;
    [SerializeField] GameObject[] prefabs1x1;
    [SerializeField] GameObject[] prefabs1x2;
    [SerializeField] GameObject wallPrefab;

    [SerializeField] GameObject[] enemies;

    void Start()
    {
        Sortie.wallPrefab = wallPrefab;
        Sortie.prefabs1x1 = prefabs1x1;
        Sortie.prefabs1x2 = prefabs1x2;
        Spawner.enemies = enemies;
        AddEndRoom();
    }

    void AddEndRoom() {
        GameObject go = Instantiate(endRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        go.GetComponent<Room>().Expand(10);

        NavMeshBaker.GetAllSurfaces();
        NavMeshBaker.Bake();
    }




}
