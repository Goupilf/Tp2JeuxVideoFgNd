using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected ManageWizard manageWizard;
    private void Awake()
    {
        manageWizard = GetComponent<ManageWizard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveToward();

    public abstract void Battle();

    public abstract void ManageStateChange();
}
