using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies whether a type is typically used for binding.</summary>
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
	public sealed class BindableTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.BindableTypeAttribute" /> class.</summary>
		// Token: 0x06000021 RID: 33 RVA: 0x0000251A File Offset: 0x0000071A
		public BindableTypeAttribute()
		{
			this.IsBindable = true;
		}

		/// <summary>Gets a value indicating that a type is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002529 File Offset: 0x00000729
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002531 File Offset: 0x00000731
		public bool IsBindable { get; set; }
	}
}
