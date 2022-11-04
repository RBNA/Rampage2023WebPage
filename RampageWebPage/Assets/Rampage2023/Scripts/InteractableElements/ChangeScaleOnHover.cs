using System;
using UnityEngine;

namespace InteractableElements
{
    [RequireComponent(typeof(ClickableElement))]
    public class ChangeScaleOnHover : MonoBehaviour
    {
        private Vector3 _originalScale;
        private ClickableElement _clickableElement;

        [SerializeField] private float _scaleFactor = 1.2f;

        private void Awake()
        {
            _originalScale = transform.localScale;
            
            _clickableElement = GetComponent<ClickableElement>();
            _clickableElement.onMouseHoverEnter.AddListener(ScaleIn);
            _clickableElement.onMouseHoverExit.AddListener(ScaleOut);
        }


        private void ScaleIn()
        {
            transform.localScale = _originalScale * _scaleFactor;
        }
     
        private void ScaleOut()
        {
            transform.localScale = _originalScale / _scaleFactor;
        }
    }
}