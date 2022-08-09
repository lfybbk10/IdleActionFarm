using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _layerWheat;
    private Animator _animator;

    [SerializeField] private float _distToHit;
    [SerializeField] private float _xHitOffset;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycastHit, 1000,_layerWheat))
            {
                if (raycastHit.collider.gameObject.GetComponent<Wheat>() && (raycastHit.point-transform.position).magnitude <= _distToHit)
                {
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(transform.DOLookAt(raycastHit.transform.position, 0, AxisConstraint.Y)
                            .OnComplete(() => _animator.SetTrigger("AttackTrigger")))
                        .Append(transform.DOLookAt(raycastHit.transform.position, 0, AxisConstraint.Y));
                    sequence.Kill(true);
                }
            }
            
        }
    }
}
