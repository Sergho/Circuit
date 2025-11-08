namespace circuit;

public interface IComponent
{
    double Value { get; }

    int GetPriority();
    bool IsDisplacing();
    StateType GetStateType();
    bool IsExternal();
}
