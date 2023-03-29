using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float maxHeight;
    private float minHeight;
    private float range = 0.05f;
    private float floatingSpeed = 0.1f;
    private float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        float y = transform.position.y;
        minHeight = y - range;
        maxHeight = y + range;
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;
        if (y > maxHeight || y < minHeight) floatingSpeed *= -1;
        transform.position += new Vector3(0,floatingSpeed * Time.deltaTime,0);
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
    }

    private void OnDisable()
    {
        Invoke(nameof(Reactivate), 30);
    }

    private void Reactivate()
    {
        gameObject.SetActive(true);
    }
}
