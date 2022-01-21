using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameSession : MonoBehaviour,IRestartable
{
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private Player _player;
    [SerializeField] private Text _timeCountdown;
    [SerializeField] private Transform _camera;
    [SerializeField] private EnemyGenerator _enemyGenerator;

    private List<Enemy> _targetEnemy;
    private Transform _startPosition;

    public event UnityAction<int> StartGame;
    public event UnityAction EndGame;
    public event UnityAction RestartGame;

    private void Start()
    {
        _startPosition = transform;
        _targetEnemy = new List<Enemy>();
        _enemyGenerator.Generate();
    }

    private void OnEnable()
    {
        _player.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _player.EnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        _targetEnemy.Remove(enemy);

        if (_targetEnemy.Count == 0)
            EndGame?.Invoke();
    }
    private void OnEnemyMovingAdded(Enemy enemy)
    {
        _targetEnemy.Add(enemy);
    }

    public void StartSession()
    {
        _enemyGenerator.StartEnemyWalk();
        _timeCountdown.text = "RED LINE!";
        Vector3 middleCameraPosition =_camera.transform.localPosition + new Vector3(6f, 1f, 0f);
        _camera.DOLocalMove(middleCameraPosition, 2).OnComplete(() => StartCoroutine(CountdownTime()));
    }

    private IEnumerator CountdownTime()
    {
        var time = new WaitForEndOfFrame();
        float countdownTime = 3;
        Vector3 finishCameraPosition = _camera.transform.localPosition + new Vector3(-1f,-1f,0f);
        while (countdownTime>=0)
        {
            _timeCountdown.text = Mathf.Round(countdownTime).ToString();
            yield return time;
            countdownTime -= Time.deltaTime;
        }
        _lineGenerator.InitRedLine();
        _lineGenerator.MovingEnemyAdded += OnEnemyMovingAdded;
        _camera.DOLocalMove(finishCameraPosition, 1).OnComplete(() => StartGame?.Invoke(_targetEnemy.Count));
        _player.enabled = true;
    }

    public void Restart()
    {
        _targetEnemy = new List<Enemy>();
        RestartGame?.Invoke();
        _enemyGenerator.Restart();
        _lineGenerator.MovingEnemyAdded -= OnEnemyMovingAdded;
        transform.position = _startPosition.position;
        transform.rotation = _startPosition.rotation;
        StartSession();
    }
}
