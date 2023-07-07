using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = System.Random;

namespace ControlStrategies
{
    public class ControlTypeWrapper : MonoBehaviour
    {
        public ControlType controlType;
        [SerializeField] private ControlStrategyAbstract standardFpControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalAutoControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalRandomControlStrategy;

        private void Start()
        {
            var controlTypes = new List<ControlType>
            {
                ControlType.StandardFpControlStrategy,
                ControlType.DirectionalControlStrategy,
                ControlType.DirectionalRandomControlStrategy,
            };
            controlType = controlTypes[new Random().Next(0, controlTypes.Count)];
        }

        public ControlStrategyAbstract GetControlStrategy()
            => controlType switch
            {
                ControlType.DirectionalControlStrategy => directionalControlStrategy,
                ControlType.StandardFpControlStrategy => standardFpControlStrategy,
                ControlType.DirectionalAutoControlStrategy => directionalAutoControlStrategy,
                ControlType.DirectionalRandomControlStrategy => directionalRandomControlStrategy,
                _ => standardFpControlStrategy
            };
    }
}