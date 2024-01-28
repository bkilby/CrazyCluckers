using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{

    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private TextMeshProUGUI PlayerCountText;
    [SerializeField] private TextMeshProUGUI RelayCode;
    [SerializeField] private TMP_InputField RelayCodeInput;
    [SerializeField] private Button RelayCodeOkButton;
    [SerializeField] private Button RelayCodeCancelButton;
    [SerializeField] private GameObject RelayCodeDialog;

    private NetworkVariable<int> PlayerCount = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    private void Awake()
    {

        HostButton.onClick.AddListener(() => 
        {

            CreateRelay();

        });

        ClientButton.onClick.AddListener(() =>
        {

            RelayCodeDialog.SetActive(true);

        });

        RelayCodeCancelButton.onClick.AddListener(() =>
        {

            RelayCodeInput.text = "";

            RelayCodeDialog.SetActive(false);

        });

        RelayCodeOkButton.onClick.AddListener(() =>
        {

            GameManager.RelayCode = RelayCodeInput.text;

            JoinRelay(GameManager.RelayCode);

            RelayCodeInput.text = "";

            RelayCodeDialog.SetActive(false);

        });

    }

    private void Update()
    {

        PlayerCountText.text = "Players: " + PlayerCount.Value.ToString();
        RelayCode.text = GameManager.RelayCode;

        if (IsServer)
        {
            PlayerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
        }

    }


    private async void CreateRelay()
    {

        var maxConnectionsToHost = 3;

        try
        {

            var allocation = await RelayService.Instance.CreateAllocationAsync(maxConnectionsToHost);

            var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            GameManager.RelayCode = joinCode;

            Debug.Log(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.StartHost();

        }
        catch (RelayServiceException exception)
        {

            Debug.Log(exception);

        }

    }

    private async void JoinRelay(string joinCode)
    {

        try
        {

            Debug.Log("Joining Relay with " + joinCode);

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();

        }
        catch (RelayServiceException exception)
        {

            Debug.Log(exception);

        }

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
