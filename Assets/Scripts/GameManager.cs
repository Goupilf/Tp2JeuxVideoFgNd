using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text nbGreenWizardText;
    [SerializeField] private Text nbBlueWizardText;
    [SerializeField] private Text greenTower1LiveText;
    [SerializeField] private Text greenTower2LiveText;
    [SerializeField] private Text greenTower3LiveText;
    [SerializeField] private Text blueTower1LiveText;
    [SerializeField] private Text blueTower2LiveText;
    [SerializeField] private Text blueTower3LiveText;
    [SerializeField] private Text greenWinText;
    [SerializeField] private Text blueWinText;

    private GameObject[] blueTowers;
    private GameObject[] greenTowers;

    private WizardManager wizardManager;
    private const int GREEN = 0;
    private const int BLUE = 1;
    private string blueTowerTag = "Blue Side Tower";
    private string greenTowerTag = "Green Side Tower";

    // Start is called before the first frame update
    void Start()
    {
        wizardManager = GameObject.Find("WizardManager").GetComponent<WizardManager>();
        blueTowers = GameObject.FindGameObjectsWithTag(blueTowerTag);
        greenTowers = GameObject.FindGameObjectsWithTag(greenTowerTag);
    }

    // Update is called once per frame
    void Update()
    {
        nbBlueWizardText.text = wizardManager.GetNbActifWizard(BLUE).ToString();
        nbGreenWizardText.text = wizardManager.GetNbActifWizard(GREEN).ToString();
        manageTowerText(greenTowers[0], greenTower1LiveText);
        manageTowerText(greenTowers[1], greenTower2LiveText);
        manageTowerText(greenTowers[2], greenTower3LiveText);
        manageTowerText(blueTowers[0], blueTower1LiveText);
        manageTowerText(blueTowers[1], blueTower2LiveText);
        manageTowerText(blueTowers[2], blueTower3LiveText);
    }

    private void manageTowerText(GameObject tower, Text towerText)
    {
        if (tower.activeInHierarchy)
        {
            towerText.text = tower.GetComponent<ManageTower>().GetLifePoint().ToString();
        } else
        {
            towerText.gameObject.SetActive(false);
        }
    }

    public void setWinnerText(int color)
    {
        if (color == BLUE)
        {
            blueWinText.gameObject.SetActive(true);
        } else
        {
            greenWinText.gameObject.SetActive(true);
        }
    }
}
