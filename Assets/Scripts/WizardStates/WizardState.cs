using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected ManageWizard manageWizard;
    public TowerManager towerManager;
    public CombatManager combatManager;
    public bool inCombat = false;

    private void Awake()
    {

        towerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        manageWizard = GetComponent<ManageWizard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveToward();

    public abstract void Init();

    public abstract void Battle();

    public abstract void ManageStateChange();
}
