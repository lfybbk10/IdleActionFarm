using System;
using System.Collections;
using UnityEngine;


public class TestAnim : MonoBehaviour
{
    private Animator _animator;

    private IEnumerator Start()
    {
        _animator = GetComponent<Animator>();
        yield return new WaitForSeconds(3);
        _animator.SetBool("IsRunning",true);
        yield return new WaitForSeconds(3);
        _animator.SetBool("IsRunning",false);
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("AttackTrigger");
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger("AttackTrigger");
    }
}
