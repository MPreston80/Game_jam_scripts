using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTracker : MonoBehaviour
{
    void Update()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.SetText(collectGum.counter.ToString());
    }
    
}
