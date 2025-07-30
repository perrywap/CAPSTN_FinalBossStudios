using System.Collections.Generic;
using UnityEngine;

public class CustomTrigger : MonoBehaviour
{
    public event System.Action<Tower> EnteredTrigger;
    public event System.Action<Tower> ExitTrigger;

    private readonly List<Tower> towersInRange = new List<Tower>();

    public List<Tower> GetCurrentTowers() => towersInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tower tower = collision.GetComponent<Tower>();
        if (tower == null)
        {
            TowerHitboxRelay relay = collision.GetComponent<TowerHitboxRelay>();
            if (relay != null)
                tower = relay.GetTower();
        }

        if (tower != null && !towersInRange.Contains(tower))
        {
            towersInRange.Add(tower);
            EnteredTrigger?.Invoke(tower);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Tower tower = collision.GetComponent<Tower>();
        if (tower == null)
        {
            TowerHitboxRelay relay = collision.GetComponent<TowerHitboxRelay>();
            if (relay != null)
                tower = relay.GetTower();
        }

        if (tower != null)
        {
            towersInRange.Remove(tower);
            ExitTrigger?.Invoke(tower);
        }
    }

    public void RemoveNullTowers()
    {
        towersInRange.RemoveAll(t => t == null);
    }
}
