namespace circuit.Tests;

public class ComponentTests
{
    public static IEnumerable<object[]> GetPriorityTestData()
    {
        yield return new object[] { new List<IComponent> { new Resistor(0), new Capacitor(0), new PowerSource(0) } };
        yield return new object[] { new List<IComponent> { new Resistor(0), new Inductance(0), new PowerSource(0) } };
    }
    public static IEnumerable<object[]> IsDisplacingTestData()
    {
        yield return new object[] { new Resistor(0), false };
        yield return new object[] { new Capacitor(0), true };
        yield return new object[] { new Inductance(0), true };
        yield return new object[] { new PowerSource(0), true };
    }
    public static IEnumerable<object[]> GetStateTypeTestData()
    {
        yield return new object[] { new Resistor(0), StateType.None };
        yield return new object[] { new Capacitor(0), StateType.Voltage };
        yield return new object[] { new Inductance(0), StateType.Current };
        yield return new object[] { new PowerSource(0), StateType.None };
    }
    public static IEnumerable<object[]> IsExternalTestData()
    {
        yield return new object[] { new Resistor(0), false };
        yield return new object[] { new Capacitor(0), false };
        yield return new object[] { new Inductance(0), false };
        yield return new object[] { new PowerSource(0), true };
    }

    [Theory]
    [MemberData(nameof(GetPriorityTestData))]
    public void GetPriority_HaveCorrectOrder(IEnumerable<IComponent> components)
    {
        int lastPriority = 0;

        foreach(IComponent component in components)
        {
            Assert.True(component.GetPriority() > lastPriority);
            lastPriority = component.GetPriority();
        }
    }

    [Theory]
    [MemberData(nameof(IsDisplacingTestData))]
    public void IsDisplacing_ReturnsCorrectResult(IComponent component, bool isDisplacing)
    {
        Assert.Equal(component.IsDisplacing(), isDisplacing);
    }

    [Theory]
    [MemberData(nameof(GetStateTypeTestData))]
    public void GetStateType_ReturnsCorrectResult(IComponent component, StateType type)
    {
        Assert.Equal(component.GetStateType(), type);
    }

    [Theory]
    [MemberData(nameof(IsExternalTestData))]
    public void IsExternal_ReturnsCorrectResult(IComponent component, bool isExternal)
    {
        Assert.Equal(component.IsExternal(), isExternal);
    }
}
