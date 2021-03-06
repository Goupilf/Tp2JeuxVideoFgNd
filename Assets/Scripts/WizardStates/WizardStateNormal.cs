using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    private const float REGEN_NORMALY = 1.0f;
    private const int LIFE_POINT_TO_FLEE = 25;
    private float moveSpeed = 1f;
    private float battleClock = 0f;
    private float timeBetweenAttacks = 2f;
    
    public override void Battle()
    {
        if (battleClock >= timeBetweenAttacks)
        {
            
            battleClock = 0;
            manageWizard.AttackEnnemiTargeted();
            
        }
        battleClock += Time.deltaTime;
        
        //throw new System.NotImplementedException();
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() <= LIFE_POINT_TO_FLEE)
        {
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Flee);
        }
        if (manageWizard.killCount >= 3) {
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Fearless);
        }
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
        ManageStateChange();
        manageWizard.RegenLifePoint(REGEN_NORMALY);
    }
}