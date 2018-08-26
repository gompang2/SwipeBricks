using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour {

    Vector2 touchDownPos;
    Vector2 currentTouchPos;
    Vector2 swipeDirection;

    public GameObject touchUI;

    bool isClick = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        { 
            touchDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchUI.SetActive(true);
            touchUI.transform.position = touchDownPos;
            isClick = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClick = false;
            touchUI.SetActive(false);
        }

        if (isClick)
        {
            currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            swipeDirection = currentTouchPos - touchDownPos;

            Vector2 temp = touchUI.transform.localScale;
            temp.y = Mathf.Sqrt(swipeDirection.x * swipeDirection.x + swipeDirection.y * swipeDirection.y);
            touchUI.transform.localScale = temp;

            //Quaternion rot = Quaternion.Euler(swipeDirection.normalized);
            touchUI.transform.eulerAngles = new Vector3(0, 180, getAngle(touchUI.transform.position.x, touchUI.transform.position.y, currentTouchPos.x, currentTouchPos.y) + 0.0f);

            Debug.Log(currentTouchPos);
        }


        //if (Input.touches.Length > 0)
        //{
        //    Touch t = Input.GetTouch(0);
        //    if (t.phase == TouchPhase.Began)
        //    {
        //        touchDownPos = new Vector2(t.position.x, t.position.y);
        //        Debug.Log(touchDownPos);
        //    }
        //    else if (t.phase == TouchPhase.Moved)
        //    {
        //        currentTouchPos = new Vector2(t.position.x, t.position.y);
        //        swipeDirection = (currentTouchPos - touchDownPos).normalized;
        //    }
        //}
    }

    private float getAngle(float x1, float y1, float x2, float y2)
    {

        float dx = x2 - x1;
        float dy = y2 - y1;

        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;

        Debug.Log(degree);

        return degree;
    }
}
