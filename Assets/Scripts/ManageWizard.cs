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
    public int damage = 20;
    private const int minDamage = 10;
    private const int maxDamage = 20;
    private const string BLUE_TOWER_TAG = "Blue Side Tower";
    private const string GREEN_TOWER_TAG = "Green Side Tower";
    private float regenClock = 0f;
    [SerializeField] private ManageWizard ennemieTargeted;
    [SerializeField] private ManageTower ennemieTargetedTower;
    public int killCount = 0;
    public bool isFearless = false;

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
    public void AttackEnnemiTargeted()
    {
        randomizeDamage();
        if (ennemieTargeted != null)
        {
            ennemieTargeted.ApplyDamage(damage);
            if (ennemieTargeted.GetLifePoint() == 0)
            {
                killCount++;
            };
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
            case WizardStateToSwitch.Fearless:
                {
                    isFearless = true;
                    wizardState = gameObject.AddComponent<WizardStateFearless>() as WizardStateFearless;
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
    public void randomizeDamage()
    {
        damage = Random.Range(minDamage, maxDamage);
    }


    //wizardState.towerManager.blueTowerTag ne devrait pas exister. Si tu veux avoir accès au tag, place-le en contante dans ce même fichier
    //Cette vérification de en combat ou non devrait être défini dans le state: lorsque je suis en état Sureté, je ne peux pas être en combat
   private void OnTriggerEnter2D(Collider2D collision) 
    {

        if (collision.gameObject.tag == blueWizardTag && gameObject.tag == greenWizardTag )//green wizard entre en collision avec blue wiz
        {
            if (isFearless)
            {
                //Debug.Log(collision.gameObject.GetComponent<ManageWizard>().ennemieTargeted.gameObject);
                //Debug.Log(this.gameObject);
                if (collision.gameObject.GetComponent<ManageWizard>().ennemieTargeted == this) //si il est fearless et cibler par ennemi
                {
                    setInCombatTrue();
                    SetTargetedEnnemy(collision);
                }

                //si pas cibler par ennemi entre pas en cbt avec
            }
            else
            {

                setInCombatTrue();
                SetTargetedEnnemy(collision);
            }
            

        }

        else if (collision.gameObject.tag == BLUE_TOWER_TAG && gameObject.tag == greenWizardTag)//green wizard entre en collision avec blue tower
        {
            setInCombatTrue();
            SetTargetedTower(collision);

        }
        else if (collision.gameObject.tag == greenWizardTag && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green wiz
        {
            if (isFearless)
            {
                if (collision.gameObject.GetComponent<ManageWizard>().ennemieTargeted == this) //si il est fearless et cibler par ennemi
                {
                    setInCombatTrue();
                    SetTargetedEnnemy(collision);
                }

                //si pas cibler par ennemi entre pas en cbt avec
            }
            else
            {

                setInCombatTrue();
                SetTargetedEnnemy(collision);
            }
        }
        else if (collision.gameObject.tag == GREEN_TOWER_TAG && gameObject.tag == blueWizardTag)//blue wizard entre en collision avec green tower
        {
            setInCombatTrue();
            SetTargetedTower(collision);

        }
    }

    private void SetTargetedTower(Collider2D collision)
    {
        if (ennemieTargeted == null)
        {
            ennemieTargetedTower = collision.gameObject.GetComponent<ManageTower>();
        }
    }

    private void SetTargetedEnnemy(Collider2D collision)
    {
        if (ennemieTargeted == null)
        {
            ennemieTargeted = collision.gameObject.GetComponent<ManageWizard>();
        }
    }

    private void setInCombatTrue()
    {
        wizardState.inCombat = true;
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
            if (collision.gameObject.tag == blueWizardTag && gameObject.tag != blueWizardTag && collision.gameObject.GetComponent<WizardStateSafety>() == null || collision.gameObject.tag == greenWizardTag && gameObject.tag != greenWizardTag && collision.gameObject.GetComponent<WizardStateSafety>() == null)
            {
                if (isFearless)
                {
                    if (collision.gameObject.GetComponent<ManageWizard>().ennemieTargeted == this) //si il est fearless et cibler par ennemi
                    {
                        setInCombatTrue();
                        SetTargetedEnnemy(collision);
                    }

                    //si pas cibler par ennemi entre pas en cbt avec
                }
                else
                {

                    setInCombatTrue();
                    SetTargetedEnnemy(collision);
                }
            }
            
        }
        if (ennemieTargetedTower == null)
        {
            if (collision.gameObject.tag == BLUE_TOWER_TAG && gameObject.tag != blueWizardTag || collision.gameObject.tag == GREEN_TOWER_TAG && gameObject.tag != greenWizardTag)
            {
                SetTargetedEnnemy(collision);
                setInCombatTrue();
            }

        }
    }
}
