using System;

namespace System.Reflection
{
	/// <summary>Defines a key/value metadata pair for the decorated assembly.</summary>
	// Token: 0x0200088B RID: 2187
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> class by using the specified metadata key and value.</summary>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		// Token: 0x0600486F RID: 18543 RVA: 0x000EE172 File Offset: 0x000EC372
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		/// <summary>Gets the metadata key.</summary>
		/// <returns>The metadata key.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x000EE188 File Offset: 0x000EC388
		public string Key { get; }

		/// <summary>Gets the metadata value.</summary>
		/// <returns>The metadata value.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x000EE190 File Offset: 0x000EC390
		public string Value { get; }
	}
}
