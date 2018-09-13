using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    int life;
    SpriteRenderer mySpr;

    public TextMesh lifeTxt;


	// Use this for initialization
	void Start () {
        mySpr = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        ChangeColor();
    }

    public void Init(int i)
    {
        life = i;
        lifeTxt.text = life.ToString();
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

    public void ChangeColor()
    {
        float gColor = (GameManager.instance.score - life + 1) * (180.0f / (float)GameManager.instance.score);

        mySpr.color = new Color32(255, (byte)gColor, 0, 255);
    }
}
