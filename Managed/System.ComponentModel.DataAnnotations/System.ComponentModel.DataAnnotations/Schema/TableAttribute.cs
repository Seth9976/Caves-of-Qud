using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Specifies the database table that a class is mapped to.</summary>
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.TableAttribute" /> class using the specified name of the table.</summary>
		/// <param name="name">The name of the table.</param>
		// Token: 0x060001BC RID: 444 RVA: 0x00005E8B File Offset: 0x0000408B
		public TableAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "name"));
			}
			this._name = name;
		}

		/// <summary>Gets the name of the table the class is mapped to.</summary>
		/// <returns>The name of the table the class is mapped to.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005EBC File Offset: 0x000040BC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets or sets the schema of the table the class is mapped to.</summary>
		/// <returns>The schema of the table the class is mapped to.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005EC4 File Offset: 0x000040C4
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00005ECC File Offset: 0x000040CC
		public string Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "value"));
				}
				this._schema = value;
			}
		}

		// Token: 0x0400011B RID: 283
		private readonly string _name;

		// Token: 0x0400011C RID: 284
		private string _schema;
	}
}
