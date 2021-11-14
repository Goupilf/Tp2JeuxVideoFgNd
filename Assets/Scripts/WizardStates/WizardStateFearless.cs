using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStated : WizardState
{
    private const float REGEN_NORMALY = 1.0f;
    private const int LIFE_POINT_TO_FLEE = 25;
    public override void Battle()
    {
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
