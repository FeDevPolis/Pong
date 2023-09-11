using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Movement
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;
    private Rigidbody2D ballRb;
    //Change color
    private SpriteRenderer ballSr;
    [SerializeField] private float changeSpeed = 1.0f;
    [SerializeField] private float timePassed = 0.0f;
    private Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.red };

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballSr = GetComponent<SpriteRenderer>();
        
        Launch();
        StartCoroutine(ChangeColorGradually());
    }
    
    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2 (xVelocity, yVelocity) * initialVelocity;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ballRb.velocity *= velocityMultiplier;       
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal 2"))
        {
            GameManager.Instance.Player1Scored();
            GameManager.Instance.Restart();
            Launch();
        }
        else
        {
            GameManager.Instance.Player2Scored();
            GameManager.Instance.Restart();
            Launch();
        }
    }

    private IEnumerator ChangeColorGradually() 
    {
        while(true)
        {
            for(int i = 0; i < colors.Length -1; i++) 
            { 
                Color initialColor = colors[i];
                Color finalColor = colors[i + 1];

                while(timePassed < 1.0f)
                {
                    timePassed += Time.deltaTime * changeSpeed;
                    ballSr.color = Color.Lerp(initialColor, finalColor, timePassed);                    
                    yield return null;
                }

                timePassed = 0.0f;
            }           
        }
    }
}
