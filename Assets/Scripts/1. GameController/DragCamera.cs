using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DragCamera : MonoBehaviour
{
    // Start is called before the first frame updateprivate Vector3 ResetCamera;
    public Camera linkedCamera;
    private BoxCollider2D boxCollider;
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    private void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }

    private void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButton(0))
            {
                Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (Drag == false)
                {
                    Drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                Drag = false;
            }
            if (Drag == true)
            {
                Camera.main.transform.position = Origin - Diference;
            }
            //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = ResetCamera;
            }

            float vertExtent = linkedCamera.orthographicSize;
            float horizExtent = vertExtent * Screen.width / Screen.height;

            Vector3 linkedCameraPos = linkedCamera.transform.position;
            Bounds areaBounds = boxCollider.bounds;

            linkedCamera.transform.position = new Vector3(
                Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                linkedCameraPos.z);
        }
    }

}


