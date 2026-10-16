using System;

namespace System.Web.Management
{
	/// <summary>Provides information associated with health events.</summary>
	// Token: 0x0200052A RID: 1322
	public sealed class WebApplicationInformation
	{
		// Token: 0x060039F6 RID: 14838 RVA: 0x00002050 File Offset: 0x00000250
		internal WebApplicationInformation()
		{
		}

		/// <summary>Gets the current application domain name.</summary>
		/// <returns>Gets the application domain name.</returns>
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x0009AC98 File Offset: 0x00098E98
		public string ApplicationDomain
		{
			get
			{
				return this.application_domain;
			}
		}

		/// <summary>Gets the application physical path.</summary>
		/// <returns>The application physical path.</returns>
		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x0009ACA0 File Offset: 0x00098EA0
		public string ApplicationPath
		{
			get
			{
				return this.application_path;
			}
		}

		/// <summary>Gets the application logical path.</summary>
		/// <returns>The application logical path.</returns>
		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x0009ACA8 File Offset: 0x00098EA8
		public string ApplicationVirtualPath
		{
			get
			{
				return this.application_virtual_path;
			}
		}

		/// <summary>Gets the application machine name.</summary>
		/// <returns>The name of the machine where the application is running.</returns>
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x0009ACB0 File Offset: 0x00098EB0
		public string MachineName
		{
			get
			{
				return this.machine_name;
			}
		}

		/// <summary>Gets the application trust level.</summary>
		/// <returns>The application trust level.</returns>
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x0009ACB8 File Offset: 0x00098EB8
		public string TrustLevel
		{
			get
			{
				return this.trust_level;
			}
		}

		/// <summary>Formats the application information.</summary>
		/// <param name="formatter">The <see cref="T:System.Web.Management.WebEventFormatter" /> that contains the tab and indentation settings used to format the Web health event information.</param>
		// Token: 0x060039FC RID: 14844 RVA: 0x00003A2B File Offset: 0x00001C2B
		public void FormatToString(WebEventFormatter formatter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x060039FD RID: 14845 RVA: 0x00003A2B File Offset: 0x00001C2B
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001F69 RID: 8041
		private string application_domain;

		// Token: 0x04001F6A RID: 8042
		private string application_path;

		// Token: 0x04001F6B RID: 8043
		private string application_virtual_path;

		// Token: 0x04001F6C RID: 8044
		private string machine_name;

		// Token: 0x04001F6D RID: 8045
		private string trust_level;
	}
}
