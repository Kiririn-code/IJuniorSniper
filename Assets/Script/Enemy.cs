using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [SerializeField] private Animator _animator;

    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private GameObject _particlePrefub;

    private const string Walk = "Walk";
    private const string GetHit = "GetHit";
    

    private bool _isMove = true;

    public void Go()
    {
        StartCoroutine(Delay());
    }

    public void SetColor()
    {
        _renderer.material = _isMove ? _redMaterial : _greenMaterial;
    }

    public void ApplyHit()
    {
        _animator.SetTrigger(GetHit);
        Instantiate(_particlePrefub, transform);
        Destroy(gameObject, 0.5f);
    }

    private IEnumerator Delay()
    {
        float runTime = Random.Range(5, 7);
        int speed = 1;
        float currentRunTime = 0;
        var time = new WaitForEndOfFrame();
        _animator.SetBool(Walk, true);
        while (currentRunTime < runTime)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return time;
            currentRunTime += Time.deltaTime;
        }
        _isMove = false;
        _animator.SetBool(Walk, false);
    }
}
