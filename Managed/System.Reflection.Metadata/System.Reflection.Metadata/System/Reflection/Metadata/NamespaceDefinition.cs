using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B1 RID: 177
	public struct NamespaceDefinition
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x00011430 File Offset: 0x0000F630
		[NullableContext(1)]
		internal NamespaceDefinition(NamespaceData data)
		{
			this._data = data;
		}

		/// <summary>Gets the unqualified name of the namespace definition.</summary>
		/// <returns>The unqualified name of the namespace definition.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00011439 File Offset: 0x0000F639
		public StringHandle Name
		{
			get
			{
				return this._data.Name;
			}
		}

		/// <summary>Gets the parent namespace.</summary>
		/// <returns>The parent namespace.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00011446 File Offset: 0x0000F646
		public NamespaceDefinitionHandle Parent
		{
			get
			{
				return this._data.Parent;
			}
		}

		/// <summary>Gets the namespace definitions that are direct children of the current namespace definition.</summary>
		/// <returns>An immutable array of namespace definitions that are direct children of the current namespace definition.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00011453 File Offset: 0x0000F653
		public ImmutableArray<NamespaceDefinitionHandle> NamespaceDefinitions
		{
			get
			{
				return this._data.NamespaceDefinitions;
			}
		}

		/// <summary>Gets all type definitions that reside directly in a namespace.</summary>
		/// <returns>An immutable array of type definition handles.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00011460 File Offset: 0x0000F660
		public ImmutableArray<TypeDefinitionHandle> TypeDefinitions
		{
			get
			{
				return this._data.TypeDefinitions;
			}
		}

		/// <summary>Gets all exported types that reside directly in a namespace.</summary>
		/// <returns>An immutable array of exported type handles.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001146D File Offset: 0x0000F66D
		public ImmutableArray<ExportedTypeHandle> ExportedTypes
		{
			get
			{
				return this._data.ExportedTypes;
			}
		}

		// Token: 0x04000439 RID: 1081
		private readonly NamespaceData _data;
	}
}
