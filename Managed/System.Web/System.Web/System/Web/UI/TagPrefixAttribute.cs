using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the tag prefix used in a Web page to identify custom controls. This class cannot be inherited.</summary>
	// Token: 0x02000228 RID: 552
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TagPrefixAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TagPrefixAttribute" /> class.</summary>
		/// <param name="namespaceName">A string that identifies the custom control namespace. </param>
		/// <param name="tagPrefix">A string that identifies the custom control prefix. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="namespaceName" /> or the <paramref name="tagPrefix" /> is null or an empty string ("").</exception>
		// Token: 0x060016AF RID: 5807 RVA: 0x0003AE48 File Offset: 0x00039048
		public TagPrefixAttribute(string namespaceName, string tagPrefix)
		{
			if (namespaceName == null || namespaceName.Length == 0)
			{
				throw new ArgumentNullException("namespaceName");
			}
			if (tagPrefix == null || tagPrefix.Length == 0)
			{
				throw new ArgumentNullException("tagPrefix");
			}
			this.namespaceName = namespaceName;
			this.tagPrefix = tagPrefix;
		}

		/// <summary>Gets the namespace prefix for the specified control.</summary>
		/// <returns>The namespace name.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0003AE95 File Offset: 0x00039095
		public string NamespaceName
		{
			get
			{
				return this.namespaceName;
			}
		}

		/// <summary>Gets the tag prefix for the specified control.</summary>
		/// <returns>The tag prefix.</returns>
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0003AE9D File Offset: 0x0003909D
		public string TagPrefix
		{
			get
			{
				return this.tagPrefix;
			}
		}

		// Token: 0x0400157D RID: 5501
		private string namespaceName;

		// Token: 0x0400157E RID: 5502
		private string tagPrefix;
	}
}
