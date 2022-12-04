namespace Hw10.DbModels;

public class ApplicationContext
{
    public List<SolvingExpression> SolvingExpressions { get; set; }

    public ApplicationContext()
    {
        SolvingExpressions = new List<SolvingExpression>();
    }

    public void Add(SolvingExpression data)
    {
        SolvingExpressions.Add(data);
    }
}