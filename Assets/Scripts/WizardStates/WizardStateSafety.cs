using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSafety : WizardState
{
    public override void Battle()
    {
        
    }

    public override void ManageStateChange()
    {
        //if(lifePoint >= 100% || !tower.isActiveInHyerarchie)
        //changeState(normalState)
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
        RegenarateLifePoint();
        ManageStateChange();
    }

    private void RegenarateLifePoint()
    {
        //x3 more fast than normal
    }
}
