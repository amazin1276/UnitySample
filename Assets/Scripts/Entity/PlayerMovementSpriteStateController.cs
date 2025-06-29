using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Expansion;

public class PlayerMovementSpriteStateController : MonoBehaviour
{
    [SerializeField]
    Animator animatorComponent;
    [SerializeField]
    Animation animationComponent;
    [SerializeField]
    AnimationClip runAnimation;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    MovePlayer movePlayer;

    void Update()
    {
        HandleIsRunState();
        ControlAnimationRunSpeed();
        FlipSprite();
    }


    void ControlAnimationRunSpeed()
    {
        bool _isNotRunAnimationSet = animationComponent.clip == runAnimation;
        if (_isNotRunAnimationSet) return;

        float _movingSpeed = movePlayer.MovingSpeed;
        
    }


    void HandleIsRunState()
    {
        bool _isRun = movePlayer.IsRun;
        string _isRunStateName = "isRun";
        animatorComponent.SetBool(_isRunStateName, _isRun);
    }


    void FlipSprite()
    {
        bool _isFlip = movePlayer.IsRunToLeft;
        spriteRenderer.flipX = _isFlip;
    }
}
