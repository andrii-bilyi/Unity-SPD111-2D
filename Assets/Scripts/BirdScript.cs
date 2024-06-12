using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    private Rigidbody2D rigidbody; //Посилання на компонент того ж, на якому скрипт

    void Start()
    {
        Debug.Log("BirdScript Start");
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector2(0, 500));
        }
    }
}
