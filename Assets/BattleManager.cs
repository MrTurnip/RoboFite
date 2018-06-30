using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public class TargetSelection
    {
        public bool selectionComplete = false;
        public List<Target> targets = new List<Target>();
    }
    public class Target
    {
        public Actor actor;
        public float distance = 0.0f;
        public bool inRange = true;

        int accuracyPenalty { get { return -((int)distance / 5); } }
    }

    public Actor selectedActor;
    public const Actor nullActor = null;
    public Actor targetedActor;

    public TargetSelection targetSelection = new TargetSelection();

    public enum Context { SearchingForCombatant, SelectingOption, ChoosingAttackType, attack_ChoosingTarget, attack_DealingDamage }
    public Context context = Context.SearchingForCombatant;

    public void SwitchTo_AttackPhase()
    {
        context = Context.attack_ChoosingTarget;
    }

    public void SwitchTo_ChoosingAttackType()
    {
        context = Context.ChoosingAttackType;
    }
    

    public void NextTurn()
    {
        selectedActor = null;
        targetedActor = null;
        OptionsCrescent optionsCrescent = GameObject.FindObjectOfType<OptionsCrescent>();
        optionsCrescent.HideOptions();
        context = Context.SearchingForCombatant;
        targetSelection.selectionComplete = false;
    }

    public void Update()
    {
        if (context == Context.attack_ChoosingTarget)
        {
            if (targetSelection.selectionComplete == false)
            {
                List<Actor> actors = new List<Actor>(GameObject.FindObjectsOfType<Actor>());

                foreach (Actor actor in actors)
                {
                    GameObject gameObject = actor.gameObject;
                    Transform transform = gameObject.transform;
                    Vector2 position = transform.position;

                    Transform thisTransform = this.transform;
                    Vector2 thisPosition = thisTransform.position;

                    float distanceBetweenThisAndActor = Vector2.Distance(thisPosition, position);

                    Target target = new Target();
                    target.actor = actor;
                    target.distance = distanceBetweenThisAndActor;
                    target.inRange = true;

                    targetSelection.targets.Add(target);
                }

                targetSelection.selectionComplete = true;
            }
        }
    }
}
