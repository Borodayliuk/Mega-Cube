using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner instance;

    Queue<Cube> _cubesQueue = new Queue<Cube>();

    [SerializeField] private int _cubesQueueCapacity = 20;
    [SerializeField] private bool _autoQueueGrow = true;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Color[] _cubeColors;

    [HideInInspector] public int maxCubeNumber;

    private int _maxPower = 12;

    private Vector3 _defaultSpawnPosition;

    private void Awake()
    {
        instance = this;

        _defaultSpawnPosition = transform.position;
        maxCubeNumber = (int) Mathf.Pow(2, _maxPower);

        InitializeCubesQueue();
    }
    private void InitializeCubesQueue()
    {
        for (int i = 0; i < _cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(_cubePrefab, _defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        _cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (_cubesQueue.Count == 0)
        {
            if (_autoQueueGrow)
            {
                _cubesQueueCapacity++;
                AddCubeToQueue();
            }
        }
        Cube cube = _cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), _defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube)
    {
        cube.cubeRigidbody.velocity = Vector3.zero;
        cube.cubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        _cubesQueue.Enqueue(cube);
    }
    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }
    private Color GetColor(int number)
    {
        return _cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
