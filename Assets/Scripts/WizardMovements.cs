using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovements : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private new string tag;
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";
    private float moveSpeed = 1;
    private float step;
    private TowerManager towerManager;
    private bool inCombat;
    private CombatManager combatManager;
    void Start()
    {
        inCombat = false;
        towerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        tag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        step = moveSpeed * Time.deltaTime;
        if (tag == blueWizardTag)
        {
            MoveTowards(greenWizardTag);
        }
        else if (tag == greenWizardTag)
        {
            MoveTowards(blueWizardTag);
        }
    }

    private void MoveTowards(string wizardTag)
    {
        GameObject closestTower = towerManager.getClosestAlliedActiveTower(wizardTag, transform);
        if (inCombat == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, step);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == blueWizardTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue wiz
        {
            inCombat = true;
            combatManager.Fire(gameObject,collision.gameObject);

        }
        else if (collision.gameObject.tag == blueTowerTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
        {
            inCombat = true;

        }
        else if (collision.gameObject.tag == greenWizardTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green wiz
        {
            inCombat = true;
        }
        else if (collision.gameObject.tag == greenTowerTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
        {
            inCombat = true;

        }
    }

    
}
