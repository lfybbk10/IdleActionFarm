using UnityEngine;
using DG.Tweening;

public class CharacterMoveController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _joystick;
    private CharacterController _characterController;
    private Animator _animator;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_joystick.Direction != Vector2.zero)
        {
            _animator.SetBool("IsRunning",true);
            Vector3 joystickDir = new Vector3(_joystick.Direction.x,0,_joystick.Direction.y);
            _characterController.Move(joystickDir * Time.deltaTime);
            transform.DOLookAt(new Vector3(joystickDir.x + transform.position.x,0,joystickDir.z+transform.position.z), 0.1f);
        }
        else
        {
            _animator.SetBool("IsRunning",false);
        }
    }
}
