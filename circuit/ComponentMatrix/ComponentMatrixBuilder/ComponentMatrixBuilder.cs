namespace circuit;

public class ComponentMatrixBuilder : IComponentMatrixBuilder
{
    public ComponentMatrixBuilder() { }

    public IComponentMatrix BuildMatrix(ISchema schema)
    {
        var matrix = new ComponentMatrix();
        ISchema tree = schema.GetTree();
        ISchema addition = schema.GetDiff(tree);
        var additionEdges = addition.GetEdges(edge => edge.Component.State?.Type != StateType.Voltage);

        foreach (IEdge additionEdge in additionEdges)
        {
            foreach (IEdge edge in tree.GetEdges())
            {
                ISchema loopSchema = (ISchema)tree.Clone();
                loopSchema.AddEdge(additionEdge);
                ILoop? loop = loopSchema.GetLoop();
                if (loop == null)
                {
                    matrix.SetElem(additionEdge.Component, edge.Component, MatrixCell.Zero);
                    continue;
                }

                Direction? direction = loop.CompareDirections(additionEdge, edge);

                if (direction == null) matrix.SetElem(additionEdge.Component, edge.Component, MatrixCell.Zero);
                if (direction == Direction.Forward) matrix.SetElem(additionEdge.Component, edge.Component, MatrixCell.Positive);
                if (direction == Direction.Backward) matrix.SetElem(additionEdge.Component, edge.Component, MatrixCell.Negative);
            }
        }

        return matrix;
    }
}
