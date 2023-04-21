using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CycleObjectsButton : MonoBehaviour
{
    public Transform createdThing;
    private Transform storeTransform;
    private Button _button;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        _button = GetComponent<Button>();

       
    }

 
    public void OnButtonClick()
    {
        Debug.Log("Right Clicked");

       
        if (currentIndex == createdThing.childCount)
        {
            currentIndex = 0;
        }
        
       SetChildOfIndexActive(currentIndex);

       currentIndex++;
       
  
    }

   

    public void SetChildOfIndexActive(int index)
    {
        Debug.Log("Your Index" + index);
        createdThing.GetChild(index).gameObject.SetActive(true);
    //childCount getting tottal number of children on instantiator
        for (int i = 0; i < createdThing.childCount; i++)
        {

            //hunter: we can write the below in a single line. neat!
            // createdThing.GetChild(i).gameObject.SetActive(i==index);
            
            if(i == index)
            {
                createdThing.GetChild(i).gameObject.SetActive(true);
                
            }
            else
            {
                createdThing.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }
    
}
