using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScrollWebPageByUnity : MonoBehaviour
{
    [SerializeField] private float _scrollDelta = 1000;

    [DllImport("__Internal")]
    private static extern void scrollWindowsToNewPos(int x, int y);

    [DllImport("__Internal")]
    private static extern int CurrentScrollPosition();

    [DllImport("__Internal")]
    private static extern bool IsMobileBrowser();

    private float _lastYMovement = 0;

    private void Update()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        float scroll = 0;

        //Deal with touch
        if (IsMobileBrowser())
        {
            if (Input.touchCount <= 0) return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                if (Math.Abs(_lastYMovement - pos.y) < 0.1f) return;

                bool scrollDown = _lastYMovement > pos.y;
                scroll = scrollDown ? 1 : -1;
                _lastYMovement = pos.y;
            }
        }
        else
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll == 0) return;
        }

        //multiply by -1 to invert final value
        int scrollVal = (int)(scroll * _scrollDelta) * -1;
        int currentScroll = CurrentScrollPosition();
        // Debug.Log($"Add {scrollVal} to scroll page that is in {currentScroll} pos.");
        scrollWindowsToNewPos(0, currentScroll + scrollVal);
#endif
    }
}