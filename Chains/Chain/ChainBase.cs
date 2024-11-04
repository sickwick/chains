namespace Chains.Chain;

public abstract class ChainBase<TResult>
where TResult : ContextBase
{
    public abstract TResult Handle(TResult context);
}