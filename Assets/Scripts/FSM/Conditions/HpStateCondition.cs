using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpStateCondition : Condition {
    private COMPARISON comparison;
    private HealthModule healthModule;
    private float hpPercentage;

    public HpStateCondition(COMPARISON comparison, HealthModule healthModule, float hpPercentage) {
        this.comparison = comparison;
        this.healthModule = healthModule;
        this.hpPercentage = Math.Clamp(hpPercentage, 0f, 1f);
    }

    public override bool Validate() {
        return base.Validate();
    }

    private bool InternalHpCompare() {
        switch(comparison) {
            case COMPARISON.EQUAL:
                return healthModule.CurrentHP == healthModule.MaxHP * hpPercentage;
            case COMPARISON.GREATER:
                return healthModule.CurrentHP > healthModule.MaxHP * hpPercentage;;
            case COMPARISON.GREATEREQUAL:
                return healthModule.CurrentHP >= healthModule.MaxHP * hpPercentage;;
            case COMPARISON.LESS:
                return healthModule.CurrentHP <= healthModule.MaxHP * hpPercentage;;
            case COMPARISON.LESSEQUAL:
                return healthModule.CurrentHP <= healthModule.MaxHP * hpPercentage;;
            default:
                return false;
        }
    }
}
