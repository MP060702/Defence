using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour // MonoBehaviour을 상속받는 클래스 
{
    public int Coin = 100; // int 타입의 변수 coin 선언
    private int _heart = 10; // int 타입의 변수 _heart 선언,  체력값 역활을 함

    public int Heart //int 타입의 변수 Heart 선언 및 정의
    {
        get { return _heart; }
    }

    public int MaxHeart = 10; //int 타입의 변수 MaxHeart를 선언 및 정의, 최대 체력값의 역활을 함

    void Start() //씬이 시작할때 작동되는 함수
    {
        _heart = MaxHeart; // 체력값에 최대체력값을 집어넣음
    }

    public void Damaged(int damage) // 함수 Damaged 선언 및 정의 
    {
        _heart -= damage; // 변수 _heart의 값에 매겨변수 damage값 만큼을 빼준다
        if (_heart <= 0)  // 만약 플레이어의 체력이 0이하거나 같다면 아래 코드 시작
        {
            GameManager.Inst.GameDefeat(); // GameManager의 함수인 GameDefeat를 호출
        }
        Debug.Log(_heart);
    }
    public void UseCoin(int coin) // 함수 UseCoin 선언 및 정의
    {
        Coin = Mathf.Clamp(Coin - coin, 0, int.MaxValue); // 변수 Coin에 최솟값 0, 최댓값 MAxValue 값 이상 이하가 되지 않도록 하는 선에서 Coin값에서 매개변수 coin값을 뺀값을 넣어준다
    }

    public bool CanUseCoin(int coin) // 논리형 함수 CanUseCoin을 선언 및 정의해준다
    {
        return Coin >= coin; // 변수 Coin의 값이 매개변수 coin의 값보다 크거나 같다면 반환해준다
    }
}
