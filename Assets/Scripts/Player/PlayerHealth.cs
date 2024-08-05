using Roulettes;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth = 100;
    public int currentPlayerHealth;

    private PlayerRoulette playerRoulette;
    [Inject]
    private void InitBindings(PlayerRoulette pr)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();
    }
    void ApplyRouletteModifiers()
    {
        var mod = playerRoulette.playerKindsMap[PlayerKind.Health].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                maxPlayerHealth *= 2;
                break;
            case PlayerModifier.Decreased:
                maxPlayerHealth /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    public void TakePlayerDamage(int damage)
    {
        currentPlayerHealth -= damage;
        Debug.Log("Player took damage: " + damage + " Current health: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void HealPlayer(int heal)
    {
        currentPlayerHealth += heal;
        if (currentPlayerHealth >= 100) currentPlayerHealth = 100;
        Debug.Log($"Player healed on: {heal}. Current health: {currentPlayerHealth}");
    }

    void PlayerDie()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}