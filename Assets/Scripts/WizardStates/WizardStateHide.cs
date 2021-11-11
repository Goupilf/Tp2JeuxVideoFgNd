using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateHide : WizardState
{
    private bool inCombat = false;

    public override void Battle()
    {
        //Si ennemi en vue, va l'attaquer.
    }

    public override void ManageStateChange()
    {
        if (inCombat) //&& lifePoint > 50%
        {

        }
        //if(lifePoint >= 100%)
        //ChangeState(Normal)
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
        if (inCombat)
        {
            Battle();
        }
        else
        {
            RegenLifePoint();
        }
        ManageStateChange();
    }

    private void RegenLifePoint()
    {
        //x2 plus rapide qu'en normal
    }
}
