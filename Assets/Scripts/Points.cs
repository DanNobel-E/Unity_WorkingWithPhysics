using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public int points { get; private set; }
    public TextMeshProUGUI PointsText;

    public void AddPoint(int val)
    {
        points += val;
        PointsText.text = points.ToString();
    }

    
}
