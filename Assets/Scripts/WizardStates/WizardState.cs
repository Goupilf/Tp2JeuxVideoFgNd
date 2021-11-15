using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected ManageWizard manageWizard;
    protected TowerManager towerManager;
    public bool inCombat = false;

    private void Awake()
    {

        towerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
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
