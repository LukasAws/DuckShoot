using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DuckMovementBossLevel : MonoBehaviour
{
    public DuckSpawnerBossLevel duckSpawner;

    float t = 0;

    private float x = 2;


    public bool isShot = false;
    public bool isTouchingGround = false;
    public bool hasTouchedGround = false;

    public DuckSpawnerBossLevel.DuckDirection duckDirection;

    RectTransform rectTransform;
    BossLevelGameManager gameManager;
    
    private float verticalSpeed = 15f;
    private Vector2 lastPos;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameManager = FindObjectOfType<BossLevelGameManager>();
        gameManager.duckMovement = this;
    }
    // Update is called once per frame
    void Update()
    {

            InvokeRepeating("MoveDuck", 0, 0);

            if (
                    (rectTransform.anchoredPosition.x < (float)-100 && duckSpawner.spawnerDuckDirection == DuckSpawnerBossLevel.DuckDirection.Left) ||
                    (rectTransform.anchoredPosition.x > (float)2020 && duckSpawner.spawnerDuckDirection == DuckSpawnerBossLevel.DuckDirection.Right) &&
                    (!isTouchingGround)
                )
            {
                Destroy(this.gameObject); // when reached edge - destroy it
            gameManager.spawnTime = Time.time;

            }

            if (isShot && !isTouchingGround && !hasTouchedGround)
            {

                GetComponent<Animator>().enabled = false;
                GetComponent<UnityEngine.UI.Image>().sprite = gameManager.fallingSprite;

                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - verticalSpeed * Time.deltaTime);
                verticalSpeed += verticalSpeed * 3 * Time.deltaTime;

                rectTransform.localRotation = Quaternion.Euler(new Vector3(180, 0, -Mathf.Atan2(rectTransform.anchoredPosition.y - lastPos.y, rectTransform.anchoredPosition.x - lastPos.x) * 180 / Mathf.PI));
            }

            //MISSING REFERNCE TO COMPONENT FIX IT NOW TODO

            if (rectTransform.anchoredPosition.y < 220f)
            {
                isTouchingGround = true;
            }
            lastPos = rectTransform.anchoredPosition;
        }
        

    public void MoveDuck()
    {
        //Debug.Log($"Time: {Time.time - gameManager.spawnTime}");
        if (Time.time - gameManager.spawnTime >= 8f)
            {
                //Debug.LogError("Duck should be moving");

                if (!isTouchingGround && !hasTouchedGround)
                    if (duckSpawner.spawnerDuckDirection == DuckSpawnerBossLevel.DuckDirection.Left)
                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - 2, rectTransform.anchoredPosition.y);
                    else
                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + 2, rectTransform.anchoredPosition.y);
                
            }
    }


}