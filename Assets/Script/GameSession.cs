using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    [SerializeField] private LineGenerator _generator;
    [SerializeField] private Player _player;
    [SerializeField] private Text _timeCountdown;
    [SerializeField] private Transform _camera;
    [SerializeField] private EnemyGenerator _enemyGenerator;

    private List<Enemy> _targetEnemy;

    public event UnityAction<int> StartGame;
    public event UnityAction EndGame;

    private void Start()
    {
        _targetEnemy = new List<Enemy>();
        _enemyGenerator.Generate();
    }

    private void OnEnable()
    {
        _generator.MovingEnemyAdded += OnEnemyMovingAdded;
        _player.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _generator.MovingEnemyAdded -= OnEnemyMovingAdded;
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
        _timeCountdown.text = "RED LINE!";
        Vector3 middleCameraPosition = new Vector3(-15.83f, 4.4f, 28.03f);
        _camera.DOMove(middleCameraPosition, 2).OnComplete(() => StartCoroutine(CountdownTime()));
    }

    private IEnumerator CountdownTime()
    {
        var time = new WaitForEndOfFrame();
        float countdownTime = 3;
        Vector3 finishCameraPosition = new Vector3(-15.81f, 3.70f, 28.03f);
        while (countdownTime>=0)
        {
            _timeCountdown.text = Mathf.Round(countdownTime).ToString();
            yield return time;
            countdownTime -= Time.deltaTime;
        }
        _generator.InitRedLine();
        _camera.DOMove(finishCameraPosition, 1).OnComplete(() => StartGame?.Invoke(_targetEnemy.Count));
        _player.enabled = true;
    }
}
