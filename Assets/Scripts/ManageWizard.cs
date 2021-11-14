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
    private Vector2 ignoreObjectPosition;
    private GameObject towerHide;
    private const string BLUE_TOWER_TAG = "Blue Side Tower";
    private const string GREEN_TOWER_TAG = "Green Side Tower";
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

    public Vector2 GetIgnoreObject()
    {
        return ignoreObjectPosition;
    }

    public void SetIgnoreObjectPosition(Vector2 ignoreObject) {
        this.ignoreObjectPosition = ignoreObject;
    }


    //wizardState.towerManager.blueTowerTag ne devrait pas exister. Si tu veux avoir acc�s au tag, place-le en contante dans ce m�me fichier
    //Cette v�rification de en combat ou non devrait �tre d�fini dans le state: lorsque je suis en �tat Suret�, je ne peux pas �tre en combat
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
        
        else if (collision.gameObject.tag == BLUE_TOWER_TAG && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
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
        else if (collision.gameObject.tag == GREEN_TOWER_TAG && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
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
            ennemieTargetedTower = null;
            wizardState.inCombat = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (ennemieTargeted == null )
        {
            if (collision.gameObject.tag == blueWizardTag && gameObject.tag != blueWizardTag || collision.gameObject.tag == greenWizardTag && gameObject.tag != greenWizardTag)
            {
                ennemieTargeted = collision.gameObject.GetComponent<ManageWizard>();
                wizardState.inCombat = true;
            }
            
        }
        if (ennemieTargetedTower == null)
        {
            if (collision.gameObject.tag == BLUE_TOWER_TAG && gameObject.tag != blueWizardTag || collision.gameObject.tag == GREEN_TOWER_TAG && gameObject.tag != greenWizardTag)
            {
                ennemieTargetedTower = collision.gameObject.GetComponent<ManageTower>();
                wizardState.inCombat = true;
            }

        }
    }
}
