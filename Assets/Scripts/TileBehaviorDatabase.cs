using System;

// TODO: this might be nicer using ScriptableObjects but I hate them
public static class TileBehaviorDatabase
{
    private static TileBehaviorFactoryBase[,] _TileBehaviorFactories = new TileBehaviorFactoryBase[Enum.GetNames(typeof(TileType)).Length, Enum.GetNames(typeof(TileEvent)).Length];
    
    public static void Init()
    {
        _TileBehaviorFactories[(int)TileType.Lava, (int)TileEvent.OnEnter] = new TileBehaviorFactory<KillTileBehavior>();
        _TileBehaviorFactories[(int)TileType.Wall, (int)TileEvent.OnDesire] = new TileBehaviorFactory<StopTileBehavior>();
    }

    public static TileBehavior CreateTileBehavior(TileType tileType, TileEvent tileEvent)
    {
        TileBehaviorFactoryBase factory = _TileBehaviorFactories[(int)tileType, (int)tileEvent];

        if (factory != null)
        {
            return factory.CreateTileBehavior();
        }

        return null;
    }

}
