using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerManager : MonoBehaviour
{
    public UnityEvent triggerEntered;
    public UnityEvent triggerOut;

    private bool deactivate = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && deactivate == false)
        {
            Debug.Log(other.name + " entre dans le trigger de " + gameObject.name);
            //triggerEntered.Invoke(); //On appelle l'évènement créé plus haut. Gestion via Unity
        }
    }

    public void deactivateTrigger()
    {
        deactivate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " quitte le trigger de " + gameObject.name);
        if (other.name == "Player" && deactivate == false)
        {
            //triggerOut.Invoke();
        }
    }
}
