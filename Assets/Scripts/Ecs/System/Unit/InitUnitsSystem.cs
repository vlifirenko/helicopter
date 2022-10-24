using Apache.Model;
using Apache.Service;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Apache.Ecs.System.Unit
{
    public class InitUnitsSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<IUnitService> _unitService;

        public void Init(IEcsSystems systems)
        {
            _unitService.Value.CreateUnit(_sceneData.Value.Apache, true);
        }
    }
}