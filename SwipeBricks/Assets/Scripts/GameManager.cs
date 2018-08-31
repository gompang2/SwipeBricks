using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    bool shootReady = true;
    public bool ShootReady { get{ return shootReady; } }
    int shootCnt = 0;

    public GameObject ballPrefab;
    public GameObject ballIndex;
    public GameObject firstBall;
    public bool isArrival = false;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (!shootReady)
        {
            int endShootBallCnt = 0;

            for (int i = 0; i < ballIndex.transform.childCount; i++)
            {
                if (!ballIndex.transform.GetChild(i).GetComponent<Ball>().isShoot)
                {
                    endShootBallCnt++;
                }
            }

            if (endShootBallCnt == ballIndex.transform.childCount) shootReady = true;
        }
	}

    public void ShootStart(Vector2 vec)
    {
        isArrival = false;
        shootCnt = 0;
        StartCoroutine(Shoot(vec));
        shootReady = false;
    }

    IEnumerator Shoot(Vector2 vec)
    {
        if(shootCnt < ballIndex.transform.childCount)
        {
            ballIndex.transform.GetChild(shootCnt).GetComponent<Ball>().isShoot = true;
            ballIndex.transform.GetChild(shootCnt++).GetComponent<Rigidbody2D>().AddForce(vec * 300);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Shoot(vec));
        }

    }
}
