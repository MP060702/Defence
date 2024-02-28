using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // MonoBehaviour을 상속받는 클래스 GameManager
{
    public static GameManager Inst
    {
        get; private set;
    }

    public PlayerCharacter playerCharacter; // PlyaerCharacter 클래스의 변수 playerCharacter 선언

    public GuardianUpgradeManager guardianUpgradeManager;
    public GuardianBuildManager guardianBuildManager;

    private void Awake() //씬이 시작될때 작동됨
    {   
        //싱글톤 형식 
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(Inst);
        }
    }

    public void GameDefeat() // GameDefeat 함수 선언 및 정의
    {

    }
    public void EnemyDead(int coin) // EnemyDead 함수 선언 및 정의
    {
        playerCharacter.Coin += coin; // playerCharcter 스크립트의 Coin값에 매개변수 coin값을 더한다
    }
}