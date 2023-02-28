using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallenCapsulesCounterManager : MonoBehaviour
{
    public int counter;
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] AstraCrewManager crew;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        counterText.SetText("Score : " + counter);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= 10)
        {
            //crew.astraCrewComments();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target")
        {
            counter++;
            counterText.SetText("Score : " + counter);
        }
    }
}
