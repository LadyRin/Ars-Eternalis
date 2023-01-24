using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    private bool[][] map = new bool[100][];

    private 

    void Start()
    {
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new bool[100];
        }

        AddCenter();
    }

    void AddCenter()
    {
        map[50][50] = true;
        GameObject center = Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        var sorties = center.GetComponentsInChildren<Sortie>();
        foreach (var sortie in sorties)
        {
            sortie.CreateRoom(prefabs[0]);
        }
    }

    


}
