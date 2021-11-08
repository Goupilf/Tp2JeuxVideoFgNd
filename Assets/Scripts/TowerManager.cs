using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject blueTower1;
    [SerializeField] private GameObject blueTower2;
    [SerializeField] private GameObject blueTower3;
    [SerializeField] private GameObject greenTower1;
    [SerializeField] private GameObject greenTower2;
    [SerializeField] private GameObject greenTower3;
    private const int NB_TOWER_EACH_SIDE = 3;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private GameObject[] blueTowers;
    private GameObject[] greenTowers;
    [SerializeField] private GameObject tower;
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NB_TOWER_EACH_SIDE; i++)
        {
            //blueTowers.Add(blueTowerArray[i]);
            //greenTowers.Add(greenTowerArray[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getRandomActifTower(int color)
    {
        List<GameObject> towerList = new List<GameObject>();
        if (color == BLUE)
        {
            towerList = blueTowers;
        }
        else if(color == GREEN)
        {
            towerList = greenTowers;
        }
        List<int> actifElementsPosition=new List<int>();
        for(int i=0;i<towerList.Count; i++)
        {
            if (towerList[i].activeInHierarchy)
            {
                actifElementsPosition.Add(i);
            }
        }
        return towerList[Random.Range(0, actifElementsPosition.Count)].transform.position;
    }

    public GameObject getClosestActiveTower(string wizardTag)
    {
        string towerTag = "";
        GameObject[] list;

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

        //list = GameObject.FindGameObjectsWithTag(towerTag);

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
        return closestTower;
    }
    
}
