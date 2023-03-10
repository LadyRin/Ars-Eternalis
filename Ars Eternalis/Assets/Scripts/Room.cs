using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Spawner[] spawners;
    Sortie[] sorties;
    
    void Awake()
    {
        spawners = GetComponentsInChildren<Spawner>();
        sorties = GetComponentsInChildren<Sortie>();
        foreach (var spawner in spawners)
        {
            spawner.setSpawnNumber(Random.Range(1, 7));
        }
    }

    public void Expand(int depth)
    {
        if (depth <= 0)
        {
            foreach (var sortie in sorties)
            {
                sortie.CreateWall();
            }
            return;
        }

        Room room;
        foreach (var sortie in sorties)
        {
            switch (sortie.CheckSpace())
            {
                case 0:
                    sortie.CreateWall();
                    break;
                case 1:
                    room = sortie.CreateRoom1x1();
                    room.Expand(depth - 1);
                    break;
                case 2:
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        room = sortie.CreateRoom1x1();
                        room.Expand(depth - 1);
                    }
                    else
                    {
                        room = sortie.CreateRoom1x2();
                        room.Expand(depth - 1);
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var spawner in spawners)
            {
                spawner.Activate();
            }
        }
    }


}
