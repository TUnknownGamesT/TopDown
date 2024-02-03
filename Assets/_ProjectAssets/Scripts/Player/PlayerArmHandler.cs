using System;
using Cysharp.Threading.Tasks;
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

    public PlayerAnimation animation;
    private Transform grooundArmChecker;
    public Transform armSpawnPoint;
    public Gunn currentArm;
    public GameObject leftHandEnd;


    public GameObject _grenade;
    private bool _shoot;
    private bool _haveGrenade = false;

    [ContextMenu("Init PlayerArmHandler")]
    private void Init()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.name == "GroundArmChecker")
                grooundArmChecker = child;

            if (child.transform.name == "ArmSpawnPoint")
                armSpawnPoint = child;
        }


        if (grooundArmChecker == null)
        {
            grooundArmChecker = new GameObject("GroundArmChecker").transform;
            grooundArmChecker.transform.parent = transform;
            grooundArmChecker.transform.localPosition = Vector3.zero + new Vector3(0, -0.982f, 0);

            SphereCollider sphereCollider = grooundArmChecker.AddComponent<SphereCollider>();
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
        UIManager.instance.SetAmoUI(currentArm.magSize, currentArm.totalAmunition);
        currentArm.SetArmHandler(this);
    }

    private void StartShooting(InputAction.CallbackContext obj)
    {
        _shoot = true;
    }

    private void StopShooting(InputAction.CallbackContext obj)
    {
        _shoot = false;
    }

    private void Update()
    {
        if (currentArm != null && _shoot)
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
        if (_haveGrenade)
        {
            _haveGrenade = false;
            UniTask.Void(async () =>
            {
                UIManager.instance.NoGrenade();
                _grenade.SetActive(true);
                
                animation.ThrowGrenade();
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                GameObject newGrenade = Instantiate(_grenade, leftHandEnd.transform.position, Quaternion.identity);
                newGrenade.transform.parent = null;
                newGrenade.tag = "Untagged";
                newGrenade.GetComponent<Rigidbody>().AddForce(armSpawnPoint.forward * 90, ForceMode.Impulse);
                newGrenade.GetComponent<Grenade>().Throw();
            });
            
        }
    }

    private void PickUpGrenade(GameObject grenade)
    {
        if (!_haveGrenade)
        {
            UIManager.instance.HasGrenade();
            _haveGrenade = true;
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
        currentArm.GetComponent<BoxCollider>().enabled = false;
        currentArm.transform.parent = armSpawnPoint;
        currentArm.transform.localPosition = Vector3.zero;
        currentArm.transform.localRotation = Quaternion.identity;
        animation?.ChangeWeapon((int)currentArm.enemyDrop);
        UIManager.instance.SetImage((int)currentArm.enemyDrop);
        currentArm.SetArmHandler(this);
    }
}