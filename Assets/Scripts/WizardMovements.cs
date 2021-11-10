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
    public Rigidbody2D projectile;
    void Start()
    {
        inCombat = false;
        towerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
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
        if (inCombat == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, step);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == blueWizardTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue wiz
        {
            Debug.Log("test");
            inCombat = true;
            Fire(gameObject,collision.gameObject);

        }
        else if (collision.gameObject.tag == blueTowerTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
        {
            Debug.Log("test");
            inCombat = true;

        }
        else if (collision.gameObject.tag == greenWizardTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green wiz
        {
            inCombat = true;
        }
        else if (collision.gameObject.tag == greenTowerTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
        {
            Debug.Log("test");
            inCombat = true;

        }
    }

    private void Fire(GameObject gameObject, GameObject collision)
    {
        Rigidbody2D test = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as Rigidbody2D;
        test.position = Vector3.MoveTowards(test.position, collision.transform.position, step);
    }
}
