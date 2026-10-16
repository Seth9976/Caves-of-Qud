using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000128 RID: 296
	internal sealed class NamespaceData
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
		public NamespaceData(StringHandle name, [Nullable(1)] string fullName, NamespaceDefinitionHandle parent, ImmutableArray<NamespaceDefinitionHandle> namespaceDefinitions, ImmutableArray<TypeDefinitionHandle> typeDefinitions, ImmutableArray<ExportedTypeHandle> exportedTypes)
		{
			this.Name = name;
			this.FullName = fullName;
			this.Parent = parent;
			this.NamespaceDefinitions = namespaceDefinitions;
			this.TypeDefinitions = typeDefinitions;
			this.ExportedTypes = exportedTypes;
		}

		// Token: 0x04000738 RID: 1848
		public readonly StringHandle Name;

		// Token: 0x04000739 RID: 1849
		[Nullable(1)]
		public readonly string FullName;

		// Token: 0x0400073A RID: 1850
		public readonly NamespaceDefinitionHandle Parent;

		// Token: 0x0400073B RID: 1851
		public readonly ImmutableArray<NamespaceDefinitionHandle> NamespaceDefinitions;

		// Token: 0x0400073C RID: 1852
		public readonly ImmutableArray<TypeDefinitionHandle> TypeDefinitions;

		// Token: 0x0400073D RID: 1853
		public readonly ImmutableArray<ExportedTypeHandle> ExportedTypes;
	}
}
