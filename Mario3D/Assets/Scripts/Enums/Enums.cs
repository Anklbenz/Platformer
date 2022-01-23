namespace Enums
{
    public enum BonusType
    {
        None = 0,
        Coin = 1,
        GrowUp = 2,
        Flower = 3,
        LifeUp = 4,
        JumpStar = 5,
        Walker = 6
    }

    public enum ExtraState
    {
        NormalState = 0,
        FlickerState = 1,
        UnstopState = 2
    }

    public enum PusherState
    {
        Cooldown = 0,
        Walk = 1,
        Engage = 2
    }

    public enum Direction
    {
        None,
        Left,
        Right,
    }

    public enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }
}