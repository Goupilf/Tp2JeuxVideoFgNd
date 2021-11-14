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
    public string blueTowerTag = "Blue Side Tower";
    public string greenTowerTag = "Green Side Tower";
    public string blueWizardTag = "Blue Wizard";
    public string greenWizardTag = "Green Wizard";
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        blueTowers = GameObject.FindGameObjectsWithTag(blueTowerTag);
        greenTowers = GameObject.FindGameObjectsWithTag(greenTowerTag);
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

    public GameObject getClosestAlliedActiveTower(string wizardTag, Transform position)
    {
        GameObject[] list ;
        list = blueTowers; //

        if (wizardTag == blueWizardTag)
        {
            list = blueTowers;
        }
        else if (wizardTag == greenWizardTag)
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

        if (wizardTag == blueWizardTag)
        {
            list = greenTowers;
        }
        else if (wizardTag == greenWizardTag)
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
