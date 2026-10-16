using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Represents a column attribute.</summary>
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ColumnAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> class.</summary>
		// Token: 0x060001AC RID: 428 RVA: 0x00005D35 File Offset: 0x00003F35
		public ColumnAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> class.</summary>
		/// <param name="name">The name of the column attribute.</param>
		// Token: 0x060001AD RID: 429 RVA: 0x00005D44 File Offset: 0x00003F44
		public ColumnAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "name"));
			}
			this._name = name;
		}

		/// <summary>Gets the name of the attribute.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005D7C File Offset: 0x00003F7C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets or sets the order of the column. </summary>
		/// <returns>The order of the column.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005D84 File Offset: 0x00003F84
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005D8C File Offset: 0x00003F8C
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._order = value;
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> represents.</summary>
		/// <returns>The name of the class that the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ColumnAttribute" /> represents.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005DA4 File Offset: 0x00003FA4
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005DAC File Offset: 0x00003FAC
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "value"));
				}
				this._typeName = value;
			}
		}

		// Token: 0x04000111 RID: 273
		private readonly string _name;

		// Token: 0x04000112 RID: 274
		private string _typeName;

		// Token: 0x04000113 RID: 275
		private int _order = -1;
	}
}
