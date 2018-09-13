using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    bool shootReady = false;
    bool isCreate = false;
    bool addBall = false;
    public bool gameOver = false;
    public bool ShootReady { get { return shootReady; } }
    int shootCnt = 0;
    public int score = 0;

    public static GameManager instance;

    public GameObject ballPrefab;
    public GameObject blockPrefab;
    public GameObject itemPrefab;

    public GameObject ballIndex;
    public GameObject genBallIndex;
    public GameObject objectIndex;

    public GameObject firstBall;

    public bool isArrival = false;

    public Text currentScore;
    public Text highScore;
    public TextMesh ballCnt;
    public GameObject gameOverUI;

    

	// Use this for initialization
	void Start () {
        instance = this;
        SpawnObject();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!shootReady)
        {
            CheckBallReady();
        }
	}

    public void ShootStart(Vector2 vec)
    {
        isArrival = false;
        shootCnt = 0;
        StartCoroutine(Shoot(vec));
        shootReady = false;
        addBall = false;
        isCreate = false;
    }

    IEnumerator Shoot(Vector2 vec)
    {
        if(shootCnt < ballIndex.transform.childCount)
        {
            ballIndex.transform.GetChild(shootCnt).GetComponent<Ball>().isShoot = true;
            ballIndex.transform.GetChild(shootCnt++).GetComponent<Rigidbody2D>().AddForce(vec * 600);
            ballCnt.text = (System.Convert.ToInt32(ballCnt.text) - 1).ToString();
            if (System.Convert.ToInt32(ballCnt.text) <= 0) ballCnt.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(Shoot(vec));
        }
    }

    void CheckBallReady()
    {
        int endShootBallCnt = 0;

        for (int i = 0; i < ballIndex.transform.childCount; i++)
        {
            if (!ballIndex.transform.GetChild(i).GetComponent<Ball>().isShoot)
            {
                endShootBallCnt++;
            }
        }

        if (endShootBallCnt == ballIndex.transform.childCount)
        {
            if (!addBall)
            {
                int temp = genBallIndex.transform.childCount;

                for (int i = 0; i < temp; i++)
                {
                    genBallIndex.transform.GetChild(0).GetComponent<Ball>().MoveAfterGeneration(firstBall.transform.position);
                }

                addBall = true;
            }

            else if (addBall && !isCreate) AddScore();
        }
    }

    void SpawnObject()
    {
        int randomVal = Random.Range(1, 6);
        List<float> posArr = new List<float>();
        isCreate = true;

        posArr.Add(-2.37f);
        posArr.Add(-1.42f);
        posArr.Add(-0.475f);
        posArr.Add(0.475f);
        posArr.Add(1.42f);
        posArr.Add(2.37f);

        int randomeNum = Random.Range(0, posArr.Count);
        Transform temp = Instantiate(itemPrefab).transform;

        temp.position = new Vector2(posArr[randomeNum], 2.7f);
        temp.parent = objectIndex.transform;
        posArr.RemoveAt(randomeNum);

        for(int i = 0; i < randomVal; i++)
        {
            randomeNum = Random.Range(0, posArr.Count);
            temp = Instantiate(blockPrefab).transform;
            temp.position = new Vector2(posArr[randomeNum], 2.7f);
            temp.parent = objectIndex.transform;
            temp.GetComponent<Block>().Init(score + 1);
            posArr.RemoveAt(randomeNum);
        }

        StartCoroutine(ObjectMoveDown(10));
    }

    void AddScore()
    {
        score++;

        currentScore.text = score.ToString();
        if (System.Convert.ToInt32(highScore.text) < score)
        {
            highScore.text = score.ToString();
        }

        SpawnObject();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverUI.SetActive(true);
        gameOverUI.transform.GetChild(0).GetComponent<Text>().text = score.ToString();
    }

    IEnumerator ObjectMoveDown(int cnt)
    {
        for(int i = 0; i < objectIndex.transform.childCount; i++)
        {
            Transform tempTransform = objectIndex.transform.GetChild(i);

            tempTransform.position -= new Vector3(0, 0.06f);
            if (tempTransform.gameObject.tag == "Block" && tempTransform.position.y <= -2.69 && !gameOver)
                GameOver(); 
        }

        yield return new WaitForSeconds(0.0005f);
        if (cnt > 1) StartCoroutine(ObjectMoveDown(--cnt));
        else
        {
            shootReady = true;
            ballCnt.transform.position = new Vector3(firstBall.transform.position.x, -3.3f);
            ballCnt.text = ballIndex.transform.childCount.ToString();
            ballCnt.gameObject.SetActive(true);
        }
    }
}
