using UnityEngine;

namespace StatisticSystem
{
    class ShotStatistic : Statistic
    {
        Time TimeOfShot;
        Vector2 DistanceOfCenter;//x+y
        EFailShot? FailShot;
    }
}