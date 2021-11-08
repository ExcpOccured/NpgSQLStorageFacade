namespace Npgsql.StorageFacade.Sdk.Options
{
    public interface IStorageFacadeOptions : IOptions
    {
        string ConnectionString { get; set; }
    }
}