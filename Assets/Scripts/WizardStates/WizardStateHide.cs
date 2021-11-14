using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateHide : WizardState
{
    private const float REGEN_NORMALY = 1.0f;
    private const float REGEN_TWICE_MORE_FAST = 0.5f;
    private float battleClock = 0f;
    private float timeBetweenAttacks = 2f;
    public override void Battle()
    {
        if (battleClock >= timeBetweenAttacks)
        {
            battleClock = 0;
            manageWizard.AttackEnnemiTargeted(manageWizard.damage);

        }
        battleClock += Time.deltaTime;
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() >= 100 || inCombat && manageWizard.GetLifePoint() >=50)
        {
            manageWizard.SetIgnoreObjectPosition(new Vector2());
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Normal);
        }
    }

    public override void MoveToward()
    {
        //Pas de déplacement lors de l'état caché
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Init()
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            Battle();
            manageWizard.RegenLifePoint(REGEN_NORMALY);
        }
        else
        {
            manageWizard.RegenLifePoint(REGEN_TWICE_MORE_FAST);
        }
        ManageStateChange();
    }
}
