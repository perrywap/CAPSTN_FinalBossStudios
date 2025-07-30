using UnityEngine;

public class TowerHitboxRelay : MonoBehaviour
{
    [SerializeField] private Tower targetTower;

    public Tower GetTower()
    {
        return targetTower;
    }
}