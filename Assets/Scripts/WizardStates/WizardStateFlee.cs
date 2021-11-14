using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFlee : WizardState
{
    private string allyTowerTag = "";
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";
    private float speed = 0f;
    private const float REGEN_NORMALY = 1.0f;
    private Vector2 closestObjectPosition;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        if (tag == blueWizardTag)
        {
            allyTowerTag = blueTowerTag;
        }
        else if(tag == greenWizardTag)
        {
            allyTowerTag = greenTowerTag;
        }
        closestObjectPosition = FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToward();
        manageWizard.RegenLifePoint(REGEN_NORMALY);
        ManageStateChange();
    }

    public override void MoveToward()
    {
        transform.position = Vector3.MoveTowards(transform.position, closestObjectPosition, speed * Time.deltaTime);
    }

    public override void Battle()
    {
        //Aucun combat en état de fuite
    }

    public override void ManageStateChange()
    {
        if(this.gameObject.transform.position == new Vector3(closestObjectPosition.x,closestObjectPosition.y, 0))
        {
            manageWizard.SetIgnoreObjectPosition(closestObjectPosition);
            manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Hide);
        }
    }

    private Vector2 findTheMostNearbyActifPosition(GameObject[] gameObjectArray)
    {
        Vector2  nearestPosition= new Vector2();
        float dist = 10;
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            float tempdist = Vector3.Distance(this.transform.position, gameObjectArray[i].transform.position);
            
            if (tempdist < dist && gameObjectArray[i].activeInHierarchy && gameObjectArray[i].transform.position!=new Vector3(manageWizard.GetIgnoreObject().x, manageWizard.GetIgnoreObject().y,0))
            {
                dist = tempdist;
                nearestPosition = gameObjectArray[i].transform.position;
            }
        }
        return nearestPosition;
    }

    private Vector2 FindTarget()
    {
        GameObject[] nearbyTrees = GameObject.FindGameObjectsWithTag("Forest");
        Vector2 mostNearbyTree = findTheMostNearbyActifPosition(nearbyTrees);
        
        GameObject[] nearbyAllyTowers = GameObject.FindGameObjectsWithTag(allyTowerTag);
        Vector2 mostNearbyAllyTower = findTheMostNearbyActifPosition(nearbyAllyTowers);

        if(Vector2.Distance(this.transform.position, mostNearbyTree) > Vector2.Distance(this.transform.position, mostNearbyAllyTower))
        {
            return mostNearbyAllyTower;
        }
        else
        {
            return mostNearbyTree;
        }
        
    }

    public override void Init() // Le contenu de init peux être mis dans une fonction start. méthode inutile à mon avis.
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin
    }
}
