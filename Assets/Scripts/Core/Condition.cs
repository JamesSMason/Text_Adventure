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
        [SerializeField] bool negate;

        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator evaluator in evaluators)
            {
                bool? result = evaluator.Evaluate(predicate, parameters);
                if (result == null)
                {
                    continue;
                }
                if (result == false)
                {
                    return negate ? true : false;
                }
            }
            return negate ? false : true;
        }
    }
}