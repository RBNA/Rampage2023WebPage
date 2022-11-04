using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RiderPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtRiderName;
    [SerializeField] private TextMeshProUGUI _txtyRiderAge;
    [SerializeField] private Image _imgRiderPicture;
    [SerializeField] private Image _imgFlag;
    
    public void Populate(RiderData data)
    {
        _txtRiderName.text = data.Name;
        _txtyRiderAge.text = data.Age.ToString();
        _imgRiderPicture.sprite = data.Photo;
        _imgFlag.sprite = data.Flag;
    }
}

[Serializable]
public struct RiderData
{
    public string Name;
    public int Age;
    public Sprite Flag;
    public Sprite Photo;
}

