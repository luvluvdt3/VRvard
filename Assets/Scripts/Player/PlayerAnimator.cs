using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
    
    public void AnimatePlayerWalk(Vector2 inputDirection)
    {
        var isWalking = inputDirection != Vector2.zero;
        animator.SetBool(IsWalking, isWalking);
    }

    public void AnimatePlayerJump(PlayerController controller)
    {
        if (!animator.GetBool(IsGrounded)) return;
        StartCoroutine(AnimatePlayerJumpCoroutine(controller));
    }

    private IEnumerator AnimatePlayerJumpCoroutine(PlayerController controller)
    {
        animator.SetBool(IsGrounded, false);
        yield return new WaitForSeconds(controller.JumpCooldown);
        yield return new WaitUntil(() => controller.IsGrounded);
        animator.SetBool(IsGrounded, true);
    }

    public void AnimatePlayerSprint(bool isSprinting) => animator.SetBool(IsSprinting, isSprinting);

    public void AnimatePlayerIdle()
    {
        animator.SetBool(IsWalking, false);
        animator.SetBool(IsSprinting, false);
        animator.SetBool(IsGrounded, true);
    }
}
