using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class GameManager : MonoBehaviour
{
    private int scriptCnt = 0;
    private bool nextFlag = false;
    private ScriptPlayer player = null;
    private string[] scriptList = new string[]
    {
    "StartScript", "SecondScript"
    };

    void Start()
    {
        NaninovelInitial(); 
    }


    async void NaninovelInitial()
    {
        Engine.OnInitializationFinished += () =>
        {
            Debug.Log("Naninovel Initialize Complete");
            nextFlag = true;
            player = Engine.GetService<ScriptPlayer>();
            player.OnStop += (Script script) =>
            {
                scriptCnt++;
                nextFlag = true;
            };
        };

        // ‰Šú‰»
        await RuntimeInitializer.InitializeAsync();
    }

    void Update()
    {
        {
            if (nextFlag)
            {
                nextFlag = false;
                if (scriptList.Length > scriptCnt)
                {
                    PlayNaninovelMessage(scriptList[scriptCnt]);
                }
            }
        }
        async void PlayNaninovelMessage(string id)
        {
            await player.PreloadAndPlayAsync(id);
        }
    }

}
