namespace Npgsql.StorageFacade.Sdk.Options
{
    public interface IOptions
    {
        bool Validate(out string errors)
        {
            errors = string.Empty;
            return true;
        }
    }
}