using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraLogic : MonoBehaviour
{
    public static float CheckDistance(GameObject camPivot, GameObject camMain, Vector2 correctionPos, float maxDistance)
    {
        RaycastHit rayHit;

        int mask = 1 << 2 | 1 << 8;
        mask = ~mask;
        if (Physics.Raycast(camPivot.transform.position + new Vector3(0, correctionPos.y, 0), -camMain.transform.forward, out rayHit, maxDistance, mask))
        {
            Vector3 hitPoint = rayHit.point;

            return Vector3.Distance(hitPoint, camPivot.transform.position + new Vector3(0, correctionPos.y, 0)) - 0.5f;
        }
        else
        {
            return maxDistance - 0.5f;
        }

    }

    public static void CheckObject(GameObject camMain, out RaycastHit rayHit, int maxDistance)
    {
        int mask = 1 << 2;
        mask = ~mask;
        Physics.Raycast(camMain.transform.position, camMain.transform.forward, out rayHit, maxDistance, mask);
    }

    public static bool CheckEnableConsume(GameObject camMain, out RaycastHit rayHit, int maxDistance)
    {
        CheckObject(camMain, out rayHit, maxDistance);

        if (rayHit.collider?.gameObject?.GetComponent<GameCharacter>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}