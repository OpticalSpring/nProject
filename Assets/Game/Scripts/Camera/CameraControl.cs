using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject CamTarget;
    public float rotationSpeed;
    public float maxDistance;
    public Vector2 correctionPos;
    public float corDistance;
    public float camDistance;
    public GameObject MainCamera;
    public GameObject CamPivot;
    public Vector3 rotateValue;


    // Start is called before the first frame update
    void Start()
    {

        MainCamera = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        CamPivot = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(CamTarget == null)
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
        gameObject.transform.position = CamTarget.transform.position;
    }

    //카메라를 회전시킨다.
    void RotateCam()
    {
        Quaternion targetRotation;

        rotateValue.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x = Mathf.Clamp(rotateValue.x, -30f, 30f);
        targetRotation = Quaternion.Euler(rotateValue);

        CamPivot.transform.rotation = targetRotation;
    }

    

    //카메라가 주변 지형지물에 걸리는지 체크한다.
    void CheckDistance()
    {

        camDistance = GameCameraLogic.CheckDistance(CamPivot, MainCamera, correctionPos, maxDistance);

        Vector3 localPos = new Vector3(corDistance, correctionPos.y, -camDistance);
        MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, localPos, Time.fixedDeltaTime * 10f);
       
    }

    


}
