using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Cri : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Sound.SoundManager.PlaySE("SE1", gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Sound.SoundManager.PlaySE("SE2", gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Sound.SoundManager.PlaySE("SE3", gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Sound.SoundManager.PlaySE("SE4", gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Sound.SoundManager.PlaySE("SE5", gameObject);

        if (Input.GetKeyDown(KeyCode.Q)) Sound.SoundManager.SetAllSEVolume(0.5f);
        if (Input.GetKeyDown(KeyCode.W)) Sound.SoundManager.SetAllSEVolume(1.0f);

        if (Input.GetKeyDown(KeyCode.O)) Sound.SoundManager.SetBGMandAllSEVolume(0.5f);
        if (Input.GetKeyDown(KeyCode.P)) Sound.SoundManager.SetBGMandAllSEVolume(1.0f);

        if (Input.GetKeyDown(KeyCode.A)) Sound.SoundManager.StopSE("SE1");
        if (Input.GetKeyDown(KeyCode.S)) Sound.SoundManager.StopSE("SE2");
        if (Input.GetKeyDown(KeyCode.D)) Sound.SoundManager.StopSE("SE3");
        if (Input.GetKeyDown(KeyCode.F)) Sound.SoundManager.StopSE("SE4");

        if (Input.GetKeyDown(KeyCode.Space)) Sound.SoundManager.StopBGMandAllSE();
    }
}
