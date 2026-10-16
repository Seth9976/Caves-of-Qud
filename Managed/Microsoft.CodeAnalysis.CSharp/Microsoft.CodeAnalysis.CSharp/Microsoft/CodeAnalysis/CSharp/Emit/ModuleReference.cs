using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048D RID: 1165
	internal sealed class ModuleReference : IModuleReference, IUnitReference, IReference, INamedEntity, IFileReference
	{
		// Token: 0x060060D7 RID: 24791 RVA: 0x001759FD File Offset: 0x00173BFD
		internal ModuleReference(PEModuleBuilder moduleBeingBuilt, ModuleSymbol underlyingModule)
		{
			this._moduleBeingBuilt = moduleBeingBuilt;
			this._underlyingModule = underlyingModule;
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x00175A13 File Offset: 0x00173C13
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x060060D9 RID: 24793 RVA: 0x00175A1C File Offset: 0x00173C1C
		string INamedEntity.Name
		{
			get
			{
				return this._underlyingModule.MetadataName;
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x00175A29 File Offset: 0x00173C29
		bool IFileReference.HasMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x00175A2C File Offset: 0x00173C2C
		string IFileReference.FileName
		{
			get
			{
				return this._underlyingModule.Name;
			}
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x00175A39 File Offset: 0x00173C39
		ImmutableArray<byte> IFileReference.GetHashValue(AssemblyHashAlgorithm algorithmId)
		{
			return this._underlyingModule.GetHash(algorithmId);
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x00175A48 File Offset: 0x00173C48
		IAssemblyReference IModuleReference.GetContainingAssembly(EmitContext context)
		{
			if (this._moduleBeingBuilt.OutputKind.IsNetModule() && this._moduleBeingBuilt.SourceModule.ContainingAssembly == this._underlyingModule.ContainingAssembly)
			{
				return null;
			}
			return this._moduleBeingBuilt.Translate(this._underlyingModule.ContainingAssembly, context.Diagnostics);
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x00175AA2 File Offset: 0x00173CA2
		public override string ToString()
		{
			return this._underlyingModule.ToString();
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x00175AAF File Offset: 0x00173CAF
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x00175AB6 File Offset: 0x00173CB6
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x00175AB9 File Offset: 0x00173CB9
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x04001958 RID: 6488
		private readonly PEModuleBuilder _moduleBeingBuilt;

		// Token: 0x04001959 RID: 6489
		private readonly ModuleSymbol _underlyingModule;
	}
}
