using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AdministradorGPGS : MonoBehaviour
{
    public TMPro.TMP_Text GPGSText;

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcesarAutenticacion);
    }

    internal void ProcesarAutenticacion(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            GPGSText.text = $"Good Auth \n {Social.localUser.userName} \n {Social.localUser.id}";
        }
        else
        {
            GPGSText.text = $"Bad Auth";
        }
    }


}
