using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceceTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceceTransitionName = sceneTransitionName;
    }
}
