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
    private List<GameObject> blueTowers;
    private List<GameObject> greenTowers;

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

    public GameObject getClosestActiveTower()
    {
        return null;
    }
}
