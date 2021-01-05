using JetBrains.Annotations;

namespace Npgsql.StorageFacade.Common.Options
{
    public class StorageFacadeOptions
    {
        public string ConnectionString { get; [UsedImplicitly]set; } = null!;
    }
}