using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E9 RID: 1513
	internal sealed class PEGlobalNamespaceSymbol : PENamespaceSymbol
	{
		// Token: 0x06007E3D RID: 32317 RVA: 0x001C5E18 File Offset: 0x001C4018
		internal PEGlobalNamespaceSymbol(PEModuleSymbol moduleSymbol)
		{
			this._moduleSymbol = moduleSymbol;
		}

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x06007E3E RID: 32318 RVA: 0x001C5E27 File Offset: 0x001C4027
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._moduleSymbol;
			}
		}

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x06007E3F RID: 32319 RVA: 0x001C5E2F File Offset: 0x001C402F
		internal override PEModuleSymbol ContainingPEModule
		{
			get
			{
				return this._moduleSymbol;
			}
		}

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x06007E40 RID: 32320 RVA: 0x001C5E37 File Offset: 0x001C4037
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x06007E41 RID: 32321 RVA: 0x001C5E3E File Offset: 0x001C403E
		public override bool IsGlobalNamespace
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x06007E42 RID: 32322 RVA: 0x001C5E41 File Offset: 0x001C4041
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._moduleSymbol.ContainingAssembly;
			}
		}

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x06007E43 RID: 32323 RVA: 0x001C5E4E File Offset: 0x001C404E
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._moduleSymbol;
			}
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x001C5E58 File Offset: 0x001C4058
		protected override void EnsureAllMembersLoaded()
		{
			if (this.lazyTypes == null || this.lazyNamespaces == null)
			{
				IEnumerable<IGrouping<string, TypeDefinitionHandle>> enumerable;
				try
				{
					enumerable = this._moduleSymbol.Module.GroupTypesByNamespaceOrThrow(StringComparer.Ordinal);
				}
				catch (BadImageFormatException)
				{
					enumerable = SpecializedCollections.EmptyEnumerable<IGrouping<string, TypeDefinitionHandle>>();
				}
				base.LoadAllMembers(enumerable);
			}
		}

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x06007E45 RID: 32325 RVA: 0x001C5EB0 File Offset: 0x001C40B0
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001DCA RID: 7626
		private readonly PEModuleSymbol _moduleSymbol;
	}
}
