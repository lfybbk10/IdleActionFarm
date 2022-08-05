using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    [SerializeField] private Transform _sellBlockPos;
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);   
        if (other.gameObject.GetComponent<Player>())
        {
            
            BlockStack blockStack = other.GetComponent<BlockStack>();
            if (blockStack.GetStackCount() > 0)
            {
                blockStack.IsSelling = true;
                StartCoroutine(blockStack.SellBlocks(_sellBlockPos.position));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            BlockStack blockStack = other.GetComponent<BlockStack>();
            if (blockStack.GetStackCount() > 0)
            {
                blockStack.IsSelling = false;
            }
        }
    }
}
