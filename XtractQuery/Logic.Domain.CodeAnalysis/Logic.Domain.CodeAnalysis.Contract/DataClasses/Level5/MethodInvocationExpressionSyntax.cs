namespace Logic.Domain.CodeAnalysis.Contract.DataClasses.Level5;

public class MethodInvocationExpressionSyntax : ExpressionSyntax
{
    public NameSyntax Name { get; private set; }
    public MethodInvocationMetadataSyntax? Metadata { get; private set; }
    public MethodInvocationParametersSyntax Parameters { get; private set; }

    public override SyntaxLocation Location => Name.Location;
    public override SyntaxSpan Span => new(Name.Span.Position, Parameters.Span.EndPosition);

    public MethodInvocationExpressionSyntax(NameSyntax name, MethodInvocationMetadataSyntax? metadata, MethodInvocationParametersSyntax parameters)
    {
        name.Parent = this;
        if (metadata != null)
            metadata.Parent = this;
        parameters.Parent = this;

        Name = name;
        Metadata = metadata;
        Parameters = parameters;

        Root.Update();
    }

    public void SetName(NameSyntax name, bool updatePosition = true)
    {
        name.Parent = this;

        Name = name;

        if (updatePosition)
            Root.Update();
    }

    public void SetMetadata(MethodInvocationMetadataSyntax? metadata, bool updatePosition = true)
    {
        if (metadata != null)
            metadata.Parent = this;
        Metadata = metadata;

        if (updatePosition)
            Root.Update();
    }

    public void SetParameters(MethodInvocationParametersSyntax parameters, bool updatePosition = true)
    {
        parameters.Parent = this;
        Parameters = parameters;

        if (updatePosition)
            Root.Update();
    }

    internal override int UpdatePosition(int position, ref int line, ref int column)
    {
        position = Name.UpdatePosition(position, ref line, ref column);
        if (Metadata != null)
            position = Metadata.UpdatePosition(position, ref line, ref column);
        position = Parameters.UpdatePosition(position, ref line, ref column);

        return position;
    }
}