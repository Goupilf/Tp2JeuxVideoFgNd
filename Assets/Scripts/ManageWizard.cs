using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWizard : MonoBehaviour
{
    private const int STARTING_LIFE = 100;
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";
    private int lifePoint = STARTING_LIFE;
    private WizardState wizardState;
    public GameObject ignoreObject;
    public GameObject towerHide;
    
    private float regenClock = 0f; 

    public enum WizardStateToSwitch { Flee, Hide, Safety, Fearless, Normal };

    // Start is called before the first frame update
    void Start()
    {
        wizardState = GetComponent<WizardState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoint == 0)
        {
            Die();
        }
    }
    

    public int GetLifePoint()
    {
        return this.lifePoint;
    }

    public void ApplyDamage(int damage)
    {
        this.lifePoint -= damage;
        if (lifePoint < 0)
        {
            lifePoint = 0;
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
        this.lifePoint = STARTING_LIFE;
    }

    public void ChangeWizardState(WizardStateToSwitch nextState)
    {
        Destroy(wizardState);

        switch (nextState)
        {
            case WizardStateToSwitch.Flee:
                {
                    wizardState = gameObject.AddComponent<WizardStateFlee>() as WizardStateFlee;
                    break;
                }
        }
    }

    public void RegenLifePoint(float regenRate)
    {
        if (regenClock >= regenRate)
        {
            regenClock = 0;
            lifePoint++;
        }
        regenClock += Time.deltaTime;
    }
    //wizardState.towerManager.blueTowerTag ne devrait pas exister. Si tu veux avoir accès au tag, place-le en contante dans ce même fichier
    //Cette vérification de en combat ou non devrait être défini dans le state: lorsque je suis en état Sureté, je ne peux pas être en combat
   private void OnTriggerEnter2D(Collider2D collision) 
    {
        //print(wizardState.towerManager.blueTowerTag);
        if (collision.gameObject.tag == blueWizardTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue wiz
        {
            wizardState.inCombat = true;
            wizardState.combatManager.Fire(gameObject, collision.gameObject);

        }
        
        else if (collision.gameObject.tag == wizardState.towerManager.blueTowerTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
        {
            wizardState.inCombat = true;

        }
        else if (collision.gameObject.tag == greenWizardTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green wiz
        {
            wizardState.inCombat = true;
        }
        else if (collision.gameObject.tag == wizardState.towerManager.greenTowerTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
        {
            wizardState.inCombat = true;

        }
    }

}
