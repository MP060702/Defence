using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] // 인스펙터에서 아래 변수들이 보이지 않게 하기 위해 사용
    public Guardian OwnGuardian; //Guardian 클래스의 변수 OwnGuardian을 선언 

    public bool CheckIsOwned() // 논리형 함수 CheckisOwned 선언 밎 정의
    {
        return OwnGuardian != null; // OwnGardian 클래스가 null이 아니라면 반환
    }

    public void ClearOwned() // 함수 ClearOwned 선언 밎 정의
    {
        OwnGuardian = null; // OwnGuardian에 null을 집어넣어줌
    }

    public void RemoveOwned() // 함수 RemoveOwned 선언 밎 정의
    {
        Destroy(OwnGuardian); // OwnGuardian을 삭제
        OwnGuardian = null; // OwnGuardian을 null 상태로 만들어줌
    }
}
