using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public bool isShoot = false;

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
            isShoot = false;
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, -2.812f);
        }
    }
}
