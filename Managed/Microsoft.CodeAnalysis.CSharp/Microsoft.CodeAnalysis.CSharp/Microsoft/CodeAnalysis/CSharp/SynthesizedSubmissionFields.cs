using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000215 RID: 533
	internal class SynthesizedSubmissionFields
	{
		// Token: 0x0600261B RID: 9755 RVA: 0x000D0871 File Offset: 0x000CEA71
		public SynthesizedSubmissionFields(CSharpCompilation compilation, NamedTypeSymbol submissionClass)
		{
			this._declaringSubmissionClass = submissionClass;
			this._compilation = compilation;
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000D0887 File Offset: 0x000CEA87
		internal int Count
		{
			get
			{
				if (this._previousSubmissionFieldMap != null)
				{
					return this._previousSubmissionFieldMap.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000D08A0 File Offset: 0x000CEAA0
		internal IEnumerable<FieldSymbol> FieldSymbols
		{
			get
			{
				if (this._previousSubmissionFieldMap != null)
				{
					return this._previousSubmissionFieldMap.Values;
				}
				return Array.Empty<FieldSymbol>();
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000D08CC File Offset: 0x000CEACC
		internal FieldSymbol GetHostObjectField()
		{
			if (this._hostObjectField != null)
			{
				return this._hostObjectField;
			}
			TypeSymbol hostObjectTypeSymbol = this._compilation.GetHostObjectTypeSymbol();
			if (hostObjectTypeSymbol != null && hostObjectTypeSymbol.Kind != SymbolKind.ErrorType)
			{
				return this._hostObjectField = new SynthesizedFieldSymbol(this._declaringSubmissionClass, hostObjectTypeSymbol, "<host-object>", false, true, false);
			}
			return null;
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000D0920 File Offset: 0x000CEB20
		internal FieldSymbol GetOrMakeField(ImplicitNamedTypeSymbol previousSubmissionType)
		{
			if (this._previousSubmissionFieldMap == null)
			{
				this._previousSubmissionFieldMap = new Dictionary<ImplicitNamedTypeSymbol, FieldSymbol>();
			}
			FieldSymbol fieldSymbol;
			if (!this._previousSubmissionFieldMap.TryGetValue(previousSubmissionType, ref fieldSymbol))
			{
				fieldSymbol = new SynthesizedFieldSymbol(this._declaringSubmissionClass, previousSubmissionType, "<" + previousSubmissionType.Name + ">", false, true, false);
				this._previousSubmissionFieldMap.Add(previousSubmissionType, fieldSymbol);
			}
			return fieldSymbol;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000D0984 File Offset: 0x000CEB84
		internal void AddToType(NamedTypeSymbol containingType, PEModuleBuilder moduleBeingBuilt)
		{
			foreach (FieldSymbol fieldSymbol in this.FieldSymbols)
			{
				moduleBeingBuilt.AddSynthesizedDefinition(containingType, fieldSymbol.GetCciAdapter());
			}
			FieldSymbol hostObjectField = this.GetHostObjectField();
			if (hostObjectField != null)
			{
				moduleBeingBuilt.AddSynthesizedDefinition(containingType, hostObjectField.GetCciAdapter());
			}
		}

		// Token: 0x04001118 RID: 4376
		private readonly NamedTypeSymbol _declaringSubmissionClass;

		// Token: 0x04001119 RID: 4377
		private readonly CSharpCompilation _compilation;

		// Token: 0x0400111A RID: 4378
		private FieldSymbol _hostObjectField;

		// Token: 0x0400111B RID: 4379
		private Dictionary<ImplicitNamedTypeSymbol, FieldSymbol> _previousSubmissionFieldMap;
	}
}
