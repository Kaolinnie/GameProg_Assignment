using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    public Text score;
    
    // Start is called before the first frame update
    void Start()
    {
        score.text = $"Final score: {GameManager.Instance.Score}";
    }
}
