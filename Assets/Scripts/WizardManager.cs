using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    [SerializeField] GameObject blueWizard;
    [SerializeField] GameObject greenWizard;
    public const int MAX_NB_WIZARD_EACH_SIDE = 20;
    public const float WIZARD_SPAWN_RATE = 5.0f;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private const int FIELD_WIDTH = 1280;
    private const int FIELD_HEIGHT = 720;

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
        int minWidthValue = 0;
        int maxWidthValue = 0;
        int minHeightValue = 0;
        int maxHeightValue = 0;
        if (color == GREEN)
        {
            minWidthValue = -(FIELD_WIDTH / 2);
            maxWidthValue = 0;
            minHeightValue = FIELD_HEIGHT / 2;
            maxHeightValue = -(FIELD_HEIGHT / 2);
        }
        else if(color == BLUE)
        {
            minWidthValue = 0;
            maxWidthValue = FIELD_WIDTH / 2;
            minHeightValue = FIELD_HEIGHT / 2;
            maxHeightValue = -(FIELD_HEIGHT / 2);
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
