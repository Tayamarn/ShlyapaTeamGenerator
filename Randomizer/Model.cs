using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Randomizer
{
	static class Model
	{
		public static List<Player> Players = new List<Player>();
		public static List<Group> Groups = new List<Group>();

		public static void AddPlayer(Player player)
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\MainDB.sdf";
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				INSERT INTO [PlayersData] 
				VALUES (
				{0},
				'{1}',
				{2}	)			
			", player.Id, player.Name,player.Score);
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

		public static void Select()
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\MainDB.sdf";
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				SELECT
				[Id],
				[Name],
				[Score]
				FROM [PlayersData]
				ORDER BY [Id]				
			");


			IDataReader reader = command.ExecuteReader();
			Players.Clear();

			while (reader.Read())
			{
				Players.Add(new Player {Id = (int) reader["Id"], Name = (string) reader["Name"], Score = (int) reader["Score"]});
			}
			command.Connection.Close();
		}

		public static void Delete(int id)
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\MainDB.sdf";
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				DELETE FROM [PlayersData]	
				WHERE [Id] = {0}", id);


			command.ExecuteNonQuery();
			Players.Clear();

			command.Connection.Close();
		}

		public static void DeleteByRow(DataGridViewRow row)
		{
			Delete((int)row.Cells["Id"].Value);
		}

		public static void UpdateScore(int id, int score)
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\MainDB.sdf";
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				UPDATE [PlayersData]
				SET [Score]	= {1}
				WHERE [Id] = {0}", id, score);


			command.ExecuteNonQuery();
			Players.Clear();

			command.Connection.Close();
		}

	}
}
