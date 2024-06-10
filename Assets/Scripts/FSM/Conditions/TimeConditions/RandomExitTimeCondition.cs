using UnityEngine;

public class RandomExitTimeCondition : Condition
{

    private float minTimeToWait;
    private float maxTimeToWait;

    private float onEnterTime;
    private float currentTimeToWait;

    public RandomExitTimeCondition (float minTimeToWait, float maxTimeToWait) {
        this.minTimeToWait = minTimeToWait;
        this.maxTimeToWait = maxTimeToWait;
    }

    public override void OnEnter() {
        onEnterTime = Time.time;
        currentTimeToWait = Random.Range(minTimeToWait, maxTimeToWait);
    }

    public override bool Validate() {
        return Time.time - onEnterTime >= currentTimeToWait;
    }

}
