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
    private TrailRenderer ballTr;
    public float startTrailWidth = 1.0f; 
    public float endTrailWidth = 1.0f;
    public float trailTime = 1.0f;
    [SerializeField] private float changeSpeed = 1.0f;
    [SerializeField] private float timePassed = 0.0f;
    private Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.red };

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        ballSr = GetComponent<SpriteRenderer>();
        ballTr = GetComponent<TrailRenderer>();

        ballTr.startWidth = startTrailWidth;
        ballTr.endWidth = endTrailWidth;

        Launch();
        StartCoroutine(ChangeColorGradually());
    }
    
    //Kick off from the middle
    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2 (xVelocity, yVelocity) * initialVelocity;
    }
    
    //Speed up everytime a player hit the ball
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ballRb.velocity *= velocityMultiplier;       
        }
    }
    //Scoring
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

    //Color trancition for ball and trail
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
                    ballTr.material.color = Color.Lerp(initialColor, finalColor, timePassed);
                    yield return null;
                }

                timePassed = 0.0f;
            }           
        }
    }

    public void ResetTrail()
    {
        ballTr.Clear(); // Clear the trail
        ballTr.time = trailTime; // Adjust the trail's duration as needed
        ballTr.startWidth = startTrailWidth; // Adjust the initial width as needed
        ballTr.endWidth = endTrailWidth; // Adjust the initial width as needed
    }

}
