using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class SquadScript : MonoBehaviour
{
    private static List<GameObject> _pointsGameObjects;
    public static float _distanceToPoint = 0.1f;
    public static List<GameObject> PointsSquad => _pointsGameObjects;
    private static int _countPoints = 0;
    private Vector3 offset;

    [SerializeField] private int _currentAlly;
    [SerializeField] private int _maxPoints = 5;
    [SerializeField] private float _distanceBehindPlayer = 2f;
    [SerializeField] private Transform SquadTransform;

    private Transform _playerTransform;

    public GameObject AllyPoint;
    public GameObject AllyPrefab;
    public float _moveSpeed;

    private DiContainer _diContainer;
    public Transform PlayerTransform { get => _playerTransform;}

    public void Initialization(Transform playerTransform) => 
        _playerTransform = playerTransform;


    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _diContainer.Bind<SquadScript>().FromInstance(this).AsSingle();
    }

    private void Start()
    {
        offset = _playerTransform.TransformDirection(Vector3.back) * -_distanceBehindPlayer;

        _pointsGameObjects = new List<GameObject>();               
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && Time.timeScale != 0)
        {
            AddAlly();
        }       
    }

    public void AddAlly()
    {     
        if(_countPoints <= _maxPoints)
        {
            _countPoints++;
            SpawnAlly(1);
        }
        else
            Debug.Log("MaxAlly");
    }

   /* public void RemoveAlly()
    {    
        if (_pointsGameObjects.Count > 0)
        {
            _pointsGameObjects[_pointsGameObjects.Count - 1].SetActive(false);
                         
            _pointsGameObjects.RemoveAt(_pointsGameObjects.Count - 1);

            
            _currentAlly--;
        }
        else
        {
            Debug.Log("Отряд пуст. Нечего удалять.");
        }
    }*/                                                                 // после прописки смерти союзника дописать


    private void SpawnAlly(int currentAlly)
    {
        if (currentAlly > _maxPoints) return;

        for (int i = 0; i < currentAlly; i++)
        {
            SpawnPoint();
            SetPointsPosition();
            SpawnAllyPrefab(_pointsGameObjects[i].transform);
        }
    }
    private void SpawnPoint()
    {
        GameObject point = Instantiate(AllyPoint, SquadTransform.position, SquadTransform.rotation);
        point.transform.parent = SquadTransform;
        _pointsGameObjects.Add(point);
    }

    private void SetPointsPosition()
    {
        int count = 1;
        foreach (GameObject point in _pointsGameObjects)
        {
            point.transform.position = SquadTransform.position;
            point.transform.rotation = SquadTransform.rotation;
            point.transform.rotation = Quaternion.Euler(0, point.transform.rotation.y + (GetAngle() * count++), 0);          
            point.transform.position = point.transform.position + point.transform.forward * _distanceBehindPlayer;          
        }     
    }
    private float GetAngle() =>
        360f / (1 + _countPoints);

    private void SpawnAllyPrefab(Transform point)
    {
        int count = 1;
        //var ally = Instantiate(AllyPrefab, point.position, Quaternion.Euler(0, point.transform.rotation.y + (GetAngle() * count++), 0) );        
        //ally.AddComponent<Allys>();

        var rot = Quaternion.Euler(0, point.transform.rotation.y + (GetAngle() * count++) , 0 );

        Allys ally =
               _diContainer.InstantiatePrefabForComponent<Allys>(AllyPrefab, point.position , rot , null);

        //ally.AddComponent<Allys>();
    }
}