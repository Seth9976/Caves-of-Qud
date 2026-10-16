using System;

namespace System.Diagnostics.Contracts
{
	/// <summary>Instructs analysis tools to assume the correctness of an assembly, type, or member without performing static verification.</summary>
	// Token: 0x020009CA RID: 2506
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractVerificationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractVerificationAttribute" /> class. </summary>
		/// <param name="value">true to require verification; otherwise, false. </param>
		// Token: 0x06005A06 RID: 23046 RVA: 0x00133D99 File Offset: 0x00131F99
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		/// <summary>Gets the value that indicates whether to verify the contract of the target. </summary>
		/// <returns>true if verification is required; otherwise, false.</returns>
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005A07 RID: 23047 RVA: 0x00133DA8 File Offset: 0x00131FA8
		public bool Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040037A5 RID: 14245
		private bool _value;
	}
}
