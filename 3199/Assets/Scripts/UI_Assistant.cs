using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;
    private void Awake()
    {
        messageText = transform.Find("message").Find("messageText").GetComponent<Text>();
    }
    private void Start()
    {
        textWriter.AddWriter(messageText, "Helloo world huihyujujnygtvhu", .1f);


    }
}
