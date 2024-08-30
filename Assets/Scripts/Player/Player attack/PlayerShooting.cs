using System;
using System.Collections;
using System.Collections.Generic;
using GameGrid;
using GamePlayer;
using OldSceneNamespace;
using Roulettes;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float launchSpeed = 5f;
    public float minLaunchSpeed = 5f;
    public float maxFlightTime = 3;

    public float scatterAngle = 5f;

    public float fireRate = 1f;
    public float nextFireTime = 0f;

    [Range(1, 100)]
    public int maxBulletsInCartridge = 7;
    public int currentBulletsInCartridge = 7;
    [Range(0.01f, 100f)]
    public float reloadTimeInterval = 2f;
    [Range(0.01f, 100f)]
    public float jamFixingTimeInterval = 5f;
    [Range(0f, 1f)]
    public float jammingOnReloadProbability = 0.333f;
    [Range(0f, 1f)]
    public float jammingOnShootProbability = 0.1f;
    private bool isReloading = false;
    private bool isJammed = false;
    private bool isJamFixing = false;
    private IEnumerator coroutineReload;
    private IEnumerator coroutineFixJam;

    [Header("Звуки эффектов")]
    [SerializeField] private AudioClip[] shootEffectClips;
    private AudioSource effectAudioSource;

    public bool IsJammed { get { return isJammed; } }

    public Action OnShoot { get { return onShoot; } set { onShoot = value; } }
    private Action onShoot;
    public Action OnReloadStarted { get { return onReloadStarted; } set { onReloadStarted = value; } }
    private Action onReloadStarted;
    public Action OnReloadFinished { get { return onReloadFinished; } set { onReloadFinished = value; } }
    private Action onReloadFinished;
    public Action OnJammed { get { return onJammed; } set { onJammed = value; } }
    private Action onJammed;
    public Action OnJamFixingStarted { get { return onJamFixingStarted; } set { onJamFixingStarted = value; } }
    private Action onJamFixingStarted;
    public Action OnJamFixingFinished { get { return onJamFixingFinished; } set { onJamFixingFinished = value; } }
    private Action onJamFixingFinished;

    private ISceneExecutor scenes;
    private PlayerRoulette playerRoulette;
    private PlayerController playerController;
    private List<GridController> gridControllerList;
    [Inject]
    private void InitBindings(
        PlayerRoulette pr,
        PlayerController pc,
        List<GridController> gcs,
        ISceneExecutor sceneExecutor)
    {
        playerRoulette = pr;
        ApplyRouletteModifiers();

        playerController = pc;

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

    void OnEnable()
    {
        playerController.weaponSwitcher.OnWeaponSwitch += OnWeaponSwitch;
    }

    void OnDisable()
    {
        playerController.weaponSwitcher.OnWeaponSwitch -= OnWeaponSwitch;
    }

    void OnWeaponSwitch()
    {
        if (isReloading)
        {
            StopCoroutine(coroutineReload);
            isReloading = false;
        }
        if (isJamFixing)
        {
            StopCoroutine(coroutineFixJam);
            isJamFixing = false;
        }
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            Attack(mousePosition, false);
        }
        if (Input.GetKeyDown(KeyCode.R) && this.gameObject.activeSelf)
        {
            if (isJammed)
            {
                coroutineFixJam = StartFixJam();
                StartCoroutine(coroutineFixJam);
            }
            else
            {
                coroutineReload = StartReload();
                StartCoroutine(StartReload());
            }
        }
    }

    public void Attack(Vector3 v3, bool isTouchInput)
    {
        if (currentBulletsInCartridge <= 0)
        {
            if (isTouchInput)
            {
                coroutineReload = StartReload();
                StartCoroutine(coroutineReload);
            }
            return;
        }

        if (isJammed)
        {
            if (isTouchInput)
            {
                coroutineFixJam = StartFixJam();
                StartCoroutine(coroutineFixJam);
            }
            return;
        }

        if (Random.Range(0f, 1f) < jammingOnShootProbability)
        {
            isJammed = true;
            onJammed?.Invoke();
            return;
        }

        if (Time.time < nextFireTime)
        {
            return;
        }

        if (isReloading)
        {
            return;
        }

        currentBulletsInCartridge--;

        onShoot?.Invoke();

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

    private IEnumerator StartReload()
    {
        if (isReloading)
        {
            yield break;
        }

        isReloading = true;
        onReloadStarted?.Invoke();

        if (Random.Range(0f, 1f) < jammingOnReloadProbability)
        {
            var delayInSeconds = Random.Range(0f, reloadTimeInterval);
            StartCoroutine(StartDelayBeforeJamAppear(delayInSeconds));
            yield break;
        }

        yield return new WaitForSeconds(reloadTimeInterval);

        currentBulletsInCartridge = maxBulletsInCartridge;
        isReloading = false;
        
        onReloadFinished?.Invoke();
    }

    private IEnumerator StartFixJam()
    {
        if (isJamFixing)
        {
            yield break;
        }

        isJamFixing = true;
        onJamFixingStarted?.Invoke();

        yield return new WaitForSeconds(jamFixingTimeInterval);

        currentBulletsInCartridge = maxBulletsInCartridge;
        isJamFixing = false;
        isJammed = false;
        
        onJamFixingFinished?.Invoke();
    }

    private IEnumerator StartDelayBeforeJamAppear(float delayInSeconds)
    {        
        // if you switch weapon in this interval, then no jam happen, which isn't good
        yield return new WaitForSeconds(delayInSeconds);

        isReloading = false;
        isJammed = true;
        onJammed?.Invoke();
    }
}
