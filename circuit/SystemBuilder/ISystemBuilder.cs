namespace circuit;

internal interface ISystemBuilder
{
    void Init();
    //ISystem GetSystem();
    ISystemMatrix GetMatrix();
}
