using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicStopper : MonoBehaviour
{
    public void StopGameMusic()
    {
        Destroy(GameMusic.Instance.gameObject);
    }
}
