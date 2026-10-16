using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005EE RID: 1518
	internal sealed class PENestedNamespaceSymbol : PENamespaceSymbol
	{
		// Token: 0x06007F57 RID: 32599 RVA: 0x001CA5ED File Offset: 0x001C87ED
		internal PENestedNamespaceSymbol(string name, PENamespaceSymbol containingNamespace, IEnumerable<IGrouping<string, TypeDefinitionHandle>> typesByNS)
		{
			this._containingNamespaceSymbol = containingNamespace;
			this._name = name;
			this._typesByNS = typesByNS;
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x06007F58 RID: 32600 RVA: 0x001CA60A File Offset: 0x001C880A
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingNamespaceSymbol;
			}
		}

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x06007F59 RID: 32601 RVA: 0x001CA612 File Offset: 0x001C8812
		internal override PEModuleSymbol ContainingPEModule
		{
			get
			{
				return this._containingNamespaceSymbol.ContainingPEModule;
			}
		}

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x06007F5A RID: 32602 RVA: 0x001CA61F File Offset: 0x001C881F
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x001CA627 File Offset: 0x001C8827
		public override bool IsGlobalNamespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x06007F5C RID: 32604 RVA: 0x001CA62A File Offset: 0x001C882A
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this.ContainingPEModule.ContainingAssembly;
			}
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x06007F5D RID: 32605 RVA: 0x001CA637 File Offset: 0x001C8837
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._containingNamespaceSymbol.ContainingPEModule;
			}
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x001CA644 File Offset: 0x001C8844
		protected override void EnsureAllMembersLoaded()
		{
			IEnumerable<IGrouping<string, TypeDefinitionHandle>> typesByNS = this._typesByNS;
			if (this.lazyTypes == null || this.lazyNamespaces == null)
			{
				base.LoadAllMembers(typesByNS);
				Interlocked.Exchange<IEnumerable<IGrouping<string, TypeDefinitionHandle>>>(ref this._typesByNS, null);
			}
		}

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x06007F5F RID: 32607 RVA: 0x001CA67C File Offset: 0x001C887C
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001E00 RID: 7680
		private readonly PENamespaceSymbol _containingNamespaceSymbol;

		// Token: 0x04001E01 RID: 7681
		private readonly string _name;

		// Token: 0x04001E02 RID: 7682
		private IEnumerable<IGrouping<string, TypeDefinitionHandle>> _typesByNS;
	}
}
