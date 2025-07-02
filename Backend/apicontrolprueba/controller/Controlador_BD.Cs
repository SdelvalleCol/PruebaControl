using MySqlConnector;
using System.Data;
using DotNetEnv;

public static class Controlador_BD
{
    // Campo de conexión (debe ser static)
    private static readonly string connectionString;

    // Constructor estático (se ejecuta una sola vez)
    static Controlador_BD()
    {
        // Cargar variables del .env
        Env.Load();

        var host = Environment.GetEnvironmentVariable("HOST");
        var user = Environment.GetEnvironmentVariable("USER");
        var password = Environment.GetEnvironmentVariable("PASSWORD");
        var database = Environment.GetEnvironmentVariable("DATABASE");

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(user) ||
            string.IsNullOrWhiteSpace(database))
        {
            throw new Exception("Faltan variables de entorno (HOST, USER, PASSWORD o DATABASE).");
        }

        connectionString = $"Server={host};User={user};Password={password};Database={database};Allow User Variables=true";
    }


    public static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }

    public static DataTable ExecuteQuery(string query)
    {
        using var connection = GetConnection();
        using var command = new MySqlCommand(query, connection);
        using var adapter = new MySqlDataAdapter(command);

        var dataTable = new DataTable();
        adapter.Fill(dataTable);
        return dataTable;
    }

    public static int ExecuteNonQuery(string sql, IDictionary<string, object> parameters)
    {
        using var conn = GetConnection();
        using var cmd = new MySqlCommand(sql, conn);

        foreach (var kv in parameters)
        {
            cmd.Parameters.AddWithValue(kv.Key, kv.Value ?? DBNull.Value);
        }

        return cmd.ExecuteNonQuery();
    }
}
