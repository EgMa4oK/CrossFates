public interface IConditionOperator
{
    public bool Apply(SimpleCondition condition);
    public bool Apply(ComplexCondition condition);
}
