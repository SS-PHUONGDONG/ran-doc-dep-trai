using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public TMP_InputField playerNameInput;
    public Button playGameButton;
    public NetworkRunner networkRunner;

    void Start()
    {
        playGameButton.onClick.AddListener(ConnectToGame);
    }
    void ConnectToGame()
    {
        var playerName = string.IsNullOrEmpty(playerNameInput.text) ?
        "Player" + Random.Range(1000, 10000) : playerNameInput.text;

        PlayerPrefs.SetString("PlayerName", playerName);
        playerNameInput.interactable = false;
        playGameButton.interactable = false;
        StartGame(GameMode.Shared);
    }
    async void StartGame(GameMode mode)
{
    if (networkRunner == null)
        networkRunner = gameObject.AddComponent<NetworkRunner>();

    networkRunner.ProvideInput = true;
    networkRunner.AddCallbacks(this);

    var scene = SceneRef.FromIndex(1);
    var sceneInfo = new NetworkSceneInfo();
    if (scene.IsValid)
        sceneInfo.AddSceneRef(scene, LoadSceneMode.Single);

    var startGameArgs = new StartGameArgs()
    {
        GameMode = mode,
        SessionName = "Main Menu",
        Scene = sceneInfo,
        SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
    };
    await networkRunner.StartGame(startGameArgs);
}

    public void OnConnectedToServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player joined: {player}");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player Left: {player}");
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }
}
