
namespace Interfaces
{
    public interface IFirstScreenActivate
    {
        bool Activated{ get; set; }
        void Standby();
        void Activate();
       }
}
