﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travling_sales_problem.SearchStratergies.LocalSearch.StepFunction
{
    static class BestNeighbourhoodStep
    {
        
        public static (Route, float) Step(IEnumerable<Route> neighbourhood)
        {
            Route? bestRoute = null;
            float bestDistance = float.MaxValue;
            foreach (Route route in neighbourhood)
            {
                float distance = route.EvaluateDistance();
                if (distance < bestDistance)
                {
                    bestRoute = route;
                    bestDistance = distance;
                }
            }
            return (bestRoute ?? throw new InvalidEnumArgumentException("Argument was empty"), bestDistance);
        }
    }
}