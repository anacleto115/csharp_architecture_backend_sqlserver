using lib_data_core.Core;
using lib_domain_context;
using lib_utilities.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace lib_data_context.Core
{
    public class Repository<T>
    {
        protected IParser<T>? parser;
        protected IConnection? connection;

        protected void Load(Dictionary<string, object>? data)
        {
            if (data == null || !data.ContainsKey("Connection"))
                connection = new ConnectionSQL(data!["stringConnection"].ToString()!);
            else
                connection = (IConnection)data["Connection"];

            if (data.ContainsKey("KeepConnection"))
                data["Connection"] = connection;
        }

        public Dictionary<string, object>? Execute(Dictionary<string, object>? data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                data = data ?? new Dictionary<string, object>();
                Load(data);
                if (connection == null || parser == null)
                {
                    response["Error"] = "lbNoConfigured";
                    return response;
                }

                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();

                var command = connection.CreateCommand(data!["Procedure"].ToString()!);
                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                parameters.Add(new Parameters("create_by", SqlDbType.NVarChar, "Services"));
                parameters.Add(new Parameters("ip", SqlDbType.NVarChar, PCDataHelper.IpPc()));
                parameters.Add(new Parameters("result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (item.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter!);
                }

                var dataset = connection.ExecuteQuery(connection.CreateAdapter());

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item!.Value!;
                }

                var list = new List<T>();
                foreach (DataRow item in dataset!.Tables[0].Rows)
                    list.Add(parser.CreateEntity(item.ItemArray!)!);

                if (!data.ContainsKey("Close") || (bool)data["Close"])
                    connection.Close();

                response["Entities"] = list;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();

                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 10);
                //    return Execute(data, count++);
                //}

                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public Dictionary<string, object>? ExecuteNonQuery(Dictionary<string, object>? data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                data = data ?? new Dictionary<string, object>();
                Load(data);
                if (connection == null || parser == null)
                {
                    response["Error"] = "lbNoConfigured";
                    return response;
                }

                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();
                var command = connection.CreateCommand(data!["Procedure"].ToString()!);

                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                parameters.Add(new Parameters("create_by", SqlDbType.NVarChar, "Services"));
                parameters.Add(new Parameters("ip", SqlDbType.NVarChar, PCDataHelper.IpPc()));
                parameters.Add(new Parameters("result", SqlDbType.Int, 0, ParameterDirection.InputOutput));
                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (parameter!.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter);
                }

                connection.ExecuteNonQuery();

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item!.Value!;
                }

                if (!data.ContainsKey("Close") || (bool)data["Close"])
                    connection.Close();

                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();

                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 40);
                //    return ExecuteNonQuery(data, count++);
                //}

                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }

        public Dictionary<string, object>? DataTables(Dictionary<string, object>? data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                data = data ?? new Dictionary<string, object>();
                Load(data);
                if (connection == null || parser == null)
                {
                    response["Error"] = "lbNoConfigured";
                    return response;
                }

                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();
                var command = connection.CreateCommand(data!["Procedure"].ToString()!);

                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                parameters.Add(new Parameters("create_by", SqlDbType.NVarChar, "Services"));
                parameters.Add(new Parameters("ip", SqlDbType.NVarChar, PCDataHelper.IpPc()));
                parameters.Add(new Parameters("result", SqlDbType.Int, 0, ParameterDirection.InputOutput));
                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (item.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter!);
                }

                var dataset = connection.ExecuteQuery(connection.CreateAdapter());

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item.Value!;
                }

                response["DataTables"] = dataset!.Tables;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();

                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 10);
                //    return Execute(data, count++);
                //}

                response = ExceptionHelper.Convert(ex, response);
                return response;
            }
        }
    }
}