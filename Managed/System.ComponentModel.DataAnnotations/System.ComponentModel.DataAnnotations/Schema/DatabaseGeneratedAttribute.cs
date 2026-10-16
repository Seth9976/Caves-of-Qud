using System;

namespace System.ComponentModel.DataAnnotations.Schema
{
	/// <summary>Represents a database generated attribute.</summary>
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class DatabaseGeneratedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute" /> class.</summary>
		/// <param name="databaseGeneratedOption">The database generated option.</param>
		// Token: 0x060001B4 RID: 436 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public DatabaseGeneratedAttribute(DatabaseGeneratedOption databaseGeneratedOption)
		{
			if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			this.DatabaseGeneratedOption = databaseGeneratedOption;
		}

		/// <summary>Gets or sets the database generated option.</summary>
		/// <returns>The database generated option.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005E08 File Offset: 0x00004008
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005E10 File Offset: 0x00004010
		public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }
	}
}
