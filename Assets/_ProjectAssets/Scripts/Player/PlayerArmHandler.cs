using System;
using System.Security.Cryptography;
using Cinemachine;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEditor.Build;
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
    
    public PlayerAnimation animation;
    private Transform grooundArmChecker;
    public Transform armSpawnPoint;
    public Gunn currentArm;
    public GameObject grenade;
    public Transform throwGrenadePoint;

    private bool shoot;
    private bool haveGrenade = false;

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

   
    protected void OnDisable()
    {
        InteractableChecker.onGrenadePickUp -= PickUpGrenade;
        UserInputController._leftClick.started -= StartShooting;
        UserInputController._leftClick.canceled -= StopShooting;
        UserInputController._reload.started -= Reload;
        UserInputController._throwGrenade.started -= ThrowGrenade;
    }


    protected void Start()
    {
        InteractableChecker.onGrenadePickUp += PickUpGrenade;
        UserInputController._leftClick.started += StartShooting;
        UserInputController._leftClick.canceled += StopShooting;
        UserInputController._reload.started += Reload;
        UserInputController._throwGrenade.started += ThrowGrenade;
        animation = GetComponent<PlayerAnimation>();
        UIManager.instance.SetAmoUI(currentArm.magSize,currentArm.totalAmunition);
        currentArm.SetArmHandler(this);
    }

    private void StartShooting(InputAction.CallbackContext obj)
    {
        shoot = true;
    }
    
    private void StopShooting(InputAction.CallbackContext obj)
    {
        shoot = false;
    }

    private void Update()
    {
        if(currentArm!=null&&shoot)
            currentArm.Shoot();
    }



    private void Reload(InputAction.CallbackContext obj)
    {
        if (currentArm != null)
        {
            currentArm.Reload();
        }  
    }

    
    private void ThrowGrenade(InputAction.CallbackContext obj)
    {
        if (haveGrenade)
        {
            haveGrenade = false;
            GameObject newGrenade = Instantiate(grenade,armSpawnPoint.position,Quaternion.identity);
            newGrenade.tag = "Untagged";
            newGrenade.GetComponent<Rigidbody>().AddForce(armSpawnPoint.forward*30,ForceMode.Impulse);
            newGrenade.GetComponent<Grenade>().Throw();
            
        }
    }

    private void PickUpGrenade(GameObject grenade)
    {
        if (!haveGrenade)
        {
            haveGrenade = true;
            Destroy(grenade);
        }
    }
    
    public void Die()
    {
        enabled = false;
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
            animation?.ChangeWeapon(1);
            UIManager.instance.SetImage(1);
        }else if (currentArm.GetType() ==typeof(AKA47))
        {
            animation?.ChangeWeapon(2);
            UIManager.instance.SetImage(2);
        }
        currentArm.SetArmHandler(this);

    }
    


}
