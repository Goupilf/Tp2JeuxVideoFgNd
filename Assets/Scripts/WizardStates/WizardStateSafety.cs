using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafety : WizardState
{
    private const float MAX_LIFE_POINT = 100;
    private const float REGEN_THREE_TIME_FASTER = 0.33f;
    public override void Battle()
    {
        
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() >= MAX_LIFE_POINT || !manageWizard.towerHide.activeInHierarchy)
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

    // Update is called once per frame
    void Update()
    {
        manageWizard.RegenLifePoint(REGEN_THREE_TIME_FASTER);
        ManageStateChange();
    }
    public override void Init()
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin
    }
}
