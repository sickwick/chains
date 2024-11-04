using Chains.Chain;

namespace Cheins.borshcht;

public class ChainAssembler
{
    private readonly StepDefiner<BorshchContext> _stepDefiner;
    public ChainAssembler()
    {
        _stepDefiner = new StepDefiner<BorshchContext>();
    }
    
    public void MakeBorshch()
    {
        var isLast = false;
        var ctx = new BorshchContext();
        
        while (!isLast)
        {
            var (step, isLastElement) = _stepDefiner.GetNextStep();
            if (isLastElement)
            {
                isLast = isLastElement;
                continue;
            }
            
            ctx = step.Handle(ctx);
        }
    }
}

public class StepDefiner<TContext>
where TContext : ContextBase
{
    private readonly ChainBuilder _builder;
    private List<ChainOrder> _steps;
    

    public StepDefiner()
    {
        _builder = new ChainBuilder();
        Init();
    }

    private int CurrentPosition { get; set; } = -1;

    private void Init()
    {
        _steps = _builder.AddStep(new ChainOrder(){ ChainName = "meat", StepNumber = 0})
            .AddStep(new ChainOrder(){ ChainName = "collect", StepNumber = 2})
            .AddStep(new ChainOrder(){ ChainName = "roast", StepNumber = 1})
            .Build();
    }
    
    public (ChainBase<TContext>?, bool) GetNextStep()
    {
        var nextStepOrder = CurrentPosition + 1;
        if (_steps.Count <= nextStepOrder)
        {
            return (null, true);
        }
        
        var nextStep = _steps.FirstOrDefault(c => c.StepNumber == nextStepOrder);
        
        var next =  StepAccessor<TContext>.Steps[nextStep.ChainName];

        if (next == null)
        {
            throw new Exception();
        }

        CurrentPosition++;

        return (next, false);
    }
}

public struct ChainOrder
{
    public string ChainName { get; set; }

    public int StepNumber { get; set; }
}

public class ChainBuilder
{
    private HashSet<ChainOrder> Steps { get; } = new();

    public ChainBuilder AddStep(ChainOrder order)
    {
        Steps.Add(order);

        return this;
    }

    public List<ChainOrder> Build()
    {
        return Steps.OrderBy(c => c.StepNumber).ToList();
    }
}

public static class StepAccessor<TContext>
where TContext : ContextBase
{
    public static readonly Dictionary<string, ChainBase<TContext>> Steps = new()
    {
        { "meat", new CookMeatChain() as ChainBase<TContext> },
        { "roast", new MakeARoastChain() as ChainBase<TContext>},
        { "collect", new PutTogetherChain() as ChainBase<TContext>}
    };
}