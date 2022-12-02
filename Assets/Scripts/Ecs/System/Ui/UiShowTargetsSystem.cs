using Apache.Ecs.Component.Unit;
using Apache.Model;
using Game.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Apache.Ecs.System.Ui
{
    public class UiShowTargetsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetsComponent>, Exc<DeadComponent>> _filter;
        private readonly EcsPoolInject<TargetsComponent> _pool;
        private readonly EcsCustomInject<SceneData> _sceneData;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var canvas = _sceneData.Value.CanvasView;
                var targets = _pool.Value.Get(entity);

                foreach (var (key, value) in targets.Targets)
                {
                    var position = key.Transform.position;
                    var uiPosition = canvas.Canvas.WorldToCanvasPosition(position, _sceneData.Value.MainCamera);
                    var uiView = value.uiView;

                    uiView.Transform.anchoredPosition = uiPosition;
                    uiView.Distance.text = $"{Mathf.CeilToInt(value.distance)} m";
                }
            }
        }
    }
}