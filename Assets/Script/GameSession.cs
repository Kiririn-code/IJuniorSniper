using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] private LineGenerator _generator;
    [SerializeField] private Player _player;
    [SerializeField] private Text _timeCountdown;
    [SerializeField] private Transform _camera;
    [SerializeField] private Switcher _switcher;
    [SerializeField] private EnemyGenerator _enemyGenerator;

    private void Start()
    {
        _enemyGenerator.Generate();
    }

    public void StartSession()
    {
        _timeCountdown.text = "RED LINE";
        _camera.DOMove(new Vector3(-12.13f, 3.95f, 28.19f), 2).OnComplete(()=>
        {
            StartCoroutine(CountdownTime());
        });
    }

    private IEnumerator CountdownTime()
    {
        var time = new WaitForEndOfFrame();
        float countdownTime = 3;
        while (countdownTime>=0)
        {
            _timeCountdown.text = Mathf.Round(countdownTime).ToString();
            yield return time;
            countdownTime -= Time.deltaTime;
        }
        _switcher.SwitchAll();
        _generator.InitRedLine();
        _player.enabled = true;
    }
}
