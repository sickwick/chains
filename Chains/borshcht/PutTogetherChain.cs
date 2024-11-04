using Chains.Chain;

namespace Cheins.borshcht;

public class PutTogetherChain : ChainBase<BorshchContext>
{
    public override BorshchContext Handle(BorshchContext context)
    {
        Console.WriteLine("PutTogetherChain");
        return context;
    }
}