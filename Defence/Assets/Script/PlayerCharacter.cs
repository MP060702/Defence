using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour // MonoBehaviour�� ��ӹ޴� Ŭ���� 
{
    public int Coin = 100; // int Ÿ���� ���� coin ����
    private int _heart = 10; // int Ÿ���� ���� _heart ����,  ü�°� ��Ȱ�� ��

    public int Heart //int Ÿ���� ���� Heart ���� �� ����
    {
        get { return _heart; }
    }

    public int MaxHeart = 10; //int Ÿ���� ���� MaxHeart�� ���� �� ����, �ִ� ü�°��� ��Ȱ�� ��

    void Start() //���� �����Ҷ� �۵��Ǵ� �Լ�
    {
        _heart = MaxHeart; // ü�°��� �ִ�ü�°��� �������
    }

    public void Damaged(int damage) // �Լ� Damaged ���� �� ���� 
    {
        _heart -= damage; // ���� _heart�� ���� �Űܺ��� damage�� ��ŭ�� ���ش�
        if (_heart <= 0)  // ���� �÷��̾��� ü���� 0���ϰų� ���ٸ� �Ʒ� �ڵ� ����
        {
            GameManager.Inst.GameDefeat(); // GameManager�� �Լ��� GameDefeat�� ȣ��
        }
        Debug.Log(_heart);
    }
    public void UseCoin(int coin) // �Լ� UseCoin ���� �� ����
    {
        Coin = Mathf.Clamp(Coin - coin, 0, int.MaxValue); // ���� Coin�� �ּڰ� 0, �ִ� MAxValue �� �̻� ���ϰ� ���� �ʵ��� �ϴ� ������ Coin������ �Ű����� coin���� ������ �־��ش�
    }

    public bool CanUseCoin(int coin) // ���� �Լ� CanUseCoin�� ���� �� �������ش�
    {
        return Coin >= coin; // ���� Coin�� ���� �Ű����� coin�� ������ ũ�ų� ���ٸ� ��ȯ���ش�
    }
}
