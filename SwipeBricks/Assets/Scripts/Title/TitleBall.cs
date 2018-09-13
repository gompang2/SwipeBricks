using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class TitleBall : MonoBehaviour {

    public bool shootReady = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot(Vector2 vec)
    {
        shootReady = false;
        transform.GetComponent<Rigidbody2D>().AddForce(vec * 600);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, -2.85f);
            shootReady = true;
        }

        if(col.gameObject.name == "Start")
        {

        }
    }

}
