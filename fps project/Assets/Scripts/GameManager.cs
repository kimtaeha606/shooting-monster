using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MonsterSpawner monsterSpawner;
    private void Awake()
    {
        monsterSpawner.Spawn();

    }
}
