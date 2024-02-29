using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuardianUpgradeManager : MonoBehaviour
{
    public GuardianStatus[] GuardianStatuses; // GuardianStatus를 담는 배열 가디언들을 업그레이트 하기위해 스크립터블 오브젝트를 담는 역활을 한다

    public Image AttackRangeImg; // 공격 범위를 보여주는 역활을 하는 image로서 attackRadius로부터 스케일 값을 넓혀주며 보여준다
    public Button UpgradeIconButton; // Guardain을 업그레이드 할 떄 사용하는 버튼으로 EventSystem을 통해 게임 매니저 오브젝트에 있는 GuardianUpgradeManager 함수에 접근하여 사용할 수 있도록 하는 역활을 ㅎ마

    private Guardian _currentUpgradeGuardian; // 현재 업그레이드 된 Guardian을 저장해주는 역활을 하는 게임 오브젝트다

    public bool bIsUpgrading = false; // Guardian을 업그레이드 할때 사용하는 논리식 
    private bool _isOnButtonHover = false;

    public void Start() // 씬을 시작할때 작동되는 Start 함수
    {
        ShowUpgradeIconAndRange(false); // ShowUpgradelconAndRange함수와 논리형 매개변수 active를 false로 호출
        GameManager.Inst.guardianBuildManager.OnBuild.AddListener(() => ShowUpgradeIconAndRange(false)); // GameManager의 이벤트 OnBuild에 ShowUpgradeIcaonAndRange함수를 매개변수와 함게 값을 넣어준다
    }

    private void Update() // 매프레임마다 작동되는 Update 함수
    {
        UpdateKeyInput(); // UpdateKeyInput 함수 호출
    }

    public void UpgradeGuardian(Guardian guardian) // UpgradeGuardian 함수 선언 및 정의
    {
        ShowUpgradeIconAndRange(true); // ShowUprageIconAndRange함수를 논리형 매개변수 active를 true로 하고 호출
        _currentUpgradeGuardian = guardian; // _currentUpgradeGuardain에 이 함수의 매개변수인 guardian의 값을 집어넣음

        Vector3 guardianPos = _currentUpgradeGuardian.transform.position; // Vector3 guardianPos를 선언 및 정의 _currentUpgradeGuardian오브젝트 즉 최근 업그레이드된 가디언의 위치값을 받아오기 위해 사용
        Vector3 attackImgPos = Camera.main.WorldToScreenPoint(guardianPos); // Vector attackImagePOs를 선언 및 정의 Camera의 WordToScreenPoint 함수를 통해 World에 있는 가디언의 좌표를 Canvas안에 있는 attackImg에 넣어준다

        float attackRadius = (_currentUpgradeGuardian.GuardianStatus.AttackRadius) + 1.5f; // 실수형 변수 attackRadius를 선억 및 정의, 최근 업그레이드된 가디언의 정보에서 공격범위 1.5f 값만큼 더해줘 즉 공격범위를 업그레이드 해주는 역활을 함
        AttackRangeImg.rectTransform.localScale = new Vector3(attackRadius, attackRadius, 1); // AttackRangeImg에 스케일을 새롭게 정해진 attackRadius의 값을 넣어준다,  새롭게 정의된 공격 범위를 보여주는 역활을 함
        AttackRangeImg.rectTransform.position = attackImgPos; // AttackRangeImg의 포지션 값을 attackingPos값과 같게 해준다

        UpgradeIconButton.transform.localScale = new Vector3(1 / attackRadius, 1 / attackRadius, 1); // 업그레이드 아이콘의 위치를 업그레이드된 공격 범위의 새로운 위치값으로 바꿔준다.
        UpgradeIconButton.onClick.AddListener(() => Upgrade(_currentUpgradeGuardian)); // 업글레이드 버튼을 눌렀을때의 Event에 Updateㅇ함수와 매개변수를 넣어줘 버튼을 눌렀을 때 실행될때 해당 함수가 실행되도록 한다.
        bIsUpgrading = true; // 업그레이드 한다는 논리식을 참으로 해준다
    }

    public void ShowUpgradeIconAndRange(bool active) // ShowUpgradeIcon함수를 선언 및 정의
    {
        AttackRangeImg.gameObject.SetActive(active); // 공격범위를 보여주는 오브젝트를 활성화
        UpgradeIconButton.gameObject.SetActive(active); // 업그레이드 버튼인 오브젝트를 활성화
    }

    private void Upgrade(Guardian guardian) // Upgrade 함수를 선언 및 정의
    {
        if (guardian.Level < GuardianStatuses.Length - 1) // 가디언의 레벨이 가디언의 상태 배열의 길이에서 - 1한 값보다 작다면 작동되는 조건문
        {
            PlayerCharacter player = GameManager.Inst.playerCharacter; // 플레이어 character 클래스에 게임매니저 playerCharacter변수를 넣음
            int cost = GuardianStatuses[guardian.Level + 1].UpgradeCost; // 정수형 매개변수 cost를 선언 밎 정의, GuardianStatuses배열의 guardian.Level + 1 숫자의 배열값에 Upgrade코스트 값을 집어넣어서 저장해주는 역활

            if (player.CanUseCoin(cost)) //플레이어 함수에서 cost만큼의 코인을 사용하는 함수의 매개변수의 값을 충족한다면
            {
                player.UseCoin(cost); // 해당 코스트 값만큼을 소비한다
                guardian.Upgrade(GuardianStatuses[guardian.Level + 1]); // 가디언의 레벨을 Upgrade함수에서 배열의 값을 추가하여 업그레이드 해준다
                bIsUpgrading = false; // 업그레이드중이라는 논리형을 거짓으로 해준다
                ShowUpgradeIconAndRange(false); // 해당 함수의 매개변수를 거짓으로 해준다
            }
        }
    }

    public void OnPointerEnter() // 버튼 외의 다른 곳을 누렀을때 UI를 뛰울지 말지를 true로 해주는 함수
    {
        _isOnButtonHover = true; // 버튼 호버를 참으로 해준다
    }
    public void OnPointerExit() //버튼 외의 다른 곳을 누렀을때 UI를 뛰울지 말지를 false로 해주는 함수
    {
        _isOnButtonHover = false; // 버튼호버를 거짓으로 해준다 
    }

    private void UpdateKeyInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isOnButtonHover)
            {
                return;
            }

            bIsUpgrading = false;
            ShowUpgradeIconAndRange(false);
        }
    }
}
