using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider _touchSlider;

    private Cube _mainCube;

    private bool _isPointerDown;
    private bool canMove;
    private Vector3 _cubePos;

    private void Start()
    {
        SpawnCube();
        canMove = true;
        _touchSlider.onPointerDownEvent += OnPointerDown;
        _touchSlider.onPointerDragEvent += OnPointerDrag;
        _touchSlider.onPointerUpEvent += OnPointerUp;
    }
    private void Update()
    {
        if (_isPointerDown)
        {
            _mainCube.transform.position = Vector3.Lerp(_mainCube.transform.position, _cubePos, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        _isPointerDown = true;
    }
    private void OnPointerDrag(float value)
    {
        if (_isPointerDown)
        {
            _cubePos = _mainCube.transform.position;
            _cubePos.x = value * _cubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if (_isPointerDown && canMove)
        {
            _isPointerDown = false;
            canMove = false;

            _mainCube.cubeRigidbody.AddForce(Vector3.forward * _pushForce, ForceMode.Impulse);

            Invoke("SpawnNewCube", 0.3f);
        }
    }
    private void SpawnNewCube()
    {
        _mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        _mainCube = CubeSpawner.instance.SpawnRandom();
        _mainCube.isMainCube = true;
        _cubePos = _mainCube.transform.position;
    }
    private void OnDestroy()
    {
        _touchSlider.onPointerDownEvent -= OnPointerDown;
        _touchSlider.onPointerDragEvent -= OnPointerDrag;
        _touchSlider.onPointerUpEvent -= OnPointerUp;
    }
}
