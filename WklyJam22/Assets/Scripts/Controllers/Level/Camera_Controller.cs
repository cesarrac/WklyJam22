using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

    public static Camera_Controller instance { get; protected set; }

    Transform target;

    public float dampTime = 0.15f, folMouseDampTime;
    float followDampTime;
    private Vector3 velocity = Vector3.zero;


    public float camXPosMin = 0, camXPosMax = 0;
    public float camYPosMin = 0, camYPosMax = 0;
    float lastDestX;
    int xBoundLeft, xBoundRight;
    public delegate void OnBoundsChanged(int left, int right);
    public event OnBoundsChanged onBoundsChanged;

    float camXOffset = 4;

    void OnEnable()
    {
        instance = this;
        followDampTime = dampTime;
    }
/* 
    public void SetTargetAndLock(Transform t, float xMIn, float xMax, float yMin, float yMax)
    {
        camXPosMin = xMIn;
        camXPosMax = xMax;
        camYPosMin = yMin;
        camYPosMax = yMax;
        target = t;
    } */
    public void SetTargetAndLock(Transform t)
    {
        target = t;
    }


    void Update()
    {
        if (target)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        dampTime = followDampTime;

        //Vector3 targetPos = target.position;
        //targetPos.x = Mathf.Ceil(target.position.x);
        //targetPos.y = Mathf.Ceil(target.position.y);
        Vector3 targetPos = target.position + new Vector3(camXOffset, 0, 0);
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(targetPos);
        Vector3 delta = targetPos - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        CheckCameraBounds();
        destination.x = Mathf.Clamp(destination.x, camXPosMin + (Camera.main.orthographicSize * 2), camXPosMax - (Camera.main.orthographicSize * 2));
        destination.y = Mathf.Clamp(destination.y, camYPosMin, camYPosMax);
    
        Vector3 moveVector = transform.position;
        moveVector = Vector3.SmoothDamp(moveVector, destination, ref velocity, dampTime);
     /*    
        if (xBoundLeft < camXPosMin || xBoundRight > camXPosMax){
            moveVector.x = transform.position.x;
        } */
        transform.position = moveVector;
    }

    void CheckCameraBounds(){
		int rightEdgeX = Mathf.RoundToInt(transform.position.x + (Camera.main.orthographicSize * 2));
		int leftEdgeX = Mathf.CeilToInt(transform.position.x - (Camera.main.orthographicSize * 2));
        if (xBoundLeft != leftEdgeX || xBoundRight != rightEdgeX){
            xBoundLeft = leftEdgeX;
            xBoundRight = rightEdgeX;
            if (onBoundsChanged != null){
                onBoundsChanged(xBoundLeft, xBoundRight);
            }
            
		   // Debug.Log("Edge left = " + leftEdgeX + " Edge right = " + rightEdgeX);
        }
    }
}
