using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTower : MonoBehaviour
{
    private const int STARTING_LIFE = 100;
    private int lifePoint;

    // Start is called before the first frame update
    void Start()
    {
        lifePoint = STARTING_LIFE;
    }

    // Update is called once per frame
    void Update()
    {
        ManageDeath();
    }

    public int GetLifePoint()
    {
        return lifePoint;
    }

    public void ApplyDamage(int damage)
    {
        lifePoint -= damage;
        if(lifePoint < 0)
        {
            lifePoint = 0;
        }
    }
   
    private void ManageDeath()
    {
        if(lifePoint == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
