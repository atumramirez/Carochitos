using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public float motion;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateWalk();
    }

    private void AnimateWalk()
    {
        _animator.SetFloat("motion", motion);
    }
}
