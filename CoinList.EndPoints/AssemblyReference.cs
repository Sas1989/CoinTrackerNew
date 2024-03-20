using System.Reflection;

namespace CoinList.EndPoints;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
