using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200002A RID: 42
	internal sealed class ImplicitlyTypedFieldBinder : Binder
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00038753 File Offset: 0x00036953
		public ImplicitlyTypedFieldBinder(Binder next, ConsList<FieldSymbol> fieldsBeingBound)
			: base(next, next.Flags)
		{
			this._fieldsBeingBound = fieldsBeingBound;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00038769 File Offset: 0x00036969
		internal override ConsList<FieldSymbol> FieldsBeingBound
		{
			get
			{
				return this._fieldsBeingBound;
			}
		}

		// Token: 0x04000079 RID: 121
		private readonly ConsList<FieldSymbol> _fieldsBeingBound;
	}
}
