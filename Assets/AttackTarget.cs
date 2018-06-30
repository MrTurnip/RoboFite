using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : MonoBehaviour
{
    public OptionsCrescent master;
    public Actor actor { get { return master.targetActor; } }
    public System.Action phase;

    public List<GameObject> subjects;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject subject in subjects)
        {
            Animator animator = subject.GetComponent<Animator>();
            animator.speed = 0;

            Collider2D collider = subject.GetComponent<Collider2D>();
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        BattleManager battleManager = GameObject.FindObjectOfType<BattleManager>();

        BattleManager.Context context = battleManager.context;
        if (context != BattleManager.Context.ChoosingAttackType)
        {
            battleManager.SwitchTo_ChoosingAttackType();
            foreach (GameObject subject in subjects)
            {
                Animator animator = subject.GetComponent<Animator>();
                animator.speed = 1;

                Collider2D collider = subject.GetComponent<Collider2D>();
                collider.enabled = true;
            }
        }
    }
}

