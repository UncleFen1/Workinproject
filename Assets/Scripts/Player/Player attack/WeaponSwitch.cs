using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public MeleeAttack meleeWeapon; // ������ �� ������ �������� ���
    public Shooting rangedWeapon; // ������ �� ������ �������� ���

    public Transform attackPoint; // �����, ������ ���������� �����

    private bool isMeleeActive = true; // ������� ��������� ������

   
    private void Start()
    {
        
        // ������ � ��������������� �������� ���
        meleeWeapon.gameObject.SetActive(true);
        rangedWeapon.gameObject.SetActive(false);
                               
    }

    private void Update()
    {
        // ������������ ����� ������ ������ �� ������� ������� Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

      
    }

    private void SwitchWeapon()
    {
        // ������ ��������� �� ���������������
        isMeleeActive = !isMeleeActive;

        // ����������/������������ ������ � ����������� �� ���������
        meleeWeapon.gameObject.SetActive(isMeleeActive);
        rangedWeapon.gameObject.SetActive(!isMeleeActive);


    }
    
}