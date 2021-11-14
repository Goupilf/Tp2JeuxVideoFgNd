using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] private GameObject blueWizard;
    [SerializeField] private GameObject greenWizard;
    [SerializeField] private BoxCollider2D blueZone;
    [SerializeField] private BoxCollider2D greenZone;
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private int nbMaxWizardEachSide = 5;
    [SerializeField] private float timeForSpawn = 5;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private List<GameObject> greenWizs = new List<GameObject>();
    private List<GameObject> blueWizs = new List<GameObject>();
    private float spawnTimer = 0;
    public const string BLUE_WIZARD_TAG = "Blue Wizard";
    public const string GREEN_WIZARD_TAG = "Green Wizard";
    private bool isGameFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        instantiateStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        initializeWizLists();
        spawnTimer += Time.deltaTime;
        if(spawnTimer > timeForSpawn && !isGameFinish)
        {
            spawnTimer = 0;
            manageWizardSpawn(BLUE, blueWizs, blueWizard);
            manageWizardSpawn(GREEN, greenWizs, greenWizard);
        }
    }

    private void manageWizardSpawn(int color, List<GameObject> wizList, GameObject prefab)
    {
        if (wizList.Count < nbMaxWizardEachSide)
        {
            if (!setInactiveWizardFromList(wizList, color))
            {
                instantiateWizard(color, towerManager.getRandomActifTowerPosition(color), prefab);
            }
        }
    }

    private Vector2 getRandomPosition (int color)
    {
        float minWidthValue = 0;
        float maxWidthValue = 0;
        float minHeightValue = 0;
        float maxHeightValue = 0;
        if (color == GREEN)
        {
            minWidthValue = greenZone.bounds.min.x;
            maxWidthValue = greenZone.bounds.max.x;
            minHeightValue = greenZone.bounds.min.y;
            maxHeightValue = greenZone.bounds.max.y;
        }
        else if(color == BLUE)
        {
            minWidthValue = blueZone.bounds.min.x;
            maxWidthValue = blueZone.bounds.max.x;
            minHeightValue = blueZone.bounds.min.y;
            maxHeightValue = blueZone.bounds.max.y;
        }
        return new Vector2(Random.Range(minWidthValue, maxWidthValue), Random.Range(minHeightValue, maxHeightValue));
    }

    private void instantiateStartGame()
    {
        instantiateWizard(BLUE, getRandomPosition(BLUE), blueWizard);
        instantiateWizard(BLUE, getRandomPosition(BLUE), blueWizard);
        instantiateWizard(BLUE, getRandomPosition(BLUE), blueWizard);
        instantiateWizard(GREEN, getRandomPosition(GREEN), greenWizard);
        instantiateWizard(GREEN, getRandomPosition(GREEN), greenWizard);
        instantiateWizard(GREEN, getRandomPosition(GREEN), greenWizard);
    }

    private void instantiateWizard(int color, Vector2 position, GameObject prefab)
    {
        GameObject wiz = Instantiate<GameObject>(prefab);
        wiz.transform.position = position;
        wiz.SetActive(true);
    }

    private bool setInactiveWizardFromList(List<GameObject> wizList, int color)
    {
        for(int i = 0; i < wizList.Count; i++)
        {
            if (!wizList[i].activeInHierarchy)
            {
                wizList[i].SetActive(true);
                wizList[i].transform.position = towerManager.getRandomActifTowerPosition(color);
                return true;
            }
        }
        return false;
    }

    private void initializeWizLists()
    {
      GameObject[] greenWizArray = GameObject.FindGameObjectsWithTag(GREEN_WIZARD_TAG);
      GameObject[] blueWizArray = GameObject.FindGameObjectsWithTag(BLUE_WIZARD_TAG);
      greenWizs.Clear();
      blueWizs.Clear();
      for (int i = 0; i < greenWizArray.Length; i++)
      {
          greenWizs.Add(greenWizArray[i]);
      }
      for (int i = 0; i < blueWizArray.Length; i++)
      {
          blueWizs.Add(blueWizArray[i]);
      }
    }

    public int GetNbActifWizard(int color)
    {
        List<GameObject> wizList;
        if(color == BLUE)
        {
            wizList = blueWizs;
        } else
        {
            wizList = greenWizs;
        }
        int nbActif = 0;
        for(int i = 0; i < wizList.Count; i++)
        {
            if (wizList[i].activeInHierarchy)
            {
                nbActif++;
            }
        }
        return nbActif;

    }

    public void SetEveryWizardsToDisableState()
    {
        for(int i = 0; i < blueWizs.Count; i++)
        {
            blueWizs[i].GetComponent<ManageWizard>().ChangeWizardState(ManageWizard.WizardStateToSwitch.Disable);
        }
        for (int i = 0; i < greenWizs.Count; i++)
        {
            greenWizs[i].GetComponent<ManageWizard>().ChangeWizardState(ManageWizard.WizardStateToSwitch.Disable);
        }
        isGameFinish = true;
    }
}
