using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D3 RID: 211
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ContractAnnotationAttribute : Attribute
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0000857D File Offset: 0x0000677D
		public ContractAnnotationAttribute([NotNull] string contract)
			: this(contract, false)
		{
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00008589 File Offset: 0x00006789
		public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
		{
			this.Contract = contract;
			this.ForceFullStates = forceFullStates;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000085A1 File Offset: 0x000067A1
		[NotNull]
		public string Contract { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000085A9 File Offset: 0x000067A9
		public bool ForceFullStates { get; }
	}
}
