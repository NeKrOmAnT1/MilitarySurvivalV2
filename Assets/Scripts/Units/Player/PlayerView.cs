using UnityEngine;

public class PlayerView
{
    private readonly Animator _animator;
    public PlayerView(Animator animator) => 
        _animator = animator;
    public void AnimationMove(Vector2 direction)
    {
        _animator.SetFloat("HorisontalMove", direction.x);
        _animator.SetFloat("VerticalMove", direction.y);
    }
}
