namespace circuit;

internal interface IComponent
{
    int GetPriority();
    bool IsDisplacing();
    StateType GetStateType();
    bool IsExternal();
}
