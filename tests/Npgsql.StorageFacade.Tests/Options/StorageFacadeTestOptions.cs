using Npgsql.StorageFacade.Sdk.Options;
using Npgsql.StorageFacade.Tests.Attributes;

namespace Npgsql.StorageFacade.Tests.Options
{
    [ConfigSection("StorageFacade")]
    public class StorageFacadeTestOptions : StorageFacadeOptions { }
}