using UnityEngine;

public enum InputAction
{
    ChaneTileType,
}

public static class InputManager
{
    public static Vector2 MouseScreenPosition
    {
        get
        {
            return Input.mousePosition;
        }
    }

    public static bool GetActionUp(InputAction inputAction)
    {
        return Input.GetMouseButtonUp(0);
    }

}
