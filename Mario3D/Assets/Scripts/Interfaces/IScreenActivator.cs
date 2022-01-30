
namespace Interfaces
{
    public interface IScreenActivator
    {
        bool Activated{ get; set; }
        void Standby();
        void Activate();
       
    }
}
