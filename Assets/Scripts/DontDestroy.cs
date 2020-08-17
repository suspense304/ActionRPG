using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] public GameObject[] objects;


    void Awake()
    {
        DontDestroyOnLoad(this);        
    }
}
