using System.Collections.Generic;
using Apache.Ecs.Component.Unit;
using Apache.Model;
using Apache.View;
using Leopotam.EcsLite;

namespace Apache.Service
{
    public class UnitService : IUnitService
    {
        private readonly EcsWorld _world;

        public UnitService(EcsWorld world)
        {
            _world = world;
        }

        public int CreateUnit(AUnitView view, bool isPlayer)
        {
            var entity = _world.NewEntity();

            _world.GetPool<UnitComponent>().Add(entity) = new UnitComponent
            {
                View = view,
                Config = view.Config
            };
            _world.GetPool<MovementComponent>().Add(entity);
            _world.GetPool<RotationComponent>().Add(entity);
            _world.GetPool<TargetsComponent>().Add(entity) = new TargetsComponent
            {
                Targets = new Dictionary<AUnitView, Target>()
            };

            if (isPlayer)
                _world.GetPool<PlayerComponent>().Add(entity);

            return entity;
        }
    }
}