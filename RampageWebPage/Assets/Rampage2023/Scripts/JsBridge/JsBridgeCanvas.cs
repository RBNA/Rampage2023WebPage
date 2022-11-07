using System;
using JetBrains.Annotations;
using UnityEngine;

public class JsBridgeCanvas : MonoBehaviour
{
    private int _pageSize = 0;
    private Vector2 _canvasSize = Vector2.zero;
    private int _initialPosition;
    
    public event Action onWebGlReady;
    public event Action<float> onSetAnimationPosition;
    
    [UsedImplicitly]
    public void JsWebGlReady()
    {
        onWebGlReady?.Invoke();
    }

    [UsedImplicitly]
    public void JsGetPageSize(int pageSize)
    {
        _pageSize = pageSize;
    }

    [UsedImplicitly]
    public void JsGetWebGlCanvasWidth(int canvasWidth)
    {
        _canvasSize.x = canvasWidth;
    }

    [UsedImplicitly]
    public void JsGetWebGlCanvasHeight(int canvasHeight)
    {
        _canvasSize.y = canvasHeight;
    }

    [UsedImplicitly]
    public void JsGetInitialWebGlPosition(int webGlInitPos)
    {
        _initialPosition = webGlInitPos;
    }

    [UsedImplicitly]
    public void JsSetAnimationPosition(float normalizedPosition)
    {
        onSetAnimationPosition?.Invoke(normalizedPosition);
    }

    public int GetPageSize()
    {
        return _pageSize;
    }
    
    public Vector2 GetCanvasSize()
    {
        return _canvasSize;
    }

    public int GetInitialPositionOnWebPage()
    {
        return _initialPosition;
    }
}