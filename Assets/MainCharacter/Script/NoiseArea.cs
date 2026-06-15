using UnityEngine;

public class NoiseArea: MonoBehaviour
{
    public SphereCollider _sphereCollider;
    public int _noiselevel;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarochitoBattler>() != null)
        {
            if (other.GetComponent<CarochitoBattler>()._isEnemy == true)
            {
                other.GetComponent<MonsterController>().Hear(transform);
            }
        }
    }

    public void ChangeNoiseLevel(float newNoise)
    {
        if (newNoise == 0)
        {
            _sphereCollider.enabled = false;
        }
        else
        {
            _sphereCollider.enabled = true;
        }

        if (newNoise <= 0.5f)
        {
            _noiselevel = 3;
        }
        else if (newNoise <= 1f)
        {
            _noiselevel = 5;
        }
        else if(newNoise > 1f)
        {
            _noiselevel = 7;
        }

        _sphereCollider.radius = _noiselevel;
    }
}
