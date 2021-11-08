using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] GameObject blueWizard;
    [SerializeField] GameObject greenWizard;
    [SerializeField] BoxCollider2D blueZone;
    [SerializeField] BoxCollider2D greenZone;
    [SerializeField] private TowerManager towerManager;
    public const int MAX_NB_WIZARD_EACH_SIDE = 20;
    public const float WIZARD_SPAWN_RATE = 5.0f;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private List<GameObject> greenWizs = new List<GameObject>();
    private List<GameObject> blueWizs = new List<GameObject>();
    private const float TIME_FOR_SPAWN = 10;
    private float spawnTimer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        instantiateStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > TIME_FOR_SPAWN)
        {
            spawnTimer = 0;
            instantiateWizard(BLUE, towerManager.getRandomActifTowerPosition(BLUE), blueWizard);
            instantiateWizard(GREEN, towerManager.getRandomActifTowerPosition(GREEN), greenWizard);
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
}
