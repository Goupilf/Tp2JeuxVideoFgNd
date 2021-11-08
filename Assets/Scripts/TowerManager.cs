using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private const int NB_TOWER_EACH_SIDE = 3;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private GameObject[] blueTowers;
    private GameObject[] greenTowers;
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";

    // Start is called before the first frame update
    void Start()
    {
        blueTowers = GameObject.FindGameObjectsWithTag(blueTowerTag);
        greenTowers = GameObject.FindGameObjectsWithTag(greenTowerTag);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Vector2 getRandomActifTowerPosition(int color)
    {
        if (color == BLUE)
        {
            int index = Random.Range(0, blueTowers.Length);
            return blueTowers[index].transform.position;
            
        }
        else if(color == GREEN)
        {
            int index = Random.Range(0, greenTowers.Length - 1);
            return greenTowers[index].transform.position;
        }
        return new Vector2();
    }

    public GameObject getClosestActiveTower(string wizardTag, Transform position)
    {
        string towerTag = "";
        GameObject[] list ;
        list = blueTowers; //

        if (wizardTag == blueWizardTag)
        {
            towerTag = blueTowerTag;
            list = blueTowers;
        }
        else if (wizardTag == greenWizardTag)
        {
            towerTag = greenTowerTag;
            list = greenTowers;
        }

        GameObject closestTower = list[1];

        float dist = 9999;
        for (int i = 0; i < list.Length; i++)
        {
            float tempdist = Vector3.Distance(position.position, list[i].transform.position);
            if (tempdist < dist && list[i].activeSelf)
            {
                dist = tempdist;
                closestTower = list[i];
            }

        }
        return closestTower;
    }
    
}
