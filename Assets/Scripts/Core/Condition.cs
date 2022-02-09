using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure.Core
{
    [Serializable]
    public class Condition
    {
        [SerializeField] string predicate;
        [SerializeField] string[] parameters;
        [SerializeField] bool negate = false;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator evaluator in evaluators)
            {
                bool? result = evaluator.Evaluate(predicate, parameters);
                if (result == null)
                {
                    continue;
                }
                if (result == negate)
                {
                    return false;
                }
            }
            return true;
        }
    }
}