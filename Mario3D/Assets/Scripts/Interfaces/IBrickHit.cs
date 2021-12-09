interface IBrickHit
{
    bool BrickInHitState { get; set; }
    void BrickHit(Character character);
}