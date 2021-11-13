using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateHide : WizardState
{
    private bool inCombat = false;
    private const float regenNormaly = 1.0f;
    private const float regenTwiceMoreFast = 0.5f;
    public override void Battle()
    {
        //Si ennemi en vue, va l'attaquer.
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() >= 100 || inCombat && manageWizard.GetLifePoint() >=50)
        {
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Normal);
        }
    }

    public override void MoveToward()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Init()
    {
        //Ici ne fait rien, mais pour monter o� il faudrait l'appeller si on en avait besoin
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            Battle();
            manageWizard.RegenLifePoint(regenNormaly);
        }
        else
        {
            manageWizard.RegenLifePoint(regenTwiceMoreFast);
        }
        ManageStateChange();
    }
}
