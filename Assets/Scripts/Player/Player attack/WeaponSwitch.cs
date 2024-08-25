using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public MeleeAttack meleeWeapon; // Ссылка на скрипт ближнего боя
    public Shooting rangedWeapon; // Ссылка на скрипт дальнего боя

    public Transform attackPoint; // Точка, откуда происходит атака

    public bool isMeleeActive = true; // Текущее состояние оружия


    private void Start()
    {
        // Начнем с активированного ближнего боя
        meleeWeapon.gameObject.SetActive(true);
        rangedWeapon.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Переключение между типами оружия по нажатию клавиши Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
    }

    public void SwitchWeapon()
    {
        // Меняем состояние на противоположное
        isMeleeActive = !isMeleeActive;

        // Активируем/деактивируем оружие в зависимости от состояния
        meleeWeapon.gameObject.SetActive(isMeleeActive);
        rangedWeapon.gameObject.SetActive(!isMeleeActive);
    }
}
