public class AllConitionOperator : IConditionOperator
{
    public bool Apply(SimpleCondition condition)
    {
        return condition.Performed;
    }

    public bool Apply(ComplexCondition complexCondition)
    {
        foreach (IPerformable condition in complexCondition.Conditions)
        {
            if (condition.Performed == false)
            {
                return false;
            }
        }
        return true;
    }
}
