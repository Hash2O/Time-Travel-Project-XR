      using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetRevolutionManager : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    [SerializeField] GameObject target;
    [SerializeField] GameObject plan;
    [SerializeField] GameObject player;

    [SerializeField] Vector3 initialPosition;
    [SerializeField] float revolutionSpeed;
    private float revolutionDistance;
    private float initialRevolutionDistance;
    private float speed;

    [SerializeField] TextMeshProUGUI planetName;

    void Start()
    {
        target = GameObject.Find("Global Zone Center Point");
        plan = GameObject.Find("Global Zone");
        player = GameObject.FindWithTag("Player");

        //Vitesse de déplacement
        speed =1.0f;

        //Distance entre la planete et le centre autour duquel elle tourne
        revolutionDistance = Vector3.Distance(transform.position, target.transform.position);

        //Stockage de la distance, pour la comparer dans l'update
        initialRevolutionDistance = revolutionDistance;

        planetName.SetText(gameObject.name);
        planetName.gameObject.transform.LookAt(player.transform);

        print("Nom : " + gameObject.name + " / Distance de départ : " + initialRevolutionDistance);
    }

    void Update()
    {
        float updateRevolutionDistance = Vector3.Distance(transform.position, target.transform.position);
        //print("Nom : " + gameObject.name + " / Distance actuelle : " + updateRevolutionDistance);

        //Axe de révolution pour les planètes
        Vector3 axis = target.transform.up;

        // Spin the object around the target at revolutionSpeed/second.
        transform.RotateAround(target.transform.position, axis, revolutionSpeed * Time.deltaTime);

        //Si la distance de révolution change, on fait appel à la méthode pour remettre la planète à la bonne distance
        if ((updateRevolutionDistance - initialRevolutionDistance < - 0.1f) || (updateRevolutionDistance - initialRevolutionDistance > 0.1f))
        {
            print("Nom : " + gameObject.name + " returning to initial position");
            returningToPosition();
        }
    }

    public void returningToPosition()
    {
        transform.localPosition = initialPosition;
    }
}
