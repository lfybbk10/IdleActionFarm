using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    private MoneyHandler _moneyHandler;
    
    [SerializeField] private GameObject _stackPosition;
    [SerializeField] private LayerMask _blockLayer;

    [SerializeField] private int _maxBlocks;
    private List<BlockOfWheat> _blocks;

    [SerializeField] private float _offsetBlocks;

    public bool IsSelling;

    [SerializeField] private float _sellBlockTime;

    public Action<String> ChangedStackCount;

    private void Awake()
    {
        _blocks = new List<BlockOfWheat>();
        _moneyHandler = GetComponent<MoneyHandler>();
    }

    private void Start()
    {
        ChangedStackCount?.Invoke(_blocks.Count + "/" + _maxBlocks);
        _moneyHandler.Received?.Invoke(_moneyHandler.GetMoneyCount().ToString());
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
        
        block.transform.SetParent(_stackPosition.transform);
        
        BlockMoveToStackObject(block);
        
        _blocks.Add(block);
        ChangedStackCount?.Invoke(_blocks.Count+"/"+_maxBlocks);
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
            if(!IsSelling)
                break;
            
            var block = _blocks[i];
            
            _blocks.RemoveAt(i);
            ChangedStackCount?.Invoke(_blocks.Count+"/"+_maxBlocks);
            _moneyHandler.ReceiveMoney();
            
            block.transform.parent = null;
            block.transform.DOMove(sellBlockPos, _sellBlockTime).OnComplete((() => Destroy(block.gameObject,0.1f)));
            yield return new WaitForSeconds(_sellBlockTime+0.5f);
        }
    }
    
    

    public int GetStackCount()
    {
        return _blocks.Count;
    }

    public int GetMaxBlocksCount()
    {
        return _maxBlocks;
    }
}
