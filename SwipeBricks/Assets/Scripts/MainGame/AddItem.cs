using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour {

    int cnt = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(cnt >= 10)
        {
            Ball tempBall = Instantiate(GameManager.instance.ballPrefab).GetComponent<Ball>();
            tempBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            tempBall.transform.parent = GameManager.instance.genBallIndex.transform;
            tempBall.transform.position = transform.position;
            tempBall.MoveAfterGeneration(GameManager.instance.firstBall.transform.position);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Ball tempBall = Instantiate(GameManager.instance.ballPrefab).GetComponent<Ball>();
            tempBall.transform.parent = GameManager.instance.genBallIndex.transform;
            tempBall.isGenerated = true;
            tempBall.isShoot = true;
            tempBall.transform.position = col.transform.position;

            Destroy(gameObject);
        }

        //else if (col.gameObject.tag == "Ground")
        //{
        //    GameManager GameManager.instance = GameObject.Find("GameManager").GetComponent<GameManager>();

        //    Ball tempBall = GameObject.Instantiate(GameManager.instance.ballPrefab).GetComponent<Ball>();
        //    tempBall.transform.parent = GameManager.instance.genBallIndex.transform;
        //    tempBall.isGenerated = true;
        //    tempBall.isShoot = true;
        //    tempBall.transform.position = new Vector2(transform.position.x, -2.85f);

        //    Destroy(gameObject);
        //}
    }
}
