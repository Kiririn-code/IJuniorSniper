using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _weaponCamera;
    [SerializeField] private Joystick _joystick;

    private void Update()
    {
        if (_joystick.Horizontal >= 0.1f || _joystick.Horizontal <= -0.1f)
        {
            transform.Rotate(new Vector3(0,_joystick.Horizontal * 0.08f, 0));
        }

        if (_joystick.Vertical >= 0.1f || _joystick.Vertical <= -0.1f)
        {
            transform.Rotate(new Vector3(0, 0, _joystick.Vertical * 0.08f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = _weaponCamera.ViewportPointToRay(new Vector3(0.5f,0.5f));
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.ApplyHit();
            }
        }
    }
}
