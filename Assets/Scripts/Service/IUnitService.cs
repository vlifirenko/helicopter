using Apache.View;

namespace Apache.Service
{
    public interface IUnitService
    {
        public int CreateUnit(AUnitView view, bool isPlayer);
    }
}