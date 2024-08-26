using System.Collections.Generic;
using GameGrid;
using OldSceneNamespace;
using Roulettes;
using UnityEngine;
using Zenject;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float launchSpeed = 5f;
    public float minLaunchSpeed = 5f;
    public float maxFlightTime = 3;

    public float scatterAngle = 5f;

    public float fireRate = 1f;
    public float nextFireTime = 0f;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] shootEffectClips;
    private AudioSource effectAudioSource;

    private ISceneExecutor scenes;
    private PlayerRoulette playerRoulette;
    private List<GridController> gridControllerList;
    [Inject]
    private void InitBindings(PlayerRoulette pr, List<GridController> gcs, ISceneExecutor sceneExecutor)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();

        gridControllerList = gcs;

        scenes = sceneExecutor;
    }
    void ApplyRouletteModifiers()
    {
        var mod = playerRoulette.playerKindsMap[PlayerKind.AttackRange].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                maxFlightTime *= 2;
                break;
            case PlayerModifier.Decreased:
                maxFlightTime /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackAccuracy].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                scatterAngle /= 2;
                break;
            case PlayerModifier.Decreased:
                scatterAngle *= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }

        mod = playerRoulette.playerKindsMap[PlayerKind.AttackRate].modifier;
        switch (mod)
        {
            case PlayerModifier.Unchanged:
                break;
            case PlayerModifier.Increased:
                fireRate *= 2;
                break;
            case PlayerModifier.Decreased:
                fireRate /= 2;
                break;
            default:
                Debug.LogWarning("_j unknown modifier");
                break;
        }
    }

    void Start()
    {
        SetupAudio();
    }

    void SetupAudio()
    {
        if (effectAudioSource == null)
        {
            effectAudioSource = gameObject.AddComponent<AudioSource>();
        }

        effectAudioSource.clip = shootEffectClips[0];
        effectAudioSource.loop = false;

        scenes.OnSetSettingsAudioScene += (SettingsScene settingsScene) => {
            effectAudioSource.volume = settingsScene.EffectValum;
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            Attack(mousePosition);
        }
    }

    public void Attack(Vector3 v3)
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        int randomValue = Random.Range(0, shootEffectClips.Length);
        effectAudioSource.clip = shootEffectClips[randomValue];
        effectAudioSource.Play();

        GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.identity);
        var bulletInstance = bulletGO.GetComponent<PlayerBullet>();
        if (bulletInstance && bulletInstance.isActiveAndEnabled)
        {
            bulletInstance.LinkPlayerRoulette(playerRoulette, gridControllerList);
        }
        else
        {
            Debug.LogWarning("can't find PlayerBullet component");
        }

        Vector2 direction = (v3 - transform.position).normalized;
        if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
        {
            direction = v3.normalized;
        }

        float randomAngle = Random.Range(-scatterAngle, scatterAngle);
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

        Vector2 scatterDirection = new Vector2(
            direction.x * Mathf.Cos(angleInRadians) - direction.y * Mathf.Sin(angleInRadians),
            direction.x * Mathf.Sin(angleInRadians) + direction.y * Mathf.Cos(angleInRadians)
        );

        float angle = Mathf.Atan2(scatterDirection.y, scatterDirection.x) * Mathf.Rad2Deg;
        bulletGO.transform.eulerAngles = new Vector3(0, 0, angle - 90f);

        Rigidbody2D rb = bulletGO.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float speed = launchSpeed;
            if (scatterDirection.magnitude < minLaunchSpeed)
            {
                scatterDirection = scatterDirection.normalized * minLaunchSpeed;
            }
            rb.velocity = scatterDirection * speed;
        }
        Destroy(bulletGO, maxFlightTime);

        nextFireTime = Time.time + 1f / fireRate;
    }
}
