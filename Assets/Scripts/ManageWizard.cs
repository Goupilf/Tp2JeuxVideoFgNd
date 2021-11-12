using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWizard : MonoBehaviour
{
    private const int STARTING_LIFE = 100;
    private int lifePoint = STARTING_LIFE;
    private WizardState wizardState;
    public GameObject ignoreObject;
    public GameObject towerHide;
    private float regenClock = 0f; 

    public enum WizardStateToSwitch { Flee, Hide, Safety, Fearless, Normal };

    // Start is called before the first frame update
    void Start()
    {
        wizardState = GetComponent<WizardState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoint == 0)
        {
            Die();
        }
    }

    public int GetLifePoint()
    {
        return this.lifePoint;
    }

    public void ApplyDamage(int damage)
    {
        this.lifePoint -= damage;
        if (lifePoint < 0)
        {
            lifePoint = 0;
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
        this.lifePoint = STARTING_LIFE;
    }

    public void ChangeWizardState(WizardStateToSwitch nextState)
    {
        Destroy(wizardState);

        switch (nextState)
        {
            case WizardStateToSwitch.Flee:
                {
                    wizardState = gameObject.AddComponent<WizardStateFlee>() as WizardStateFlee;
                    break;
                }
        }
    }

    public void RegenLifePoint(float regenRate)
    {
        if (regenClock >= regenRate)
        {
            regenClock = 0;
            lifePoint++;
        }
        regenClock += Time.deltaTime;
    }
}
