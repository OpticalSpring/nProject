using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacterLogic : MonoBehaviour
{
    public static bool CheckObstacle(GameObject gameCharacter)
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 9;
        mask = ~mask;

        if (Physics.SphereCast(gameCharacter.transform.position + new Vector3(0, 1, 0), 0.2f, gameCharacter.transform.forward, out rayHit, 0.3f, mask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static Vector3 CalcMovePosition(GameObject gameCharacter, float speed)
    {
        return Vector3.zero;
    }


}
