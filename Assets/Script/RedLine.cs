using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RedLine : MonoBehaviour
{
    private float _destroyTime = 2;
    private float _speed = 30;

    public event UnityAction<Enemy> MovingEnemyDetected;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.SetColor();
            if(enemy.IsMove)
            {
                MovingEnemyDetected(enemy);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(TranslateLineCoroutine());
    }

    private IEnumerator TranslateLineCoroutine()
    {
        var time = new WaitForEndOfFrame();
        float lifeTime = 0;
        while (lifeTime <= _destroyTime) 
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            lifeTime += Time.deltaTime;
            yield return time;
        }
        Destroy(gameObject);
    }
}
