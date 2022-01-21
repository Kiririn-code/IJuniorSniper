using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    [SerializeField] private Camera _weaponCamera;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _particlePrefub;

    public event UnityAction<Enemy> EnemyKilled;

    public void PlayGame()
    {
        var avatar = _animator.gameObject.GetComponent<Transform>();
        avatar.Rotate(new Vector3(0, -120, 0));
        _animator.SetBool("Play", true);
    }

    private void Update()
    {
        if (_joystick.Horizontal >= 0.1f || _joystick.Horizontal <= -0.1f)
            transform.Rotate(new Vector3(0, _joystick.Horizontal * 0.08f, 0));

        if (_joystick.Vertical >= 0.1f || _joystick.Vertical <= -0.1f)
            transform.Rotate(new Vector3(0, 0, _joystick.Vertical * 0.08f));

        if (Input.GetMouseButtonUp(0))
            Shoot();
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = _weaponCamera.ViewportPointToRay(new Vector3(0.5f,0.5f));
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                    EnemyKilled?.Invoke(enemy);
                    enemy.ApplyHit();
                var hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Instantiate(_particlePrefub, hitPoint, Quaternion.identity);
            }
        }
    }
}
