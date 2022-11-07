using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TracksManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _ddlRiders;
    
    private List<TrackController> _tracks = new();
    
    void Awake()
    {
        //get all children that have the track controller script
        _tracks = GetComponentsInChildren<TrackController>().ToList();

        List<string> riders = new();
        for (int i = 0; i < _tracks.Count; i++)
        {
            //Get first Rider name
            string riderName = _tracks[i].GetRiderData().Name.Split(' ')[0];
            riders.Add(riderName);
        }
        
        //add ALL option as last option
        riders.Add("ALL");
       
        _ddlRiders.ClearOptions();
        _ddlRiders.AddOptions(riders);
        
        //Select All
        _ddlRiders.value = riders.Count;
        RiderSelected(riders.Count);
    }
    
    [UsedImplicitly]
    public void RiderSelected(int val)
    {
        if (val >= _tracks.Count)
        {
            for (int i = 0; i < _tracks.Count; i++)
            {
                _tracks[i].gameObject.SetActive(true);
            }
            
            return;
        }
        
        for (int i = 0; i < _tracks.Count; i++)
        {
            bool isSelectedTrack = i == val;
            
            _tracks[i].gameObject.SetActive(isSelectedTrack);
            
            if (isSelectedTrack)
            {
                _tracks[i].ShowRiderPanel();
            }
        }
    }
}
