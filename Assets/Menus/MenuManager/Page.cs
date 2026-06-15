using UnityEngine;

public class Page : MonoBehaviour
{
    public virtual void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
