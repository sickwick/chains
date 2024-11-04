using Chains.Chain;

namespace Cheins.borshcht;

public class MakeARoastChain : ChainBase<BorshchContext>
{
    public override BorshchContext Handle(BorshchContext context)
    {
        Console.WriteLine("Making a roast chain");
        return context;
    }
}