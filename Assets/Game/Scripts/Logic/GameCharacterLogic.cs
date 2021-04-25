using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterLogic : MonoBehaviour
{
    public static bool CheckObstacle(GameObject gameCharacter, Vector3 directionVector)
    {
        RaycastHit rayHit;
        int mask = 1 << 2;
        mask = ~mask;

        if (Physics.SphereCast(gameCharacter.transform.position + new Vector3(0, 1, 0), 0.2f, directionVector, out rayHit, 0.5f, mask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Vector3 GetSlidingVector(GameObject gameCharacter, Vector3 directionVector, float speed)
    {
        RaycastHit rayHit;
        int mask = 1 << 2;
        mask = ~mask;

        //앞에 장애물이 타밎될 경우 슬라이딩 벡터를 통해 벽에 미끄러지는 것을 구현하여 답답하지 않은 조작을 의도
        Vector3 S;
        Vector3 V = Vector3.Normalize(directionVector);
        Vector3 nPoint = gameCharacter.transform.position;
        if (Physics.SphereCast(gameCharacter.transform.position, 0.2f, directionVector, out rayHit, 0.8f, mask))
        {
            S = V - rayHit.normal * (Vector3.Dot(V, rayHit.normal));
            if (!Physics.Raycast(gameCharacter.transform.position, S, out rayHit, 0.5f, mask))
            { 
                nPoint += S * 10;
                nPoint = Vector3.MoveTowards(gameCharacter.transform.position, nPoint, Mathf.Abs(S.x + S.y + S.z) * speed * Time.deltaTime * 1.0f);
            }
        }
        return nPoint;
    }

    public static bool CheckGround(GameObject gameCharacter, out Vector3 ground)
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8;
        mask = ~mask;
        ground = Vector3.zero;

        if (Physics.SphereCast(gameCharacter.transform.position + new Vector3(0, 1, 0), 0.2f, Vector3.down, out rayHit, 1f, mask))
        {
            ground = rayHit.point;
            return true;
        }
        else
        {
            return false;
        }
    }



}
