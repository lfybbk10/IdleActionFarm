using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private TextView _stackOccupancy, _moneyCount;
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        _player.GetComponent<BlockStack>().ChangedStackCount += _stackOccupancy.UpdateText;
        _player.GetComponent<MoneyHandler>().Received += _moneyCount.UpdateText;
    }
}
