using System.Data;

namespace lib_data_core.Core
{
    public class Parameters
    {
        public string? Name;
        public SqlDbType? Type;
        public object? Value;
        public ParameterDirection? Direction;

        public Parameters(string? name, SqlDbType? type, object value, ParameterDirection? direction = ParameterDirection.Input)
        {
            Name = name;
            Type = type;
            Value = value;
            Direction = direction;
        }
    }
}