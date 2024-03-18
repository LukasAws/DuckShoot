using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public DuckDirection spawnerDuckDirection;

    public GameObject[] antys;
    private Transform spawnerTransform;

    float time;

    public bool duckiesSpawnable;

    GameObject lastDuckSpawned;

 
    void Start()
    {
        spawnerTransform = transform;


        if (spawnerTransform == null)
        {
            Debug.LogError("Prefab or spawner transforms not assigned properly.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((lastDuckSpawned == null || Vector3.Distance(lastDuckSpawned.transform.position, spawnerTransform.position) > 4f) && Equals(lastDuckSpawned))
        {
            int random0Antis = UnityEngine.Random.Range(0, 3);
            GameObject selectedAntis;
            switch (random0Antis)
            {
                case 0:
                    selectedAntis = antys[0];
                    break;
                case 1:
                    selectedAntis = antys[1];
                    break;
                case 2:
                    selectedAntis = antys[2];
                    break;
                default:
                    selectedAntis = null;
                    break;
            }

            GameObject newAntis = Instantiate<GameObject>(selectedAntis, spawnerTransform.position, spawnerTransform.rotation);
            newAntis.GetComponent<DuckMovement>().duckSpawner = this;
            newAntis.GetComponent<DuckMovement>().duckDirection = spawnerDuckDirection;

            lastDuckSpawned = newAntis;

            SpawnDuckies(newAntis, selectedAntis);

            time = 0;
        }

    }
    public enum DuckDirection
    {
        Right,
        Left,
    }

    private void SpawnDuckies(GameObject newAntis, GameObject selectedAntis)
    {
        int doesDuckieSpawn = Random.Range(0, 100);
        if (doesDuckieSpawn <= 10 && duckiesSpawnable)
        {
            int numberOfDuckies = Random.Range(1, 6);
            float offsetSize = 0.25f;

            for (int i = 0; i < numberOfDuckies; i++)
            {
                Vector3 duckieOffset = spawnerTransform.position + new Vector3(Random.Range(-offsetSize, offsetSize), -0.1f, Random.Range(0.1f, offsetSize));

                GameObject littleDuckie = Instantiate<GameObject>(selectedAntis, duckieOffset, spawnerTransform.rotation);
                littleDuckie.transform.localScale = newAntis.transform.localScale * 0.5f;
                littleDuckie.transform.SetParent(newAntis.transform);
                littleDuckie.name += "_Child";
                littleDuckie.GetComponent<DuckMovement>().duckSpawner = newAntis.GetComponent<DuckMovement>().duckSpawner;
                littleDuckie.GetComponent<DuckMovement>().isChild = true;
            }
        }
    }

    public bool Equals(GameObject newAntis){
        if (!newAntis)
            return true;
        return this.spawnerDuckDirection == newAntis.GetComponent<DuckMovement>().duckDirection;
    }
}
