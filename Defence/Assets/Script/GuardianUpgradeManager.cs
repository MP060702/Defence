using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuardianUpgradeManager : MonoBehaviour
{
    public GuardianStatus[] GuardianStatuses; // GuardianStatus�� ��� �迭 �������� ���׷���Ʈ �ϱ����� ��ũ���ͺ� ������Ʈ�� ��� ��Ȱ�� �Ѵ�

    public Image AttackRangeImg; // ���� ������ �����ִ� ��Ȱ�� �ϴ� image�μ� attackRadius�κ��� ������ ���� �����ָ� �����ش�
    public Button UpgradeIconButton; // Guardain�� ���׷��̵� �� �� ����ϴ� ��ư���� EventSystem�� ���� ���� �Ŵ��� ������Ʈ�� �ִ� GuardianUpgradeManager �Լ��� �����Ͽ� ����� �� �ֵ��� �ϴ� ��Ȱ�� ����

    private Guardian _currentUpgradeGuardian; // ���� ���׷��̵� �� Guardian�� �������ִ� ��Ȱ�� �ϴ� ���� ������Ʈ��

    public bool bIsUpgrading = false; // Guardian�� ���׷��̵� �Ҷ� ����ϴ� ���� 
    private bool _isOnButtonHover = false;

    public void Start() // ���� �����Ҷ� �۵��Ǵ� Start �Լ�
    {
        ShowUpgradeIconAndRange(false); // ShowUpgradelconAndRange�Լ��� ���� �Ű����� active�� false�� ȣ��
        GameManager.Inst.guardianBuildManager.OnBuild.AddListener(() => ShowUpgradeIconAndRange(false)); // GameManager�� �̺�Ʈ OnBuild�� ShowUpgradeIcaonAndRange�Լ��� �Ű������� �԰� ���� �־��ش�
    }

    private void Update() // �������Ӹ��� �۵��Ǵ� Update �Լ�
    {
        UpdateKeyInput(); // UpdateKeyInput �Լ� ȣ��
    }

    public void UpgradeGuardian(Guardian guardian) // UpgradeGuardian �Լ� ���� �� ����
    {
        ShowUpgradeIconAndRange(true); // ShowUprageIconAndRange�Լ��� ���� �Ű����� active�� true�� �ϰ� ȣ��
        _currentUpgradeGuardian = guardian; // _currentUpgradeGuardain�� �� �Լ��� �Ű������� guardian�� ���� �������

        Vector3 guardianPos = _currentUpgradeGuardian.transform.position; // Vector3 guardianPos�� ���� �� ���� _currentUpgradeGuardian������Ʈ �� �ֱ� ���׷��̵�� ������� ��ġ���� �޾ƿ��� ���� ���
        Vector3 attackImgPos = Camera.main.WorldToScreenPoint(guardianPos); // Vector attackImagePOs�� ���� �� ���� Camera�� WordToScreenPoint �Լ��� ���� World�� �ִ� ������� ��ǥ�� Canvas�ȿ� �ִ� attackImg�� �־��ش�

        float attackRadius = (_currentUpgradeGuardian.GuardianStatus.AttackRadius) + 1.5f; // �Ǽ��� ���� attackRadius�� ���� �� ����, �ֱ� ���׷��̵�� ������� �������� ���ݹ��� 1.5f ����ŭ ������ �� ���ݹ����� ���׷��̵� ���ִ� ��Ȱ�� ��
        AttackRangeImg.rectTransform.localScale = new Vector3(attackRadius, attackRadius, 1); // AttackRangeImg�� �������� ���Ӱ� ������ attackRadius�� ���� �־��ش�,  ���Ӱ� ���ǵ� ���� ������ �����ִ� ��Ȱ�� ��
        AttackRangeImg.rectTransform.position = attackImgPos; // AttackRangeImg�� ������ ���� attackingPos���� ���� ���ش�

        UpgradeIconButton.transform.localScale = new Vector3(1 / attackRadius, 1 / attackRadius, 1); // ���׷��̵� �������� ��ġ�� ���׷��̵�� ���� ������ ���ο� ��ġ������ �ٲ��ش�.
        UpgradeIconButton.onClick.AddListener(() => Upgrade(_currentUpgradeGuardian)); // ���۷��̵� ��ư�� ���������� Event�� Update���Լ��� �Ű������� �־��� ��ư�� ������ �� ����ɶ� �ش� �Լ��� ����ǵ��� �Ѵ�.
        bIsUpgrading = true; // ���׷��̵� �Ѵٴ� ������ ������ ���ش�
    }

    public void ShowUpgradeIconAndRange(bool active) // ShowUpgradeIcon�Լ��� ���� �� ����
    {
        AttackRangeImg.gameObject.SetActive(active); // ���ݹ����� �����ִ� ������Ʈ�� Ȱ��ȭ
        UpgradeIconButton.gameObject.SetActive(active); // ���׷��̵� ��ư�� ������Ʈ�� Ȱ��ȭ
    }

    private void Upgrade(Guardian guardian) // Upgrade �Լ��� ���� �� ����
    {
        if (guardian.Level < GuardianStatuses.Length - 1) // ������� ������ ������� ���� �迭�� ���̿��� - 1�� ������ �۴ٸ� �۵��Ǵ� ���ǹ�
        {
            PlayerCharacter player = GameManager.Inst.playerCharacter; // �÷��̾� character Ŭ������ ���ӸŴ��� playerCharacter������ ����
            int cost = GuardianStatuses[guardian.Level + 1].UpgradeCost; // ������ �Ű����� cost�� ���� �G ����, GuardianStatuses�迭�� guardian.Level + 1 ������ �迭���� Upgrade�ڽ�Ʈ ���� ����־ �������ִ� ��Ȱ

            if (player.CanUseCoin(cost)) //�÷��̾� �Լ����� cost��ŭ�� ������ ����ϴ� �Լ��� �Ű������� ���� �����Ѵٸ�
            {
                player.UseCoin(cost); // �ش� �ڽ�Ʈ ����ŭ�� �Һ��Ѵ�
                guardian.Upgrade(GuardianStatuses[guardian.Level + 1]); // ������� ������ Upgrade�Լ����� �迭�� ���� �߰��Ͽ� ���׷��̵� ���ش�
                bIsUpgrading = false; // ���׷��̵����̶�� ������ �������� ���ش�
                ShowUpgradeIconAndRange(false); // �ش� �Լ��� �Ű������� �������� ���ش�
            }
        }
    }

    public void OnPointerEnter() // ��ư ���� �ٸ� ���� �������� UI�� �ٿ��� ������ true�� ���ִ� �Լ�
    {
        _isOnButtonHover = true; // ��ư ȣ���� ������ ���ش�
    }
    public void OnPointerExit() //��ư ���� �ٸ� ���� �������� UI�� �ٿ��� ������ false�� ���ִ� �Լ�
    {
        _isOnButtonHover = false; // ��ưȣ���� �������� ���ش� 
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
