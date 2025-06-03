namespace Assets.Scripts
{
    public interface HealthListener
    {
        public void OnHealthChanged(int healthPoints);
        public void SetMaxHealth(int maxHealth);
    }
}
