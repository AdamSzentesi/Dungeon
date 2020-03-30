public class Hero : Character
{
    protected override void OnDeath()
    {
        // TODO: this should be called from level logic
        Level.Instance.GameOver();
    }
}
