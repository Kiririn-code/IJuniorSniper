using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Animator _animator;

    private bool _isMove = true;
    private const string Walk = "Walk";
    private const string Die = "Die";
    public event UnityAction<Enemy> Died;

    public bool IsMove { get => _isMove; }
    public bool IsRed { get; private set; }

    public void StartWalkForward()
    {
        StartCoroutine(WalkForwardCoroutine());
    }

    public void SetColor()
    {
        _renderer.material.color = _isMove ? Color.red : Color.green;
        IsRed = _isMove ? true : false;
    }

    public void ApplyHit()
    {
        _renderer.material.color = Color.white;
        _animator.SetTrigger(Die);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Died?.Invoke(this);
        Destroy(gameObject, 2f);
    }

    private IEnumerator WalkForwardCoroutine()
    {
        float runTime = Random.Range(5, 7);
        int speed = 1;
        float currentRunTime = 0;
        var time = new WaitForEndOfFrame();
        _animator.SetBool(Walk, _isMove);
        while (currentRunTime < runTime)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return time;
            currentRunTime += Time.deltaTime;
        }
        _isMove = false;
        _animator.SetBool(Walk, _isMove);
    }
}
