using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public BasicMovement Movment;
    public AnimationManager Anim;
    public bool Aiming = false;

    private void Awake()
    {
       if(Instance == null)
       {
            Instance = this;
            DontDestroyOnLoad(gameObject);
       }
       else
       {
            Destroy(gameObject);
       }
    }
    
    
    
}
