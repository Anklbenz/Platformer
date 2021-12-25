namespace MyEnums
{
    public enum BonusType
    {
        none = 0,
        coin = 1,
        growUp = 2,
        flower = 3,
        lifesUp = 4,
        jumpStar = 5,
        walker = 6
    }

    public enum PusherState
    {
        Cooldown = 0,
        Walk = 1,
        Engage = 2
    }

    public enum Direction
    {
        none,
        left,
        rigth,
    }

    public enum Axis
    {
        horisontal = 0,
        vertical = 1
    }
}