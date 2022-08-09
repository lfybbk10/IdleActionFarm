using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private float _moneyForBlock;
    private float _moneyCount;
    

    [SerializeField] private GameObject _coinPrefab;
    private GameObject _coin;

    [SerializeField] private MoneyIcon _moneyIconUI;

    public Action<String> Received;

    private void Start()
    {
        InitCoin();
    }

    private void InitCoin()
    {
        _coin = Instantiate(_coinPrefab, Vector3.zero, Quaternion.identity,_moneyIconUI.transform.parent);
        _coin.transform.position = _moneyIconUI.transform.parent.TransformPoint(Vector3.zero);
        Canvas canvas = _coin.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        _coin.SetActive(false);
    }

    public void ReceiveMoney()
    {
        StartCoroutine(AnimateCounter());
        _coin.GetComponent<Coin>().AnimateCoin(_moneyIconUI);
    }

    private IEnumerator AnimateCounter()
    {
        for (int i = 1; i <= _moneyForBlock; i++)
        {
            _moneyCount += 1;
            Received?.Invoke(_moneyCount.ToString());
            yield return new WaitForSeconds(0.05f);
        }
    }

    public float GetMoneyCount()
    {
        return _moneyCount;
    }

}
