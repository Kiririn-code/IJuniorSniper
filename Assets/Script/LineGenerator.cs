using UnityEngine;
using UnityEngine.Events;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private RedLine _prefab;
    [SerializeField] private Transform _ground;

    private RedLine _currentLine;

    public event UnityAction<Enemy> MovingEnemyAdded;

    public void Restart()
    {
        _currentLine.MovingEnemyDetected -= OnMovingEnemyDetected;
        InitRedLine();
    }
    public void InitRedLine()
    {
        _currentLine = Instantiate(_prefab, _ground.position, Quaternion.identity);
        _currentLine.MovingEnemyDetected += OnMovingEnemyDetected;
    }

    private void OnMovingEnemyDetected(Enemy enemy)
    {
        MovingEnemyAdded?.Invoke(enemy);
    }
}
