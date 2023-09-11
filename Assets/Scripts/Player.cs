using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private bool isPlayer1;
    private float yBound = 3.75f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement;

        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Player1");
        }
        else
        {
            movement = Input.GetAxisRaw("Player2");
        }

        //transform.position += new Vector3(0, movement * speed * Time.deltaTime, 0);
        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime,-yBound, yBound);
        transform.position = paddlePosition;
    }
}
