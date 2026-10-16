using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000016 RID: 22
	internal sealed class ConstantFieldsInProgress
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x000333B2 File Offset: 0x000315B2
		internal ConstantFieldsInProgress(SourceFieldSymbol fieldOpt, HashSet<SourceFieldSymbolWithSyntaxReference> dependencies)
		{
			this._fieldOpt = fieldOpt;
			this._dependencies = dependencies;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000333C8 File Offset: 0x000315C8
		public bool IsEmpty
		{
			get
			{
				return this._fieldOpt == null;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000333D3 File Offset: 0x000315D3
		internal void AddDependency(SourceFieldSymbolWithSyntaxReference field)
		{
			this._dependencies.Add(field);
		}

		// Token: 0x04000046 RID: 70
		private readonly SourceFieldSymbol _fieldOpt;

		// Token: 0x04000047 RID: 71
		private readonly HashSet<SourceFieldSymbolWithSyntaxReference> _dependencies;

		// Token: 0x04000048 RID: 72
		internal static readonly ConstantFieldsInProgress Empty = new ConstantFieldsInProgress(null, null);
	}
}
