public class MoneySystem
{
    public int Money { get; private set; }

    public void AddMoney(int value) =>
        Money += value;

    public void SpendMoney(int value) =>
        Money -= value;
}
