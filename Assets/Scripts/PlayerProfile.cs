public class PlayerProfile
{
    private static PlayerProfile _instance;
    public static PlayerProfile Instance => _instance ??= new PlayerProfile();

    public string playerName;
    private double balance;
    private double moneyPerClick;
    private double moneyPerSecond;
    private double moneyPerHour;

    public double GetBalance() => balance;
    public double GetMoneyPerClick() => moneyPerClick;
    public double GetMoneyPerSecond() => moneyPerSecond;
    public double GetMoneyPerHour() => moneyPerHour;

    public void SetBalance(double value)
    { 
        balance = value;
    }
    public void SetMoneyPerClick(double value)
    {
        moneyPerClick = value;
    }

    public void SetMoneyPerSecond(double value)
    {
        moneyPerSecond = value;
    }

    public void SetMoneyPerHour(double value)
    {
        moneyPerHour = value;
    }
}
