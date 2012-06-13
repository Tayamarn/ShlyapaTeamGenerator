using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Randomizer
{
	static class Model
	{
        public static MySortableBindingList<Player> Players = new MySortableBindingList<Player>();
		//public static List<Player> Players = new List<Player>();
		public static List<Group> Groups = new List<Group>();
        static string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\MainDB.sdf";

		public static void AddPlayer(Player player)
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				INSERT INTO [PlayersData] 
				VALUES (
				{0},
				'{1}',
				{2},
                {3},
                {4})			
			", player.Id, player.Name,player.Score, player.AssignedGroupNo, player.Position);
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

		public static void Select()
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
			connection.ConnectionString = "Data Source=" + dbfile;
			command.Connection = connection;
			command.Connection.Open();
			command.Parameters.Clear();
			command.CommandText = String.Format(@"
				SELECT
				[Id],
				[Name],
				[Score],
                [GroupNo],
                [Position]
				FROM [PlayersData]
				ORDER BY [Id]				
			");


			IDataReader reader = command.ExecuteReader();
			Players.Clear();

			while (reader.Read())
			{
                Player p = new Player
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Score = (int)reader["Score"],
                    Position = reader["Position"] == DBNull.Value ? 0 : (int)reader["Position"]
                };
                p.AssignGroup(reader["GroupNo"] == DBNull.Value ? 0 : (int)reader["GroupNo"]);
				Players.Add(p);
			}
			command.Connection.Close();
		}

		public static void Delete(int id)
		{
			IDbCommand command = new SqlCeCommand();
			IDbConnection connection = new SqlCeConnection();
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

//        public static void AssignGroup(this Player player, int group_id)
//        {
//            IDbCommand command = new SqlCeCommand();
//            IDbConnection connection = new SqlCeConnection();
//            connection.ConnectionString = "Data Source=" + dbfile;
//            command.Connection = connection;
//            command.Connection.Open();
//            command.Parameters.Clear();
//            command.CommandText = String.Format(@"
//				INSERT INTO [Player_Group] 
//				VALUES (
//				{0},
//				'{1}')			
//			", player.Id, group_id);
//            command.ExecuteNonQuery();
//            Players.Clear();

//            command.Connection.Close();
//        }

	}



    public class MySortableBindingList<T> : BindingList<T>
    {

        // reference to the list provided at the time of instantiation
        List<T> originalList;
        ListSortDirection sortDirection;
        PropertyDescriptor sortProperty;

        // function that refereshes the contents
        // of the base classes collection of elements
        Action<MySortableBindingList<T>, List<T>>
                       populateBaseList = (a, b) => a.ResetItems(b);

        // a cache of functions that perform the sorting
        // for a given type, property, and sort direction
        static Dictionary<string, Func<List<T>, IEnumerable<T>>>
           cachedOrderByExpressions = new Dictionary<string, Func<List<T>,
                                                     IEnumerable<T>>>();

        public MySortableBindingList()
        {
            originalList = new List<T>();
        }

        public MySortableBindingList(IEnumerable<T> enumerable)
        {
            originalList = enumerable.ToList();
            populateBaseList(this, originalList);
        }

        public MySortableBindingList(List<T> list)
        {
            originalList = list;
            populateBaseList(this, originalList);
        }

        protected override void ApplySortCore(PropertyDescriptor prop,
                                ListSortDirection direction)
        {
            /*
             Look for an appropriate sort method in the cache if not found .
             Call CreateOrderByMethod to create one. 
             Apply it to the original list.
             Notify any bound controls that the sort has been applied.
             */

            sortProperty = prop;

            var orderByMethodName = sortDirection ==
                ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

            if (!cachedOrderByExpressions.ContainsKey(cacheKey))
            {
                CreateOrderByMethod(prop, orderByMethodName, cacheKey);
            }

            ResetItems(cachedOrderByExpressions[cacheKey](originalList).ToList());
            ResetBindings();
            sortDirection = sortDirection == ListSortDirection.Ascending ?
                            ListSortDirection.Descending : ListSortDirection.Ascending;
        }


        private void CreateOrderByMethod(PropertyDescriptor prop,
                     string orderByMethodName, string cacheKey)
        {

            /*
             Create a generic method implementation for IEnumerable<T>.
             Cache it.
            */

            var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
            var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda =
                Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter,
                                  accesedMember), lambdaParameter);
            var orderByMethod = typeof(Enumerable).GetMethods()
                                          .Where(a => a.Name == orderByMethodName &&
                                                       a.GetParameters().Length == 2)
                                          .Single()
                                          .MakeGenericMethod(typeof(T), prop.PropertyType);

            var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(
                                        Expression.Call(orderByMethod,
                                                new Expression[] { sourceParameter, 
                                                               propertySelectorLambda }),
                                                sourceParameter);

            cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
        }

        protected override void RemoveSortCore()
        {
            ResetItems(originalList);
        }

        private void ResetItems(List<T> items)
        {

            base.ClearItems();

            for (int i = 0; i < items.Count; i++)
            {
                base.InsertItem(i, items[i]);
            }
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                // indeed we do
                return true;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return sortProperty;
            }
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            originalList = base.Items.ToList();
        }
    }
}
