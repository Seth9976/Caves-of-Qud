using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B6 RID: 438
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CompilationReference : MetadataReference, IEquatable<CompilationReference>
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0003923B File Offset: 0x0003743B
		public Compilation Compilation
		{
			get
			{
				return this.CompilationCore;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600126A RID: 4714
		internal abstract Compilation CompilationCore { get; }

		// Token: 0x0600126B RID: 4715 RVA: 0x00039243 File Offset: 0x00037443
		internal CompilationReference(MetadataReferenceProperties properties)
			: base(properties)
		{
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003924C File Offset: 0x0003744C
		internal static MetadataReferenceProperties GetProperties(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases, bool embedInteropTypes)
		{
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			if (compilation.IsSubmission)
			{
				throw new NotSupportedException(CodeAnalysisResources.CannotCreateReferenceToSubmission);
			}
			if (compilation.Options.OutputKind == OutputKind.NetModule)
			{
				throw new NotSupportedException(CodeAnalysisResources.CannotCreateReferenceToModule);
			}
			return new MetadataReferenceProperties(MetadataImageKind.Assembly, aliases, embedInteropTypes);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003929B File Offset: 0x0003749B
		public new CompilationReference WithAliases(IEnumerable<string> aliases)
		{
			return this.WithAliases(ImmutableArray.CreateRange<string>(aliases));
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000392AC File Offset: 0x000374AC
		public new CompilationReference WithAliases([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases)
		{
			return this.WithProperties(base.Properties.WithAliases(aliases));
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000392D0 File Offset: 0x000374D0
		public new CompilationReference WithEmbedInteropTypes(bool value)
		{
			return this.WithProperties(base.Properties.WithEmbedInteropTypes(value));
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000392F2 File Offset: 0x000374F2
		public new CompilationReference WithProperties(MetadataReferenceProperties properties)
		{
			if (properties == base.Properties)
			{
				return this;
			}
			if (properties.Kind == MetadataImageKind.Module)
			{
				throw new ArgumentException(CodeAnalysisResources.CannotCreateReferenceToModule);
			}
			return this.WithPropertiesImpl(properties);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00039320 File Offset: 0x00037520
		internal sealed override MetadataReference WithPropertiesImplReturningMetadataReference(MetadataReferenceProperties properties)
		{
			if (properties.Kind == MetadataImageKind.Module)
			{
				throw new NotSupportedException(CodeAnalysisResources.CannotCreateReferenceToModule);
			}
			return this.WithPropertiesImpl(properties);
		}

		// Token: 0x06001272 RID: 4722
		internal abstract CompilationReference WithPropertiesImpl(MetadataReferenceProperties properties);

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0003933E File Offset: 0x0003753E
		[Nullable(2)]
		public override string Display
		{
			[NullableContext(2)]
			get
			{
				return this.Compilation.AssemblyName;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003934B File Offset: 0x0003754B
		[NullableContext(2)]
		public bool Equals(CompilationReference other)
		{
			return other != null && (this == other || (object.Equals(this.Compilation, other.Compilation) && object.Equals(base.Properties, other.Properties)));
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00039388 File Offset: 0x00037588
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CompilationReference);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00039398 File Offset: 0x00037598
		public override int GetHashCode()
		{
			return Hash.Combine(this.Compilation.GetHashCode(), base.Properties.GetHashCode());
		}
	}
}
