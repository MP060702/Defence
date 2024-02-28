using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour //MomoBehaviou�� ��ӹ޴� Ŭ���� EnmeySpanwer
{   
    public Transform SpawnPosition; // Enemy ������Ʈ�� ������ ��ġ�� ���ϴ� Transform Ÿ���� ����
    public GameObject[] WayPoints;  // ������ �ٲ��ً� ���Ǵ� ���ӿ�����Ʈ�μ� ���ӿ�����Ʈ Ÿ���� �迭
    public GameObject EnemyPrefab;  // EnemySpawner�� ��ȯ��ų Enemy ������
    public float SpawnCycleTime = 1f; //�Ǽ� Ÿ���� ���� ��ȯ ����Ŭ�� ������ �ð��� ����ϴ� ����

    private bool _bCanSpawn = true; // ���� Ÿ���� �����μ� ���� ��ȯ�� �������� ���� ��, ���� ���θ� �������� ��Ȱ�� �Ѵ�

    private void Start() // ���� ���۵ɶ� �۵��Ǵ� �Լ�
    {
        Activate(); // �Լ� Active ȣ�� 
    }

    public void Activate() // �Լ� Active ���� �� ����
    {
        StartCoroutine(SpawnEnemy()); //StartCoruntine �Լ��� ���� SpawnEnemy �ڷ�ƾ�� �۵���Ų��
    }

    public void DeActivate() // �Լ� DeActive ���� �� ����
    {
        StopCoroutine(SpawnEnemy()); // StopCoruntine �Լ��� ���� �۵����� �ڷ�ƾ SpawnEnemy�� �ߴܽ�Ŵ
    }

    IEnumerator SpawnEnemy() // SpawnEnemy �ڷ�ƾ�� ���� �� ����
    {
        while (_bCanSpawn) // _bCanSpawn ������ True�� ���� �۵��ϴ� while �ݺ���,  ���� ������ ���������� Enemy�� �������ֱ� ���� �ݺ��ϴ� ��Ȱ�� �Ѵ�
        {
            yield return new WaitForSeconds(SpawnCycleTime); // WaitForSecond�� ��ȯ�Ͽ� ���� �ȿ� �ִ� �ð�(��)�� ������ �ش� ���� �׿� �κ��� �����Ų��

            GameObject EnemyInst = Instantiate(EnemyPrefab, SpawnPosition.position, Quaternion.identity); // ���� ������Ʈ Ÿ�� EnemyInst�� ���� �� �����Ͽ� Instatie�Լ��� ���� ���� �ȿ��ִ� EnemyPrefab ������Ʈ��
                                                                                                          // SpawnPosition�� ��ġ�� �������� �����Ѵ�

            Enemy EnemyCom = EnemyInst.GetComponent<Enemy>(); // Enemy Ŭ���� Ÿ���� EnemyCom�� ���� �� �����Ͽ� ���ӿ�����Ʈ EnemyInst���� Enemy ���۳�Ʈ�� ã�´�

            if (EnemyCom) // ���� EnemyCom�� �����Ѵٸ� if�� �ȿ� �ִ� �ڵ带 ����
            {
                EnemyCom.WayPoints = WayPoints;// GameObject EnemyPrefab�ȿ� �ִ� Enemy Ŭ���� WayPoints �迭 ������ EnemySpawner Ŭ������ WayPoints �迭 ���� ����ִ´�
            }
        }
    }

}