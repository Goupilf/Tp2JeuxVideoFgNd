using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFlee : WizardState
{
    protected string allyTowerTag = "";
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";
    private float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        if (tag == blueWizardTag)
        {
            allyTowerTag = blueTowerTag;
        }
        else if(tag == greenWizardTag)
        {
            allyTowerTag = greenTowerTag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToward();
    }

    public override void MoveToward()
    {
        GameObject closestObject = FindTarget();
        transform.position = Vector3.MoveTowards(transform.position, closestObject.transform.position, speed) * Time.deltaTime;
    }

    public override void Battle()
    {
        
    }

    public override void ManageStateChange()
    {
        wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Hide);
        wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Safety);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Forest")
        {
            ManageStateChange();
        } else if(collision.gameObject.tag == allyTowerTag)
        {
            ManageStateChange();
        }
    }

    private GameObject findTheMostNearbyActif(GameObject[] gameObjectArray)
    {
        float dist = 0;
        GameObject closestObject = new GameObject();
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            float tempdist = Vector3.Distance(this.transform.position, gameObjectArray[i].transform.position);
            if (tempdist < dist && gameObjectArray[i].activeInHierarchy && gameObjectArray[i]!=ignoreObject)
            {
                dist = tempdist;
                closestObject = gameObjectArray[i];
            }
        }
        return closestObject;
    }

    private GameObject FindTarget()
    {
        GameObject[] nearbyTrees = GameObject.FindGameObjectsWithTag("Forest");
        GameObject mostNearbyTree = findTheMostNearbyActif(nearbyTrees);
        GameObject[] nearbyAllyTowers = GameObject.FindGameObjectsWithTag(allyTowerTag);
        GameObject mostNearbyAllyTower = findTheMostNearbyActif(nearbyAllyTowers);
        GameObject[] nearbyGameObject = new GameObject[2];
        nearbyGameObject[0] = mostNearbyTree;
        nearbyGameObject[1] = mostNearbyAllyTower;
        return findTheMostNearbyActif(nearbyGameObject);
    }
}
