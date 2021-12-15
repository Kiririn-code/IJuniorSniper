using UnityEngine;
using UnityEngine.Events;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private RedLine _prefab;
    [SerializeField] private Transform _ground;

    public event UnityAction StartRedLine;

    public void InitRedLine()
    {
        Instantiate(_prefab, _ground.position, Quaternion.identity);
        StartRedLine?.Invoke();
    }
}
