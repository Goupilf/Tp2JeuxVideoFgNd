using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected WizardManager wizardManager;

    private void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToward()
    {

    }

    void Battle()
    {

    }

    void ManageStateChange()
    {

    }
}
