using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void OnEnterState(EnemyManager enemyManager);
    void OnUpdateState(EnemyManager enemyManager);
    void OnExitState(EnemyManager enemyManager);
}
