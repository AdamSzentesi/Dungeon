using System;

public static class TileBehaviorDatabase
{
    private static TileBehaviorFactoryBase[,] _TileBehaviorFactories = new TileBehaviorFactoryBase[Enum.GetNames(typeof(TileType)).Length, Enum.GetNames(typeof(TileEvent)).Length];
    
    public static void Init()
    {
        _TileBehaviorFactories[(int)TileType.Lava, (int)TileEvent.OnEnter] = new TileBehaviorFactory<KillTileBehavior>();
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
