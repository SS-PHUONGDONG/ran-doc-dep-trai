using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NetworkPrefabRef playerPrefab;
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, 0f);
            Runner.Spawn(
                playerPrefab,
                spawnPosition,
                Quaternion.identity, player);
        }
    }
}
