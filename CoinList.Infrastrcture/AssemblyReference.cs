using System.Reflection;

namespace CoinList.Infrastrcture;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
