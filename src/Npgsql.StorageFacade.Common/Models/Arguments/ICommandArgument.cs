namespace Npgsql.StorageFacade.Common.Models.Arguments
{
    public interface ICommandArgument
    {
        string ArgumentVariableName { get; }

        object ArgumentVariableValue { get; }
    }
}