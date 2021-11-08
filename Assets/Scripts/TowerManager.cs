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
    private List<GameObject> blueTowers = new List<GameObject>();
    private List<GameObject> greenTowers = new List<GameObject>();
    [SerializeField] private GameObject tower;

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

    public GameObject getClosestActiveTower()
    {
        return null;
    }
}
