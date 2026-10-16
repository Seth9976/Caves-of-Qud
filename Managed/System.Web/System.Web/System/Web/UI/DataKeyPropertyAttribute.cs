using System;

namespace System.Web.UI
{
	/// <summary>Specifies the default property of a control that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> property binds to at run time.</summary>
	// Token: 0x02000155 RID: 341
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataKeyPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class by using the name of the data-key property attribute.</summary>
		/// <param name="name">The name of the data-key property attribute.</param>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00028751 File Offset: 0x00026951
		public DataKeyPropertyAttribute(string name)
		{
			this._name = name;
		}

		/// <summary>Gets the name of the data-key property attribute.</summary>
		/// <returns>The name of the data-key property attribute.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00028760 File Offset: 0x00026960
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Compares the name of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> object to a specified object.</summary>
		/// <returns>true if the name is the same as the object; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00028768 File Offset: 0x00026968
		public override bool Equals(object obj)
		{
			DataKeyPropertyAttribute dataKeyPropertyAttribute = obj as DataKeyPropertyAttribute;
			return dataKeyPropertyAttribute != null && string.Equals(this._name, dataKeyPropertyAttribute.Name, StringComparison.Ordinal);
		}

		/// <summary>Gets the hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</summary>
		/// <returns>The hash code for an instance of the <see cref="T:System.Web.UI.DataKeyPropertyAttribute" /> class.</returns>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00028793 File Offset: 0x00026993
		public override int GetHashCode()
		{
			if (this.Name == null)
			{
				return 0;
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x0400122A RID: 4650
		private readonly string _name;
	}
}
