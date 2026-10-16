using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020005C7 RID: 1479
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		// Token: 0x06002FDE RID: 12254 RVA: 0x000A51DD File Offset: 0x000A33DD
		public Authorization(string token)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = true;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		// Token: 0x06002FDF RID: 12255 RVA: 0x000A51F8 File Offset: 0x000A33F8
		public Authorization(string token, bool finished)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_Complete = finished;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme. </param>
		// Token: 0x06002FE0 RID: 12256 RVA: 0x000A5213 File Offset: 0x000A3413
		public Authorization(string token, bool finished, string connectionGroupId)
			: this(token, finished, connectionGroupId, false)
		{
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000A521F File Offset: 0x000A341F
		internal Authorization(string token, bool finished, string connectionGroupId, bool mutualAuth)
		{
			this.m_Message = ValidationHelper.MakeStringNull(token);
			this.m_ConnectionGroupId = ValidationHelper.MakeStringNull(connectionGroupId);
			this.m_Complete = finished;
			this.m_MutualAuth = mutualAuth;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000A524E File Offset: 0x000A344E
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000A5256 File Offset: 0x000A3456
		public string ConnectionGroupId
		{
			get
			{
				return this.m_ConnectionGroupId;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>true if the authentication process is complete; otherwise, false.</returns>
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000A525E File Offset: 0x000A345E
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000A5266 File Offset: 0x000A3466
		internal void SetComplete(bool complete)
		{
			this.m_Complete = complete;
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000A526F File Offset: 0x000A346F
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000A5278 File Offset: 0x000A3478
		public string[] ProtectionRealm
		{
			get
			{
				return this.m_ProtectionRealm;
			}
			set
			{
				string[] array = ValidationHelper.MakeEmptyArrayNull(value);
				this.m_ProtectionRealm = array;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000A5293 File Offset: 0x000A3493
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x000A52A5 File Offset: 0x000A34A5
		public bool MutuallyAuthenticated
		{
			get
			{
				return this.Complete && this.m_MutualAuth;
			}
			set
			{
				this.m_MutualAuth = value;
			}
		}

		// Token: 0x04001A7D RID: 6781
		private string m_Message;

		// Token: 0x04001A7E RID: 6782
		private bool m_Complete;

		// Token: 0x04001A7F RID: 6783
		private string[] m_ProtectionRealm;

		// Token: 0x04001A80 RID: 6784
		private string m_ConnectionGroupId;

		// Token: 0x04001A81 RID: 6785
		private bool m_MutualAuth;

		// Token: 0x04001A82 RID: 6786
		internal string ModuleAuthenticationType;
	}
}
