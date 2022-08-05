using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    [SerializeField] private GameObject _stack;
    [SerializeField] private LayerMask _blockLayer;
    private List<BlockOfWheat> _blocks;
    [SerializeField] private float _maxBlocks;

    [SerializeField] private float _offsetBlocks;

    private void Awake()
    {
        _blocks = new List<BlockOfWheat>();
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f,_blockLayer);
        if (colliders.Length>0 && _blocks.Count<_maxBlocks)
        {
            foreach (var block in colliders)
            {
                BlockOfWheat blockOfWheat = block.GetComponent<BlockOfWheat>();
                if (!blockOfWheat.IsStacked)
                {
                    CollectBlock(blockOfWheat);
                }
            }
        }
    }

    private void CollectBlock(BlockOfWheat block)
    {
        block.IsStacked = true;
        
        block.rigidbody.useGravity = false;
        block.rigidbody.isKinematic = true;
        block.GetComponent<BoxCollider>().enabled = false;
        
        block.transform.SetParent(_stack.transform);
        
        BlockMoveToStackObject(block);
        
        _blocks.Add(block);
    }

    private void BlockMoveToStackObject(BlockOfWheat block)
    {
        float heightBlock = block.GetComponent<BoxCollider>().bounds.size.y;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(
                block.transform.DOLocalMove(new Vector3(0, (heightBlock + _offsetBlocks) * _blocks.Count), 0.5f))
            .Append(block.transform.DOLocalRotate(Vector3.zero, 0.2f)).OnComplete(()=> sequence.Kill());
    }

    public int GetStackCount()
    {
        return _blocks.Count;
    }
}
