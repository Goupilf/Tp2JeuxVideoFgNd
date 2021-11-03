using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] GameObject blueWizard;
    [SerializeField] GameObject greenWizard;
    [SerializeField] BoxCollider2D blueZone;
    [SerializeField] BoxCollider2D greenZone;
    public const int MAX_NB_WIZARD_EACH_SIDE = 20;
    public const float WIZARD_SPAWN_RATE = 5.0f;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private List<GameObject> greenWizs = new List<GameObject>();
    private List<GameObject> blueWizs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instantiateStartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        instantiateWizard(BLUE, blueWizard);
        instantiateWizard(BLUE, blueWizard);
        instantiateWizard(BLUE, blueWizard);
        instantiateWizard(GREEN, greenWizard);
        instantiateWizard(GREEN, greenWizard);
        instantiateWizard(GREEN, greenWizard);
    }

    private void instantiateWizard(int color, GameObject prefab)
    {
        GameObject wiz = Instantiate<GameObject>(prefab);
        wiz.transform.position = getRandomPosition(color);
        wiz.SetActive(true);
    }
}
