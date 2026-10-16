using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x020003F3 RID: 1011
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		// Token: 0x060020FA RID: 8442 RVA: 0x00071A67 File Offset: 0x0006FC67
		public ToolboxItemFilterAttribute(string filterString)
			: this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter. </param>
		// Token: 0x060020FB RID: 8443 RVA: 0x00071A71 File Offset: 0x0006FC71
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			this.FilterString = filterString ?? string.Empty;
			this.FilterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x00071A90 File Offset: 0x0006FC90
		public string FilterString { get; }

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x00071A98 File Offset: 0x0006FC98
		public ToolboxItemFilterType FilterType { get; }

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00071AA0 File Offset: 0x0006FCA0
		public override object TypeId
		{
			get
			{
				string text;
				if ((text = this._typeId) == null)
				{
					text = (this._typeId = base.GetType().FullName + this.FilterString);
				}
				return text;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x060020FF RID: 8447 RVA: 0x00071AD8 File Offset: 0x0006FCD8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00071B29 File Offset: 0x0006FD29
		public override int GetHashCode()
		{
			return this.FilterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <returns>true if the specified object has a matching filter string; otherwise, false.</returns>
		/// <param name="obj">The object to test for a matching filter string. </param>
		// Token: 0x06002101 RID: 8449 RVA: 0x00071B38 File Offset: 0x0006FD38
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00071B67 File Offset: 0x0006FD67
		public override string ToString()
		{
			return this.FilterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.FilterType);
		}

		// Token: 0x0400100B RID: 4107
		private string _typeId;
	}
}
