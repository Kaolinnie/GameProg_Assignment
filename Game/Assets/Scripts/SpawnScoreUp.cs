using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnScoreUp : MonoBehaviour
{

    public GameObject scoreUp;
    void Start()
    {
        foreach (Transform child in transform)
        {
            Instantiate(scoreUp, child.position, Quaternion.identity);
        }
    }
}