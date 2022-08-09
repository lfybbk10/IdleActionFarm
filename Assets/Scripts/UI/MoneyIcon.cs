using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyIcon : MonoBehaviour
{
    public void Shake()
    {
        transform.DOShakePosition(1,10);
    }
}
