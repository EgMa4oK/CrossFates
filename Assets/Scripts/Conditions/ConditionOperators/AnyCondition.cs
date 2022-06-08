public class AnyConditionOperator : IConditionOperator
{
    public bool Apply(SimpleCondition condition)
    {
        return condition.Performed;
    }

    public bool Apply(ComplexCondition complexCondition)
    {
        foreach (IPerformable condition in complexCondition.Conditions)
        {
            if (condition.Performed)
            {
                return true;
            }
        }
        return false;
    }
}
