using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPlate : MonoBehaviour
{
    public Text score;

    private GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = $"{instance.Score}";
    }
}
