public class KillTileBehavior : TileBehavior
{
    public override void Execute(Character character, Tile tile)
    {
        if (character) character.Die();
    }
}
