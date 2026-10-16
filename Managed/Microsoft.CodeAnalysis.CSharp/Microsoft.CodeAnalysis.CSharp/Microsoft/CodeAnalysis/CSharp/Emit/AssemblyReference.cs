using System;
using System.Collections.Generic;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000486 RID: 1158
	internal sealed class AssemblyReference : IAssemblyReference, IModuleReference, IUnitReference, IReference, INamedEntity
	{
		// Token: 0x0600608D RID: 24717 RVA: 0x0017549F File Offset: 0x0017369F
		internal AssemblyReference(AssemblySymbol assemblySymbol)
		{
			this._targetAssembly = assemblySymbol;
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x0600608E RID: 24718 RVA: 0x001754AE File Offset: 0x001736AE
		public AssemblyIdentity Identity
		{
			get
			{
				return this._targetAssembly.Identity;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x001754BB File Offset: 0x001736BB
		public Version AssemblyVersionPattern
		{
			get
			{
				return this._targetAssembly.AssemblyVersionPattern;
			}
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x001754C8 File Offset: 0x001736C8
		public override string ToString()
		{
			return this._targetAssembly.ToString();
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001754D5 File Offset: 0x001736D5
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06006092 RID: 24722 RVA: 0x001754DE File Offset: 0x001736DE
		string INamedEntity.Name
		{
			get
			{
				return this.Identity.Name;
			}
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x001754EB File Offset: 0x001736EB
		IAssemblyReference IModuleReference.GetContainingAssembly(EmitContext context)
		{
			return this;
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001754EE File Offset: 0x001736EE
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001754F5 File Offset: 0x001736F5
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x001754F8 File Offset: 0x001736F8
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x04001954 RID: 6484
		private readonly AssemblySymbol _targetAssembly;
	}
}
