using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    private const int LIFE_POINT_TO_FLEE = 25;
    private float moveSpeed = 1f;
    private const float REGEN_NORMALY = 1.0f;
    
    public override void Battle()
    {
        manageWizard.AttackEnnemiTargeted(manageWizard.damage);
        //throw new System.NotImplementedException();
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() <= LIFE_POINT_TO_FLEE)
        {
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Flee);
        }
        //if(killNumber >= 5)
        //changeState -> Fearless
    }

    public override void MoveToward()
    {
        GameObject closestTower = towerManager.getClosestEnemyActiveTower(gameObject.tag, transform);
        if (inCombat == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Init()
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin

        // Update is called once per frame
        
    }
    void Update()
    {
        if (!inCombat)
        {
            MoveToward();
        }
        else if (inCombat)
        {
            Battle();
        }
        manageWizard.RegenLifePoint(REGEN_NORMALY);
    }
}