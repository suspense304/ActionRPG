﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField] public ChasingMethod currentMethod;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       switch(currentMethod)
        {
            case ChasingMethod.Linear: 
                GetComponent<LinearChasing>().CheckDistance();
                break;
            case ChasingMethod.Patrolling:
                GetComponent<Patrol>().CheckDistance();
                break;
            case ChasingMethod.Shooter:
                GetComponent<RangedAttacker>().CheckDistance();
                break;
        }
    }
}
