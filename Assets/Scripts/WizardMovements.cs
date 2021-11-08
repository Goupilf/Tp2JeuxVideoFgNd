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
    void Start()
    {
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
        string towerTag = "";
        
        if (wizardTag == blueWizardTag)
        {
            towerTag = blueTowerTag;
        }
        else if (wizardTag == greenWizardTag)
        {
            towerTag = greenTowerTag;
        }

        list = GameObject.FindGameObjectsWithTag(towerTag);
        
        GameObject closestTower = list[1];

        float dist = 9999;
        for (int i = 0; i < list.Length; i++)
        {
           float tempdist = Vector3.Distance(transform.position, list[i].transform.position);
            if (tempdist < dist)
            {
                dist = tempdist;
                closestTower = list[i]; 
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, step);
    }
}
