using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [Header("Canvas group")]
    [SerializeField] private CanvasGroup _startMenu;
    [SerializeField] private CanvasGroup _playMenu;
    [SerializeField] private CanvasGroup _winMenu;
    [Header("Text group")]
    [SerializeField] private Text _totalRedEnemyCount;
    [SerializeField] private Text _currentRedEnemyCount;
    [Header("Other group")]
    [SerializeField] private Player _player;
    [SerializeField] private Pointer _pointer;
    [SerializeField] private GameSession _session;

    private int _currentKilledEnemyCount = 0;

    private void OnEnable()
    {
        _session.StartGame += OnGameStart;
        _session.EndGame += OnGameEnd;
        //_session.RestartGame += OnGameRestart;
        _player.EnemyKilled += OnEnemyKilled;
    }
    private void OnDisable()
    {
        _session.StartGame -= OnGameStart;
        _session.EndGame -= OnGameEnd;
        //_session.RestartGame -= OnGameRestart;
        _player.EnemyKilled -= OnEnemyKilled;
    }

    private void OnGameStart(int value)
    {
        Debug.Log("start" + Time.time);
        ChangeState(_startMenu);
        ChangeState(_playMenu);
        _totalRedEnemyCount.text = "/" + value.ToString();
    }

    private void OnGameEnd()
    {
        _pointer.StartMove();
        ChangeState(_playMenu);
        ChangeState(_winMenu);
    }

    public void OnGameRestart()
    {
        _currentRedEnemyCount.text = " ";
        ChangeState(_winMenu);
        ChangeState(_startMenu);
        Debug.Log("rest" + Time.time);
    }

    private void ChangeState(CanvasGroup group)
    {
        group.alpha = group.alpha == 1 ? 0 : 1;
        group.blocksRaycasts = !group.blocksRaycasts;
        group.interactable = !group.interactable;
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        if (enemy.IsRed)
        {
            int _currentEnemyKilled = ++_currentKilledEnemyCount;
            _currentRedEnemyCount.text = _currentEnemyKilled.ToString();
        }
    }
}
