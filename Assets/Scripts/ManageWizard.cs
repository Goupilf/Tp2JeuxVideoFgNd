using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWizard : MonoBehaviour
{
    private const int STARTING_LIFE = 100;
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";
    [SerializeField] private int lifePoint = STARTING_LIFE;
    private WizardState wizardState;
    public GameObject ignoreObject;
    private GameObject towerHide;
    public string blueTowerTag = "Blue Side Tower";
    public string greenTowerTag = "Green Side Tower";
    public int damage = 1;
    private float regenClock = 0f;
    [SerializeField] private ManageWizard ennemieTargeted;
    [SerializeField] private ManageTower ennemieTargetedTower;

    public enum WizardStateToSwitch { Flee, Hide, Safety, Fearless, Normal , Disable};

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
    public void AttackEnnemiTargeted(int damage)
    {

        if (ennemieTargeted != null)
        {
            ennemieTargeted.ApplyDamage(damage);
        }
        else if (ennemieTargetedTower != null)
        {
            ennemieTargetedTower.ApplyDamage(damage);
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
            case WizardStateToSwitch.Hide:
                {
                    wizardState = gameObject.AddComponent<WizardStateHide>() as WizardStateHide;
                    break;
                }
            case WizardStateToSwitch.Safety:
                {
                    wizardState = gameObject.AddComponent<WizardStateSafety>() as WizardStateSafety;
                    break;
                }
            case WizardStateToSwitch.Disable:
                {
                    wizardState = null;
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
            if(lifePoint > STARTING_LIFE)
            {
                lifePoint = STARTING_LIFE;
            }
        }
        regenClock += Time.deltaTime;
    }

    public GameObject GetTowerHide()
    {
        return towerHide;
    }

    public void SetTowerHide(GameObject tower)
    {
        towerHide = tower;
    }

    public GameObject GetIgnoreObject()
    {
        return ignoreObject;
    }

    public void SetIgnoreObject(GameObject ignoreObject) {
        this.ignoreObject = ignoreObject;
    }


    //wizardState.towerManager.blueTowerTag ne devrait pas exister. Si tu veux avoir accès au tag, place-le en contante dans ce même fichier
    //Cette vérification de en combat ou non devrait être défini dans le state: lorsque je suis en état Sureté, je ne peux pas être en combat
   private void OnTriggerEnter2D(Collider2D collision) 
    {
        //print(wizardState.towerManager.blueTowerTag);
        if (collision.gameObject.tag == blueWizardTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue wiz
        {
            wizardState.inCombat = true;
            if (ennemieTargeted == null)
            {
                ennemieTargeted = collision.gameObject.GetComponent<ManageWizard>();
            }

        }
        
        else if (collision.gameObject.tag == blueTowerTag && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
        {
            wizardState.inCombat = true;
            if (ennemieTargeted == null)
            {
                ennemieTargetedTower = collision.gameObject.GetComponent<ManageTower>();
            }

        }
        else if (collision.gameObject.tag == greenWizardTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green wiz
        {
            wizardState.inCombat = true;
            if (ennemieTargeted == null)
            {
                ennemieTargeted = collision.gameObject.GetComponent<ManageWizard>();
            }
        }
        else if (collision.gameObject.tag == greenTowerTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
        {
            wizardState.inCombat = true;
            if (ennemieTargeted == null)
            {
                ennemieTargetedTower = collision.gameObject.GetComponent<ManageTower>();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ManageWizard>() == ennemieTargeted) {
            ennemieTargeted = null;
            wizardState.inCombat = false;
        }
    }
}
