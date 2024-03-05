using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public enum DuckDirection
    {
        Right,
        Left,
    }

    public DuckDirection duckDirection;

    public GameObject[] antys;
    private Transform spawnerTransform;


    void Start()
    {
        spawnerTransform = transform;


        if (spawnerTransform == null)
        {
            Debug.LogError("Prefab or spawner transforms not assigned properly.");
            return;
        }

    }

    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if (time > 1.5f)
        {
            int random0Antis = Random.Range(0, 3);
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
            if (duckDirection == DuckDirection.Left)
                newAntis.GetComponent<DuckMovement>().duckSpawner = this;
            else
                newAntis.GetComponent<DuckMovement>().duckSpawner = this;



            time = 0;
        }
        
    }
}
