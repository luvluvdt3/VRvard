using System.Collections;
using UnityEngine;

public class InputAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int HasTestedWalking = Animator.StringToHash("hasTestedWalk");
    private static readonly int HasTestedJump= Animator.StringToHash("hasTestedJump");
    private static readonly int HasTestedSprint = Animator.StringToHash("hasTestedSprint");

    public void TestWalking()
    {
        animator.SetBool(HasTestedWalking, true);
    }
    
    public void TestJump()
    {
        animator.SetBool(HasTestedJump, true);
    }
    
    public void TestSprint()
    {
        animator.SetBool(HasTestedSprint, true);
    }
}
