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
}
