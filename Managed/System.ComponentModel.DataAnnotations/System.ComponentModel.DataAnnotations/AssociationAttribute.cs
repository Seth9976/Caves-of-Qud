using System;
using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that an entity member represents a data relationship, such as a foreign key relationship.</summary>
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class AssociationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.AssociationAttribute" /> class.</summary>
		/// <param name="name">The name of the association. </param>
		/// <param name="thisKey">A comma-separated list of the property names of the key values on the <paramref name="thisKey" /> side of the association.</param>
		/// <param name="otherKey">A comma-separated list of the property names of the key values on the <paramref name="otherKey" /> side of the association.</param>
		// Token: 0x06000018 RID: 24 RVA: 0x000024A0 File Offset: 0x000006A0
		public AssociationAttribute(string name, string thisKey, string otherKey)
		{
			this.name = name;
			this.thisKey = thisKey;
			this.otherKey = otherKey;
		}

		/// <summary>Gets the name of the association.</summary>
		/// <returns>The name of the association.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024BD File Offset: 0x000006BD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the property names of the key values on the ThisKey side of the association.</summary>
		/// <returns>A comma-separated list of the property names that represent the key values on the ThisKey side of the association.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024C5 File Offset: 0x000006C5
		public string ThisKey
		{
			get
			{
				return this.thisKey;
			}
		}

		/// <summary>Gets the property names of the key values on the OtherKey side of the association.</summary>
		/// <returns>A comma-separated list of the property names that represent the key values on the OtherKey side of the association.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024CD File Offset: 0x000006CD
		public string OtherKey
		{
			get
			{
				return this.otherKey;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the association member represents a foreign key.</summary>
		/// <returns>true if the association represents a foreign key; otherwise, false.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024D5 File Offset: 0x000006D5
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000024DD File Offset: 0x000006DD
		public bool IsForeignKey
		{
			get
			{
				return this.isForeignKey;
			}
			set
			{
				this.isForeignKey = value;
			}
		}

		/// <summary>Gets a collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.ThisKey" /> property.</summary>
		/// <returns>A collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.ThisKey" /> property.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024E6 File Offset: 0x000006E6
		public IEnumerable<string> ThisKeyMembers
		{
			get
			{
				return AssociationAttribute.GetKeyMembers(this.ThisKey);
			}
		}

		/// <summary>Gets a collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.OtherKey" /> property.</summary>
		/// <returns>A collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.OtherKey" /> property.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024F3 File Offset: 0x000006F3
		public IEnumerable<string> OtherKeyMembers
		{
			get
			{
				return AssociationAttribute.GetKeyMembers(this.OtherKey);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002500 File Offset: 0x00000700
		private static string[] GetKeyMembers(string key)
		{
			return key.Replace(" ", string.Empty).Split(',', StringSplitOptions.None);
		}

		// Token: 0x04000036 RID: 54
		private string name;

		// Token: 0x04000037 RID: 55
		private string thisKey;

		// Token: 0x04000038 RID: 56
		private string otherKey;

		// Token: 0x04000039 RID: 57
		private bool isForeignKey;
	}
}
