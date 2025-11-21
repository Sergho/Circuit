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
                    matrix.SetElem(
                        new ComponentMatrixPair(additionEdge.Component, additionEdge.Current),
                        new ComponentMatrixPair(edge.Component, edge.Current),
                        MatrixCell.Zero
                    );
                    continue;
                }

                var rowPair = new ComponentMatrixPair(additionEdge.Component, additionEdge.Current);
                var colPair = new ComponentMatrixPair(edge.Component, edge.Current);
                Direction? direction = loop.CompareDirections(additionEdge, edge);

                if (direction == null) matrix.SetElem(rowPair, colPair, MatrixCell.Zero);
                if (direction == Direction.Forward) matrix.SetElem(rowPair, colPair, MatrixCell.Positive);
                if (direction == Direction.Backward) matrix.SetElem(rowPair, colPair, MatrixCell.Negative);
            }
        }

        return matrix;
    }
}
