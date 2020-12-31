using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationId
{ 
    Idle=0,
    Walk=1,
    Jump=2,
    Fall=3

}
public class AnimatorController : MonoBehaviour
{


    Animator animator;
    public void PlayAnimation(AnimationId animationId) {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
        animator.Play(animationId.ToString());
    }
}
