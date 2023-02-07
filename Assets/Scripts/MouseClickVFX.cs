using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickVFX : MonoBehaviour
{

    [SerializeField] private GameObject ClickVFX;
    Camera cam;
    Vector3 loc;

    private void Awake()
    {
        cam = Camera.main;
        loc = new Vector3(0, 0, 0);
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            loc.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
            loc.y = cam.ScreenToWorldPoint(Input.mousePosition).y;

            ClickVFX.transform.position = loc;
            StartCoroutine(ToggleClick());
        }
    }
    IEnumerator ToggleClick()
    {
        ClickVFX.SetActive(true);
        yield return new WaitForSeconds(.5f);
        ClickVFX.SetActive(false);
    }
}
