using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WaveInfo", menuName = "Scriptable Object/WaveInfo")]

public class WaveInfo : ScriptableObject
{
    public int wavenum = 0;
    public int EnemyCount = 0;
    public int UpgradedEnmeyCount = 0;
}

public class EnemySpawner : MonoBehaviour //MomoBehaviou�� ��ӹ޴� Ŭ���� EnmeySpanwer
{   
    
    public Transform SpawnPosition; // Enemy ������Ʈ�� ������ ��ġ�� ���ϴ� Transform Ÿ���� ����
    public GameObject[] WayPoints;  // ������ �ٲ��ً� ���Ǵ� ���ӿ�����Ʈ�μ� ���ӿ�����Ʈ Ÿ���� �迭
    public WaveInfo[] WaveInfo;
    public GameObject Enemy;
    public GameObject UpgradedEnemy;
    public Text WaveNum;
    public float SpawnCycleTime = 1f; //�Ǽ� Ÿ���� ���� ��ȯ ����Ŭ�� ������ �ð��� ����ϴ� ����
    private int waveIndex;

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
        while (waveIndex < WaveInfo.Length) // _bCanSpawn ������ True�� ���� �۵��ϴ� while �ݺ���,  ���� ������ ���������� Enemy�� �������ֱ� ���� �ݺ��ϴ� ��Ȱ�� �Ѵ�
        {
            WaveInfo currentWave = WaveInfo[waveIndex];
            
            for (int i = 0; i < currentWave.EnemyCount; i++)
            {
                yield return new WaitForSeconds(SpawnCycleTime);
                
                 
                GameObject EnemyInst = Instantiate(Enemy, SpawnPosition.position, Quaternion.identity);

                Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();

                if (EnemyCom)
                {
                    EnemyCom.WayPoints = WayPoints;
                }
            }

            for(int j = 0; j < currentWave.UpgradedEnmeyCount; j++)
            {
                yield return new WaitForSeconds(SpawnCycleTime);


                GameObject EnemyInst = Instantiate(UpgradedEnemy, SpawnPosition.position, Quaternion.identity);

                Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();

                if (EnemyCom)
                {
                    EnemyCom.WayPoints = WayPoints;
                }
            }

            yield return new WaitForSeconds(4f);
            WaveNum.text = $"Wave {waveIndex + 2}";
            WaveNum.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);
            WaveNum.gameObject.SetActive(false);

            waveIndex++;
        }
    }


}