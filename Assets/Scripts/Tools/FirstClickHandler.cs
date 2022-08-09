using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    public bool isCursorDeleted;

    public void DeleteCursor()
    {
        Destroy(_cursor);
        isCursorDeleted = true;
    }
}
