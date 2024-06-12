using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    // Start is called before the first frame update
    private Text uitext;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    

    public void AddWriter (Text uitext, string textToWrite, float timePerCharacter)
    {
        this.uitext = uitext;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        
        characterIndex = 0;
    }
    private void Update()
    {
        while(uitext != null)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                uitext.text =  textToWrite.Substring(0,characterIndex);
                
                

                if (characterIndex >= textToWrite.Length)
                {
                    uitext = null;
                    return;
                }
            }
        }
    }
}
