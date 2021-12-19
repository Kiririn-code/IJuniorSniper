using UnityEngine;
using UnityEngine.Events;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private RedLine _prefab;
    [SerializeField] private Transform _ground;

    private RedLine _currentLine;

    public event UnityAction<Enemy> MovingEnemyAdded;

    private void OnDisable()
    {
        if(_currentLine !=null)
        _currentLine.MovingEnemyDetected -= OnMovingEnemyDetected;
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
