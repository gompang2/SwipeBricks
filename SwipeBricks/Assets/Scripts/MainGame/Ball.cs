using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    bool isLeft = false;
    float speed;
    Vector2 target;

    public bool isShoot = false;
    public bool isGenerated = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, -2.85f);

            if (!isGenerated)
            {
                if (!GameManager.instance.isArrival)
                {
                    GameManager.instance.firstBall = gameObject;
                    GameManager.instance.isArrival = true;
                    isShoot = false;
                }

                else if (GameManager.instance.isArrival)
                {
                    target = GameManager.instance.firstBall.transform.position;
                    speed = target.x - transform.position.x;
                    isLeft = speed >= 0 ? true : false;
                    StartCoroutine(Move());
                }
            }
        }
    }
    
    public void MoveAfterGeneration(Vector2 vec)
    {

        transform.parent = GameManager.instance.ballIndex.transform;
        target = GameManager.instance.firstBall.transform.position;
        speed = target.x - transform.position.x;
        isLeft = speed >= 0 ? true : false;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        transform.position += new Vector3(speed, 0) * 0.08f;


        if (isLeft ? transform.position.x >= target.x : transform.position.x <= target.x)
        {
            transform.position = target;
            isShoot = false;
            if (isGenerated)
            {
                isGenerated = false;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            gameObject.layer = 8;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }

        else
        {
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(Move());
        }
    }
}
