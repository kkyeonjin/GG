using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagObjects : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 mousePos;
    public Camera canvasCam;
    public Ray R; // Get the ray from mouse position
    public RaycastHit hit;
    public int objValue, objWeight;
    public bool onBag;
    public bool firstIn, firstOut;

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        R = canvasCam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = canvasCam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.

        transform.position = P;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Bag");
        if (Physics.Raycast(R, out hit, Mathf.Infinity, layerMask))
        {
            onBag = true;
            if(onBag)
            {
                if(!firstIn)
                {
                    BagManage.instance.totalValue += objValue;
                    BagManage.instance.totalWeight += objWeight;
                    firstIn = true;
                    firstOut = false;
                }
            }
            Debug.Log(BagManage.instance.totalValue);
        }
        else
        {
            if(onBag)
            {
                if (!firstOut)
                {
                    firstOut = true;
                    BagManage.instance.totalValue -= objValue;
                    BagManage.instance.totalWeight -= objWeight;
                }
            }
            onBag = false;
            firstIn = false;
        }
    }
}
