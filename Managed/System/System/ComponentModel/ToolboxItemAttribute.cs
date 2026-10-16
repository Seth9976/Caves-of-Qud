using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Represents an attribute of a toolbox item.</summary>
	// Token: 0x020003A5 RID: 933
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06001E6C RID: 7788 RVA: 0x0006BF55 File Offset: 0x0006A155
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and specifies whether to use default initialization values.</summary>
		/// <param name="defaultType">true to create a toolbox item attribute for a default type; false to associate no default toolbox item support for this attribute. </param>
		// Token: 0x06001E6D RID: 7789 RVA: 0x0006BF62 File Offset: 0x0006A162
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this._toolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified name of the type.</summary>
		/// <param name="toolboxItemTypeName">The names of the type of the toolbox item and of the assembly that contains the type. </param>
		// Token: 0x06001E6E RID: 7790 RVA: 0x0006BF78 File Offset: 0x0006A178
		public ToolboxItemAttribute(string toolboxItemTypeName)
		{
			toolboxItemTypeName.ToUpper(CultureInfo.InvariantCulture);
			this._toolboxItemTypeName = toolboxItemTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified type of the toolbox item.</summary>
		/// <param name="toolboxItemType">The type of the toolbox item. </param>
		// Token: 0x06001E6F RID: 7791 RVA: 0x0006BF93 File Offset: 0x0006A193
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this._toolboxItemType = toolboxItemType;
			this._toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
		}

		/// <summary>Gets or sets the type of the toolbox item.</summary>
		/// <returns>The type of the toolbox item.</returns>
		/// <exception cref="T:System.ArgumentException">The type cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0006BFB0 File Offset: 0x0006A1B0
		public Type ToolboxItemType
		{
			get
			{
				if (this._toolboxItemType == null && this._toolboxItemTypeName != null)
				{
					try
					{
						this._toolboxItemType = Type.GetType(this._toolboxItemTypeName, true);
					}
					catch (Exception ex)
					{
						throw new ArgumentException(SR.Format("Failed to create ToolboxItem of type: {0}", this._toolboxItemTypeName), ex);
					}
				}
				return this._toolboxItemType;
			}
		}

		/// <summary>Gets or sets the name of the type of the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>The fully qualified type name of the current toolbox item.</returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x0006C018 File Offset: 0x0006A218
		public string ToolboxItemTypeName
		{
			get
			{
				if (this._toolboxItemTypeName == null)
				{
					return string.Empty;
				}
				return this._toolboxItemTypeName;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06001E72 RID: 7794 RVA: 0x0006C030 File Offset: 0x0006A230
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemAttribute toolboxItemAttribute = obj as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0006C060 File Offset: 0x0006A260
		public override int GetHashCode()
		{
			if (this._toolboxItemTypeName != null)
			{
				return this._toolboxItemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04000F50 RID: 3920
		private Type _toolboxItemType;

		// Token: 0x04000F51 RID: 3921
		private string _toolboxItemTypeName;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to the default, <see cref="T:System.Drawing.Design.ToolboxItem" />. This field is read-only.</summary>
		// Token: 0x04000F52 RID: 3922
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to null. This field is read-only.</summary>
		// Token: 0x04000F53 RID: 3923
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);
	}
}
