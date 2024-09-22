using UnityEditor;
using UnityEngine;

public class EnemyCreateTool : EditorWindow
{
    private GameObject _enemyModel;  
    private float _health = 100f;
    private float _moveSpeed = 5f;
    private float _damage = 10f;
    private string _prefabName = "NewEnemyPrefab";

    private const string healthBarPrefabPath = "Assets/Prefabs/EnemyHealthBar.prefab"; 
    [MenuItem("Create Enemy/Enemy Prefab Creator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyCreateTool>("Enemy Prefab Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("�������� ������� �����", EditorStyles.boldLabel);

        _enemyModel = (GameObject)EditorGUILayout.ObjectField("������ �����", _enemyModel, typeof(GameObject), false);
        _health = EditorGUILayout.FloatField("��������", _health);
        _moveSpeed = EditorGUILayout.FloatField("�������� ������������", _moveSpeed);
        _damage = EditorGUILayout.FloatField("����", _damage);
        _prefabName = EditorGUILayout.TextField("��� �������", _prefabName);

        if (GUILayout.Button("������� ������ �����"))
        {
            CreateEnemyPrefab();
        }
    }

    private void CreateEnemyPrefab()
    {
        if (_enemyModel == null)
        {
            EditorUtility.DisplayDialog("������", "�������� ������ �����", "OK");
            return;
        }
        GameObject enemy = Instantiate(_enemyModel);
        enemy.name = _prefabName;

        Enemy enemyComponent = enemy.AddComponent<Enemy>();
        enemyComponent.health = _health;
        enemyComponent.moveSpeed = _moveSpeed;
        enemyComponent.damage = _damage;

        GameObject healthBarPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(healthBarPrefabPath);
      
            GameObject healthBar = Instantiate(healthBarPrefab);
            healthBar.transform.SetParent(enemy.transform);  
            healthBar.transform.localPosition = new Vector3(0, 2, 0); 
        
      

        string prefabFolderPath = "Assets/Prefabs/Enemies";
        if (!AssetDatabase.IsValidFolder(prefabFolderPath))
        {
            AssetDatabase.CreateFolder("Assets/Prefabs", "Enemies");
        }

        string prefabPath = $"{prefabFolderPath}/{_prefabName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(enemy, prefabPath);

        DestroyImmediate(enemy);

        EditorUtility.DisplayDialog("�������", "������ ������!", "OK");

        AssetDatabase.Refresh();
    }
}
