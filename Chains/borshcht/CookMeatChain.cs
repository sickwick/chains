using Chains.Chain;

namespace Cheins.borshcht;

public class CookMeatChain : ChainBase<BorshchContext>
{
    public override BorshchContext Handle(BorshchContext context)
    {
        Console.WriteLine("Cooking meat");

        return context;
    }
}