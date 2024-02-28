using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour //MomoBehaviou을 상속받는 클래스 EnmeySpanwer
{   
    public Transform SpawnPosition; // Enemy 오브젝트를 생성할 위치를 정하는 Transform 타입의 변수
    public GameObject[] WayPoints;  // 방향을 바꿔줄떄 사용되는 게임오브젝트로서 게임오브젝트 타입의 배열
    public GameObject EnemyPrefab;  // EnemySpawner가 소환시킬 Enemy 프리팹
    public float SpawnCycleTime = 1f; //실수 타입의 적의 소환 사이클의 딜레이 시간을 담당하는 변수

    private bool _bCanSpawn = true; // 논리형 타입의 변수로서 적을 소환할 것인지에 대한 참, 거짓 여부를 결정짔는 역활을 한다

    private void Start() // 씬이 시작될때 작동되는 함수
    {
        Activate(); // 함수 Active 호출 
    }

    public void Activate() // 함수 Active 선언 및 정의
    {
        StartCoroutine(SpawnEnemy()); //StartCoruntine 함수를 통한 SpawnEnemy 코루틴을 작동시킨다
    }

    public void DeActivate() // 함수 DeActive 선언 및 정의
    {
        StopCoroutine(SpawnEnemy()); // StopCoruntine 함수를 통해 작동중인 코루틴 SpawnEnemy를 중단시킴
    }

    IEnumerator SpawnEnemy() // SpawnEnemy 코루틴을 정의 및 선언
    {
        while (_bCanSpawn) // _bCanSpawn 논리형이 True일 때만 작동하는 while 반복문,  게임 내에서 지속적으로 Enemy를 스폰해주기 위해 반복하는 역활을 한다
        {
            yield return new WaitForSeconds(SpawnCycleTime); // WaitForSecond를 반환하여 가로 안에 있는 시간(초)가 지나면 해당 구문 및에 부분을 실행시킨다

            GameObject EnemyInst = Instantiate(EnemyPrefab, SpawnPosition.position, Quaternion.identity); // 게임 오브젝트 타입 EnemyInst를 선언 및 정의하여 Instatie함수를 통해 가로 안에있는 EnemyPrefab 오브젝트를
                                                                                                          // SpawnPosition의 위치에 동적으로 생성한다

            Enemy EnemyCom = EnemyInst.GetComponent<Enemy>(); // Enemy 클래스 타입의 EnemyCom을 선언 및 정의하여 게임오브젝트 EnemyInst에서 Enemy 컴퍼넌트를 찾는다

            if (EnemyCom) // 만약 EnemyCom이 존재한다면 if문 안에 있는 코드를 실행
            {
                EnemyCom.WayPoints = WayPoints;// GameObject EnemyPrefab안에 있는 Enemy 클래스 WayPoints 배열 변수에 EnemySpawner 클래스의 WayPoints 배열 값을 집어넣는다
            }
        }
    }

}