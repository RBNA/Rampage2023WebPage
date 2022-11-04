using System;
using UnityEngine;

namespace InteractableElements
{
    public class InteractableElementsManager : MonoBehaviour
    {
        [SerializeField] private Camera _sceneCamera;

        private ClickableElement _lastHoverElement;

        private void Update()
        {
            RayCastElements();
        }

        void RayCastElements()
        {
            Ray ray = _sceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, float.PositiveInfinity))
            {
                ClickableElement clickableElement = hit.transform.GetComponent<ClickableElement>();
                if (!clickableElement)
                {
                    //Mouse Exit
                    if (_lastHoverElement)
                    {
                        _lastHoverElement.onMouseHoverExit?.Invoke();
                        _lastHoverElement = null;
                    }

                    return;
                }

                //Left Mouse click
                if (Input.GetMouseButtonDown(0))
                {
                    _lastHoverElement.onMouseClick?.Invoke();
                }

                //It's a new element
                if (!_lastHoverElement)
                {
                    _lastHoverElement = clickableElement;
                    _lastHoverElement.onMouseHoverEnter?.Invoke();
                    return;
                }

                //Mouse Stay
                if (_lastHoverElement.Equals(clickableElement))
                {
                    _lastHoverElement.onMouseHoverStay?.Invoke();
                    return;
                }

                //Mouse Exit from last element to a new one
                _lastHoverElement.onMouseHoverExit?.Invoke();
                _lastHoverElement = clickableElement;

                //Mouse Enter from a old element to a new one
                _lastHoverElement.onMouseHoverEnter?.Invoke();
            }
            else
            {
                //Mouse Exit
                if (_lastHoverElement)
                {
                    _lastHoverElement.onMouseHoverExit?.Invoke();
                    _lastHoverElement = null;
                }
            }
        }
    }
}