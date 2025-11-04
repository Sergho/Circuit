namespace circuit;

internal interface IComponent
{
    double Value { get; }

    int GetPriority();
    bool IsDisplacing();
    StateType GetStateType();
    bool IsExternal();
}
