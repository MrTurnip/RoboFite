using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
    public const int ACCURACY_FACES = 100;

    public enum Type { Firearm, Thrown, Melee, Unarmed, Explosive, Artillary, Laser, Chemical }
    public enum Subtype { Bludgeon, Puncture, Slash, Fire, Poison, Electricity }
    public event System.Action DamagingTarget;

    public class AccuracyCheck
    {
       // public int accuracyRollValue;
        public bool attackMissed = false;
    }
    [System.Serializable]
    public class DamageCheck
    {
        public int amountOfDice = 1, amountOfFaces = 5, dieBonus = 0, finalBonus = 0;
        public int finalValue = 0;
    }

    public class Attack
    {
        public AccuracyCheck accuracyCheck = new AccuracyCheck();
        public DamageCheck damageCheck = new DamageCheck();

        public int totalDamageDealt = 0;
    }

    public void ResetAllValues()
    {
        attack.damageCheck.amountOfDice = 1;
        attack.damageCheck.amountOfFaces = 10;
        attack.damageCheck.dieBonus = 0;
        attack.damageCheck.finalBonus = 0;
        attack.damageCheck.finalValue = 0;
        attackerAccuracy = 0;
        subjectDodge = 0;
        weaponAccuracy = 50;
        amountOfAttacks = 1;
        attacksMade = 0;
    }

    public void SetToSniperRifle1()
    {
        ResetAllValues();
        attack.damageCheck.amountOfDice = 1;
        attack.damageCheck.amountOfFaces = 50;
        attack.damageCheck.finalBonus = 50;
        weaponAccuracy = 75;
        amountOfAttacks = 1;
    }

    public void SetToAssaultRifle1()
    {
        ResetAllValues();
        amountOfAttacks = 10;
        attack.damageCheck.amountOfDice = 1;
        attack.damageCheck.amountOfFaces = 10;
        weaponAccuracy = 50;
        Debug.Log("FDFD");
    }

    private Attack attack = new Attack();
    public float checkRate = 0.1f;
    private float checkWait = 0;
    private TextMesh damageReadout;

    private List<Attack> attacks = new List<Attack>();

    public Actor offender, defender;
    public Type type;
    public Subtype subtype;
    public DamageCheck damageCheck;
    public int attackerAccuracy, subjectDodge, weaponAccuracy;
    public int amountOfAttacks = 1, attacksMade = 0;
    public float aftermathTime = aftermathViewReset;
    public const float aftermathViewReset = 2.0f;

    public int AccuracyScore { get { return (weaponAccuracy - attackerAccuracy) + subjectDodge; } }

    public enum Phase { Idle, Attacking, ViewingAftermath, Finished }
    public Phase phase = Phase.Idle;

    public void ResetStats()
    {
        offender = null;
        defender = null;
        attack = new Attack();
        attacksMade = 0;
    }

    public void AttackTarget(Actor offender, Actor defender)
    {
        this.offender = offender;
        this.defender = defender;
        phase = Phase.Attacking;
    }

    public void RollAttack()
    {
        attack.damageCheck.finalValue = 0;

        int roll = Random.Range(1, ACCURACY_FACES + 1);
        if (roll <= AccuracyScore)
        {
            attack.accuracyCheck.attackMissed = false;

            for (int i = 0; i < attack.damageCheck.amountOfDice; i++)
            {
                attack.damageCheck.finalValue += Random.Range(1, attack.damageCheck.amountOfFaces + 1);
                attack.damageCheck.finalValue += attack.damageCheck.dieBonus;
            }

            attack.damageCheck.finalValue += attack.damageCheck.finalBonus;
            attack.totalDamageDealt += attack.damageCheck.finalValue;
            defender.health.DamageDirect(attack);
        }
        else
        {
            attack.accuracyCheck.attackMissed = true;
        }
        

        attacksMade++;
    }
    
    public void Awake()
    {
        damageReadout = GetComponentInChildren<TextMesh>();
    }

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == Phase.Attacking)
        {
            if (attacksMade < amountOfAttacks)
            {
                checkWait += Time.deltaTime;

                if (checkWait >= checkRate)
                {
                    RollAttack();

                    

                    if (attack.accuracyCheck.attackMissed == false)
                    {
                        damageReadout.text = attack.damageCheck.finalValue.ToString();
                        
                    }
                    else
                    {
                        damageReadout.text = "Miss";
                    }

                    checkWait = 0;
                }
            }
            else
            {
                phase = Phase.ViewingAftermath;
            }
        }

        if (phase == Phase.ViewingAftermath)
        {
            if (aftermathTime > 0)
            {
                aftermathTime -= Time.deltaTime;
            }
            else
            {
                aftermathTime = aftermathViewReset;
                phase = Phase.Finished;
                damageReadout.text = "";
                ResetAllValues();
            }
        }

        if (phase == Phase.Finished)
        {

            BattleManager battleManager = GameObject.FindObjectOfType<BattleManager>();
            battleManager.NextTurn();
            phase = Phase.Idle;
            ResetStats();
        }
    }
}
