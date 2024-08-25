using GameEventBus;
using OldSceneNamespace;
using Roulettes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    private bool isTakingDamage = false;
    private bool isDead = false;

    public SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] damageEffectClips;
    private AudioSource effectAudioSource;

    private ISceneExecutor scenes;
    private EnemyRoulette enemyRoulette;
    private EventBus eventBus;
    public void LinkEnemyRoulette(EnemyRoulette er, EventBus eb, ISceneExecutor sceneExecutor)
    {
        enemyRoulette = er;
        eventBus = eb;
        ApplyRouletteModifiers();
        
        scenes = sceneExecutor;
    }
    void ApplyRouletteModifiers()
    {
        var mod = enemyRoulette.enemyKindsMap[EnemyKind.Health].modifier;
        switch (mod)
        {
            case EnemyModifier.Unchanged:
                break;
            case EnemyModifier.Increased:
                maxHealth *= 2;
                break;
            case EnemyModifier.Decreased:
                maxHealth /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;

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

    public void TakeDamage(int damage)
    {
        int randomValue = Random.Range(0, damageEffectClips.Length);
        effectAudioSource.clip = damageEffectClips[randomValue];
        effectAudioSource.Play();

        currentHealth -= damage;
        Debug.Log($"{name} Enemy took damage: " + damage + " Current health: " + currentHealth);
        isTakingDamage = true;

        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Update()
    {
        if (isTakingDamage)
        {
            animator.SetBool("EnemyDamage", true);
            isTakingDamage = false;
        }

        else
        {
            animator.SetBool("EnemyDamage", false);
        }
    }

    public void HealEnemy(int value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log("Enemy healed: " + value + " Current health: " + currentHealth);
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        spriteRenderer.sprite = deathSprite;
        
        // order matters
        // DestroyComponents<CapsuleCollider2D>();
        var componentsToDestroy = new List<System.Type>
        {
            typeof(EnemyMovement),
            typeof(EnemyEnvironmentIntersection),
            typeof(CapsuleCollider2D),
            typeof(AudioSource),
            typeof(EdgeCollider2D),
            typeof(Rigidbody2D),
            typeof(EnemyShooting),
            typeof(EnemyMeleeAttack),
            typeof(Animator)
        };
        foreach (var componentType in componentsToDestroy)
        {
            DestroyComponents(componentType);
        }

        Destroy(this);
        // Destroy(gameObject);
    }

    void Die()
    {
        animator.SetBool("EnemyDeath", true);
        Debug.Log("Enemy died!");
        eventBus.Raise(new EnemyDieEvent());
        StartCoroutine(DeathCoroutine());
    }

    void DestroyComponents<T>() where T : Component
    {
        foreach (var component in GetComponents<T>())
        {
            Destroy(component);
        }
    }

    void DestroyComponents(System.Type componentType)
    {
        foreach (var component in GetComponents(componentType))
        {
            Destroy(component);
        }
    }
}
