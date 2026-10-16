using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that ASP.NET server controls use to identify a validation property. This class cannot be inherited.</summary>
	// Token: 0x02000240 RID: 576
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ValidationPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the validation property. </param>
		// Token: 0x060017B2 RID: 6066 RVA: 0x0003E2F6 File Offset: 0x0003C4F6
		public ValidationPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the ASP.NET server control's validation property.</summary>
		/// <returns>The name of the validation property.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0003E305 File Offset: 0x0003C505
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040015F7 RID: 5623
		private string name;
	}
}
