using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour {

    public static NavMeshSurface[] surfaces;

    public static void GetAllSurfaces() {
        surfaces = FindObjectsOfType<NavMeshSurface>();
    }
    public static void Bake() {
        foreach (var surface in surfaces) {
            surface.BuildNavMesh();
        }
    }

}