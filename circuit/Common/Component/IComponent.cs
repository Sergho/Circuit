namespace circuit;

public interface IComponent
{
    IState? State { get; }
    double Value { get; }

    int GetPriority();
    bool IsDisplacing();
    bool IsExternal();
}
