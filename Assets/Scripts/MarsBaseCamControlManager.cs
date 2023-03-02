using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarsBaseCamControlManager : MonoBehaviour
{
    [SerializeField] List<GameObject> baseCameras;

    [SerializeField] TextMeshProUGUI affichage;

    private float chrono;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            baseCameras[0].SetActive(false);
            baseCameras[1].SetActive(true);
            baseCameras[2].SetActive(false);
            baseCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            baseCameras[0].SetActive(false);
            baseCameras[1].SetActive(false);
            baseCameras[2].SetActive(true);
            baseCameras[3].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            baseCameras[0].SetActive(true);
            baseCameras[1].SetActive(false);
            baseCameras[2].SetActive(false);
            baseCameras[3].SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            baseCameras[0].SetActive(false);
            baseCameras[1].SetActive(false);
            baseCameras[2].SetActive(false);
            baseCameras[3].SetActive(true);
        }

        chrono += Time.deltaTime;

        print(chrono);

        if(chrono > 10f)
        {
            affichage.SetText("Planet : Mars - Location : Twix Base - Time : 25h13 (MLT)");
        }

        
            
       

    }
}
