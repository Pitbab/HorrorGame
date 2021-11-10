using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationManager : MonoBehaviour
{
    private Animator PlayerAnimator;
    private const string Jumping = "Jumping";
    private const string Trapped = "Trapped";
    [SerializeField] private Rig ArmRig;
    private bool Aim = false;
    private const float TimeToMoveArm = 0.4f;

    private void Start()
    {
        PlayerManager.Instance.Anim = this;
        PlayerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(GetAnimName());
    }

    public void PlayjumpAnim()
    {
        PlayerAnimator.SetTrigger(Jumping);
    }
    

    public string GetAnimName()
    {
        string playingAnim = " ";
        if (this != null)
        {
            playingAnim = PlayerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            return playingAnim;
        }

        return null;
    }

    public void SetMovingBlend(float HorAxis, float VerAxis)
    {
        PlayerAnimator.SetFloat("Horizontal", HorAxis);
        PlayerAnimator.SetFloat("Vertical", VerAxis);
    }

    public void PlayTrapped()
    {
        PlayerAnimator.SetBool(Trapped, true);
    }

    public void StopTrapped()
    {
        PlayerAnimator.SetBool(Trapped, false);
    }

    public void Aiming()
    {
        Aim = !Aim;
        PlayerManager.Instance.Aiming = Aim;
        if (Aim)
        {
            StartCoroutine(MoveArm(1));
        }
        else
        {
            StartCoroutine(MoveArm(0));
        }

    }

    private IEnumerator MoveArm(float weight)
    {
        float timer = 0;

        float CurrentValue = ArmRig.weight;

        while (timer < TimeToMoveArm)
        {
            timer += Time.deltaTime;
            float lerpValue = timer / TimeToMoveArm;

            ArmRig.weight = Mathf.Lerp(CurrentValue, weight, lerpValue);
            yield return null;
        }

    }



}
