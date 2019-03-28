using System;
using System.Collections.Generic;
using LeitorImagem.Models;
using Npgsql;

namespace LeitorImagem.Data
{
    public class LeitorData : IDisposable
    {
        NpgsqlConnection conn = null;

        public LeitorData()
        {
            // Connect to a PostgreSQL database
           conn = new NpgsqlConnection(@"Server=127.0.0.1;User Id=postgres; 
                            Password=pwd;Database=postgres;");
          conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public List<Erro> Read(String[] palavras)
        {
            List<Erro> erros = new List<Erro>();

            string search = "";

            foreach(var palavra in palavras) {
                search = search + " %" + palavra + "%";
            }

            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM listaerros WHERE titulo iLIKE '" + search + "'", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader reader = command.ExecuteReader();
 
            // Output rows
            while (reader.Read())
            {
                Erro erro = new Erro();
                erro.Id = reader.GetInt32(0);
                erro.Titulo = reader.GetString(1);
                erro.Solucao = reader.GetString(2);
                erro.ImagemUrl = reader.GetString(3);

                erros.Add(erro);
            }

            return erros;
        }
    }
}