using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F7 RID: 1271
	internal sealed class MissingModuleSymbolWithName : MissingModuleSymbol
	{
		// Token: 0x06006BB1 RID: 27569 RVA: 0x0018F548 File Offset: 0x0018D748
		public MissingModuleSymbolWithName(AssemblySymbol assembly, string name)
			: base(assembly, -1)
		{
			this._name = name;
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x06006BB2 RID: 27570 RVA: 0x0018F559 File Offset: 0x0018D759
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x0018F561 File Offset: 0x0018D761
		public override int GetHashCode()
		{
			return Hash.Combine(this.assembly.GetHashCode(), StringComparer.OrdinalIgnoreCase.GetHashCode(this._name));
		}

		// Token: 0x06006BB4 RID: 27572 RVA: 0x0018F584 File Offset: 0x0018D784
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			MissingModuleSymbolWithName missingModuleSymbolWithName = obj as MissingModuleSymbolWithName;
			return missingModuleSymbolWithName != null && this.assembly.Equals(missingModuleSymbolWithName.assembly, compareKind) && string.Equals(this._name, missingModuleSymbolWithName._name, 5);
		}

		// Token: 0x04001ABA RID: 6842
		private readonly string _name;
	}
}
