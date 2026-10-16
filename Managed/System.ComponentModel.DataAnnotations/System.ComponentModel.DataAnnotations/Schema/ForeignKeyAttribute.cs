using System;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Denotes a property used as a foreign key in a relationship.</summary>
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ForeignKeyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute" /> class.</summary>
		/// <param name="name">The name of the associated foreign key.</param>
		// Token: 0x060001B7 RID: 439 RVA: 0x00005E19 File Offset: 0x00004019
		public ForeignKeyAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The argument '{0}' cannot be null, empty or contain only white space.", "name"));
			}
			this._name = name;
		}

		/// <summary>Gets the name of the associated foreign key.</summary>
		/// <returns>The name of the associated foreign key.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005E4A File Offset: 0x0000404A
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000119 RID: 281
		private readonly string _name;
	}
}
