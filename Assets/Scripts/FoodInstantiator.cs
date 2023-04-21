using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstantiator : MonoBehaviour
{
    private int currentIndex;
    
    void Awake()
    {
        //find the object cycle buttons
        //set their creating thing to me
        var buttons = GameObject.FindObjectsOfType<CycleObjectsButton>();
        foreach (var button in buttons)
        {
            //set that buttons instantiator to me (my transform), the just created instantiator
            button.createdThing = transform;
        }
    }
    
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var pos = Input.GetTouch(0).position;
            if (pos.x > Screen.width / 2)
            {
                CycleActiveChild(1);
            }
            else
            {
                CycleActiveChild(-1);

            }

            //on click anywhere on screen
        }
    }
    
    public void CycleActiveChild(int dir)
    {

        if (currentIndex == transform.childCount)
        {
            currentIndex = 0;
        }

        if (currentIndex < 0)
        {
            currentIndex = transform.childCount - 1;
        }
        SetChildOfIndexActive(currentIndex);
        currentIndex+= dir;
    }

    public void SetChildOfIndexActive(int index)
    {
        Debug.Log("Your Index" + index);
        transform.GetChild(index).gameObject.SetActive(true);
        //childCount getting total number of children on instantiator
        for (int i = 0; i < transform.childCount; i++)
        {
            //hunter: we can write the below in a single line. neat!
            // createdThing.GetChild(i).gameObject.SetActive(i==index);
            if(i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
