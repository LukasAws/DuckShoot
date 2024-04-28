using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairSekimas : MonoBehaviour
{
    private GameObject crosshair;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        crosshair = this.gameObject;
        cam = Camera.main;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = new Vector3();

        point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        crosshair.transform.position = point;
    }
}
