using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LVL3BossRoom : MonoBehaviour
{

    public CustomBoxCollider customBoxCollider;
    public GameObject skullCamera;
    public GameObject boss;
    public PlayerMovement inputManager;

    public GameObject skullDoor;
    
    private int enemyNumbers;

    private void OnEnable()
    {
        EnemyBrain.onEnemyDie += OpenBossDoor;
    }

    private void OnDisable()
    {
        EnemyBrain.onEnemyDie -= OpenBossDoor;
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyNumbers = customBoxCollider.GetEnemyNumbers();
    }
    

    private void OpenBossDoor()
    {
       
        enemyNumbers--;
        if (enemyNumbers <= 0)
        {
            inputManager.enabled = false;
            skullCamera.SetActive(true);
            UniTask.Void(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));

                LeanTween.move(skullDoor,
                        new Vector3(skullDoor.transform.position.x, skullDoor.transform.position.y + 10,
                            skullDoor.transform.position.z), 2f)
                    .setEase(LeanTweenType.easeOutCubic);

                await UniTask.Delay(TimeSpan.FromSeconds(2));
                inputManager.enabled = true;
                skullCamera.SetActive(false);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inputManager.enabled = false;
            skullCamera.SetActive(true);
            UniTask.Void(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                LeanTween.move(skullDoor,
                        new Vector3(skullDoor.transform.position.x, skullDoor.transform.position.y - 10,
                            skullDoor.transform.position.z), 2f)
                    .setEase(LeanTweenType.easeOutCubic);
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                inputManager.enabled = true;
                skullCamera.SetActive(false);
                boss.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
            });

        }
        
    }
}
