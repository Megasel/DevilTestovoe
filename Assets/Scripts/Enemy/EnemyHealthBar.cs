using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = FindAnyObjectByType<Camera>();
    }

}
