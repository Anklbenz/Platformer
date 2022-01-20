namespace Character
{
    public class InRowCounter
    {
        public int Count { get; private set; } = 0;

        public void Reset() {
            Count = 0;
        }

        public void Inreace() {
            Count++;
        }    
    }
}