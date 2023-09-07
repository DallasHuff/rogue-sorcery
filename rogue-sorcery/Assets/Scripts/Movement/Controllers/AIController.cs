using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private Transform enemyTrans;
    public override float RetrieveMoveInput()
    {
        //if (playerTrans.position.x > enemyTrans.position.x)
        //{
        //    return 1f;
        //}
        return -1f;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
}
