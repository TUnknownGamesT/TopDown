using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LVL3BossRoom : MonoBehaviour
{
    public CustomBoxCollider customBoxCollider;
    public GameObject skullCamera;
    public GameObject boss;
    public PlayerMovement inputManager;

    public GameObject skullDoor;
    public GameObject final;

    private bool bossDefeated;

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
            if (!bossDefeated)
            {
                bossDefeated = true;
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
            else
            {
                UniTask.Void(async () =>
                {
                    final.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(6));
                    ScenesManager.instance.LoadScene(0);
                });
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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