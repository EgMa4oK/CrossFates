using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometr : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = rb.velocity.magnitude.ToString();
    }
}
