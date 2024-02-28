using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // MonoBehaviour�� ��ӹ޴� Enemy Ŭ����
{
    private GameObject _currentWayPoint; // GameObject Ÿ���� ������ ����, ������ ���� ����Ʈ�μ��� ��Ȱ�� �Ѵ�
    private int _wayPointCount = 0; // int Ÿ���� ������ ���� �� ����,  ��������Ʈ�� ������ ���� ��Ȱ�� �Ѵ�.
    private Vector3 _moveDirection = Vector3.zero; // Vector3 Ÿ���� ������ ���� �� ����,  �ش� Ŭ������ ���� �ִ� ������Ʈ�� �̵����� ��Ȱ�� �ϴ� ����
    private int _hp = 5; // int Ÿ���� ������ ���� �G ����, �ش� Enemy�� ü�°� ��Ȱ�� �Ѵ�

    [HideInInspector] // �ν����� â���� �������� ������ �ʰ� �ϱ� ���� ���
    public GameObject[] WayPoints; // ������ �ٲ��ً� ���Ǵ� ���ӿ�����Ʈ�μ� ���ӿ�����Ʈ Ÿ���� �迭
    public int MaxHp = 5; // Enemy�� �ִ� ü�°����μ��� ��Ȱ�� �ϴ� int Ÿ���� ����
    public float MoveSpeed = 10; // Enemy�� �̵��ӵ� ���� ��Ȱ�� �ϴ� float Ÿ���� ����
    public int StealCoin = 100; // Enemy�� óġ ������ ��� ������ �� ��Ȱ�� �ϴ� int Ÿ���� ������ ���� �G ����
    public int Damage = 1; // Enemy�� �ִ� ������ ���� ��Ȱ�� �ϴ� int Ÿ���� ���� 

    private void Start() // ���� ���ۉ����� �۵��Ǵ� �Լ�
    {
        _hp = MaxHp; // ü�°��� �ִ� ü�°��� �־��ش�
        _currentWayPoint = WayPoints[0]; // ���� ���� ����Ʈ ���� WayPoints �迭�� 0��° ���� �־��ش�
        SetRotationByDirection(); // SetRotationByDirection �Լ��� ȣ���Ѵ�
    }

    private void Update() // �� �����Ӹ��� �۵��Ǵ� �Լ�
    {
        transform.position += _moveDirection * MoveSpeed * Time.deltaTime; // ������Ʈ�� ��ġ���� (�̵����� * �̵��ӵ� * �ð�)���� ������ ������Ʈ�� ������ �� �ֵ��� ���ش�
        Vector3 TargetPosition = _currentWayPoint.transform.position; // Vector3 ������ ���� �������� ��ġ���� �ִ´�
        TargetPosition.y = transform.position.y; // TargetPostion.y ���� ���� Enemy Ŭ���� ������Ʈ�� ��ġ y���� �ִ´�

        if (Vector3.Distance(transform.position, TargetPosition) <= 0.02f) // Vector3.Distance�� ���� ���� ������Ʈ�� ��ġ����, TargetPosition�� ��ġ���� �Ÿ��� ����Ͽ� 0.02f���� �۰ų� ���ٸ� �Ʒ� �ڵ带 �۵�
        {
            if (_wayPointCount >= WayPoints.Length - 1) // ���� wayPointCount ������ ��� ����, WayPoints �迭�� ���� ���� -1�� ���� ������ ���� ũ�ų� ���ٸ� �Ʒ� �ڵ带 �۵�
            {
                GameManager.Inst.playerCharacter.Damaged(Damage);
                Destroy(gameObject); // �ڱ� �ڽ� ������Ʈ�� ����
                return; // ��ȯ�Ѵ�
            }

            _wayPointCount = Mathf.Clamp(_wayPointCount + 1, 0, WayPoints.Length); //������ ī��Ʈ ���� Mathf.Clamp �Լ��� ���� ������ ���� 1�� �����ְ� �ּҰ��� 0���� �ִ��� WayPoints�迭�� ���̰����� �Ͽ� �ش� �� ���� ����� �ʵ��� ��
            _currentWayPoint = WayPoints[_wayPointCount]; // ���� ������ ���� WayPoints�迭�� _WayPointCount���� ĭ �迭�� ���� ����

            SetRotationByDirection(); // SetRotationByDirection �Լ��� ȣ���Ѵ�
        }
    }

    private void SetRotationByDirection() // SetRotationByDirection �Լ��� ���� �G ����,  �ش� �Լ��� 
    {
        _moveDirection = _currentWayPoint.transform.position - transform.position; // �̵������� ����������� ��ġ������ - �ڽ��� ��ġ���� �������� �ϸ� �ڱ� �ڽ��� �̵���Ų��
        _moveDirection.y = 0; // _moveDirection�� y���� 0���� ���ش�
        _moveDirection.Normalize(); // ���� Normalize �Լ��� ���� ����ȭ �����ش�

        transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up); // ���� ������Ʈ�� ȸ������ LookRotation�Լ��� ���� �Ű����� _moveDirection�� ȸ����ų ���� ���ͷ� �ϰ� ���� ���͸� �ٶ󺸰� ���� ���� ���� �����Ѵ�
    }

    private void OnTriggerEnter(Collider other) // Trigger�� ������� �۵��Ǵ� �Լ� 
    {
        if (other.CompareTag("Projectile")) // ���� "Projectile"�±��� collider�� ��Ҵٸ� �Ʒ� �ڵ� �۵�
        {
            _hp = Mathf.Clamp(_hp - 1, 0, MaxHp); // ü�°��� �ּҰ� 0�� �ִ� �ִ�ü�°��� ����� �ʴ� ������ ü���� -1 �Ѵ�

            if (_hp <= 0) // ���� ü���� 0���� �۰ų� ���ٸ�
            {
                gameObject.SetActive(false); // ���� ������Ʈ�� SetActive(false)�Ͽ� ������ ������ �ʵ��� �ϰ� �۵��� �����
                GameManager.Inst.EnemyDead(StealCoin); // ���� �޴������� EnemyDead�Լ��� �۵���Ű�� �Ű�������� stealCoin���� ��´� 
                Destroy(gameObject); //�ش� ������Ʈ�� ����
            }
        }
    }
}