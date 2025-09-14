using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // íîâûé Input System
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI moneyPerClickText;
    [SerializeField] private TextMeshProUGUI moneyPerSecondText;

    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    void Start()
    {
        PlayerProfile.Instance.SetMoneyPerClick(1);
        PlayerProfile.Instance.SetMoneyPerSecond(2);
        PlayerProfile.Instance.SetMoneyPerHour(3);
    }

    void Update()
    {
        AddMoneyPerSecond();
        UpdateUI();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            if (!IsPointerOverUI(screenPos))
                OnClick();
        }
    }

    public void OnClick()
    {
        AddMoneyToBalance(PlayerProfile.Instance.GetMoneyPerClick());
    }

    private void AddMoneyToBalance(double amount)
    {
        PlayerProfile.Instance.SetBalance(PlayerProfile.Instance.GetBalance() + amount);
    }

    private void AddMoneyPerSecond()
    {
        double moneyToAdd = PlayerProfile.Instance.GetMoneyPerSecond() * Time.deltaTime;
        AddMoneyToBalance(moneyToAdd);
    }

    private void UpdateUI()
    {
        balanceText.text = $"Áàëàíñ: {PlayerProfile.Instance.GetBalance():F0}";
        moneyPerClickText.text = $"Çà êëèê: {PlayerProfile.Instance.GetMoneyPerClick():F0}";
        moneyPerSecondText.text = $"Â ñåêóíäó: {PlayerProfile.Instance.GetMoneyPerSecond():F2}";
    }

    private bool IsPointerOverUI(Vector2 screenPos)
    {
        if (uiRaycaster == null || eventSystem == null) return false;

        var pointerData = new PointerEventData(eventSystem)
        {
            position = screenPos
        };

        var results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);
        return results.Count > 0;
    }
}
