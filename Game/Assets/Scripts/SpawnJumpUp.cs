using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnJumpUp : MonoBehaviour
{

    public GameObject jumpUp;
    void Start()
    {
        foreach (Transform child in transform)
        {
            Instantiate(jumpUp, child.position, Quaternion.identity);
        }
    }
}