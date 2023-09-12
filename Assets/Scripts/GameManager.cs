using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text player1ScoreText;
    [SerializeField] private TMP_Text player2ScoreText;

    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Transform ballTransform;

    private int player1Score;
    private int player2Score;
    
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance; 
        }
    }    
    
    public void Player1Scored()
    {
        player1Score++;
        player1ScoreText.text = player1Score.ToString();   
        
    }

    public void Player2Scored()
    {
        player2Score++;
        player2ScoreText.text = player2Score.ToString();
    }

    //Kick off from the middle after goal
    public void Restart()
    {
        player1Transform.position = new Vector2(player1Transform.position.x, 0);
        player2Transform.position = new Vector2(player2Transform.position.x, 0);
        ballTransform.position = new Vector2(0, 0);

        Ball ball = ballTransform.GetComponent<Ball>();
        if (ball != null)
        {
            ball.ResetTrail();
        }

    }
}
