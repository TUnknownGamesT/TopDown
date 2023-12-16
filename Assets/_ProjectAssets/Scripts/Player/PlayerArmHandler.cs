using System;
using Cinemachine;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerArmHandler : MonoBehaviour
{

    #region Singleton

    public static PlayerArmHandler instance;
    private void Awake()
    {
       // Init();

        instance = FindObjectOfType<PlayerArmHandler>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    private PlayerAnimation _animation;
    private Transform grooundArmChecker;
    public Transform armSpawnPoint;
    public Gunn currentArm;


    [ContextMenu("Init PlayerArmHandler")]
    private void  Init()
    {
        foreach (Transform child in transform)
        {
            if(child.transform.name == "GroundArmChecker")
                grooundArmChecker = child;
            
            if(child.transform.name == "ArmSpawnPoint")
                armSpawnPoint = child;
        }
        

        if (grooundArmChecker == null)
        {
            grooundArmChecker = new GameObject("GroundArmChecker").transform;
            grooundArmChecker.transform.parent = transform;
            grooundArmChecker.transform.localPosition = Vector3.zero + new Vector3(0,-0.982f,0);
            
            SphereCollider sphereCollider =  grooundArmChecker.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = 0.83f;
        }

    }

    public void Die()
    {
        this.enabled = false;
    }
    protected void OnDisable()
    {
        UserInputController._leftClick.performed -= Shoot;
    }


    protected void Start()
    {
        UserInputController._leftClick.performed += Shoot;
        _animation = GetComponent<PlayerAnimation>();
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (currentArm != null)
        {
            currentArm.Shoot();
        }   
    }


    public void ChangeArm(GameObject arm)
    {
        Destroy(currentArm.gameObject);

        currentArm = arm.GetComponent<Gunn>();
        currentArm.GetComponent<Rigidbody>().isKinematic = true;
        currentArm.GetComponent<BoxCollider>().enabled= false;
        currentArm.transform.parent = armSpawnPoint;
        currentArm.transform.localPosition = Vector3.zero;
        currentArm.transform.localRotation = Quaternion.identity;
        if (currentArm.GetType() == typeof(Pistol))
        {
            _animation?.ChangeWeapon(1);
        }else if (currentArm.GetType() ==typeof(AKA47))
        {
            _animation?.ChangeWeapon(2);
        }
        

    }


}
