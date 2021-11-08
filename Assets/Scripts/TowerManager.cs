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
    [SerializeField] private GameObject tower;
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

    public Vector2 getRandomActifTower(int color)
    {
        GameObject[] towerList = new GameObject[NB_TOWER_EACH_SIDE];
        if (color == BLUE)
        {
            towerList = blueTowers;
        }
        else if(color == GREEN)
        {
            towerList = greenTowers;
        }
        List<int> actifElementsPosition=new List<int>();
        for(int i=0;i<towerList.Length; i++)
        {
            if (towerList[i].activeInHierarchy)
            {
                actifElementsPosition.Add(i);
            }
        }
        int randomTowerIndex = actifElementsPosition[Random.Range(0, actifElementsPosition.Count)];
        return towerList[randomTowerIndex].transform.position;
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
