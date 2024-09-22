using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [Header("Gravity handling")]
    private float _currentAttractionCharacter = 0;
    [SerializeField] private float _gravityForce = 20;

    [Header("Character components")]
    private CharacterController _characterController;
    [Header("Camera")]
    [Header("Animations")]
    [SerializeField] private Animator _animator;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }


    public void MoveCharacter(Vector3 moveDirection)
    {
        
        moveDirection = moveDirection * _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _characterController.Move(moveDirection * Time.deltaTime);
        PlayAnimations(Mathf.Clamp01(moveDirection.magnitude));
    }
    private void PlayAnimations(float moveValue)
    {
        _animator.SetFloat("BlendMoveValue", moveValue, 0.05f, Time.deltaTime);

    }
   
    
  
}
