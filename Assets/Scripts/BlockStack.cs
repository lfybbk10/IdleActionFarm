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

    public bool IsSelling;

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

    public IEnumerator SellBlocks(Vector3 sellBlockPos)
    {
        for (int i = _blocks.Count-1; i >= 0; i--)
        {
            var block = _blocks[i];
            if(!IsSelling)
                yield break;
            
            _blocks.RemoveAt(i);
            block.transform.parent = null;
            block.transform.DOMove(sellBlockPos, 1).OnComplete((() => Destroy(block.gameObject,0.1f)));
            yield return new WaitForSeconds(2.5f);
        }
    }
    
    

    public int GetStackCount()
    {
        return _blocks.Count;
    }
}
