using System.Collections.Generic;
using System.Linq;
using Apache.Ecs.Component.Unit;
using Apache.Model;
using Apache.Model.Config;
using Apache.Service;
using Apache.View;
using Apache.View.Ui;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace Apache.Ecs.System.Unit
{
    public class LookingTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsPoolInject<UnitComponent> _unitPool;
        private readonly EcsPoolInject<TargetsComponent> _targetsPool;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<IAudioService> _audioService;
        private readonly EcsCustomInject<AudioConfig> _audioConfig;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                var config = unit.Config;
                ref var targets = ref _targetsPool.Value.Get(entity);
                var position = unit.View.Transform.position;
                var radius = config.lookingTargetDistance;

                var colliders = Physics.SphereCastAll(position, radius, unit.View.Transform.forward, 0f);
                var delItems = targets.Targets.ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value);

                foreach (var collider in colliders)
                {
                    if (collider.transform == unit.View.Transform)
                        continue;

                    if (collider.transform.TryGetComponent<AUnitView>(out var unitView))
                    {
                        var distance = Vector3.Distance(position, unitView.Transform.position);
                        if (!targets.Targets.ContainsKey(unitView))
                        {
                            // add
                            var view = CreateUiTargetView();
                            var target = new Target
                            {
                                distance = distance,
                                uiView = view
                            };
                            view.Show();
                            targets.Targets.Add(unitView, target);
                            _audioService.Value.PlayOneShot(_audioConfig.Value.targetFound);
                        }
                        else
                            // update
                            targets.Targets[unitView].distance = distance;

                        if (delItems.ContainsKey(unitView))
                            delItems.Remove(unitView);
                    }
                }

                foreach (var (key, value) in delItems)
                {
                    Object.Destroy(targets.Targets[key].uiView.GameObject);
                    targets.Targets.Remove(key);
                }
            }
        }

        private UiTarget CreateUiTargetView()
        {
            var canvas = _sceneData.Value.CanvasView;
            return Object.Instantiate(canvas.Targets.Prefab, canvas.Targets.Transform);
        }
    }
}