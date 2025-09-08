using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // новый Input System
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI moneyPerClickText;
    [SerializeField] private TextMeshProUGUI moneyPerSecondText;

    [SerializeField] private PlayerProfile playerProfile;

    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    void Start()
    {
        playerProfile.SetMoneyPerClick(1);
        playerProfile.SetMoneyPerSecond(2);
        playerProfile.SetMoneyPerHour(3);
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
        AddMoneyToBalance(playerProfile.GetMoneyPerClick());
    }

    private void AddMoneyToBalance(double amount)
    {
        playerProfile.SetBalance(playerProfile.GetBalance() + amount);
    }

    private void AddMoneyPerSecond()
    {
        double moneyToAdd = playerProfile.GetMoneyPerSecond() * Time.deltaTime;
        AddMoneyToBalance(moneyToAdd);
    }

    private void UpdateUI()
    {
        balanceText.text = $"Баланс: {playerProfile.GetBalance():F0}";
        moneyPerClickText.text = $"За клик: {playerProfile.GetMoneyPerClick():F0}";
        moneyPerSecondText.text = $"В секунду: {playerProfile.GetMoneyPerSecond():F2}";
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
