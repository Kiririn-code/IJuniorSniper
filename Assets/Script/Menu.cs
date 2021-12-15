using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Open(GameObject canvas)
    {
        canvas.SetActive(true);
    }

    public void Close(GameObject canvas)
    {
        canvas.SetActive(false);
    }
}
