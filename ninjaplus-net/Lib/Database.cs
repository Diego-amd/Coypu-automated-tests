using System;
using System.IO;
using Npgsql;

namespace NinjaPlus.Lib
{
    public static class Database
    {
        //método separado para caso seja necessário mais métodos, já está pronto para uso
        private static NpgsqlConnection Connection()
        {
            var connectionString = "Host=pgdb;Username=postgres;Password=qaninja;Database=ninjaplus"; //string de conexão com o banco. Host é o nome do container
            var connection = new NpgsqlConnection(connectionString); //tipo de retorno, objeto NpgsqlConnection
            connection.Open(); //abre a conexão com o banco

            return connection;
        }

        public static void InsertMovies()
        {
            var dataSql = Environment.CurrentDirectory + "\\Data\\data.sql"; //caminho do arquivo de script do banco
            var query = File.ReadAllText(dataSql); //carregamento do arquivo SQL em string

            var command = new NpgsqlCommand(query, Connection());
            command.ExecuteReader(); //execução da query

            Connection().Close(); //fechamento da conexão
        }

        public static void RemoveByTitle(string title)
        {
            var query = $"DELETE FROM public.movies WHERE title = '{title}';";

            var command = new NpgsqlCommand(query, Connection());
            command.ExecuteReader(); //execução da query

            Connection().Close(); //fechamento da conexão
        }
    }
}