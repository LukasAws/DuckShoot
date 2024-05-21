using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DuckSpawnerBossLevel : MonoBehaviour
{
    public DuckDirection spawnerDuckDirection;
    public int duckSpawnerID;
    public GameObject antis;

    private BossLevelGameManager gameManager;
    private RectTransform spawnerTransform;


    void Start()
    {
        gameManager = FindObjectOfType<BossLevelGameManager>();
        spawnerTransform = GetComponent<RectTransform>();


        if (spawnerTransform == null)
        {
            Debug.LogError("Prefab or spawner transforms not assigned properly.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (FindAnyObjectByType<DuckMovementBossLevel>() is null && this.duckSpawnerID == gameManager.randomValueForSpawner)
        {
            GameObject newAntis = Instantiate<GameObject>(antis, new Vector2(spawnerDuckDirection == DuckDirection.Right ? -80f : 2000f, 900f + Random.Range(-50f, 50f)), new Quaternion(0, spawnerDuckDirection == DuckDirection.Right ? 0 : 180, 0, 1));
            newAntis.GetComponent<DuckMovementBossLevel>().duckSpawner = this;
            newAntis.GetComponent<DuckMovementBossLevel>().duckDirection = spawnerDuckDirection;
            newAntis.transform.SetParent(spawnerTransform.parent.parent, true);
            newAntis.transform.SetSiblingIndex(1);
        }

    }
    public enum DuckDirection
    {
        Right,
        Left,
    }

    public bool Equals(GameObject newAntis){
        if (!newAntis)
            return true;
        return this.spawnerDuckDirection == newAntis.GetComponent<DuckMovementBossLevel>().duckDirection;
    }
}
