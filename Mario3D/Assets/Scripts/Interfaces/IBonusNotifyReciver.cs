interface  IBonusNotifyReciver 
{
    void BonusSpawn(IBonus bonus);
    void Subscribe(IBonus sender);
    void UnSubscribe(IBonus sender);
}
