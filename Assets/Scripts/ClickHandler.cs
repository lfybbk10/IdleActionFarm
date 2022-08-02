using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerWheat;
    private Camera _camera;
    private Animator _animator;

    private float _distToHit = 0.6f;
    private float _xHitOffset = 0.2f;
    
    private void Awake()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition = Input.mousePosition;

            
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycastHit, 1000,_layerWheat))
            {
                if (raycastHit.collider.gameObject.GetComponent<Wheat>() && (raycastHit.point-transform.position).magnitude <= _distToHit)
                {
                    transform.DOLookAt(raycastHit.transform.position, 0,AxisConstraint.Y).OnComplete(()=>_animator.SetTrigger("AttackTrigger"));
                }
            }
            
        }
    }
}
