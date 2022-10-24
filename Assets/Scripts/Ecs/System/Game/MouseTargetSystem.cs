using Apache.Ecs.Component.Unit;
using Apache.Model;
using Apache.Model.Config;
using Apache.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Apache.Ecs.System.Game
{
    public class MouseTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<TargetComponent> _targetPool;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<CommonConfig> _commonConfig;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var ray = _sceneData.Value.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _commonConfig.Value.enemyLayer))
                    TakeAim(hit.transform);
                else
                    RemoveAim();
            }
        }

        private void TakeAim(UnityEngine.Component hit)
        {
            foreach (var entity in _filter.Value)
            {
                var view = hit.GetComponent<AUnitView>();
                var target = new TargetComponent
                {
                    View = view
                };

                if (!_targetPool.Value.Has(entity))
                    _targetPool.Value.Add(entity) = target;
                else
                    _targetPool.Value.Get(entity) = target;
            }
        }

        private void RemoveAim()
        {
            foreach (var entity in _filter.Value)
            {
                if (_targetPool.Value.Has(entity))
                    _targetPool.Value.Del(entity);
            }
        }
    }
}