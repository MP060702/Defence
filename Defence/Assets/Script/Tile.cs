using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] // �ν����Ϳ��� �Ʒ� �������� ������ �ʰ� �ϱ� ���� ���
    public Guardian OwnGuardian; //Guardian Ŭ������ ���� OwnGuardian�� ���� 

    public bool CheckIsOwned() // ���� �Լ� CheckisOwned ���� �G ����
    {
        return OwnGuardian != null; // OwnGardian Ŭ������ null�� �ƴ϶�� ��ȯ
    }

    public void ClearOwned() // �Լ� ClearOwned ���� �G ����
    {
        OwnGuardian = null; // OwnGuardian�� null�� ����־���
    }

    public void RemoveOwned() // �Լ� RemoveOwned ���� �G ����
    {
        Destroy(OwnGuardian); // OwnGuardian�� ����
        OwnGuardian = null; // OwnGuardian�� null ���·� �������
    }
}
