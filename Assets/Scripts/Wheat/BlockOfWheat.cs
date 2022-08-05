using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockOfWheat : MonoBehaviour
{
    public bool IsStacked { get; set; }
    
    
    [HideInInspector] public Rigidbody rigidbody;
    
    private float _xOffset = 0.6f;
    private float _yOffset = 0.6f;

    [SerializeField] private float _destroyTime;
    private float _destroyTimer;

    private float _wiggleOffset = 0.03f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.AddForce(new Vector3(_xOffset, _yOffset, 0) * 3,ForceMode.Impulse);
    }

    private void Update()
    {
        _destroyTimer += Time.deltaTime;
        if (_destroyTimer > _destroyTime && !IsStacked)
        {
            Destroy(gameObject);
        }
        
    }
}
