using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;

    private void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveToward();

    public abstract void Battle();

    public abstract void ManageStateChange();
}
