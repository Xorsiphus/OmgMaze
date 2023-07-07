using Enums;
using UnityEngine;

namespace ControlStrategies
{
    public class ControlTypeWrapper : MonoBehaviour
    {
        public ControlType controlType;
        [SerializeField] private ControlStrategyAbstract standardFpControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalAutoControlStrategy;
        [SerializeField] private ControlStrategyAbstract directionalRandomControlStrategy;


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