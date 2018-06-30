using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{

    [System.Serializable]
    public class Health
    {
        public int vitality, armor, shield;
        public int maxVitality, maxArmor, maxShield;
        public bool IsDead { get { return vitality <= 0; } }
        public HealthInformation healthReadings;

        public float VitalityPercentage
        {
            get
            {
                if (maxVitality == 0)
                    return 0;
                return (float)vitality / (float)maxVitality;
            }
        }
        public float ArmorPercentage
        {
            get
            {
                if (maxArmor == 0)
                    return 0;
                return (float)armor / (float)maxArmor;
            }
        }
        public float ShieldPercentage
        {
            get
            {
                if (maxShield == 0)
                    return 0;
                return (float)shield / (float)maxShield;
            }
        }

        public void DamageDirect(DamageTarget.Attack attack)
        {
            int inGoing = attack.damageCheck.finalValue;

            for (; shield > 0 && inGoing > 0; shield--, inGoing--)
            {

            }

            if (armor > 0 && inGoing > 0)
            {
                inGoing -= 5;
                if (inGoing < 1)
                    inGoing = 1;

                for (; armor > 0 && inGoing > 0; armor--, inGoing--) { }
            }

            vitality -= inGoing;
            if (vitality < 0)
                vitality = 0;

            healthReadings.ResetHealthBars();
        }
    }

    public string name;
    public enum Type { combatant, button }
    public Type type;

    public GameObject gameobject;


    private BattleManager battleManager;

    public enum AttackType { None, Primary, Secondary, Ultimate }
    public AttackType attackType = AttackType.None;

    public UnityEvent primaryAttack, secondaryAttack, primaryAbility, secondaryAbility, ultimateAbility, activeOption;

    public Health health = new Health();

    private void ResetWeapon()
    {
        DamageTarget damageTarget = GameObject.FindObjectOfType<DamageTarget>();
        DamageTarget.DamageCheck damageCheck = damageTarget.damageCheck;
     
    }

    public void Awake()
    {
        gameobject = this.gameObject;
    }

    public void Start()
    {
        battleManager = GameObject.FindObjectOfType<BattleManager>();
    }

    public void OnMouseDown()
    {
        BattleManager battleManager = GameObject.FindObjectOfType<BattleManager>();


        if (battleManager.selectedActor == null)
        {
            if (battleManager.context == BattleManager.Context.SearchingForCombatant && this.type == Type.combatant)
            {
                battleManager.selectedActor = this;
                battleManager.context = BattleManager.Context.SelectingOption;
                OptionsCrescent optionsCrescent = GameObject.FindObjectOfType<OptionsCrescent>();
                optionsCrescent.RevealOptions();
                optionsCrescent.transform.position = this.transform.position;
            }
        }

        if (battleManager.context == BattleManager.Context.attack_ChoosingTarget)
        {
            if (battleManager.selectedActor != this && battleManager.targetedActor == null)
            {
                battleManager.targetedActor = this;
                battleManager.context = BattleManager.Context.attack_DealingDamage;
                //battleManager.selectedActor.primaryAttack.Invoke();

                DamageTarget damageTarget = GameObject.FindObjectOfType<DamageTarget>();
                damageTarget.AttackTarget(battleManager.selectedActor, battleManager.targetedActor);
                damageTarget.transform.position = this.transform.position;

            }
        }
    }

}
