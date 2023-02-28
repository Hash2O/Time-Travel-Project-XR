using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ThrownWeaponManager : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] GameObject thrownGameObject;
    [SerializeField] TextMeshProUGUI affichage;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        affichage.SetText("Infos Debug");
    }

    private void Update()
    {
        RaycastHit hit;
 
        if (Physics.Raycast(tip.position, tip.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(tip.position, tip.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            print("Distance : " + hit.distance + " Hit object : " + hit.collider.name);
            affichage.SetText("Distance : " + hit.distance + " Hit object : " + hit.collider.name);
            if(hit.distance < 0.1f)
            {
                Rigidbody objectRb = thrownGameObject.GetComponent<Rigidbody>();
                MeshRenderer objectMr = thrownGameObject.GetComponent<MeshRenderer>();
                objectMr.material.color = Color.red;
                objectRb.isKinematic = true;
                affichage.SetText("Is Kinematic : " + objectRb.isKinematic + " Target hit !");
            }
        }
        else
        {
            Debug.DrawRay(tip.position, tip.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    public void OnAimedShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(tip.position, tip.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.distance > 0.1f)
            {
                thrownGameObject.transform.LookAt(hit.point);
            }
        }
    }

    public void OnSelectObject()
    {
        Rigidbody objectRb = thrownGameObject.GetComponent<Rigidbody>();
        if (objectRb.isKinematic != false)
        {
            MeshRenderer objectMr = thrownGameObject.GetComponent<MeshRenderer>();
            objectMr.material.color = Color.blue;
            objectRb.isKinematic = false;
            affichage.SetText("Is Kinematic : " + objectRb.isKinematic);
        }
    }

}
