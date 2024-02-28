using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // MonoBehaviour�� ��ӹ޴� Ŭ���� GameManager
{
    public static GameManager Inst
    {
        get; private set;
    }

    public PlayerCharacter playerCharacter; // PlyaerCharacter Ŭ������ ���� playerCharacter ����

    public GuardianUpgradeManager guardianUpgradeManager;
    public GuardianBuildManager guardianBuildManager;

    private void Awake() //���� ���۵ɶ� �۵���
    {   
        //�̱��� ���� 
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(Inst);
        }
    }

    public void GameDefeat() // GameDefeat �Լ� ���� �� ����
    {

    }
    public void EnemyDead(int coin) // EnemyDead �Լ� ���� �� ����
    {
        playerCharacter.Coin += coin; // playerCharcter ��ũ��Ʈ�� Coin���� �Ű����� coin���� ���Ѵ�
    }
}