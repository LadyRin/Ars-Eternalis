using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HudManager : MonoBehaviour
{
    [SerializeField] private UIDocument hud;
    private VisualElement root;
    private ProgressBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadHealthBar();
    }

    public void SetHealth(float vie)
    {
        if (healthBar == null) LoadHealthBar();
        healthBar.value = vie;
    }

    public void SetMaxHealth(float vieMax)
    {
        if (healthBar == null) LoadHealthBar();
        healthBar.highValue = vieMax;
    }

    private void LoadHealthBar()
    {
        if (healthBar != null) return;
        
        root = hud.rootVisualElement;
        healthBar = root.Q<ProgressBar>("health-bar");
        Debug.Log(healthBar.name);
        healthBar.lowValue = 0;
    }
}
