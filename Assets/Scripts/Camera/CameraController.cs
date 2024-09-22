using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _height;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private float _areaRadius;
    private Vector3 _currentDirection;
    void Start()
    {
        transform.position = new Vector3(_playerTransform.transform.position.x,_playerTransform.transform.position.y + _height,_playerTransform.transform.position.z - _areaRadius);
       // transform.rotation = Quaternion.LookRotation(_playerTransform.transform.position - transform.position); 
    }

    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        _currentDirection = new Vector3(_playerTransform.transform.position.x, _playerTransform.transform.position.y + _height, _playerTransform.transform.position.z - _areaRadius);
        transform.position = Vector3.Lerp(transform.position, _currentDirection, _areaRadius * Time.deltaTime);
    }
}
