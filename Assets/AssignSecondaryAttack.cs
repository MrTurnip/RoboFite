using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignSecondaryAttack : MonoBehaviour {

    public void OnMouseDown()
    {
        BattleManager battleManager = GameObject.FindObjectOfType<BattleManager>();
        if (battleManager.context == BattleManager.Context.ChoosingAttackType)
        {
            battleManager.selectedActor.secondaryAttack.Invoke();
            battleManager.selectedActor.attackType = Actor.AttackType.Secondary;
            battleManager.context = BattleManager.Context.attack_ChoosingTarget;
           
        }
    }
}
