using System;

public interface IPerformable
{
    public bool Performed { get; }

    public event Action OnPerformed;

}
