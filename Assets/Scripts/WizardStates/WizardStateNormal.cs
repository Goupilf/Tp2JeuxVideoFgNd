using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{

    private float moveSpeed = 1f;

    public override void Battle()
    {
        throw new System.NotImplementedException();
    }

    public override void ManageStateChange()
    {

    }

    public override void MoveToward()
    {
        GameObject closestTower = towerManager.getClosestEnemyActiveTower(gameObject.tag, transform);
        if (inCombat == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestTower.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Init()
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin

        // Update is called once per frame
        
    }
    void Update()
    {
        if (!inCombat)
        {
            MoveToward();
        }
        
    }
}