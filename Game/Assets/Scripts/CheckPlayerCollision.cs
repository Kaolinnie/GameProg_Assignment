using System;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{
    private GameManager instance;
    
    private void Start()
    {
        instance = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        string colTag = collision.gameObject.tag;

        switch (colTag)
        {
            case "JumpUp":
                instance.hasDoubleJump = true;
                collision.gameObject.SetActive(false);
                break;
            case "ScoreUp":
                instance.IncrementScore(50);
                collision.gameObject.SetActive(false);
                break;
            case "Finish":
                instance.NextLevel();
                break;
            case "Death":
                instance.Gameover();
                break;
        }
    }
}