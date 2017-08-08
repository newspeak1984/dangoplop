﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SizeType
{
	Ball,
	LargeBall,
	MediumBall,
	SmallBall

}



public class Ball_Behavioiur : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rb;
    public int thrust;
    private CircleCollider2D circle;
	public GameObject Ball;
	private GameObject Projectile;
	private SizeType type = SizeType.LargeBall;
	public float LargeBallScale;
	public float MedBallScale;
	public float SmallBallScale;
	public float Ball1TranslateX;
	public float Ball2TranslateX;
	public float Ball1TranslateY;
	public float Ball2TranslateY;
	public bool GameStart = true;
    public Vector2 position;
    public double maxHeight;
    public bool entered;






    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        rb.AddForce(Vector2.right * thrust);
        position = rb.transform.position;
        entered = false;
        
        
		
	}


		

	// Update is called once per frame
	void Update () {
        circle.isTrigger = false;
		var largeballscale = new Vector3 (LargeBallScale, LargeBallScale, 1);
		if (GameStart == true) {
			Ball.transform.localScale = largeballscale;
			GameStart = false;
		}
        position = rb.transform.position;
        if (position.y <= maxHeight)
        {
            entered = true;
        }
        if (entered && position.y >= maxHeight)
        {
            rb.AddForce(Vector2.down * 9);
            
        }



    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            circle.isTrigger = true;

        }
    }


	void HandleSplit(){
		Projectile = GameObject.FindGameObjectWithTag ("Projectile");

		if (type != SizeType.SmallBall) {
			
			var ball1Obj = Instantiate (Ball);
			var ball2Obj = Instantiate (Ball);
			Ball_Behavioiur ball1 = ball1Obj.GetComponent<Ball_Behavioiur> ();
			Ball_Behavioiur ball2 = ball2Obj.GetComponent<Ball_Behavioiur> ();
			ball1Obj.GetComponent<Rigidbody2D> ();
			ball2Obj.GetComponent<Rigidbody2D> ();

			Vector2 temp = transform.position;
			ball1Obj.transform.position = temp;
			ball1Obj.transform.Translate (Ball1TranslateX, Ball1TranslateY, 0, Space.World);
			ball2Obj.transform.position = temp;
			ball2Obj.transform.Translate (Ball2TranslateX, Ball2TranslateY, 0, Space.World);
			var medballscale = new Vector3 (MedBallScale, MedBallScale, 1);
			var smallballscale = new Vector3 (SmallBallScale, SmallBallScale, 1);

			if (type == SizeType.LargeBall) {
				ball1Obj.transform.localScale = medballscale;
				ball2Obj.transform.localScale = medballscale;
                ball1.type = SizeType.MediumBall;
				ball2.type = SizeType.MediumBall;
                ball1.mediumHeight();
                ball2.mediumHeight();


            }

			else if (type == SizeType.MediumBall) {
				ball1Obj.transform.localScale = smallballscale;
                ball2Obj.transform.localScale = smallballscale;
                ball1.type = SizeType.SmallBall;
				ball2.type = SizeType.SmallBall;
                ball1.smallHeight();
                ball2.smallHeight();


            }

		}
		if (Projectile == true) {
			Destroy (gameObject);
		}
	}




	void OnTriggerEnter2D(Collider2D blip){
		if (blip.gameObject.tag == "Projectile") {
			HandleSplit ();
		}
	}

    public void mediumHeight()
    {
        maxHeight = -1;
    }
    public void smallHeight()
    {
        maxHeight = -2;
    }

    public void speedChange(int newSpeed)
    {
        thrust = newSpeed;
    }
}
