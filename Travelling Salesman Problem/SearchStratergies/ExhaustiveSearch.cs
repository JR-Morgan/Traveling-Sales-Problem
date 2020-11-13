﻿
using System;
using System.Collections.Generic;
using TSP.SearchStratergies.LocalSearch.StepFunction;
using TSP.SearchStratergies.LocalSearch.StepFunctions;

namespace TSP.Solution_Stratergies
{
    public class ExhaustiveSearch : ISearchStrategy
    {
        public event ISearchStrategy.ItterationCompleteEventHandler? OnItterationComplete;
        public override string ToString() => "Exhaustive Search";


        IStepFunction step;

        public ExhaustiveSearch()
        {
            step = new LowestCost();
        }


        public Log Compute(Graph graph)
        {
            DateTime startTime = DateTime.Now;

            List<Route> routes = CalculateAllValidRoutes(graph);
            int routesEvaluated = 0;


            Route? bestRoute = default;
            foreach (Route route in routes)
            {
                routesEvaluated++;
                bestRoute = bestRoute == null? route : step.StepP(bestRoute, route);


                OnItterationComplete?.Invoke(this, new Log()
                {
                    timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds,
                    numberOfRoutesEvaluated = routesEvaluated,
                    itteration = routesEvaluated,
                    bestRouteCost = bestRoute != null ? bestRoute.Cost() : float.MaxValue,
                    bestRoute = bestRoute != null ? bestRoute.ToIdArray() : Array.Empty<int>(),
                });
            }

            return new Log()
            {
                timeToCompute = (float)DateTime.Now.Subtract(startTime).TotalMilliseconds,
                numberOfRoutesEvaluated = routesEvaluated,
                itteration = routesEvaluated,
                bestRouteCost = bestRoute != null ? bestRoute.Cost() : float.MaxValue,
                bestRoute = bestRoute != null ? bestRoute.ToIdArray() : Array.Empty<int>(),
            };

        }

        private static List<Route> CalculateAllValidRoutes(Graph graph)
        {
            List<Route> routes = new List<Route>();

            CalculateRoute(new Route(graph.StartNode, graph.nodes.Count), routes, graph.nodes);

            return routes;
        }

        
        private static void CalculateRoute(Route lastRoute, List<Route> routes, IEnumerable<Node> neighbours)
        {
            if (lastRoute.IsCompleted)
            {
                routes.Add(lastRoute);
            }
            else
            {
                foreach (Node node in neighbours)
                {
                    if (lastRoute.AddCheck(node))
                    {
                        Route currentRoute = lastRoute.Copy();
                        currentRoute.AddUnchecked(node);
                        CalculateRoute(currentRoute, routes, neighbours);
                    }

                }
            }

        }
    }
}
