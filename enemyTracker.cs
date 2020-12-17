using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyTracker : MonoBehaviour
{
    void Update()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
        text.SetText(CharController.kills.ToString());
    }
}
