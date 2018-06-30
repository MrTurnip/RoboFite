using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPrimaryAttack : MonoBehaviour {

    public void OnMouseDown()
    {
        BattleManager battleManager = GameObject.FindObjectOfType<BattleManager>();
        if (battleManager.context == BattleManager.Context.ChoosingAttackType)
        {
            battleManager.selectedActor.primaryAttack.Invoke();
            battleManager.selectedActor.attackType = Actor.AttackType.Primary;
            battleManager.context = BattleManager.Context.attack_ChoosingTarget;
            
        }
    }
}
