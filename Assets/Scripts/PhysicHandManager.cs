using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicHandManager : MonoBehaviour
{
    [SerializeField] private Transform[] _hands;    //Transform des deux mains dans un tableau

    [SerializeField] private bool _colliderActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColliders()
    {
        if(_colliderActivated)
        {
            foreach(var hand in _hands)
            {
                Collider[] _colliders = hand.GetComponentsInChildren<Collider>();
                foreach(var collider in _colliders)
                {
                    collider.enabled = false;
                }
            }
            _colliderActivated = true;
        }
        else
        {
            foreach (var hand in _hands)
            {
                Collider[] _colliders = hand.GetComponentsInChildren<Collider>();
                foreach (var collider in _colliders)
                {
                    collider.enabled = true;
                }
            }
            _colliderActivated = false;
        }
    }
}
