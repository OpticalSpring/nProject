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
    public GameObject mainCamera;
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

        camDistance = GameCameraLogic.CheckDistance(cameraPivot, mainCamera, correctionPos, maxDistance);

        Vector3 localPos = new Vector3(corDistance, correctionPos.y, -camDistance);
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, localPos, Time.fixedDeltaTime * 10f);
       
    }

    


}
