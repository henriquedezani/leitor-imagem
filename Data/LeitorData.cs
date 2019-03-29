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
           conn = new NpgsqlConnection(@"Server=ec2-184-73-153-64.compute-1.amazonaws.com;
                            User Id=lewzwsulfsxjpk; 
                            Password=86ca6f04167a8036e53b8fab435339cb5e497c00fdf520294d0dd68084d8448d;
                            Database=da2ne88b6nh58i;
                            sslmode=Prefer;Trust Server Certificate=true;");

            // conn = new NpgsqlConnection(@"Host=ec2-184-73-153-64.compute-1.amazonaws.com;Database=da2ne88b6nh58i;sslmode=Prefer;Trust Server Certificate=true;Username=lewzwsulfsxjpk;Password=86ca6f04167a8036e53b8fab435339cb5e497c00fdf520294d0dd68084d8448d");
          conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public List<Erro> Read()
        {
            List<Erro> erros = new List<Erro>();

            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM listaerros", conn);

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

        public List<Erro> Read(String[] palavras)
        {
            List<Erro> erros = new List<Erro>();

            string search = "";

            foreach(var palavra in palavras) {
                search = search + "%" + palavra + "%";
            }

            string sql = "SELECT * FROM listaerros WHERE titulo iLIKE '" + search + "'";

            NpgsqlCommand command = new NpgsqlCommand(sql, conn);

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