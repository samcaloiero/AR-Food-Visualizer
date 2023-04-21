using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectsToCycle;
    public GameObject destroyObject;
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    private int currentIndex = 0;
    private int hitCount = 0;
    public TextMeshPro text;
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        currentIndex = 0;
    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        //getting touch position on screen, vec 2
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
 
    void Update()
    {
        //dont move ur food over ui buttons
         if (IsPointerOverUIObject())
            return;
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        // if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        // {
        //     var hitPose = hits[0].pose;
        //     hitCount += 1;
        //    // if (spawnedObject == null)
        //    // {
        //    if (hitCount ==1)
        //    {
        //        spawnedObject = Instantiate(objectsToCycle, hitPose.position, hitPose.rotation);   
        //    }
        //    else if (hitCount == 2)
        //    {
        //        Destroy(spawnedObject);
        //    }
        //    else
        //    {
        //        hitCount = 0;
        //    }
        //   //  }
        //     // else
        //     // {
        //     //     spawnedObject.transform.position = hitPose.position;
        //     // }
        //     
        // }
        // Original Version
        //spawn in food
         if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
         { 
             var hitPose = hits[0].pose;
             hitCount += 1;
              if (spawnedObject == null)
              {
                  Destroy(destroyObject);
                 spawnedObject = Instantiate(objectsToCycle, hitPose.position, hitPose.rotation);   
             
              }
             else
             {
                 spawnedObject.transform.position = hitPose.position;
             }
             
         }
    }
    
    //prevents clicking on buttons
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
 
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
