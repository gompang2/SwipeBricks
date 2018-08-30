using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    bool shootReady = true;
    public bool ShootReady { get{ return shootReady; } }

    public GameObject ball;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot(Vector2 vec)
    {
        ball.GetComponent<Rigidbody2D>().AddForce(vec * 300);
        shootReady = false;
    }
}
