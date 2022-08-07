using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticId = 0;
    [SerializeField] private TMP_Text[] _numbersText;

    [HideInInspector] public int cubeID;
    [HideInInspector] public Color cubeColor;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public Rigidbody cubeRigidbody;
    [HideInInspector] public bool isMainCube;

    private MeshRenderer _cubeMeshRenderer;

    private void Awake()
    {
        cubeID = staticId++;
        _cubeMeshRenderer = GetComponent<MeshRenderer>();
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        cubeColor = color;
        _cubeMeshRenderer.material.color = cubeColor;
    }

    public void SetNumber(int number)
    {
        cubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            _numbersText[i].text = "" + cubeNumber;
        }
    }
}
