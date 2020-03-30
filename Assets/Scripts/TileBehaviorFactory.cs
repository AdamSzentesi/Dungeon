public abstract class TileBehaviorFactoryBase
{
    public abstract TileBehavior CreateTileBehavior();
}

public class TileBehaviorFactory<T> : TileBehaviorFactoryBase where T : TileBehavior, new()
{
    public override TileBehavior CreateTileBehavior()
    {
        return new T();
    }
}
