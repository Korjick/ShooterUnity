using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Enemy curEnemy = Instantiate(enemy, transform);
            curEnemy.transform.localPosition = new Vector3(Random.Range(-36, -51) * 1.0f, 13.5f, Random.Range(-18, 5) * 1.0f);
            curEnemy.player = player;
            yield return new WaitForSeconds(1f);
        }
    }
}
