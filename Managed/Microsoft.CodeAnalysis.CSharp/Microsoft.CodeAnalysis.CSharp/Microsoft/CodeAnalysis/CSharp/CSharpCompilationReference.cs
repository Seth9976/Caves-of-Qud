using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000105 RID: 261
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class CSharpCompilationReference : CompilationReference
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0006FA06 File Offset: 0x0006DC06
		public new CSharpCompilation Compilation { get; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0006FA0E File Offset: 0x0006DC0E
		internal override Compilation CompilationCore
		{
			get
			{
				return this.Compilation;
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0006FA16 File Offset: 0x0006DC16
		public CSharpCompilationReference(CSharpCompilation compilation, ImmutableArray<string> aliases = default(ImmutableArray<string>), bool embedInteropTypes = false)
			: base(CompilationReference.GetProperties(compilation, aliases, embedInteropTypes))
		{
			this.Compilation = compilation;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0006FA2D File Offset: 0x0006DC2D
		private CSharpCompilationReference(CSharpCompilation compilation, MetadataReferenceProperties properties)
			: base(properties)
		{
			this.Compilation = compilation;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0006FA3D File Offset: 0x0006DC3D
		internal override CompilationReference WithPropertiesImpl(MetadataReferenceProperties properties)
		{
			return new CSharpCompilationReference(this.Compilation, properties);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0006FA4B File Offset: 0x0006DC4B
		private string GetDebuggerDisplay()
		{
			return CSharpResources.CompilationC + this.Compilation.AssemblyName;
		}
	}
}
