using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInitiator : MonoBehaviour
{

    #region Singleton

    public static EnemyInitiator instance;

    private void Awake()
    {
        instance = FindObjectOfType<EnemyInitiator>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public List<EnemyType> enemyTypes;
    public List<GameObject> enemyLootArms;
    public GameObject alertPrefab;
    public GameObject knife;

    public EnemyType GetEnemyStats(Constants.EnemyType enemy)
    {
        return enemyTypes.Find(x => x.enemyType == enemy);
    }

    public void InstantiateArm(Constants.EnemyType enemy, Vector3 position)
    {
        GameObject arm = enemyLootArms.FirstOrDefault(x => x.GetComponent<Gunn>().enemyDrop == enemy);
        Instantiate(arm, position, Quaternion.identity);
    }

    public GameObject GetArm(Constants.EnemyType enemy)
    {
        GameObject arm = enemyLootArms.Find(x => x.GetComponent<Gunn>().enemyDrop == enemy);
        
        if (arm == null)
            return enemyLootArms[2];
        
        return arm;
    }
    
    public GameObject GetKnife()
    {
        return knife;
    }
    
    public void InstantiateAlert(Vector3 position)
    {
      CustomBoxCollider customBoxCollider = Instantiate(alertPrefab, position, Quaternion.identity).GetComponent<CustomBoxCollider>();
      customBoxCollider.AlertEnemies();
    }


}
