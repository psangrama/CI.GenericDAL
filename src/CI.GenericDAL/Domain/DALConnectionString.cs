namespace CI.GenericDAL.Domain
{
	public class DALConnectionString
	{
		/// <summary>
		/// Server instance name
		/// </summary>
		protected string Server { get; set; }

		/// <summary>
		/// Name of the default database
		/// </summary>
		protected string Database { get; set; }

		/// <summary>
		/// Credential user id (if needed by the server)
		/// </summary>
		protected string UserID { get; set; }

		/// <summary>
		/// Get/set the password (if needed by the server)
		/// </summary>
		protected string Password { get; set; }

		/// <summary>
		/// Get/set the name of this object (use this name to retrieve it later)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Flag to validate connection string parameters for the connection string are valid
		/// </summary>
		private bool IsValid
		{
			get
			{
				bool hasServer = !string.IsNullOrEmpty(this.Server);
				bool hasDB = !string.IsNullOrEmpty(this.Database);
				bool hasUid = !string.IsNullOrEmpty(this.UserID);
				bool hasPwd = !string.IsNullOrEmpty(this.Password);

				bool isValid = (hasServer && hasDB);
				isValid &= ((!hasUid && !hasPwd) || (hasUid && hasPwd));

				return isValid;
			}
		}

		/// <summary>
		/// Get the connection string. 
		/// </summary>
		public string ConnectionString
		{
			get
			{
				string value = string.Empty;
				if (this.IsValid)
				{
					// Check for windows auth or sql auth
					var dbCred = string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(Password) ? " Integrated Security = true; " : $"User ID = {UserID}; Password = {Password}";
					value = $"data source={Server}; initial catalog={Database}; {dbCred}";
						
				}
				else
				{
					throw new InvalidOperationException("One or more required connection string parameters were not specified.");
				}
				return value;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Name">The connection string name (I've been using the name of the database)</param>
		/// <param name="Server">The server instance</param>
		/// <param name="Database">The initial database to connec to</param>
		/// <param name="UserID">The user id credential</param>
		/// <param name="Password">The password credential</param>
		public DALConnectionString(string Name, string Server, string Database, string UserID = null, string Password = null)
		{
			this.Name = Name;
			this.Server = Server;
			this.Database = Database;
			this.UserID = UserID;
			this.Password = Password;
		}

		/// <summary>
		/// Override that returns the (decorated) connection string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.ConnectionString;
		}

	}
}
