using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    [SerializeField] private RectTransform _pointer;
    [SerializeField] private Text _scoreText;

    private Sequence _sequence;

    private int _score = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ScoreZone>(out ScoreZone zone))
        {
            _scoreText.text = (_score * zone.ScoreFactor).ToString();
        }
    }

    public void PointerGo()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(
        _pointer.DOMoveX(225, 1).SetLoops(100, LoopType.Yoyo).SetEase(Ease.InQuad));
        StartCoroutine(StopDelay());
    }

    private IEnumerator StopDelay()
    {
        float countdown = Random.Range(3f, 5.5f);
        float realTime = 0;
        var time = new WaitForEndOfFrame();
        _sequence.Play();
        while (realTime < countdown)
        {
            realTime += Time.deltaTime;
            yield return time;
        }
        _sequence.Kill(false);
    }
}
