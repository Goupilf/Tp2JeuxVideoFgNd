using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private const int GREEN = 0;
    private const int BLUE = 1;
    private GameObject[] blueTowers;
    private GameObject[] greenTowers;
    private const string BLUE_TOWER_TAG = "Blue Side Tower";
    private const string GREEN_TOWER_TAG = "Green Side Tower";
    public const string BLUE_WIZARD_TAG = "Blue Wizard";
    public const string GREEN_WIZARD_TAG = "Green Wizard";
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        blueTowers = GameObject.FindGameObjectsWithTag(BLUE_TOWER_TAG);
        greenTowers = GameObject.FindGameObjectsWithTag(GREEN_TOWER_TAG);
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        manageWin();
    }

    public Vector2 getRandomActifTowerPosition(int color)
    {
        if (color == BLUE)
        {
            int index = Random.Range(0, getActiveTowerArray(blueTowers).Count);
            return getActiveTowerArray(blueTowers)[index].transform.position;
            
        }
        else if(color == GREEN)
        {
            int index = Random.Range(0, getActiveTowerArray(greenTowers).Count);
            return getActiveTowerArray(greenTowers)[index].transform.position;
        }
        return new Vector2();
    }

    private List<GameObject> getActiveTowerArray(GameObject[] towerArray)
    {
        List<GameObject> newList = new List<GameObject>();
        for(int i = 0; i < towerArray.Length; i++)
        {
            if (towerArray[i].activeInHierarchy)
            {
                newList.Add(towerArray[i]);
            }
        }
        return newList;
    }

    public GameObject getClosestAlliedActiveTower(string wizardTag, Transform position)
    {
        GameObject[] list ;
        list = blueTowers; //

        if (wizardTag == BLUE_WIZARD_TAG)
        {
            list = blueTowers;
        }
        else if (wizardTag == GREEN_WIZARD_TAG)
        {
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
    public GameObject getClosestEnemyActiveTower(string wizardTag, Transform position)
    {
        GameObject[] list;
        list = blueTowers; //

        if (wizardTag == BLUE_WIZARD_TAG)
        {
            list = greenTowers;
        }
        else if (wizardTag == GREEN_WIZARD_TAG)
        {
            list = blueTowers;
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

    private void manageWin()
    {
        if (verifyWinner(blueTowers)) //Green win?
        {
            gameManager.GetComponent<GameManager>().setWinnerText(GREEN);
            GameObject.Find("WizardManager").GetComponent<WizardManager>().SetEveryWizardsToDisableState();
        } else if (verifyWinner(greenTowers)) //Blue win?
        {
            gameManager.GetComponent<GameManager>().setWinnerText(BLUE);
            GameObject.Find("WizardManager").GetComponent<WizardManager>().SetEveryWizardsToDisableState();
        }
    }

    private bool verifyWinner(GameObject[] towerArray)
    {
        for (int i = 0; i < towerArray.Length; i++)
        {
            if (towerArray[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
