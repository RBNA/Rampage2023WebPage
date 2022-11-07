using System;
using Dreamteck.Splines;
using InteractableElements;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(SplineRenderer), typeof(ClickableElement))]
public class TrackController : MonoBehaviour
{
    [SerializeField] private GameObject _particleObject;
    [SerializeField] private RiderPanel _riderPanel;
    [SerializeField] private RiderData _riderData;
    [SerializeField] private float _highlightScaleMultiplier = 1.25f;
    [SerializeField] private Material _highlightMaterial;
    
    [SerializeField] private bool _debugSpline;

    [ShowIf("_debugSpline")] [SerializeField] [Range(0.0f, 1.0f)]
    private float _currentTrackPos = 0;

    private SplineComputer _splineComputer;
    private SplineRenderer _splineRenderer;
    private SplinePositioner _splinePositioner;
    private ClickableElement _clickableElement;

    private float _lastPositionSet = -1;
    private float _originalPointSize;
    private MeshRenderer _meshRenderer;
    private Material _originalMaterial;

    private void Awake()
    {
        _splineComputer = GetComponent<SplineComputer>();
        _splineRenderer = GetComponent<SplineRenderer>();
        _splinePositioner = _particleObject.GetComponent<SplinePositioner>();

        _splineRenderer.clipFrom = 0;
        _splinePositioner.clipFrom = 0;

        _meshRenderer = GetComponent<MeshRenderer>();
        _originalPointSize = _splineComputer.GetPointSize(0);
        _originalMaterial = _meshRenderer.material;

        ConfigTrackInteractions();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (_debugSpline)
        {
            if (Math.Abs(_lastPositionSet - _currentTrackPos) < 0.001f) return;
            _lastPositionSet = _currentTrackPos;

            SetTrackPosition(_currentTrackPos);
        }
#endif
    }

    private void ConfigTrackInteractions()
    {
        _clickableElement = GetComponent<ClickableElement>();
        _clickableElement.onMouseClick.AddListener(OnMouseClickTrack);
        _clickableElement.onMouseHoverEnter.AddListener(OnMouseHoverTrack);
        _clickableElement.onMouseHoverExit.AddListener(OnMouseHoverExitTrack);
    }
    
    private void ChangeColor(float sizeMultiplier, Material material)
    {
        //     for (int i = 0; i < _splineComputer.pointCount; i++)
        //     {
        //         _splineComputer.SetPointSize(i, _originalPointSize * sizeMultiplier);
        //     }
        _meshRenderer.material = material;
    }

    private void OnMouseHoverTrack()
    {
        ChangeColor(_highlightScaleMultiplier, _highlightMaterial);
    }

    private void OnMouseHoverExitTrack()
    {
        float reduceMultiplier = 2 * _originalPointSize - (_originalPointSize * _highlightScaleMultiplier);
        ChangeColor(reduceMultiplier, _originalMaterial);
    }

    private void OnMouseClickTrack()
    {
        ShowRiderPanel();
    }

    public void ShowRiderPanel()
    {
        _riderPanel.Populate(_riderData);
        _riderPanel.gameObject.SetActive(true);
    }
    
    public void SetTrackPosition(float value)
    {
        float clampedValue = Mathf.Clamp01(value);

        //The spline was made inverse so when 0 = totally revealed, and 1 = nothing revealed
        _splineRenderer.clipFrom = 1 - clampedValue;
        _splinePositioner.clipFrom = 1 - clampedValue;

        bool trackBeenRevealed = clampedValue is > 0 and < 1;
        _particleObject.SetActive(trackBeenRevealed);
    }

    public RiderData GetRiderData()
    {
        return _riderData;
    }
}