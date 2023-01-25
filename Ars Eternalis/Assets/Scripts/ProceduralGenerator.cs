using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    private bool[][][] map = new bool[100][][];

    void Start()
    {
        for (int i = 0; i < map.Length; i++)
        {
            bool[][] zMap = new bool[100][];
            for (int j = 0; j < zMap.Length; j++)
            {
                zMap[j] = new bool[2];
            }
            map[i] = zMap;
        }

        AddCenter();
    }

    void AddCenter()
    {
        map[50][50][0] = true;
        GameObject center = Instantiate(prefabs[Random.Range(1, 12)], new Vector3(0, 0, 0), Quaternion.identity);
        var sorties = center.GetComponentsInChildren<Sortie>();
        Debug.Log(sorties.Length);
        foreach (var sortie in sorties)
        {
            sortie.CreateRoom(prefabs[13]); //Random.Range(0, prefabs.Length)
        }
    }




}
