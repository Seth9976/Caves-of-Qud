using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BD RID: 445
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MetadataReferenceResolver
	{
		// Token: 0x060012AB RID: 4779
		[NullableContext(2)]
		public abstract override bool Equals(object other);

		// Token: 0x060012AC RID: 4780
		public abstract override int GetHashCode();

		// Token: 0x060012AD RID: 4781
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<PortableExecutableReference> ResolveReference(string reference, [Nullable(2)] string baseFilePath, MetadataReferenceProperties properties);

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x000399C4 File Offset: 0x00037BC4
		public virtual bool ResolveMissingAssemblies
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000399C7 File Offset: 0x00037BC7
		[return: Nullable(2)]
		public virtual PortableExecutableReference ResolveMissingAssembly(MetadataReference definition, AssemblyIdentity referenceIdentity)
		{
			return null;
		}
	}
}
