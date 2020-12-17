using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthTracker : MonoBehaviour
{
    void Update()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.SetText(CharController.mooseHealth.ToString() + "/30");
    }
}
