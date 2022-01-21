using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy _prefub;
    [SerializeField] private Button _startButton;
    [SerializeField, Range(5, 15)] private int _enemyCount;

    private List<Enemy> _enemies = new List<Enemy>();

    public void Generate()
    {
        Quaternion enemyRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 randomPoint = transform.position - new Vector3(Random.Range(-1.51f, 1.51f), 0, Random.Range(-6.01f, 6.01f));
           var enemy = Instantiate(_prefub, randomPoint, enemyRotation, transform);
            _enemies.Add(enemy);
            enemy.Died += (ctx => _enemies.Remove(ctx));
           // _startButton.onClick.AddListener(enemy.StartWalkForward);
        }
    }

    public void Restart()
    {
        foreach (var enemy in _enemies)
        {
            _startButton.onClick.RemoveListener(enemy.StartWalkForward);
            Destroy(enemy.gameObject);
        }
        _enemies = new List<Enemy>();
        Generate();
    }

    public void StartEnemyWalk()
    {
        foreach (var enemy in _enemies)
        {
            enemy.StartWalkForward();
        }
    }
}
