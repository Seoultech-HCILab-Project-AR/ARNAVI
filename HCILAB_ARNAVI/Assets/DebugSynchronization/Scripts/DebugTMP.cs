using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugTMP : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();

        text.text = "Hello HCILAB";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
