using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObstaclesDB : ScriptableObject
{
    public GameObject[] obstacles;
    
    public int Count => obstacles.Length;

    public GameObject GetRandomObstacle => obstacles[Random.Range(0, Count)];
}
