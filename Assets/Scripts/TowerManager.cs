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
        blueTowers = new List<GameObject>();
        blueTowers.Add(blueTower1);
        blueTowers.Add(blueTower2);
        blueTowers.Add(blueTower3);
        greenTowers = new List<GameObject>();
        greenTowers.Add(greenTower1);
        greenTowers.Add(greenTower2);
        greenTowers.Add(greenTower3);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public Vector2 getRandomActifTowerPosition(int color)
    {
        GameObject[] towerList = new GameObject[NB_TOWER_EACH_SIDE];
        if (color == BLUE)
        {
            for(int i=0;i< NB_TOWER_EACH_SIDE; i++)
            {
                towerList[i] = blueTowers[i];
            }
        }
        else if(color == GREEN)
        {
            for (int i = 0; i < NB_TOWER_EACH_SIDE; i++)
            {
                towerList[i] = greenTowers[i];
            }
        }

        Debug.Log(towerList.Length);

        List<int> actifElementsPosition=new List<int>();
        for(int i=0;i<towerList.Length; i++)
        {
            if (towerList[i].activeInHierarchy)
            {
                actifElementsPosition.Add(i);
            }
        }
        int randomTowerIndex = actifElementsPosition[Random.Range(0, actifElementsPosition.Count - 1)];
        return towerList[0].transform.position;
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
