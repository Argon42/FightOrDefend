using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YodeGroup.Utility
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorExtension : MonoBehaviour
    {
        [SerializeField] private string defaultParameter;

        private Animator _animator;

        public Animator Animator
        {
            get
            {
                if (_animator is null)
                    _animator = GetComponent<Animator>();
                return _animator;
            }
        }

        public void SetTrue(string parameter) =>
            Animator.SetBool(parameter, true);

        public void SetFalse(string parameter) =>
            Animator.SetBool(parameter, false);

        public void SetTrueToDefaultParameter() =>
            Animator.SetBool(defaultParameter, true);

        public void SetTriggerToDefaultParameter() =>
            Animator.SetTrigger(defaultParameter);

        public void ResetTriggerToDefaultParameter() =>
            Animator.ResetTrigger(defaultParameter);

        public void SetFalseToDefaultParameter() =>
            Animator.SetBool(defaultParameter, false);

        public void InvertBool(string parameter) =>
            Animator.SetBool(parameter, !Animator.GetBool(parameter));

        public void InvertToDefaultParameter() =>
            Animator.SetBool(defaultParameter, !Animator.GetBool(defaultParameter));

        public void SetBoolToDefaultParameter(bool value) =>
            Animator.SetBool(defaultParameter, value);

        public void SetIntToDefaultParameter(int value) =>
            Animator.SetInteger(defaultParameter, value);

        public void SetFloatToDefaultParameter(float value) =>
            Animator.SetFloat(defaultParameter, value);
    }
}