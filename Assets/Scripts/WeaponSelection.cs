public class WeaponSelection
{
    public string CurrentWeaponName { get; private set; }

    public void AddCurrentWeaponName(string currentWeaponName)
    {
        CurrentWeaponName = currentWeaponName;
    }
}
