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
    private GameObject[] list;
    private TowerManager towerManager;
    void Start()
    {
        GameObject test = GameObject.FindGameObjectWithTag("TowerManager");
        towerManager = test.GetComponent<TowerManager>();
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
        GameObject closestTower = towerManager.getClosestActiveTower(wizardTag, transform);
        transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, step);
    }
}
