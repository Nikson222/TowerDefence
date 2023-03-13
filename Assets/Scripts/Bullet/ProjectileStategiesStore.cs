using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ProjectileStrategies
{
    public enum ProjectileStrategy
    {
        StraightMovement
    }


    public static class ProjectileStategiesStore
    {
        static public Dictionary<ProjectileStrategy, ProjectileMovementStrategy> ProjectileStrateries = new Dictionary<ProjectileStrategy, ProjectileMovementStrategy>()
        {
            { ProjectileStrategy.StraightMovement, new StraightProjectileMovementStrategy() }
        };
    }
}


