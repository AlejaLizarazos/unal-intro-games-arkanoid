using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    private const string BALL_PREFAB_PATH = "Prefabs/Ball";
    private Rigidbody2D _rb;
    private float _powerUpSpeed = 4;

    Transform paddle;
    Transform ball;
    Paddle paddleObject;
    private GameObject[] _ball;

    private Ball _ballPrefab;

    private SpriteRenderer _renderer;

    Sprite sprite;

    private const string SIZE_PATH = "Sprites/PowerUps/PaddleSize/Size_{0}";
    private const string MULTI_PATH = "Sprites/PowerUps/MultiBall/Multi_{0}";
    private const string VELOCITY_PATH = "Sprites/PowerUps/BallVelocity/Vel_{0}";

    string path = null;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate() {
        _rb.velocity = - new Vector2(0,1)  * _powerUpSpeed;
    }

    private void Update() {
        _renderer.sprite = Resources.Load<Sprite>(path);

        paddle = GameObject.FindWithTag("Paddle").GetComponent<Transform>();
        paddleObject = GameObject.FindWithTag("Paddle").GetComponent<Paddle>();

        _ball = GameObject.FindGameObjectsWithTag("Ball");

        if (Vector3.Distance(transform.position,paddle.transform.position) < 1){

            string type = GetComponentInChildren<SpriteRenderer>().sprite.name;

            if (type == "Vel_0"){
                foreach (GameObject ball in _ball){
                    ball.GetComponent<Ball>().Set_Speed(8);   
                }
            }
            else if (type == "Vel_1"){
                foreach (GameObject ball in _ball){
                    ball.GetComponent<Ball>().Set_Speed(3);   
                }
            }
            else if (type == "Size_0"){
                paddle.transform.localScale = new Vector3(1.5F, 1, 1);
            }
            else if (type == "Size_1"){
                paddle.transform.localScale = new Vector3(0.5F, 1, 1);
            }
            else if (type == "Multi_0"){
                foreach (GameObject ball in _ball)
                {
                    int ballCount = ArkanoidController._balls.Count;

                    if (ballCount <= 3){
                        Vector2 BallPosition = ball.transform.position;
                        for(int i=0;i<(3-ballCount);i++) {
                            Ball newBall = CreateBallAt(BallPosition);
                            newBall.Init();
                            ArkanoidController._balls.Add(newBall);
                        }
                    }
                }
            }

            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void ApplyPowerUpSprite(float random){

        if(random < 0.2f){
            path = string.Format(VELOCITY_PATH,0);
        } 
        else if(random < 0.4f && random >= 0.2f){
            path = string.Format(VELOCITY_PATH,1);
        } 
        else if(random < 0.6f && random >= 0.4f){
            path = string.Format(MULTI_PATH,0);
        } 
        else if(random < 0.8f && random >= 0.6f){
            path = string.Format(SIZE_PATH,0);
        } 
        else if(random < 1.0f && random >= 0.8f){
            path = string.Format(SIZE_PATH,1);
        } 

    }

    private Ball CreateBallAt(Vector2 position){

        _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
        
        return Instantiate(_ballPrefab, position, Quaternion.identity);
    }
}
