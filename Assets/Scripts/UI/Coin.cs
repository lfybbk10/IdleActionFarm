using DG.Tweening;
using UnityEngine;


public class Coin : MonoBehaviour
{
    public void AnimateCoin(MoneyIcon moneyIcon)
    {
        gameObject.SetActive(true);
        transform.DOMove(moneyIcon.transform.position, 1f).OnComplete((() =>
        {
            gameObject.SetActive(false);
            transform.position = moneyIcon.transform.parent.TransformPoint(Vector3.zero);
            
            moneyIcon.Shake();
        }));
    }
}
