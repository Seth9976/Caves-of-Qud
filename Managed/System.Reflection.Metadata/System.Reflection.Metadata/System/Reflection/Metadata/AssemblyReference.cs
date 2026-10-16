using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct AssemblyReference
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00009573 File Offset: 0x00007773
		public AssemblyName GetAssemblyName()
		{
			return this._reader.GetAssemblyName(this.Name, this.Version, this.Culture, this.PublicKeyOrToken, AssemblyHashAlgorithm.None, this.Flags);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000959F File Offset: 0x0000779F
		internal AssemblyReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000095AF File Offset: 0x000077AF
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000095BD File Offset: 0x000077BD
		private bool IsVirtual
		{
			get
			{
				return (this._treatmentAndRowId & 2147483648U) > 0U;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000095CE File Offset: 0x000077CE
		public Version Version
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualVersion();
				}
				if (this.RowId == this._reader.WinMDMscorlibRef)
				{
					return AssemblyReference.s_version_4_0_0_0;
				}
				return this._reader.AssemblyRefTable.GetVersion(this.RowId);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000960E File Offset: 0x0000780E
		public AssemblyFlags Flags
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualFlags();
				}
				return this._reader.AssemblyRefTable.GetFlags(this.RowId);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009635 File Offset: 0x00007835
		public StringHandle Name
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualName();
				}
				return this._reader.AssemblyRefTable.GetName(this.RowId);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000965C File Offset: 0x0000785C
		public StringHandle Culture
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualCulture();
				}
				return this._reader.AssemblyRefTable.GetCulture(this.RowId);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009683 File Offset: 0x00007883
		public BlobHandle PublicKeyOrToken
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualPublicKeyOrToken();
				}
				return this._reader.AssemblyRefTable.GetPublicKeyOrToken(this.RowId);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000096AA File Offset: 0x000078AA
		public BlobHandle HashValue
		{
			get
			{
				if (this.IsVirtual)
				{
					return this.GetVirtualHashValue();
				}
				return this._reader.AssemblyRefTable.GetHashValue(this.RowId);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000096D1 File Offset: 0x000078D1
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			if (this.IsVirtual)
			{
				return this.GetVirtualCustomAttributes();
			}
			return new CustomAttributeHandleCollection(this._reader, AssemblyReferenceHandle.FromRowId(this.RowId));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000096FD File Offset: 0x000078FD
		private Version GetVirtualVersion()
		{
			return AssemblyReference.s_version_4_0_0_0;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009704 File Offset: 0x00007904
		private AssemblyFlags GetVirtualFlags()
		{
			return this._reader.AssemblyRefTable.GetFlags(this._reader.WinMDMscorlibRef);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009721 File Offset: 0x00007921
		private StringHandle GetVirtualName()
		{
			return StringHandle.FromVirtualIndex(this.GetVirtualNameIndex((AssemblyReferenceHandle.VirtualIndex)this.RowId));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009734 File Offset: 0x00007934
		private StringHandle.VirtualIndex GetVirtualNameIndex(AssemblyReferenceHandle.VirtualIndex index)
		{
			switch (index)
			{
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime:
				return StringHandle.VirtualIndex.System_Runtime;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime:
				return StringHandle.VirtualIndex.System_Runtime_InteropServices_WindowsRuntime;
			case AssemblyReferenceHandle.VirtualIndex.System_ObjectModel:
				return StringHandle.VirtualIndex.System_ObjectModel;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime;
			case AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime_UI_Xaml;
			case AssemblyReferenceHandle.VirtualIndex.System_Numerics_Vectors:
				return StringHandle.VirtualIndex.System_Numerics_Vectors;
			default:
				return StringHandle.VirtualIndex.System_Runtime_WindowsRuntime;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009764 File Offset: 0x00007964
		private StringHandle GetVirtualCulture()
		{
			return default(StringHandle);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000977C File Offset: 0x0000797C
		private BlobHandle GetVirtualPublicKeyOrToken()
		{
			AssemblyReferenceHandle.VirtualIndex rowId = (AssemblyReferenceHandle.VirtualIndex)this.RowId;
			if (rowId - AssemblyReferenceHandle.VirtualIndex.System_Runtime_WindowsRuntime <= 1)
			{
				return this._reader.AssemblyRefTable.GetPublicKeyOrToken(this._reader.WinMDMscorlibRef);
			}
			return BlobHandle.FromVirtualIndex(((this._reader.AssemblyRefTable.GetFlags(this._reader.WinMDMscorlibRef) & AssemblyFlags.PublicKey) > (AssemblyFlags)0) ? BlobHandle.VirtualIndex.ContractPublicKey : BlobHandle.VirtualIndex.ContractPublicKeyToken, 0);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000097E0 File Offset: 0x000079E0
		private BlobHandle GetVirtualHashValue()
		{
			return default(BlobHandle);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000097F6 File Offset: 0x000079F6
		private CustomAttributeHandleCollection GetVirtualCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, AssemblyReferenceHandle.FromRowId(this._reader.WinMDMscorlibRef));
		}

		// Token: 0x040002B9 RID: 697
		private readonly MetadataReader _reader;

		// Token: 0x040002BA RID: 698
		private readonly uint _treatmentAndRowId;

		// Token: 0x040002BB RID: 699
		private static readonly Version s_version_4_0_0_0 = new Version(4, 0, 0, 0);
	}
}
