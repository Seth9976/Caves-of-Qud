using System;
using System.Reflection;

namespace System.Web.UI
{
	/// <summary>Acts as the base class for all property entry classes.</summary>
	// Token: 0x02000219 RID: 537
	public abstract class PropertyEntry
	{
		// Token: 0x06001607 RID: 5639 RVA: 0x00002050 File Offset: 0x00000250
		internal PropertyEntry()
		{
		}

		/// <summary>Gets the type of the class that declares this member.</summary>
		/// <returns>The <see cref="T:System.Type" /> that declares this member.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00039073 File Offset: 0x00037273
		public Type DeclaringType
		{
			get
			{
				return this.pinfo.DeclaringType;
			}
		}

		/// <summary>Gets or sets the value pertaining to the filter portion of an expression.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the filter on an expression.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00039080 File Offset: 0x00037280
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x00039088 File Offset: 0x00037288
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets the property name that the expression applies to.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the property name.</returns>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00039091 File Offset: 0x00037291
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00039099 File Offset: 0x00037299
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets an object containing attributes of the property the expression applies to.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> containing the attributes of the property.</returns>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000390A2 File Offset: 0x000372A2
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x000390AA File Offset: 0x000372AA
		public PropertyInfo PropertyInfo
		{
			get
			{
				return this.pinfo;
			}
			set
			{
				this.pinfo = value;
			}
		}

		/// <summary>Gets or sets the type of the entry.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the entry.</returns>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000390B3 File Offset: 0x000372B3
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x000390BB File Offset: 0x000372BB
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04001556 RID: 5462
		private Type type;

		// Token: 0x04001557 RID: 5463
		private string name;

		// Token: 0x04001558 RID: 5464
		private string filter;

		// Token: 0x04001559 RID: 5465
		private PropertyInfo pinfo;
	}
}
