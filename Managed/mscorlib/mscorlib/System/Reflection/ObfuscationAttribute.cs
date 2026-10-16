using System;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x020008B4 RID: 2228
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>true if an obfuscation tool should remove the attribute after processing; otherwise, false. The default is true.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x000EF227 File Offset: 0x000ED427
		// (set) Token: 0x060049BB RID: 18875 RVA: 0x000EF22F File Offset: 0x000ED42F
		public bool StripAfterObfuscation { get; set; } = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>true if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, false. The default is true.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x000EF238 File Offset: 0x000ED438
		// (set) Token: 0x060049BD RID: 18877 RVA: 0x000EF240 File Offset: 0x000ED440
		public bool Exclude { get; set; } = true;

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>true if the attribute is to apply to the members of the type; otherwise, false. The default is true.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x000EF249 File Offset: 0x000ED449
		// (set) Token: 0x060049BF RID: 18879 RVA: 0x000EF251 File Offset: 0x000ED451
		public bool ApplyToMembers { get; set; } = true;

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options. </summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060049C0 RID: 18880 RVA: 0x000EF25A File Offset: 0x000ED45A
		// (set) Token: 0x060049C1 RID: 18881 RVA: 0x000EF262 File Offset: 0x000ED462
		public string Feature { get; set; } = "all";
	}
}
