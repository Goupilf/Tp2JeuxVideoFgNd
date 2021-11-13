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
        manageWizard.RegenLifePoint(REGEN_NORMALY);
    }

    public override void MoveToward()
    {
        GameObject closestObject = FindTarget();
        transform.position = Vector3.MoveTowards(transform.position, closestObject.transform.position, speed) * Time.deltaTime;
    }

    public override void Battle()
    {
        //Aucun combat en état de fuite
    }

    public override void ManageStateChange()
    {
        //Changement d'état fait à la collision
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject!= manageWizard.GetIgnoreObject())
        {
            if(collision.gameObject.tag == "Forest")
            {
                manageWizard.SetIgnoreObject(collision.gameObject);
                manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Hide);
            } else if(collision.gameObject.tag == allyTowerTag)
            {
                manageWizard.SetIgnoreObject(collision.gameObject);
                manageWizard.ChangeWizardState(ManageWizard.WizardStateToSwitch.Safety);
            }
        }
        
    }

    private GameObject findTheMostNearbyActif(GameObject[] gameObjectArray)
    {
        float dist = 0;
        GameObject closestObject = new GameObject();
        for (int i = 0; i < gameObjectArray.Length; i++)
        {
            float tempdist = Vector3.Distance(this.transform.position, gameObjectArray[i].transform.position);
            if (tempdist < dist && gameObjectArray[i].activeInHierarchy && gameObjectArray[i]!= manageWizard.ignoreObject)
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

    public override void Init() // Le contenu de init peux être mis dans une fonction start. méthode inutile à mon avis.
    {
        //Ici ne fait rien, mais pour monter où il faudrait l'appeller si on en avait besoin
    }
}
