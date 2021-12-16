using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private GameObject _particlePrefub;

    private Material _startMaterial;
    private bool _isMove = true;
    private const string Walk = "Walk";
    private const string Die = "Die";


    public bool IsMove { get => _isMove; }
    public bool IsRed { get; private set; }

    public void StartWalkForward()
    {
        StartCoroutine(WalkForwardCoroutine());
    }

    public void SetColor()
    {
        _startMaterial = _renderer.material;
        _renderer.material = _isMove ? _redMaterial : _greenMaterial;
        IsRed = _isMove ? true : false;
    }

    public void ApplyHit()
    {
        _renderer.material = _startMaterial;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        _animator.SetTrigger(Die);
        Instantiate(_particlePrefub, transform);
        Destroy(gameObject, 1.5f);
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
