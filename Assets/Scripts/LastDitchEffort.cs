using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LastDitchEffort : MonoBehaviour
{
    public Transform createdThing;
    private int currentIndex = 0;

    public void SetChildOfIndexActive(int index)
    {
        Debug.Log("Your Index" + index);
        createdThing.GetChild(index).gameObject.SetActive(true);
        //childCount getting tottal number of children on instantiator
    }

    public GameObject objectsToCycle;

    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    private int hitCount = 0;
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        currentIndex = 0;
    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
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
         if (IsPointerOverUIObject())
            return;
        
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            hitCount += 1;
           // if (spawnedObject == null)
           // {
           if (hitCount ==1)
           {
               spawnedObject = Instantiate(objectsToCycle, hitPose.position, hitPose.rotation);   
           }
           else if (hitCount == 2)
           {
               Destroy(spawnedObject);
           }
           else
           {
               hitCount = 0;
           }
          //  }
            // else
            // {
            //     spawnedObject.transform.position = hitPose.position;
            // }
            
        }
        
    }
    
    /// <summary>
    /// Cast a ray to test if Input.mousePosition is over any UI object in EventSystem.current. This is a replacement
    /// for IsPointerOverGameObject() which does not work on Android in 4.6.0f3
    /// </summary>
    private bool IsPointerOverUIObject() {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
 
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
