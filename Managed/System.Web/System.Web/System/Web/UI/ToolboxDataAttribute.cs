using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Specifies the default tag generated for a custom control when it is dragged from a toolbox in a tool such as Microsoft Visual Studio.</summary>
	// Token: 0x02000237 RID: 567
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ToolboxDataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> class. </summary>
		/// <param name="data">The string to be set as the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" />.</param>
		// Token: 0x0600176E RID: 5998 RVA: 0x0003DBFB File Offset: 0x0003BDFB
		public ToolboxDataAttribute(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the string representing the initial values of the control's property, which is used in a visual designer for creating an instance of the control.</summary>
		/// <returns>A string representing the initial values for this attribute.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0003DC0A File Offset: 0x0003BE0A
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object is equal to the given object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to.</param>
		// Token: 0x06001770 RID: 6000 RVA: 0x0003DC14 File Offset: 0x0003BE14
		public override bool Equals(object obj)
		{
			ToolboxDataAttribute toolboxDataAttribute = obj as ToolboxDataAttribute;
			return toolboxDataAttribute != null && toolboxDataAttribute.Data == this.data;
		}

		/// <summary>Returns the hash code of the custom control.</summary>
		/// <returns>A 32-bit signed integer representing the hash code.</returns>
		// Token: 0x06001771 RID: 6001 RVA: 0x0003DC3E File Offset: 0x0003BE3E
		public override int GetHashCode()
		{
			if (this.data == null)
			{
				return -1;
			}
			return this.data.GetHashCode();
		}

		/// <summary>Tests whether the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> object contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property.</summary>
		/// <returns>true, if the <see cref="T:System.Web.UI.ToolboxDataAttribute" /> contains the default value for the <see cref="P:System.Web.UI.ToolboxDataAttribute.Data" /> property; otherwise, false.</returns>
		// Token: 0x06001772 RID: 6002 RVA: 0x0003DC55 File Offset: 0x0003BE55
		public override bool IsDefaultAttribute()
		{
			return this.data == null || this.data.Length == 0;
		}

		/// <summary>Represents the default <see cref="T:System.Web.UI.ToolboxDataAttribute" /> value for a custom control.</summary>
		// Token: 0x040015E4 RID: 5604
		public static readonly ToolboxDataAttribute Default = new ToolboxDataAttribute(string.Empty);

		// Token: 0x040015E5 RID: 5605
		private string data;
	}
}
