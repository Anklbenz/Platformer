public interface IMoveProvider
{
    bool MovingUp { get; }
    bool MovingDown { get; }
    bool IsGrounded { get; }
    void Bounce();
}