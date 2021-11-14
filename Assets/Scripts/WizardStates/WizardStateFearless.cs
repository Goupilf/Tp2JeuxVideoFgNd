using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFearless : WizardState
{
    private const float REGEN_NORMALY = 1.0f;
    private const int LIFE_POINT_TO_FLEE = 25;
    private float battleClock = 0f;
    private float timeBetweenAttacks = 2f;
    private float moveSpeedFearless = 2f;
    public override void Battle()
    {
        if (battleClock >= timeBetweenAttacks)
        {
            
            battleClock = 0;
            manageWizard.AttackEnnemiTargeted();

        }
        battleClock += Time.deltaTime;
    }

    public override void Init()
    {
    }

    public override void ManageStateChange()
    {
        if (manageWizard.GetLifePoint() <= LIFE_POINT_TO_FLEE)
        {
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Flee);
        }
    }

    public override void MoveToward()
    {
        GameObject closestTower = towerManager.getClosestEnemyActiveTower(gameObject.tag, transform);
        if (inCombat == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, moveSpeedFearless * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
