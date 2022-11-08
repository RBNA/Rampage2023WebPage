using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class RiderStatistics : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private TextMeshProUGUI _txtName;
    [SerializeField] private TextMeshProUGUI _txtSpeed;
    [SerializeField] private TextMeshProUGUI _txtHeight;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);
    }
    
    [UsedImplicitly]
    public void Populate(TriggerData triggerData)
    {
        gameObject.SetActive(true);
        
        _txtName.text = triggerData.RiderName;
        _txtSpeed.text = triggerData.Speed + "Km/h";
        _txtHeight.text = triggerData.Height + "Mts";

        _lineRenderer.SetPosition(1, triggerData.transform.position);
    }
}