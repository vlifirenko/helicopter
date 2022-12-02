using System.Collections.Generic;
using Apache.Model;
using Apache.View;

namespace Apache.Ecs.Component.Unit
{
    public struct TargetsComponent
    {
        public Dictionary<AUnitView, Target> Targets;
    }
}