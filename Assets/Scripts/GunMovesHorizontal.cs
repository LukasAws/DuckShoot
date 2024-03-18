using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GunMovesHorizontal : MonoBehaviour
{
    SpriteRenderer gunSprite;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gunSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x > 10 || mousePos.x < Screen.width - 10)
        gunSprite.gameObject.transform.position = new Vector3(mousePos.x+1, gunSprite.transform.position.y, gunSprite.transform.position.z);
    }
}
