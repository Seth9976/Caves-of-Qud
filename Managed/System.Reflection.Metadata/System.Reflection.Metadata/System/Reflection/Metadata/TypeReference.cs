using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000DB RID: 219
	public readonly struct TypeReference
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00012F76 File Offset: 0x00011176
		[NullableContext(1)]
		internal TypeReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00012F86 File Offset: 0x00011186
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00012F94 File Offset: 0x00011194
		private TypeRefTreatment Treatment
		{
			get
			{
				return (TypeRefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00012FA0 File Offset: 0x000111A0
		private TypeReferenceHandle Handle
		{
			get
			{
				return TypeReferenceHandle.FromRowId(this.RowId);
			}
		}

		/// <summary>Gets the resolution scope in which the target type is defined and is uniquely identified by the specified <see cref="P:System.Reflection.Metadata.TypeReference.Namespace" /> and <see cref="P:System.Reflection.Metadata.TypeReference.Name" />.</summary>
		/// <returns>The resolution scope in which the target type is uniquely defined.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00012FAD File Offset: 0x000111AD
		public EntityHandle ResolutionScope
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetResolutionScope(this.Handle);
				}
				return this.GetProjectedResolutionScope();
			}
		}

		/// <summary>Gets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00012FD4 File Offset: 0x000111D4
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		/// <summary>Gets the full name of the namespace where the target type is defined.</summary>
		/// <returns>The full name of the namespace where the target type is defined, or a handle whose the <see cref="P:System.Reflection.Metadata.StringHandle.IsNil" /> property is <see langword="true" /> if the type is nested or defined in a root namespace.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00012FFB File Offset: 0x000111FB
		public StringHandle Namespace
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return this._reader.TypeRefTable.GetNamespace(this.Handle);
				}
				return this.GetProjectedNamespace();
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00013024 File Offset: 0x00011224
		private EntityHandle GetProjectedResolutionScope()
		{
			TypeRefTreatment treatment = this.Treatment;
			if (treatment - TypeRefTreatment.SystemDelegate <= 1)
			{
				return AssemblyReferenceHandle.FromVirtualIndex(AssemblyReferenceHandle.VirtualIndex.System_Runtime);
			}
			if (treatment != TypeRefTreatment.UseProjectionInfo)
			{
				return default(AssemblyReferenceHandle);
			}
			return MetadataReader.GetProjectedAssemblyRef(this.RowId);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001306F File Offset: 0x0001126F
		private StringHandle GetProjectedName()
		{
			if (this.Treatment == TypeRefTreatment.UseProjectionInfo)
			{
				return MetadataReader.GetProjectedName(this.RowId);
			}
			return this._reader.TypeRefTable.GetName(this.Handle);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001309C File Offset: 0x0001129C
		private StringHandle GetProjectedNamespace()
		{
			TypeRefTreatment treatment = this.Treatment;
			if (treatment - TypeRefTreatment.SystemDelegate <= 1)
			{
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.System);
			}
			if (treatment != TypeRefTreatment.UseProjectionInfo)
			{
				return default(StringHandle);
			}
			return MetadataReader.GetProjectedNamespace(this.RowId);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000130D9 File Offset: 0x000112D9
		internal TypeRefSignatureTreatment SignatureTreatment
		{
			get
			{
				if (this.Treatment == TypeRefTreatment.None)
				{
					return TypeRefSignatureTreatment.None;
				}
				return this.GetProjectedSignatureTreatment();
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000130EB File Offset: 0x000112EB
		private TypeRefSignatureTreatment GetProjectedSignatureTreatment()
		{
			if (this.Treatment == TypeRefTreatment.UseProjectionInfo)
			{
				return MetadataReader.GetProjectedSignatureTreatment(this.RowId);
			}
			return TypeRefSignatureTreatment.None;
		}

		// Token: 0x040004E5 RID: 1253
		private readonly MetadataReader _reader;

		// Token: 0x040004E6 RID: 1254
		private readonly uint _treatmentAndRowId;
	}
}
