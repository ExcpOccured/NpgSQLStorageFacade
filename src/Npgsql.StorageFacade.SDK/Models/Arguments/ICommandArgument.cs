namespace Npgsql.StorageFacade.Sdk.Models.Arguments
{
    public interface ICommandArgument
    {
        string ArgumentVariableName { get; }

        object ArgumentVariableValue { get; }
    }
}