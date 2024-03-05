using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMoves : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite[] sprites;
    // 0 - left
    // 1 - center
    // 2 - right

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x < Screen.width / 3)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (Input.mousePosition.x > Screen.width / 3 && Input.mousePosition.x < 2 * Screen.width / 3)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            spriteRenderer.sprite = sprites[2];
        }
    }
}
