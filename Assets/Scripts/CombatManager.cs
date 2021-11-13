using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour //Toute cette classe devrait se trouver dans le manageWizard selon moi.
{
    [SerializeField] GameObject blueProjectile;
    [SerializeField] GameObject greenProjectile;
    private List<GameObject> blueProjectileList;
    private List<GameObject> greenProjectileList;
    private const int NB_MAX_PROJECTILE = 50;
    private float step;
    private float missileSpeed = 10f;
    private string blueWizardTag = "Blue Wizard";
    private string greenWizardTag = "Green Wizard";

    // Start is called before the first frame update
    void Start()
    {
        blueProjectileList = new List<GameObject>();
        greenProjectileList = new List<GameObject>();
        step = missileSpeed * Time.deltaTime;
        for (int i = 0; i < NB_MAX_PROJECTILE; i++)
        {
            blueProjectileList.Add(instantiateProjectile(blueProjectile));
            greenProjectileList.Add(instantiateProjectile(greenProjectile));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject instantiateProjectile( GameObject prefab)
    {
        GameObject proj = Instantiate<GameObject>(prefab);
        proj.SetActive(false);
        return proj;
    }
    public void Fire(GameObject gameObject, GameObject collision)//ici t'as un paramètre jamais utiliser. À effacé si non nécessaire
    {
        List<GameObject> projList = blueProjectileList; //L'utilisation d'un tableau de projectile inutile. Tu veux seulement faire x nombre de dégâts à l'adversaire.
        if (gameObject.tag == blueWizardTag)
        {
            projList = blueProjectileList;
        }
        else if (gameObject.tag == greenWizardTag)
        {
            projList = greenProjectileList;
        }

        for (int i = 0; i < projList.Count; i++)
        {
            if (!projList[i].activeInHierarchy)
            {
                projList[i].SetActive(true);
                projList[i].transform.position = gameObject.transform.position;
                //projList[i].transform
                break;
            }
        }


    }
        
   
}
