using System;
using JetBrains.Annotations;
using UnityEngine;

public class JsBridgeScroll : MonoBehaviour
{
    public event Action<int> onScrollPositionChanged;
    public event Action<int> onDeltaXChanged;
    public event Action<int> onDeltaYChanged;
    public event Action<int> onDeltaZChanged;

    private int _currentScrollPosition = 0;
    private int _pageSize = 0;
    
    [UsedImplicitly]
    public void JsScrollPositionChanged(int scrollPosition)
    {
        _currentScrollPosition = scrollPosition;
        onScrollPositionChanged?.Invoke(scrollPosition);
    }
    
    [UsedImplicitly]
    public void JsDeltaXChanged(int deltaX)
    {
        onDeltaXChanged?.Invoke(deltaX);
    }
    
    [UsedImplicitly]
    public void JsDeltaYChanged(int deltaY)
    {
        onDeltaYChanged?.Invoke(deltaY);
    }
    
    [UsedImplicitly]
    public void JsDeltaZChanged(int deltaZ)
    {
        onDeltaZChanged?.Invoke(deltaZ);
    }

    public int GetCurrentScrollPosition()
    {
        return _currentScrollPosition;
    }

    public float GetCurrentScrollPositionNormalized()
    {
        return (float)_currentScrollPosition/_pageSize;
    }
}
