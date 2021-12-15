using System.Collections;
using UnityEngine;

public class RedLine : MonoBehaviour
{
    private float _destroyTime = 2;
    private float _speed = 30;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.SetColor();
        }
    }
    private void Start()
    {
        StartCoroutine(TranslateLine());
    }

    private IEnumerator TranslateLine()
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
