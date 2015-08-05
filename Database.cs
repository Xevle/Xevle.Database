using System.Collections.Generic;
using System.Data;
using System;

namespace Xevle.Database
{
	public abstract class Database
	{
		#region Properties
		public bool Connected { get; protected set; }
		#endregion

		#region Abstract methods
		#region Connection
		public abstract bool Connect();

		public abstract void Disconnect();
		#endregion

		#region Queries
		public abstract int ExecuteNonQuery(string sqlCommand);

		public abstract DataTable ExecuteQuery(string sqlCommand);
		#endregion

		#region Edits
		public abstract void InsertData(DataTable insertData);

		public abstract void UpdateData(DataTable updateData, string primaryKey);

		public abstract void RemoveData(string table, string key, string value);
		#endregion

		#region Transactions
		public abstract void StartTransaction();

		public abstract void CommitTransaction();
		#endregion

		#region Tables
		public abstract void CreateTable(DataTable table);

		public void CreateTables(List<DataTable> tables)
		{
			foreach (DataTable i in tables)
			{
				CreateTable(i);
			}
		}

		public abstract void RemoveTable(string tableName);

		public void RemoveTables(List<string> tables)
		{
			foreach (string table in tables)
			{
				RemoveTable(table);
			}
		}

		public abstract List<string> GetTables();

		public DataTable GetTable(string tableName)
		{
			string sqlCommand = String.Format("SELECT * FROM \"{0}\";", tableName);
			DataTable ret = ExecuteQuery(sqlCommand);
			ret.TableName = tableName;
			return ret;
		}

		public abstract DataTable GetTableStructure(string tableName);

		public bool ExistsTable(string tableName)
		{
			List<string> tables = GetTables();
			if (tables.IndexOf(tableName) == -1) return false;
			return true;
		}
		#endregion

		#region Misc methods
		public abstract string GetDatabaseSystemDataType(string datatype);
		#endregion
		#endregion

	}
}
