namespace circuit;

public interface ISystemBuilder
{
    void Init();
    ISystem GetSystem();
}
