using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004FB RID: 1275
	internal struct NamespaceExtent : IEquatable<NamespaceExtent>
	{
		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x06006BEF RID: 27631 RVA: 0x0018F8AB File Offset: 0x0018DAAB
		public NamespaceKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06006BF0 RID: 27632 RVA: 0x0018F8B3 File Offset: 0x0018DAB3
		public ModuleSymbol Module
		{
			get
			{
				if (this._kind == NamespaceKind.Module)
				{
					return (ModuleSymbol)this._symbolOrCompilation;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x0018F8CF File Offset: 0x0018DACF
		public AssemblySymbol Assembly
		{
			get
			{
				if (this._kind == NamespaceKind.Assembly)
				{
					return (AssemblySymbol)this._symbolOrCompilation;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06006BF2 RID: 27634 RVA: 0x0018F8EB File Offset: 0x0018DAEB
		public CSharpCompilation Compilation
		{
			get
			{
				if (this._kind == NamespaceKind.Compilation)
				{
					return (CSharpCompilation)this._symbolOrCompilation;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x0018F907 File Offset: 0x0018DB07
		public override string ToString()
		{
			return string.Format("{0}: {1}", this._kind, this._symbolOrCompilation);
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x0018F924 File Offset: 0x0018DB24
		internal NamespaceExtent(ModuleSymbol module)
		{
			this._kind = NamespaceKind.Module;
			this._symbolOrCompilation = module;
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x0018F934 File Offset: 0x0018DB34
		internal NamespaceExtent(AssemblySymbol assembly)
		{
			this._kind = NamespaceKind.Assembly;
			this._symbolOrCompilation = assembly;
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x0018F944 File Offset: 0x0018DB44
		internal NamespaceExtent(CSharpCompilation compilation)
		{
			this._kind = NamespaceKind.Compilation;
			this._symbolOrCompilation = compilation;
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x0018F954 File Offset: 0x0018DB54
		public override bool Equals(object obj)
		{
			return obj is NamespaceExtent && this.Equals((NamespaceExtent)obj);
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x0018F96C File Offset: 0x0018DB6C
		public bool Equals(NamespaceExtent other)
		{
			return object.Equals(this._symbolOrCompilation, other._symbolOrCompilation);
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x0018F97F File Offset: 0x0018DB7F
		public override int GetHashCode()
		{
			if (this._symbolOrCompilation != null)
			{
				return this._symbolOrCompilation.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04001ABD RID: 6845
		private readonly NamespaceKind _kind;

		// Token: 0x04001ABE RID: 6846
		private readonly object _symbolOrCompilation;
	}
}
