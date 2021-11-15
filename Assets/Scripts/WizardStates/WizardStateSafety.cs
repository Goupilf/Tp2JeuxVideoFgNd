using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafety : WizardState
{
    private const float MAX_LIFE_POINT = 100;
    private const float REGEN_THREE_TIME_FASTER = 0.33f;
    public override void Battle()
    {
        //Aucun combat lorsqu'en suret�
    }

    public override void ManageStateChange()
    {
        if(manageWizard.GetLifePoint() >= MAX_LIFE_POINT || !manageWizard.GetTowerHide().activeInHierarchy)
        {
            manageWizard.SetTowerHide(new GameObject());
            manageWizard.SetIgnoreObjectPosition(new Vector2());
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Normal);
            gameObject.GetComponent<ManageWizard>().isSafe = false;
        }
    }

    public override void MoveToward()
    {
        //Aucun d�placement lorsqu'en suret�
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ManageWizard>().isSafe = true;
    }

    // Update is called once per frame
    void Update()
    {
        manageWizard.RegenLifePoint(REGEN_THREE_TIME_FASTER);
        ManageStateChange();
    }
}
