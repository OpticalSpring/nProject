using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    public GameObject camTarget;
    public float rotationSpeed;
    public float maxDistance;
    public Vector2 correctionPos;
    public float corDistance;
    public float camDistance;
    GameObject mainCamera;
    GameObject cameraPivot;
    public Vector3 rotateValue;


    // Start is called before the first frame update
    void Start()
    {

        mainCamera = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        cameraPivot = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(camTarget == null)
        {
            return;
        }
        FollowCam();
        RotateCam();
        CheckDistance();
    }

    //카메라가 타겟을 따라간다.
    void FollowCam()
    {
        gameObject.transform.position = camTarget.transform.position;
    }

    //카메라를 회전시킨다.
    void RotateCam()
    {
        Quaternion targetRotation;

        rotateValue.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x = Mathf.Clamp(rotateValue.x, -30f, 30f);
        targetRotation = Quaternion.Euler(rotateValue);

        cameraPivot.transform.rotation = targetRotation;
    }

    

    //카메라가 주변 지형지물에 걸리는지 체크한다.
    void CheckDistance()
    {
        RaycastHit rayHit;

        
        int mask = 1 << 2 | 1 << 8;
        mask = ~mask;
        if (Physics.Raycast(cameraPivot.transform.position+ new Vector3(0, correctionPos.y, 0), -mainCamera.transform.forward, out rayHit, maxDistance, mask))
        {
            Vector3 hitPoint = rayHit.point;

            camDistance = Vector3.Distance(hitPoint, cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0)) - 0.5f;
        }
        else
        {
            camDistance = maxDistance - 0.5f;
        }

        
        

        
        //if (Physics.Raycast(cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0), cameraPivot.transform.right, out rayHit, 3.0f, mask))
        //{
        //    Vector3 hitPoint = rayHit.point;
        //    corDistance = Vector3.Distance(hitPoint, cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0))-0.7f;
        //    if (corDistance > correctionPos.x) corDistance = correctionPos.x;
        //}
        //else
        //{
        //    corDistance = correctionPos.x;
        //}


        Vector3 localPos = new Vector3(corDistance, correctionPos.y, -camDistance);
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, localPos, Time.fixedDeltaTime * 10f);
       
    }

    


}
