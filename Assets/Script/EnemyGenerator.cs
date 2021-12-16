using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy _prefub;
    [SerializeField] private Button _startButton;
    [SerializeField, Range(5, 15)] private int _enemyCount;

    public void Generate()
    {
        Quaternion enemyRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 randomPoint = new Vector3(Random.Range(-2f, 1f), 0, Random.Range(35f, 20f));
           var enemy = Instantiate(_prefub, randomPoint, enemyRotation, transform);
            _startButton.onClick.AddListener(enemy.StartWalkForward);
        }
    }
}
