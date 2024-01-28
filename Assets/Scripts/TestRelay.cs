using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using UnityEngine;

public class TestRelay : MonoBehaviour
{

    private async void Start()
    {

        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {

            Debug.Log("Signed In " + AuthenticationService.Instance.PlayerId);

        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }

}
