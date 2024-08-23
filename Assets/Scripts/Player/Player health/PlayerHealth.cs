using OldSceneNamespace;
using Roulettes;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth = 100;
    public int currentPlayerHealth;
    public GameObject deathPrefab;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] damageEffectClips;
    private AudioSource effectAudioSource;

    private ISceneExecutor scenes;
    private PlayerRoulette playerRoulette;
    [Inject]
    private void InitBindings(PlayerRoulette pr, ISceneExecutor sceneExecutor)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();
    
        scenes = sceneExecutor;
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
        Time.timeScale = 1;

        SetupAudio();
    }

    void SetupAudio()
    {
        if (effectAudioSource == null)
        {
            effectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        effectAudioSource.clip = damageEffectClips[0];
        effectAudioSource.loop = false;

        scenes.OnSetSettingsAudioScene += (SettingsScene settingsScene) => {
            effectAudioSource.volume = settingsScene.EffectValum;
        };
    }

    public void TakePlayerDamage(int damage)
    {
        int randomValue = Random.Range(0, damageEffectClips.Length);
        effectAudioSource.clip = damageEffectClips[randomValue];
        effectAudioSource.Play();

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

        if (deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }        
        
        Destroy(gameObject);

    }
}