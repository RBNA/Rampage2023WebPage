using UnityEngine;
using UnityEngine.Events;

namespace InteractableElements
{
    [RequireComponent(typeof(Collider))]
    public class ClickableElement : MonoBehaviour
    {
        public UnityEvent onMouseHoverEnter;
        public UnityEvent onMouseHoverStay;
        public UnityEvent onMouseHoverExit;
        public UnityEvent onMouseClick;
    }
}
