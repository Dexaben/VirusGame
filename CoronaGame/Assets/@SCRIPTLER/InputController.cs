using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    GameObject touchcircle;
    int layer = 8;
    public List<touchLocation> touches = new List<touchLocation>();
    public Transform gameArea;
    public class touchLocation
    {
        public int touchId;
        public GameObject touchCircle;
        public touchLocation(int touchID,GameObject touchCIRCLE)
        {
            touchId = touchID;
            touchCircle = touchCIRCLE;
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return new Vector3(touchPosition.x, touchPosition.y);
    }
    GameObject createCircle(Touch t)
    {
        GameObject c = Instantiate(touchcircle,gameArea) as GameObject;
        c.name = "Touch" + t.fingerId;
       
        c.transform.position = getTouchPosition(t.position);
        return c;
    }
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began)
            {
                touches.Add(new touchLocation(t.fingerId, createCircle(t)));
            }
            else if(t.phase == TouchPhase.Ended)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                Destroy(thisTouch.touchCircle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            ++i;
        }
          
        
    }
}
