public interface IFryable
{
    void Fry();
    bool GiveHeat(float damage, out float health);
    void ReturnHealth();
}