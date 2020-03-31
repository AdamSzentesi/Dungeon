public class StopTileBehavior : TileBehavior
{
    public override void Execute(Character character, Tile tile)
    {
        character.Stop();
    }
}
