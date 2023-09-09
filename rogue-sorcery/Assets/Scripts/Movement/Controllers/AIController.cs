using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public Transform playerTrans;
    public Transform enemyTrans;
    public override float RetrieveMoveInput()
    {
        if (playerTrans != null && enemyTrans != null)
        {
            if (playerTrans.position.x > enemyTrans.position.x)
            {
                return 1f;
            }
        }
        return -1f;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override bool RetrieveDashInput()
    {
        return true;
    }
}
