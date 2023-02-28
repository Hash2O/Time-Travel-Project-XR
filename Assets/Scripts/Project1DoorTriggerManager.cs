using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Project1DoorTriggerManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TextMeshProUGUI introText;
    private bool isInformed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isInformed == false)
        {
            isInformed = true;
            print("Entering door trigger");
            audioSource.Play();
            introText.SetText("Bonjour, Capitaine. Je suis Maman, l'Intelligence Artificielle chargée de vous aider à diriger ce vaisseau. \n Conformément à votre demande, je vous rappelle que vous devez 'inspecter' la soute, afin de montrer à l'équipage comment on range convenablement. \n Vous trouverez tout ce qu'il vous faut pour cela sur la caisse en face de vous. \n Amusez-vous bien, capitaine.");
        }
    }
}
