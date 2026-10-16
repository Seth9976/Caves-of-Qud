using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages storage of membership information for an ASP.NET application in a SQL Server database.</summary>
	// Token: 0x020004C8 RID: 1224
	public class SqlMembershipProvider : MembershipProvider
	{
		// Token: 0x06003769 RID: 14185 RVA: 0x0008EA40 File Offset: 0x0008CC40
		private DbConnection CreateConnection()
		{
			if (!this.schemaIsOk && !(this.schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(this.factory, this.connectionString.ConnectionString, "membership", "1")))
			{
				throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
			}
			if (this.connectionString == null)
			{
				throw new ProviderException("Connection string for the SQL Membership Provider has not been provided.");
			}
			DbConnection dbConnection;
			try
			{
				dbConnection = this.factory.CreateConnection();
				dbConnection.ConnectionString = this.connectionString.ConnectionString;
				dbConnection.Open();
			}
			catch (Exception ex)
			{
				throw new ProviderException("Unable to open SQL connection for the SQL Membership Provider.", ex);
			}
			return dbConnection;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x0008EAE4 File Offset: 0x0008CCE4
		private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			return this.AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x0008EAF0 File Offset: 0x0008CCF0
		private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x0008EB28 File Offset: 0x0008CD28
		private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, DbType type, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			dbParameter.DbType = type;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x0008EB68 File Offset: 0x0008CD68
		private static int GetReturnValue(DbParameter returnValue)
		{
			object value = returnValue.Value;
			if (!(value is int))
			{
				return -1;
			}
			return (int)value;
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x0008EB8C File Offset: 0x0008CD8C
		private void CheckParam(string pName, string p, int length)
		{
			if (p == null)
			{
				throw new ArgumentNullException(pName);
			}
			if (p.Length == 0 || p.Length > length || p.IndexOf(',') != -1)
			{
				throw new ArgumentException(string.Format("invalid format for {0}", pName));
			}
		}

		/// <summary>Modifies a user's password.</summary>
		/// <returns>true if the password was updated successfully. false if the supplied old password is invalid, the user is locked out, or the user does not exist in the database.</returns>
		/// <param name="username">The user to update the password for. </param>
		/// <param name="oldPassword">The current password for the specified user. </param>
		/// <param name="newPassword">The new password for the specified user. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -<paramref name="oldPassword" /> is an empty string or longer than 128 characters.- or -<paramref name="newPassword" /> is an empty string or longer than 128 characters.- or -The encoded version of <paramref name="newPassword" /> is greater than 128 characters.- or -The change-password action was canceled by a subscriber to the <see cref="E:System.Web.Security.Membership.ValidatingPassword" /> event, and the <see cref="P:System.Web.Security.ValidatePasswordEventArgs.FailureInformation" /> property was null.- or -The length of <paramref name="newPassword" /> is less than the minimum length specified in the <see cref="P:System.Web.Security.SqlMembershipProvider.MinRequiredPasswordLength" /> property.- or -The number of non-alphabetic characters in <paramref name="newPassword" /> is less than the required number of non-alphabetic characters specified in the <see cref="P:System.Web.Security.SqlMembershipProvider.MinRequiredNonAlphanumericCharacters" /> property.- or -<paramref name="newPassword" /> does not pass the regular expression defined in the <see cref="P:System.Web.Security.SqlMembershipProvider.PasswordStrengthRegularExpression" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.- or -<paramref name="oldPassword" /> is null.- or -<paramref name="newPassword" /> is null.</exception>
		/// <exception cref="T:System.Web.Security.MembershipPasswordException">
		///   <paramref name="username" /> was not found in the database.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An error occurred while setting the new password value at the database. </exception>
		/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
		// Token: 0x0600376F RID: 14191 RVA: 0x0008EBC8 File Offset: 0x0008CDC8
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			if (oldPassword != null)
			{
				oldPassword = oldPassword.Trim();
			}
			if (newPassword != null)
			{
				newPassword = newPassword.Trim();
			}
			this.CheckParam("username", username, 256);
			this.CheckParam("oldPassword", oldPassword, 128);
			this.CheckParam("newPassword", newPassword, 128);
			if (!this.CheckPassword(newPassword))
			{
				throw new ArgumentException(string.Format("New Password invalid. New Password length minimum: {0}. Non-alphanumeric characters required: {1}.", this.MinRequiredPasswordLength, this.MinRequiredNonAlphanumericCharacters));
			}
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				SqlMembershipProvider.PasswordInfo passwordInfo = this.ValidateUsingPassword(username, oldPassword);
				if (passwordInfo != null)
				{
					this.EmitValidatingPassword(username, newPassword, false);
					string text = this.EncodePassword(newPassword, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
					DbCommand dbCommand = this.factory.CreateCommand();
					dbCommand.Connection = dbConnection;
					dbCommand.CommandText = "aspnet_Membership_SetPassword";
					dbCommand.CommandType = CommandType.StoredProcedure;
					this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
					this.AddParameter(dbCommand, "@UserName", username);
					this.AddParameter(dbCommand, "@NewPassword", text);
					this.AddParameter(dbCommand, "@PasswordFormat", (int)passwordInfo.PasswordFormat);
					this.AddParameter(dbCommand, "@PasswordSalt", passwordInfo.PasswordSalt);
					this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
					DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
					dbCommand.ExecuteNonQuery();
					if (SqlMembershipProvider.GetReturnValue(dbParameter) != 0)
					{
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Updates the password question and answer for a user in the SQL Server membership database.</summary>
		/// <returns>true if the update was successful; otherwise, false. A value of false is also returned if the <paramref name="password" /> is incorrect, the user is locked out, or the user does not exist in the database.</returns>
		/// <param name="username">The user to change the password question and answer for. </param>
		/// <param name="password">The password for the specified user. </param>
		/// <param name="newPasswordQuestion">The new password question for the specified user.</param>
		/// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -<paramref name="password" /> is an empty string or is longer than 128 characters.- or -<paramref name="newPasswordQuestion" /> is an empty string or is longer than 256 characters.- or -<paramref name="newPasswordAnswer" /> is an empty string or is longer than 128 characters.- or -The encoded version of <paramref name="newPasswordAnswer" /> is longer than 128 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.- or -<paramref name="password" /> is null.- or -<paramref name="newPasswordQuestion" /> is null and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.- or -<paramref name="newPasswordAnswer" /> is null and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An error occurred when changing the password question and answer in the database.</exception>
		// Token: 0x06003770 RID: 14192 RVA: 0x0008ED6C File Offset: 0x0008CF6C
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			if (newPasswordQuestion != null)
			{
				newPasswordQuestion = newPasswordQuestion.Trim();
			}
			if (newPasswordAnswer != null)
			{
				newPasswordAnswer = newPasswordAnswer.Trim();
			}
			this.CheckParam("username", username, 256);
			if (this.RequiresQuestionAndAnswer)
			{
				this.CheckParam("newPasswordQuestion", newPasswordQuestion, 128);
			}
			if (this.RequiresQuestionAndAnswer)
			{
				this.CheckParam("newPasswordAnswer", newPasswordAnswer, 128);
			}
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				SqlMembershipProvider.PasswordInfo passwordInfo = this.ValidateUsingPassword(username, password);
				if (passwordInfo != null)
				{
					string text = this.EncodePassword(newPasswordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
					DbCommand dbCommand = this.factory.CreateCommand();
					dbCommand.Connection = dbConnection;
					dbCommand.CommandType = CommandType.StoredProcedure;
					dbCommand.CommandText = "aspnet_Membership_ChangePasswordQuestionAndAnswer";
					this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
					this.AddParameter(dbCommand, "@UserName", username);
					this.AddParameter(dbCommand, "@NewPasswordQuestion", newPasswordQuestion);
					this.AddParameter(dbCommand, "@NewPasswordAnswer", text);
					DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
					dbCommand.ExecuteNonQuery();
					if (SqlMembershipProvider.GetReturnValue(dbParameter) != 0)
					{
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Adds a new user to the SQL Server membership database.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns null.</returns>
		/// <param name="username">The user name for the new user. </param>
		/// <param name="password">The password for the new user. </param>
		/// <param name="email">The e-mail address for the new user. </param>
		/// <param name="passwordQuestion">The password question for the new user.</param>
		/// <param name="passwordAnswer">The password answer for the new user.</param>
		/// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
		/// <param name="providerUserKey">A <see cref="T:System.Guid" /> that uniquely identifies the membership user in the SQL Server database.</param>
		/// <param name="status">One of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> values, indicating whether the user was created successfully.</param>
		// Token: 0x06003771 RID: 14193 RVA: 0x0008EEB8 File Offset: 0x0008D0B8
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			if (username != null)
			{
				username = username.Trim();
			}
			if (password != null)
			{
				password = password.Trim();
			}
			if (email != null)
			{
				email = email.Trim();
			}
			if (passwordQuestion != null)
			{
				passwordQuestion = passwordQuestion.Trim();
			}
			if (passwordAnswer != null)
			{
				passwordAnswer = passwordAnswer.Trim();
			}
			if (username == null || username.Length == 0 || username.Length > 256 || username.IndexOf(',') != -1)
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}
			if (password == null || password.Length == 0 || password.Length > 128)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (!this.CheckPassword(password))
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			this.EmitValidatingPassword(username, password, true);
			if (this.RequiresUniqueEmail && (email == null || email.Length == 0))
			{
				status = MembershipCreateStatus.InvalidEmail;
				return null;
			}
			if (this.RequiresQuestionAndAnswer && (passwordQuestion == null || passwordQuestion.Length == 0 || passwordQuestion.Length > 256))
			{
				status = MembershipCreateStatus.InvalidQuestion;
				return null;
			}
			if (this.RequiresQuestionAndAnswer && (passwordAnswer == null || passwordAnswer.Length == 0 || passwordAnswer.Length > 128))
			{
				status = MembershipCreateStatus.InvalidAnswer;
				return null;
			}
			if (providerUserKey != null && !(providerUserKey is Guid))
			{
				status = MembershipCreateStatus.InvalidProviderUserKey;
				return null;
			}
			if (providerUserKey == null)
			{
				providerUserKey = Guid.NewGuid();
			}
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[16];
			randomNumberGenerator.GetBytes(array);
			string text = Convert.ToBase64String(array);
			password = this.EncodePassword(password, this.PasswordFormat, text);
			if (this.RequiresQuestionAndAnswer)
			{
				passwordAnswer = this.EncodePassword(passwordAnswer, this.PasswordFormat, text);
			}
			if (password.Length > 128)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (this.RequiresQuestionAndAnswer && passwordAnswer.Length > 128)
			{
				status = MembershipCreateStatus.InvalidAnswer;
				return null;
			}
			status = MembershipCreateStatus.Success;
			MembershipUser membershipUser;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				try
				{
					DbCommand dbCommand = this.factory.CreateCommand();
					dbCommand.Connection = dbConnection;
					dbCommand.CommandText = "aspnet_Membership_CreateUser";
					dbCommand.CommandType = CommandType.StoredProcedure;
					DateTime utcNow = DateTime.UtcNow;
					this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
					this.AddParameter(dbCommand, "@UserName", username);
					this.AddParameter(dbCommand, "@Password", password);
					this.AddParameter(dbCommand, "@PasswordSalt", text);
					this.AddParameter(dbCommand, "@Email", email);
					this.AddParameter(dbCommand, "@PasswordQuestion", passwordQuestion);
					this.AddParameter(dbCommand, "@PasswordAnswer", passwordAnswer);
					this.AddParameter(dbCommand, "@IsApproved", isApproved);
					this.AddParameter(dbCommand, "@CurrentTimeUtc", utcNow);
					this.AddParameter(dbCommand, "@CreateDate", utcNow);
					this.AddParameter(dbCommand, "@UniqueEmail", this.RequiresUniqueEmail);
					this.AddParameter(dbCommand, "@PasswordFormat", (int)this.PasswordFormat);
					this.AddParameter(dbCommand, "@UserId", ParameterDirection.InputOutput, providerUserKey);
					DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
					dbCommand.ExecuteNonQuery();
					int returnValue = SqlMembershipProvider.GetReturnValue(dbParameter);
					if (returnValue == 0)
					{
						membershipUser = this.GetUser(username, false);
					}
					else
					{
						if (returnValue == 6)
						{
							status = MembershipCreateStatus.DuplicateUserName;
						}
						else if (returnValue == 7)
						{
							status = MembershipCreateStatus.DuplicateEmail;
						}
						else if (returnValue == 10)
						{
							status = MembershipCreateStatus.DuplicateProviderUserKey;
						}
						else
						{
							status = MembershipCreateStatus.ProviderError;
						}
						membershipUser = null;
					}
				}
				catch (Exception)
				{
					status = MembershipCreateStatus.ProviderError;
					membershipUser = null;
				}
			}
			return membershipUser;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0008F228 File Offset: 0x0008D428
		private bool CheckPassword(string password)
		{
			if (password.Length < this.MinRequiredPasswordLength)
			{
				return false;
			}
			if (this.MinRequiredNonAlphanumericCharacters > 0)
			{
				int num = 0;
				for (int i = 0; i < password.Length; i++)
				{
					if (!char.IsLetterOrDigit(password[i]))
					{
						num++;
					}
				}
				return num >= this.MinRequiredNonAlphanumericCharacters;
			}
			return true;
		}

		/// <summary>Removes a user's membership information from the SQL Server membership database.</summary>
		/// <returns>true if the user was deleted; otherwise, false. A value of false is also returned if the user does not exist in the database.</returns>
		/// <param name="username">The name of the user to delete.</param>
		/// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x06003773 RID: 14195 RVA: 0x0008F284 File Offset: 0x0008D484
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			this.CheckParam("username", username, 256);
			SqlMembershipProvider.DeleteUserTableMask deleteUserTableMask = SqlMembershipProvider.DeleteUserTableMask.MembershipUsers;
			if (deleteAllRelatedData)
			{
				deleteUserTableMask |= SqlMembershipProvider.DeleteUserTableMask.UsersInRoles | SqlMembershipProvider.DeleteUserTableMask.Profiles | SqlMembershipProvider.DeleteUserTableMask.WebPartStateUser;
			}
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Users_DeleteUser";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@UserName", username);
				this.AddParameter(dbCommand, "@TablesToDeleteFrom", (int)deleteUserTableMask);
				this.AddParameter(dbCommand, "@NumTablesDeletedFrom", ParameterDirection.Output, 0);
				DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				if ((int)dbCommand.Parameters["@NumTablesDeletedFrom"].Value == 0)
				{
					flag = false;
				}
				else if (SqlMembershipProvider.GetReturnValue(dbParameter) == 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Generates a random password that is at least 14 characters long.</summary>
		/// <returns>A random password that is at least 14 characters long.</returns>
		// Token: 0x06003774 RID: 14196 RVA: 0x0008F388 File Offset: 0x0008D588
		public virtual string GeneratePassword()
		{
			return Membership.GeneratePassword(this.MinRequiredPasswordLength, this.MinRequiredNonAlphanumericCharacters);
		}

		/// <summary>Returns a collection of membership users for which the e-mail address field contains the specified e-mail address.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="emailToMatch">The e-mail address to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emailToMatch" /> is longer than 256 characters.- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003775 RID: 14197 RVA: 0x0008F39C File Offset: 0x0008D59C
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			this.CheckParam("emailToMatch", emailToMatch, 256);
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex must be >= 0");
			}
			if (pageSize < 0)
			{
				throw new ArgumentException("pageSize must be >= 0");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			MembershipUserCollection membershipUserCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_FindUsersByEmail";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@PageIndex", pageIndex);
				this.AddParameter(dbCommand, "@PageSize", pageSize);
				this.AddParameter(dbCommand, "@EmailToMatch", emailToMatch);
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
				membershipUserCollection = this.BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
			}
			return membershipUserCollection;
		}

		/// <summary>Gets a collection of membership users where the user name contains the specified user name to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains the total number of matched users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("") or is longer than 256 characters.- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than 1.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		// Token: 0x06003776 RID: 14198 RVA: 0x0008F49C File Offset: 0x0008D69C
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			this.CheckParam("usernameToMatch", usernameToMatch, 256);
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex must be >= 0");
			}
			if (pageSize < 0)
			{
				throw new ArgumentException("pageSize must be >= 0");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			MembershipUserCollection membershipUserCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_FindUsersByName";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@PageIndex", pageIndex);
				this.AddParameter(dbCommand, "@PageSize", pageSize);
				this.AddParameter(dbCommand, "@UserNameToMatch", usernameToMatch);
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
				membershipUserCollection = this.BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
			}
			return membershipUserCollection;
		}

		/// <summary>Gets a collection of all the users in the SQL Server membership database.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all the users in the database for the configured <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" />.</returns>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> plus <paramref name="pageSize" /> minus one exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003777 RID: 14199 RVA: 0x0008F59C File Offset: 0x0008D79C
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex must be >= 0");
			}
			if (pageSize < 0)
			{
				throw new ArgumentException("pageSize must be >= 0");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			MembershipUserCollection membershipUserCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_GetAllUsers";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@PageIndex", pageIndex);
				this.AddParameter(dbCommand, "@PageSize", pageSize);
				this.AddParameter(dbCommand, "@ReturnValue", ParameterDirection.ReturnValue, null);
				membershipUserCollection = this.BuildMembershipUserCollection(dbCommand, pageIndex, pageSize, out totalRecords);
			}
			return membershipUserCollection;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x0008F67C File Offset: 0x0008D87C
		private MembershipUserCollection BuildMembershipUserCollection(DbCommand command, int pageIndex, int pageSize, out int totalRecords)
		{
			DbDataReader dbDataReader = null;
			MembershipUserCollection membershipUserCollection2;
			try
			{
				MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
				dbDataReader = command.ExecuteReader();
				while (dbDataReader.Read())
				{
					membershipUserCollection.Add(this.GetUserFromReader(dbDataReader, null, null));
				}
				totalRecords = Convert.ToInt32(command.Parameters["@ReturnValue"].Value);
				membershipUserCollection2 = membershipUserCollection;
			}
			catch (Exception)
			{
				totalRecords = 0;
				membershipUserCollection2 = null;
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Close();
				}
			}
			return membershipUserCollection2;
		}

		/// <summary>Returns the number of users currently accessing the application.</summary>
		/// <returns>The number of users currently accessing the application.</returns>
		// Token: 0x06003779 RID: 14201 RVA: 0x0008F704 File Offset: 0x0008D904
		public override int GetNumberOfUsersOnline()
		{
			int returnValue;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DateTime utcNow = DateTime.UtcNow;
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_GetNumberOfUsersOnline";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@CurrentTimeUtc", utcNow.ToString());
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@MinutesSinceLastInActive", this.userIsOnlineTimeWindow.Minutes);
				DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteScalar();
				returnValue = SqlMembershipProvider.GetReturnValue(dbParameter);
			}
			return returnValue;
		}

		/// <summary>Returns the password for the specified user name from the SQL Server membership database.</summary>
		/// <returns>The password for the specified user name.</returns>
		/// <param name="username">The user to retrieve the password for. </param>
		/// <param name="passwordAnswer">The password answer for the user. </param>
		/// <exception cref="T:System.Web.Security.MembershipPasswordException">
		///   <paramref name="passwordAnswer" /> is invalid. - or -The membership user identified by <paramref name="username" /> is locked out.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.Web.Security.SqlMembershipProvider.EnablePasswordRetrieval" /> is set to false. </exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="username" /> is not found in the membership database.- or -An error occurred while retrieving the password from the database. </exception>
		/// <exception cref="T:System.ArgumentException">One of the parameter values exceeds the maximum allowed length.- or -<paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -<paramref name="passwordAnswer" /> is an empty string and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.- or -<paramref name="passwordAnswer" /> is greater than 128 characters.- or -The encoded version of <paramref name="passwordAnswer" /> is greater than 128 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.- or -<paramref name="passwordAnswer" /> is null and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.</exception>
		// Token: 0x0600377A RID: 14202 RVA: 0x0008F7C4 File Offset: 0x0008D9C4
		public override string GetPassword(string username, string passwordAnswer)
		{
			if (!this.EnablePasswordRetrieval)
			{
				throw new NotSupportedException("this provider has not been configured to allow the retrieval of passwords");
			}
			this.CheckParam("username", username, 256);
			if (this.RequiresQuestionAndAnswer)
			{
				this.CheckParam("passwordAnswer", passwordAnswer, 128);
			}
			SqlMembershipProvider.PasswordInfo passwordInfo = this.GetPasswordInfo(username);
			if (passwordInfo == null)
			{
				throw new ProviderException("An error occurred while retrieving the password from the database");
			}
			string text = this.EncodePassword(passwordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
			string text2 = null;
			string text3;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_GetPassword";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@UserName", username);
				this.AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", this.MaxInvalidPasswordAttempts);
				this.AddParameter(dbCommand, "@PasswordAttemptWindow", this.PasswordAttemptWindow);
				this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
				this.AddParameter(dbCommand, "@PasswordAnswer", text);
				DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				int returnValue = SqlMembershipProvider.GetReturnValue(dbParameter);
				if (returnValue == 3)
				{
					throw new MembershipPasswordException("Password Answer is invalid");
				}
				if (returnValue == 99)
				{
					throw new MembershipPasswordException("The user account is currently locked out");
				}
				if (dbDataReader.Read())
				{
					text2 = dbDataReader.GetString(0);
					dbDataReader.Close();
				}
				if (passwordInfo.PasswordFormat == MembershipPasswordFormat.Clear)
				{
					text3 = text2;
				}
				else if (passwordInfo.PasswordFormat == MembershipPasswordFormat.Encrypted)
				{
					text3 = this.DecodePassword(text2, passwordInfo.PasswordFormat);
				}
				else
				{
					text3 = text2;
				}
			}
			return text3;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x0008F990 File Offset: 0x0008DB90
		private MembershipUser GetUserFromReader(DbDataReader reader, string username, object userId)
		{
			int num = 0;
			if (username == null)
			{
				num = 1;
			}
			if (userId != null)
			{
				username = reader.GetString(8);
			}
			return new MembershipUser(this.Name, (username == null) ? reader.GetString(0) : username, (userId == null) ? reader.GetGuid(8 + num) : userId, reader.IsDBNull(num) ? null : reader.GetString(num), reader.IsDBNull(1 + num) ? null : reader.GetString(1 + num), reader.IsDBNull(2 + num) ? null : reader.GetString(2 + num), reader.GetBoolean(3 + num), reader.GetBoolean(9 + num), reader.GetDateTime(4 + num).ToLocalTime(), reader.GetDateTime(5 + num).ToLocalTime(), reader.GetDateTime(6 + num).ToLocalTime(), reader.GetDateTime(7 + num).ToLocalTime(), reader.GetDateTime(10 + num).ToLocalTime());
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x0008FA88 File Offset: 0x0008DC88
		private MembershipUser BuildMembershipUser(DbCommand query, string username, object userId)
		{
			MembershipUser membershipUser;
			try
			{
				using (DbConnection dbConnection = this.CreateConnection())
				{
					query.Connection = dbConnection;
					using (DbDataReader dbDataReader = query.ExecuteReader())
					{
						if (!dbDataReader.Read())
						{
							membershipUser = null;
						}
						else
						{
							membershipUser = this.GetUserFromReader(dbDataReader, username, userId);
						}
					}
				}
			}
			catch (Exception)
			{
				membershipUser = null;
			}
			finally
			{
				query.Connection = null;
			}
			return membershipUser;
		}

		/// <summary>Returns information from the SQL Server membership database for a user and provides an option to update the last activity date/time stamp for the user.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If no user is found in the database for the specified <paramref name="username" /> value, null is returned.</returns>
		/// <param name="username">The name of the user to get information for. </param>
		/// <param name="userIsOnline">true to update the last activity date/time stamp for the user; false to return user information without updating the last activity date/time stamp for the user. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> exceeds 256 characters.- or -<paramref name="username" /> contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x0600377D RID: 14205 RVA: 0x0008FB18 File Offset: 0x0008DD18
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			if (username == null)
			{
				throw new ArgumentNullException("username");
			}
			if (username.Length == 0)
			{
				return null;
			}
			this.CheckParam("username", username, 256);
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = "aspnet_Membership_GetUserByName";
			dbCommand.CommandType = CommandType.StoredProcedure;
			this.AddParameter(dbCommand, "@UserName", username);
			this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
			this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
			this.AddParameter(dbCommand, "@UpdateLastActivity", userIsOnline);
			return this.BuildMembershipUser(dbCommand, username, null);
		}

		/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier and updates the last activity date/time stamp for the user, if specified.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier. If no user is found in the database for the specified <paramref name="providerUserKey" /> value, null is returned.</returns>
		/// <param name="providerUserKey">The unique identifier for the user.</param>
		/// <param name="userIsOnline">true to update the last-activity date/time stamp for the specified user; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerUserKey" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerUserKey" /> is not of type <see cref="T:System.Guid" />.</exception>
		// Token: 0x0600377E RID: 14206 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			DbCommand dbCommand = this.factory.CreateCommand();
			dbCommand.CommandText = "aspnet_Membership_GetUserByUserId";
			dbCommand.CommandType = CommandType.StoredProcedure;
			this.AddParameter(dbCommand, "@UserId", providerUserKey);
			this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
			this.AddParameter(dbCommand, "@UpdateLastActivity", userIsOnline);
			return this.BuildMembershipUser(dbCommand, string.Empty, providerUserKey);
		}

		/// <summary>Gets the user name associated with the specified e-mail address.</summary>
		/// <returns>The user name associated with the specified e-mail address. If no match is found, this method returns null.</returns>
		/// <param name="email">The e-mail address to search for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="email" /> exceeds 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">More than one user with the same e-mail address exists in the database and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is true.</exception>
		// Token: 0x0600377F RID: 14207 RVA: 0x0008FC30 File Offset: 0x0008DE30
		public override string GetUserNameByEmail(string email)
		{
			this.CheckParam("email", email, 256);
			string text2;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_GetUserByEmail";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@Email", email);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				string text = null;
				if (dbDataReader.Read())
				{
					text = dbDataReader.GetString(0);
				}
				dbDataReader.Close();
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0008FCDC File Offset: 0x0008DEDC
		private bool GetBoolConfigValue(NameValueCollection config, string name, bool def)
		{
			bool flag = def;
			string text = config[name];
			if (text != null)
			{
				try
				{
					flag = bool.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ProviderException(string.Format("{0} must be true or false", name), ex);
				}
			}
			return flag;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0008FD24 File Offset: 0x0008DF24
		private int GetIntConfigValue(NameValueCollection config, string name, int def)
		{
			int num = def;
			string text = config[name];
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ProviderException(string.Format("{0} must be an integer", name), ex);
				}
			}
			return num;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x0008FD6C File Offset: 0x0008DF6C
		private int GetEnumConfigValue(NameValueCollection config, string name, Type enumType, int def)
		{
			int num = def;
			string text = config[name];
			if (text != null)
			{
				try
				{
					num = (int)Enum.Parse(enumType, text);
				}
				catch (Exception ex)
				{
					throw new ProviderException(string.Format("{0} must be one of the following values: {1}", name, string.Join(",", Enum.GetNames(enumType))), ex);
				}
			}
			return num;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x0008FDCC File Offset: 0x0008DFCC
		private string GetStringConfigValue(NameValueCollection config, string name, string def)
		{
			string text = def;
			string text2 = config[name];
			if (text2 != null)
			{
				text = text2;
			}
			return text;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x0008FDEC File Offset: 0x0008DFEC
		private void EmitValidatingPassword(string username, string password, bool isNewUser)
		{
			ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, isNewUser);
			this.OnValidatingPassword(e);
			if (!e.Cancel)
			{
				return;
			}
			if (e.FailureInformation == null)
			{
				throw new ProviderException("Password validation canceled");
			}
			throw e.FailureInformation;
		}

		/// <summary>Initializes the SQL Server membership provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Security.SqlMembershipProvider" /> instance to initialize. </param>
		/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the membership provider. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="config" /> is null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The enablePasswordRetrieval, enablePasswordReset, requiresQuestionAndAnswer, or requiresUniqueEmail attribute is set to a value other than a Boolean.- or -The maxInvalidPasswordAttempts or the passwordAttemptWindow attribute is set to a value other than a positive integer.- or -The minRequiredPasswordLength attribute is set to a value other than a positive integer, or the value is greater than 128.- or -The minRequiredNonalphanumericCharacters attribute is set to a value other than zero or a positive integer, or the value is greater than 128.- or -The value for the passwordStrengthRegularExpression attribute is not a valid regular expression.- or -The applicationName attribute is set to a value that is greater than 256 characters.- or -The passwordFormat attribute specified in the application configuration file is an invalid <see cref="T:System.Web.Security.MembershipPasswordFormat" /> enumeration.- or -The passwordFormat attribute is set to <see cref="F:System.Web.Security.MembershipPasswordFormat.Hashed" /> and the enablePasswordRetrieval attribute is set to true in the application configuration.- or -The passwordFormat attribute is set to Encrypted and the machineKey configuration element specifies AutoGenerate for the decryptionKey attribute.- or -The connectionStringName attribute is empty or does not exist in the application configuration.- or - The value of the connection string for the connectionStringName attribute value is empty, or the specified connectionStringName does not exist in the application configuration file.- or - The value for the commandTimeout attribute is set to a value other than zero or a positive integer.- or -The application configuration file for this <see cref="T:System.Web.Security.SqlMembershipProvider" /> instance contains an unrecognized attribute.</exception>
		/// <exception cref="T:System.Web.HttpException">The current trust level is less than Low.</exception>
		/// <exception cref="T:System.InvalidOperationException">The provider has already been initialized prior to the current call to the <see cref="M:System.Web.Security.SqlMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</exception>
		// Token: 0x06003785 RID: 14213 RVA: 0x0008FE2C File Offset: 0x0008E02C
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			base.Initialize(name, config);
			this.applicationName = this.GetStringConfigValue(config, "applicationName", "/");
			this.enablePasswordReset = this.GetBoolConfigValue(config, "enablePasswordReset", true);
			this.enablePasswordRetrieval = this.GetBoolConfigValue(config, "enablePasswordRetrieval", false);
			this.requiresQuestionAndAnswer = this.GetBoolConfigValue(config, "requiresQuestionAndAnswer", true);
			this.requiresUniqueEmail = this.GetBoolConfigValue(config, "requiresUniqueEmail", false);
			this.passwordFormat = (MembershipPasswordFormat)this.GetEnumConfigValue(config, "passwordFormat", typeof(MembershipPasswordFormat), 1);
			this.maxInvalidPasswordAttempts = this.GetIntConfigValue(config, "maxInvalidPasswordAttempts", 5);
			this.minRequiredPasswordLength = this.GetIntConfigValue(config, "minRequiredPasswordLength", 7);
			this.minRequiredNonAlphanumericCharacters = this.GetIntConfigValue(config, "minRequiredNonalphanumericCharacters", 1);
			this.passwordAttemptWindow = this.GetIntConfigValue(config, "passwordAttemptWindow", 10);
			this.passwordStrengthRegularExpression = this.GetStringConfigValue(config, "passwordStrengthRegularExpression", "");
			MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
			this.userIsOnlineTimeWindow = membershipSection.UserIsOnlineTimeWindow;
			if (this.passwordFormat == MembershipPasswordFormat.Hashed && this.enablePasswordRetrieval)
			{
				throw new ProviderException("password retrieval cannot be used with hashed passwords");
			}
			string text = config["connectionStringName"];
			if (this.applicationName.Length > 256)
			{
				throw new ProviderException("The ApplicationName attribute must be 256 characters long or less.");
			}
			if (text == null || text.Length == 0)
			{
				throw new ProviderException("The ConnectionStringName attribute must be present and non-zero length.");
			}
			this.connectionString = WebConfigurationManager.ConnectionStrings[text];
			this.factory = ((this.connectionString == null || string.IsNullOrEmpty(this.connectionString.ProviderName)) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(this.connectionString.ProviderName));
		}

		/// <summary>Resets a user's password to a new, automatically generated password.</summary>
		/// <returns>The new password for the specified user.</returns>
		/// <param name="username">The user to reset the password for. </param>
		/// <param name="passwordAnswer">The password answer for the specified user. </param>
		/// <exception cref="T:System.Web.Security.MembershipPasswordException">
		///   <paramref name="passwordAnswer" /> is invalid. - or -The user account is currently locked.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.Web.Security.SqlMembershipProvider.EnablePasswordReset" /> is set to false. </exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="username" /> is not found in the membership database.- or -The change password action was canceled by a subscriber to the <see cref="E:System.Web.Security.Membership.ValidatingPassword" /> event and the <see cref="P:System.Web.Security.ValidatePasswordEventArgs.FailureInformation" /> property was null.- or -An error occurred while retrieving the password from the database. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -<paramref name="passwordAnswer" /> is an empty string, or is longer than 128 characters, and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.- or -<paramref name="passwordAnswer" /> is longer than 128 characters after encoding.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.- or -<paramref name="passwordAnswer" /> is null and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresQuestionAndAnswer" /> is true.</exception>
		/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
		// Token: 0x06003786 RID: 14214 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		public override string ResetPassword(string username, string passwordAnswer)
		{
			if (!this.EnablePasswordReset)
			{
				throw new NotSupportedException("this provider has not been configured to allow the resetting of passwords");
			}
			this.CheckParam("username", username, 256);
			if (this.RequiresQuestionAndAnswer)
			{
				this.CheckParam("passwordAnswer", passwordAnswer, 128);
			}
			string text4;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				SqlMembershipProvider.PasswordInfo passwordInfo = this.GetPasswordInfo(username);
				if (passwordInfo == null)
				{
					throw new ProviderException(username + "is not found in the membership database");
				}
				string text = this.GeneratePassword();
				this.EmitValidatingPassword(username, text, false);
				string text2 = this.EncodePassword(text, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
				string text3 = this.EncodePassword(passwordAnswer, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt);
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_ResetPassword";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@UserName", username);
				this.AddParameter(dbCommand, "@NewPassword", text2);
				this.AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", this.MaxInvalidPasswordAttempts);
				this.AddParameter(dbCommand, "@PasswordAttemptWindow", this.PasswordAttemptWindow);
				this.AddParameter(dbCommand, "@PasswordSalt", passwordInfo.PasswordSalt);
				this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
				this.AddParameter(dbCommand, "@PasswordFormat", (int)passwordInfo.PasswordFormat);
				this.AddParameter(dbCommand, "@PasswordAnswer", text3);
				DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				int returnValue = SqlMembershipProvider.GetReturnValue(dbParameter);
				if (returnValue == 0)
				{
					text4 = text;
				}
				else
				{
					if (returnValue == 3)
					{
						throw new MembershipPasswordException("Password Answer is invalid");
					}
					if (returnValue == 99)
					{
						throw new MembershipPasswordException("The user account is currently locked out");
					}
					throw new ProviderException("Failed to reset password");
				}
			}
			return text4;
		}

		/// <summary>Updates information about a user in the SQL Server membership database.</summary>
		/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="user" /> is null. - or -The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> is null.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is null and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to true.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> is an empty string (""), contains a comma, or is longer than 256 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is longer than 256 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> is an empty string and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to true.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> property of <paramref name="user" /> was not found in the database.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property of <paramref name="user" /> was equal to an existing e-mail address in the database and <see cref="P:System.Web.Security.SqlMembershipProvider.RequiresUniqueEmail" /> is set to true.- or -The user update failed.</exception>
		// Token: 0x06003787 RID: 14215 RVA: 0x000901FC File Offset: 0x0008E3FC
		public override void UpdateUser(MembershipUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (user.UserName == null)
			{
				throw new ArgumentNullException("user.UserName");
			}
			if (this.RequiresUniqueEmail && user.Email == null)
			{
				throw new ArgumentNullException("user.Email");
			}
			this.CheckParam("user.UserName", user.UserName, 256);
			if (user.Email.Length > 256 || (this.RequiresUniqueEmail && user.Email.Length == 0))
			{
				throw new ArgumentException("invalid format for user.Email");
			}
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_Membership_UpdateUser";
				dbCommand.CommandType = CommandType.StoredProcedure;
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@UserName", user.UserName);
				this.AddParameter(dbCommand, "@Email", (user.Email == null) ? DBNull.Value : user.Email);
				this.AddParameter(dbCommand, "@Comment", (user.Comment == null) ? DBNull.Value : user.Comment);
				this.AddParameter(dbCommand, "@IsApproved", user.IsApproved);
				this.AddParameter(dbCommand, "@LastLoginDate", DateTime.UtcNow);
				this.AddParameter(dbCommand, "@LastActivityDate", DateTime.UtcNow);
				this.AddParameter(dbCommand, "@UniqueEmail", this.RequiresUniqueEmail);
				this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
				DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				int returnValue = SqlMembershipProvider.GetReturnValue(dbParameter);
				if (returnValue == 1)
				{
					throw new ProviderException("The UserName property of user was not found in the database.");
				}
				if (returnValue == 7)
				{
					throw new ProviderException("The Email property of user was equal to an existing e-mail address in the database and RequiresUniqueEmail is set to true.");
				}
				if (returnValue != 0)
				{
					throw new ProviderException("Failed to update user");
				}
			}
		}

		/// <summary>Verifies that the specified user name and password exist in the SQL Server membership database.</summary>
		/// <returns>true if the specified username and password are valid; otherwise, false. A value of false is also returned if the user does not exist in the database.</returns>
		/// <param name="username">The name of the user to validate. </param>
		/// <param name="password">The password for the specified user. </param>
		// Token: 0x06003788 RID: 14216 RVA: 0x00090408 File Offset: 0x0008E608
		public override bool ValidateUser(string username, string password)
		{
			if (username.Length == 0)
			{
				return false;
			}
			this.CheckParam("username", username, 256);
			this.EmitValidatingPassword(username, password, false);
			SqlMembershipProvider.PasswordInfo passwordInfo = this.ValidateUsingPassword(username, password);
			if (passwordInfo != null)
			{
				passwordInfo.LastLoginDate = DateTime.UtcNow;
				this.UpdateUserInfo(username, passwordInfo, true, true);
				return true;
			}
			return false;
		}

		/// <summary>Clears the user's locked-out status so that the membership user can be validated.</summary>
		/// <returns>true if the membership user was successfully unlocked; otherwise, false. A value of false is also returned if the user does not exist in the database.</returns>
		/// <param name="username">The name of the membership user to clear the locked-out status for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string, is longer than 256 characters, or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x06003789 RID: 14217 RVA: 0x00090460 File Offset: 0x0008E660
		public override bool UnlockUser(string username)
		{
			this.CheckParam("username", username, 256);
			using (DbConnection dbConnection = this.CreateConnection())
			{
				try
				{
					DbCommand dbCommand = this.factory.CreateCommand();
					dbCommand.Connection = dbConnection;
					dbCommand.CommandText = "aspnet_Membership_UnlockUser";
					dbCommand.CommandType = CommandType.StoredProcedure;
					this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
					this.AddParameter(dbCommand, "@UserName", username);
					DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
					dbCommand.ExecuteNonQuery();
					if (SqlMembershipProvider.GetReturnValue(dbParameter) != 0)
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					throw new ProviderException("Failed to unlock user", ex);
				}
			}
			return true;
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x00090528 File Offset: 0x0008E728
		private void UpdateUserInfo(string username, SqlMembershipProvider.PasswordInfo pi, bool isPasswordCorrect, bool updateLoginActivity)
		{
			this.CheckParam("username", username, 256);
			using (DbConnection dbConnection = this.CreateConnection())
			{
				try
				{
					DbCommand dbCommand = this.factory.CreateCommand();
					dbCommand.Connection = dbConnection;
					dbCommand.CommandText = "aspnet_Membership_UpdateUserInfo";
					dbCommand.CommandType = CommandType.StoredProcedure;
					this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
					this.AddParameter(dbCommand, "@UserName", username);
					this.AddParameter(dbCommand, "@IsPasswordCorrect", isPasswordCorrect);
					this.AddParameter(dbCommand, "@UpdateLastLoginActivityDate", updateLoginActivity);
					this.AddParameter(dbCommand, "@MaxInvalidPasswordAttempts", this.MaxInvalidPasswordAttempts);
					this.AddParameter(dbCommand, "@PasswordAttemptWindow", this.PasswordAttemptWindow);
					this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
					this.AddParameter(dbCommand, "@LastLoginDate", pi.LastLoginDate);
					this.AddParameter(dbCommand, "@LastActivityDate", pi.LastActivityDate);
					DbParameter dbParameter = this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
					dbCommand.ExecuteNonQuery();
					SqlMembershipProvider.GetReturnValue(dbParameter);
				}
				catch (Exception ex)
				{
					throw new ProviderException("Failed to update Membership table", ex);
				}
			}
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x0009069C File Offset: 0x0008E89C
		private SqlMembershipProvider.PasswordInfo ValidateUsingPassword(string username, string password)
		{
			MembershipUser user = this.GetUser(username, true);
			if (user == null)
			{
				return null;
			}
			if (!user.IsApproved || user.IsLockedOut)
			{
				return null;
			}
			SqlMembershipProvider.PasswordInfo passwordInfo = this.GetPasswordInfo(username);
			if (passwordInfo == null)
			{
				return null;
			}
			if (this.EncodePassword(password, passwordInfo.PasswordFormat, passwordInfo.PasswordSalt) != passwordInfo.Password)
			{
				this.UpdateUserInfo(username, passwordInfo, false, false);
				return null;
			}
			return passwordInfo;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x00090704 File Offset: 0x0008E904
		private SqlMembershipProvider.PasswordInfo GetPasswordInfo(string username)
		{
			SqlMembershipProvider.PasswordInfo passwordInfo;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Membership_GetPasswordWithFormat";
				this.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "@UserName", username);
				this.AddParameter(dbCommand, "@UpdateLastLoginActivityDate", false);
				this.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.Now);
				this.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				if (!dbDataReader.Read())
				{
					passwordInfo = null;
				}
				else
				{
					passwordInfo = new SqlMembershipProvider.PasswordInfo(dbDataReader.GetString(0), (MembershipPasswordFormat)dbDataReader.GetInt32(1), dbDataReader.GetString(2), dbDataReader.GetInt32(3), dbDataReader.GetInt32(4), dbDataReader.GetBoolean(5), dbDataReader.GetDateTime(6), dbDataReader.GetDateTime(7));
				}
			}
			return passwordInfo;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x00090808 File Offset: 0x0008EA08
		private string EncodePassword(string password, MembershipPasswordFormat passwordFormat, string salt)
		{
			byte[] array;
			byte[] array2;
			switch (passwordFormat)
			{
			case MembershipPasswordFormat.Clear:
				return password;
			case MembershipPasswordFormat.Hashed:
			{
				array = Encoding.Unicode.GetBytes(password);
				array2 = Convert.FromBase64String(salt);
				byte[] array3 = new byte[array2.Length + array.Length];
				Buffer.BlockCopy(array2, 0, array3, 0, array2.Length);
				Buffer.BlockCopy(array, 0, array3, array2.Length, array.Length);
				string text = ((MembershipSection)WebConfigurationManager.GetSection("system.web/membership")).HashAlgorithmType;
				if (text.Length == 0)
				{
					text = MachineKeySection.Config.Validation.ToString();
					if (text.StartsWith("alg:"))
					{
						text = text.Substring(4);
					}
				}
				using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text))
				{
					KeyedHashAlgorithm keyedHashAlgorithm = hashAlgorithm as KeyedHashAlgorithm;
					if (keyedHashAlgorithm != null)
					{
						keyedHashAlgorithm.Key = MachineKeySection.Config.GetValidationKey();
					}
					hashAlgorithm.TransformFinalBlock(array3, 0, array3.Length);
					return Convert.ToBase64String(hashAlgorithm.Hash);
				}
				break;
			}
			case MembershipPasswordFormat.Encrypted:
				break;
			default:
				return null;
			}
			array = Encoding.Unicode.GetBytes(password);
			array2 = Convert.FromBase64String(salt);
			byte[] array4 = new byte[array.Length + array2.Length];
			Array.Copy(array2, 0, array4, 0, array2.Length);
			Array.Copy(array, 0, array4, array2.Length, array.Length);
			return Convert.ToBase64String(this.EncryptPassword(array4));
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x00090964 File Offset: 0x0008EB64
		private string DecodePassword(string password, MembershipPasswordFormat passwordFormat)
		{
			switch (passwordFormat)
			{
			case MembershipPasswordFormat.Clear:
				return password;
			case MembershipPasswordFormat.Hashed:
				throw new ProviderException("Hashed passwords cannot be decoded.");
			case MembershipPasswordFormat.Encrypted:
				return Encoding.Unicode.GetString(this.DecryptPassword(Convert.FromBase64String(password)));
			default:
				return null;
			}
		}

		/// <summary>Gets or sets the name of the application to store and retrieve membership information for.</summary>
		/// <returns>The name of the application to store and retrieve membership information for. The default is the <see cref="P:System.Web.HttpRequest.ApplicationPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" /> property to an empty string or null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.SqlMembershipProvider.ApplicationName" /> property to a string that is longer than 256 characters.</exception>
		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x0009099F File Offset: 0x0008EB9F
		// (set) Token: 0x06003790 RID: 14224 RVA: 0x000909A7 File Offset: 0x0008EBA7
		public override string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
			set
			{
				this.applicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to allow users to reset their passwords.</summary>
		/// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000909B0 File Offset: 0x0008EBB0
		public override bool EnablePasswordReset
		{
			get
			{
				return this.enablePasswordReset;
			}
		}

		/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to allow users to retrieve their passwords.</summary>
		/// <returns>true if the membership provider supports password retrieval; otherwise, false. The default is false.</returns>
		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000909B8 File Offset: 0x0008EBB8
		public override bool EnablePasswordRetrieval
		{
			get
			{
				return this.enablePasswordRetrieval;
			}
		}

		/// <summary>Gets a value indicating the format for storing passwords in the SQL Server membership database.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values, indicating the format for storing passwords in the SQL Server database.</returns>
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000909C0 File Offset: 0x0008EBC0
		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return this.passwordFormat;
			}
		}

		/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to require the user to answer a password question for password reset and retrieval.</summary>
		/// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.</returns>
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000909C8 File Offset: 0x0008EBC8
		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				return this.requiresQuestionAndAnswer;
			}
		}

		/// <summary>Gets a value indicating whether the SQL Server membership provider is configured to require a unique e-mail address for each user name.</summary>
		/// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is false.</returns>
		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000909D0 File Offset: 0x0008EBD0
		public override bool RequiresUniqueEmail
		{
			get
			{
				return this.requiresUniqueEmail;
			}
		}

		/// <summary>Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.</summary>
		/// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000909D8 File Offset: 0x0008EBD8
		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				return this.maxInvalidPasswordAttempts;
			}
		}

		/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
		/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000909E0 File Offset: 0x0008EBE0
		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return this.minRequiredNonAlphanumericCharacters;
			}
		}

		/// <summary>Gets the minimum length required for a password.</summary>
		/// <returns>The minimum length required for a password. </returns>
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000909E8 File Offset: 0x0008EBE8
		public override int MinRequiredPasswordLength
		{
			get
			{
				return this.minRequiredPasswordLength;
			}
		}

		/// <summary>Gets the time window between which consecutive failed attempts to provide a valid password or password answers are tracked.</summary>
		/// <returns>The time window, in minutes, during which consecutive failed attempts to provide a valid password or password answers are tracked. The default is 10 minutes. If the interval between the current failed attempt and the last failed attempt is greater than the <see cref="P:System.Web.Security.SqlMembershipProvider.PasswordAttemptWindow" /> property setting, each failed attempt is treated as if it were the first failed attempt.</returns>
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000909F0 File Offset: 0x0008EBF0
		public override int PasswordAttemptWindow
		{
			get
			{
				return this.passwordAttemptWindow;
			}
		}

		/// <summary>Gets the regular expression used to evaluate a password.</summary>
		/// <returns>A regular expression used to evaluate a password.</returns>
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000909F8 File Offset: 0x0008EBF8
		public override string PasswordStrengthRegularExpression
		{
			get
			{
				return this.passwordStrengthRegularExpression;
			}
		}

		// Token: 0x04001DF3 RID: 7667
		private bool enablePasswordReset;

		// Token: 0x04001DF4 RID: 7668
		private bool enablePasswordRetrieval;

		// Token: 0x04001DF5 RID: 7669
		private int maxInvalidPasswordAttempts;

		// Token: 0x04001DF6 RID: 7670
		private MembershipPasswordFormat passwordFormat;

		// Token: 0x04001DF7 RID: 7671
		private bool requiresQuestionAndAnswer;

		// Token: 0x04001DF8 RID: 7672
		private bool requiresUniqueEmail;

		// Token: 0x04001DF9 RID: 7673
		private int minRequiredNonAlphanumericCharacters;

		// Token: 0x04001DFA RID: 7674
		private int minRequiredPasswordLength;

		// Token: 0x04001DFB RID: 7675
		private int passwordAttemptWindow;

		// Token: 0x04001DFC RID: 7676
		private string passwordStrengthRegularExpression;

		// Token: 0x04001DFD RID: 7677
		private TimeSpan userIsOnlineTimeWindow;

		// Token: 0x04001DFE RID: 7678
		private ConnectionStringSettings connectionString;

		// Token: 0x04001DFF RID: 7679
		private DbProviderFactory factory;

		// Token: 0x04001E00 RID: 7680
		private string applicationName;

		// Token: 0x04001E01 RID: 7681
		private bool schemaIsOk;

		// Token: 0x020004C9 RID: 1225
		[Flags]
		private enum DeleteUserTableMask
		{
			// Token: 0x04001E03 RID: 7683
			MembershipUsers = 1,
			// Token: 0x04001E04 RID: 7684
			UsersInRoles = 2,
			// Token: 0x04001E05 RID: 7685
			Profiles = 4,
			// Token: 0x04001E06 RID: 7686
			WebPartStateUser = 8
		}

		// Token: 0x020004CA RID: 1226
		private sealed class PasswordInfo
		{
			// Token: 0x0600379C RID: 14236 RVA: 0x00090A00 File Offset: 0x0008EC00
			internal PasswordInfo(string password, MembershipPasswordFormat passwordFormat, string passwordSalt, int failedPasswordAttemptCount, int failedPasswordAnswerAttemptCount, bool isApproved, DateTime lastLoginDate, DateTime lastActivityDate)
			{
				this._password = password;
				this._passwordFormat = passwordFormat;
				this._passwordSalt = passwordSalt;
				this._failedPasswordAttemptCount = failedPasswordAttemptCount;
				this._failedPasswordAnswerAttemptCount = failedPasswordAnswerAttemptCount;
				this._isApproved = isApproved;
				this._lastLoginDate = lastLoginDate;
				this._lastActivityDate = lastActivityDate;
			}

			// Token: 0x1700117D RID: 4477
			// (get) Token: 0x0600379D RID: 14237 RVA: 0x00090A50 File Offset: 0x0008EC50
			// (set) Token: 0x0600379E RID: 14238 RVA: 0x00090A58 File Offset: 0x0008EC58
			public string Password
			{
				get
				{
					return this._password;
				}
				set
				{
					this._password = value;
				}
			}

			// Token: 0x1700117E RID: 4478
			// (get) Token: 0x0600379F RID: 14239 RVA: 0x00090A61 File Offset: 0x0008EC61
			// (set) Token: 0x060037A0 RID: 14240 RVA: 0x00090A69 File Offset: 0x0008EC69
			public MembershipPasswordFormat PasswordFormat
			{
				get
				{
					return this._passwordFormat;
				}
				set
				{
					this._passwordFormat = value;
				}
			}

			// Token: 0x1700117F RID: 4479
			// (get) Token: 0x060037A1 RID: 14241 RVA: 0x00090A72 File Offset: 0x0008EC72
			// (set) Token: 0x060037A2 RID: 14242 RVA: 0x00090A7A File Offset: 0x0008EC7A
			public string PasswordSalt
			{
				get
				{
					return this._passwordSalt;
				}
				set
				{
					this._passwordSalt = value;
				}
			}

			// Token: 0x17001180 RID: 4480
			// (get) Token: 0x060037A3 RID: 14243 RVA: 0x00090A83 File Offset: 0x0008EC83
			// (set) Token: 0x060037A4 RID: 14244 RVA: 0x00090A8B File Offset: 0x0008EC8B
			public int FailedPasswordAttemptCount
			{
				get
				{
					return this._failedPasswordAttemptCount;
				}
				set
				{
					this._failedPasswordAttemptCount = value;
				}
			}

			// Token: 0x17001181 RID: 4481
			// (get) Token: 0x060037A5 RID: 14245 RVA: 0x00090A94 File Offset: 0x0008EC94
			// (set) Token: 0x060037A6 RID: 14246 RVA: 0x00090A9C File Offset: 0x0008EC9C
			public int FailedPasswordAnswerAttemptCount
			{
				get
				{
					return this._failedPasswordAnswerAttemptCount;
				}
				set
				{
					this._failedPasswordAnswerAttemptCount = value;
				}
			}

			// Token: 0x17001182 RID: 4482
			// (get) Token: 0x060037A7 RID: 14247 RVA: 0x00090AA5 File Offset: 0x0008ECA5
			// (set) Token: 0x060037A8 RID: 14248 RVA: 0x00090AAD File Offset: 0x0008ECAD
			public bool IsApproved
			{
				get
				{
					return this._isApproved;
				}
				set
				{
					this._isApproved = value;
				}
			}

			// Token: 0x17001183 RID: 4483
			// (get) Token: 0x060037A9 RID: 14249 RVA: 0x00090AB6 File Offset: 0x0008ECB6
			// (set) Token: 0x060037AA RID: 14250 RVA: 0x00090ABE File Offset: 0x0008ECBE
			public DateTime LastLoginDate
			{
				get
				{
					return this._lastLoginDate;
				}
				set
				{
					this._lastLoginDate = value;
				}
			}

			// Token: 0x17001184 RID: 4484
			// (get) Token: 0x060037AB RID: 14251 RVA: 0x00090AC7 File Offset: 0x0008ECC7
			// (set) Token: 0x060037AC RID: 14252 RVA: 0x00090ACF File Offset: 0x0008ECCF
			public DateTime LastActivityDate
			{
				get
				{
					return this._lastActivityDate;
				}
				set
				{
					this._lastActivityDate = value;
				}
			}

			// Token: 0x04001E07 RID: 7687
			private string _password;

			// Token: 0x04001E08 RID: 7688
			private MembershipPasswordFormat _passwordFormat;

			// Token: 0x04001E09 RID: 7689
			private string _passwordSalt;

			// Token: 0x04001E0A RID: 7690
			private int _failedPasswordAttemptCount;

			// Token: 0x04001E0B RID: 7691
			private int _failedPasswordAnswerAttemptCount;

			// Token: 0x04001E0C RID: 7692
			private bool _isApproved;

			// Token: 0x04001E0D RID: 7693
			private DateTime _lastLoginDate;

			// Token: 0x04001E0E RID: 7694
			private DateTime _lastActivityDate;
		}
	}
}
