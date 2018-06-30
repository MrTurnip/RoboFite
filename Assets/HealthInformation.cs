using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthInformation : MonoBehaviour {

    public Actor actor;
    public StatisticBar vitality, armor, shield;

    public event System.Action HealthAdjusted;

    private void SetGameObjectXScale(GameObject go, float newScale)
    {
        Vector3 scale = go.transform.localScale;
        scale.x = newScale;
        go.transform.localScale = scale;
    }

    private void SetVitalityXScale()
    {
        SetGameObjectXScale(vitality.gameObject, actor.health.VitalityPercentage);
    }

    private void SetArmorXScale()
    {
        SetGameObjectXScale(armor.gameObject, actor.health.ArmorPercentage);
    }

    private void SetShieldXScale()
    {
        SetGameObjectXScale(shield.gameObject, actor.health.ShieldPercentage);
    }

    public void ResetHealthBars()
    {
        SetVitalityXScale();
        SetArmorXScale();
        SetShieldXScale();
    }

    // Use this for initialization
    void Start () {
        HealthAdjusted += ResetHealthBars;

        DamageTarget damageTarget = GameObject.FindObjectOfType<DamageTarget>();

        ResetHealthBars();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
