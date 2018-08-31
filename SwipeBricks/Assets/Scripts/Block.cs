using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    int life;

    public TextMesh lifeTxt;

	// Use this for initialization
	void Start () {
        life = 1;
        lifeTxt.text = life.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            life--;
            if (life <= 0) Destroy(gameObject);
            lifeTxt.text = life.ToString();
        }
    }
}
