using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Objet à instancier
    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<GameObject> _zombiePrefabs;

    //Pour déterminer la position de spawn
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private List<Transform> _spawnPoints;

    //[SerializeField] private Transform _secondarySpawnPoint;

    //Wave of zombies
    [SerializeField] private int _nombreDeZombies;

    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private InputActionReference _actionSpawnZombies; //Pour écouter la nouvelle map GameInputs dans Starter Assets

    //private float spawnRangeX = 50;

    //private float startDelay = 2.0f;
    //private float repeatRate = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateXobjectsRandomly();
        //InvokeRepeating("InstantiateXobjectsAtTwoSpawnPoints", startDelay, repeatRate);
        //SpawnRandomZombie();

        _actionSpawnZombies.action.Enable(); //On active la fonction
        _actionSpawnZombies.action.performed += OnSpawnZombie;

    }

    //Action qui tient compte du contexte de l'input
    private void OnSpawnZombie(InputAction.CallbackContext obj)
    {
        InstantiateXObjectsAtRandomSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateObject()
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab);
    }

    public void InstantiateXobjectsAtSpawnPoint()
    {
        for (int i = 0; i < _nombreDeZombies; i++)
        {
            InstantiateObjectAtSpawnPoint();
        }
    }

    public void InstantiateObjectAtSpawnPoint()
    {
        //Instancier un zombie dans la scène
        Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
    }

    public void InstantiateXObjectsAtRandomSpawnPoint()
    {
        for(int i = 0; i < _nombreDeZombies; i++)
        {
            int aleatoire = Random.Range(0, _spawnPoints.Count); //Nbre max jamais atteint puisque int
            int zombieRandom = Random.Range(0, _zombiePrefabs.Count);
            _prefab = _zombiePrefabs[zombieRandom];
            _spawnPoint = _spawnPoints[aleatoire]; //On désigne le spawn point choisi aléatoirement
            InstantiateObjectAtSpawnPoint(); //Fonction juste au dessus
        }
    }



    public void InstantiateObjectAtPosition(int x, int z)
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateObjectAtPositionWithRandomDirection(int x, int z)
    {
        //Instancier un zombie dans la scène
        GameObject.Instantiate(_prefab, new Vector3(x, 0.04999924f, z), Quaternion.identity);
    }

    public void InstantiateXobjectsRandomly()
    {
        for(int i = 0; i < _nombreDeZombies; i++)
        {
            int a, b;
            a = Random.Range(-50, 51); 
            b = Random.Range(-50, 51);
            InstantiateObjectAtPosition(a, b);
        }
    }

    /*
    void SpawnRandomZombie()
    {
 
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);

        for (int i = 0; i < _nombreDeZombies; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeX, spawnRangeX));
            Instantiate(zombiePrefabs[zombieIndex], spawnPos, zombiePrefabs[zombieIndex].transform.rotation);
        }
    }
    */

    private void OnSpawnZombies()
    {
        InstantiateObjectAtSpawnPoint();
    }

    /*
public void InstantiateXobjectsAtTwoSpawnPoints()
{
    for (int i = 0; i < _nombreDeZombies; i++)
    {
        InstantiateObjectAtTwoSpawnPoints();
    }
}
*/

    /*
public void InstantiateObjectAtTwoSpawnPoints()
{
    //Instancier un zombie dans la scène
    GameObject.Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
    GameObject.Instantiate(_prefab, _secondarySpawnPoint.position, _secondarySpawnPoint.rotation);
}
*/

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    //Sélection aléatoire d'une scene
    /*
    int nextSceneIndex = Random.Range(0, 4);
    SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    */

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        {
            SceneManager.LoadScene(index);
        }
    }

    public void LoadPreviousScene()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

}
