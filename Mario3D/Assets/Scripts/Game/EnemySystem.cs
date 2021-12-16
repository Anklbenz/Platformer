using System.Collections.Generic;
using UnityEngine;
public class EnemySystem
{
    List<ActiveEnemy> _enemys = new List<ActiveEnemy>();

    public void Attach(ActiveEnemy enemy) {
        _enemys.Add(enemy);
    }

    public void Detach(ActiveEnemy enemy) {
        _enemys.Remove(enemy);
    }

    public void EventNotifySubsribe() {
        foreach (var enemy in _enemys) {
            enemy.Subsribe();
        }
    }
}
