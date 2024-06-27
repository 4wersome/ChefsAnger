using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBarUpdate : MonoBehaviour
{
    private float maxHP;
    private float currentHP;
    public Image healthBar;
    private UnityAction<GlobalEventArgs> barUpdate;

     private void Awake()
    {
        barUpdate += HealthBarUpdated;
        GlobalEventManager.AddListener(GlobalEventIndex.PlayerHealthUpdated, barUpdate);
    }

    private void HealthBarUpdated(GlobalEventArgs message)
    {
        GlobalEventArgsFactory.PlayerHealthUpdatedParser(message, out maxHP, out currentHP);
        healthBar.fillAmount = currentHP / maxHP;
    }
    
    void Update()
    {
        
    }
}
