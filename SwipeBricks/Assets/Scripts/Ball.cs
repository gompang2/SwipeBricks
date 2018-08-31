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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item")
        {
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();

            Ball tempBall = GameObject.Instantiate(GM.ballPrefab).GetComponent<Ball>();
            tempBall.isGenerated = true;
            tempBall.transform.position = col.transform.position;

            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, -2.82f);

            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (!isGenerated)
            {
                if (!GM.isArrival)
                {
                    GM.firstBall = gameObject;
                    GM.isArrival = true;
                    isShoot = false;
                }

                else if (GM.isArrival)
                {
                    target = GM.firstBall.transform.position;
                    speed = target.x - transform.position.x;
                    isLeft = speed >= 0 ? true : false;
                    StartCoroutine(Move());
                }
            }
        }
    }


    IEnumerator Move()
    {
        transform.position += new Vector3(speed, 0) * 0.08f;


        if (isLeft ? transform.position.x >= target.x : transform.position.x <= target.x)
        {
            transform.position = target;
            isShoot = false;
        }
        else
        {
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(Move());
        }
    }
}
