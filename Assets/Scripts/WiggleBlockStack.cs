using System;
using DG.Tweening;
using UnityEngine;


public class WiggleBlockStack : MonoBehaviour
{
    private BlockStack _blockStack;
    private Rigidbody _playerRigidbody;

    private float _wiggleOffset = 0.03f;

    private void Awake()
    {
        Player player = FindObjectOfType<Player>();
        _blockStack = player.GetComponent<BlockStack>();
        _playerRigidbody = player.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.DOLocalMove(new Vector3(0, transform.localPosition.y-_wiggleOffset, transform.localPosition.z), 1.5f).OnComplete((() =>
            transform.DOLocalMove(new Vector3(0, transform.localPosition.y+_wiggleOffset, transform.localPosition.z), 1.5f).SetLoops(-1, LoopType.Yoyo)));
    }
}
