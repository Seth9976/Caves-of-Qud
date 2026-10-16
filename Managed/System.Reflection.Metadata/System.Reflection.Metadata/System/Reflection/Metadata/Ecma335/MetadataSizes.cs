using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides information on sizes of various metadata structures.</summary>
	// Token: 0x02000105 RID: 261
	public sealed class MetadataSizes
	{
		/// <summary>Exact (unaligned) heap sizes.</summary>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001B387 File Offset: 0x00019587
		public ImmutableArray<int> HeapSizes { get; }

		/// <summary>Table row counts.</summary>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001B38F File Offset: 0x0001958F
		public ImmutableArray<int> RowCounts { get; }

		/// <summary>External table row count.</summary>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0001B397 File Offset: 0x00019597
		public ImmutableArray<int> ExternalRowCounts { get; }

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001B3A0 File Offset: 0x000195A0
		internal MetadataSizes(ImmutableArray<int> rowCounts, ImmutableArray<int> externalRowCounts, ImmutableArray<int> heapSizes, int metadataVersionByteCount, bool isStandaloneDebugMetadata)
		{
			this.RowCounts = rowCounts;
			this.ExternalRowCounts = externalRowCounts;
			this.HeapSizes = heapSizes;
			this.MetadataVersionPaddedLength = BitArithmetic.Align(metadataVersionByteCount + 1, 4);
			this.PresentTablesMask = MetadataSizes.ComputeNonEmptyTableMask(rowCounts);
			this.ExternalTablesMask = MetadataSizes.ComputeNonEmptyTableMask(externalRowCounts);
			bool flag = this.IsPresent(TableIndex.EncLog) || this.IsPresent(TableIndex.EncMap);
			bool flag2 = !flag;
			this.IsEncDelta = flag;
			this.IsCompressed = flag2;
			this.BlobReferenceIsSmall = flag2 && heapSizes[2] <= 65535;
			this.StringReferenceIsSmall = flag2 && heapSizes[1] <= 65535;
			this.GuidReferenceIsSmall = flag2 && heapSizes[3] <= 65535;
			this.CustomAttributeTypeCodedIndexIsSmall = this.IsReferenceSmall(3, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.MemberRef
			});
			this.DeclSecurityCodedIndexIsSmall = this.IsReferenceSmall(2, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.TypeDef
			});
			this.EventDefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.Event });
			this.FieldDefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.Field });
			this.GenericParamReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.GenericParam });
			this.HasConstantCodedIndexIsSmall = this.IsReferenceSmall(2, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.Param,
				TableIndex.Property
			});
			this.HasCustomAttributeCodedIndexIsSmall = this.IsReferenceSmall(5, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.Field,
				TableIndex.TypeRef,
				TableIndex.TypeDef,
				TableIndex.Param,
				TableIndex.InterfaceImpl,
				TableIndex.MemberRef,
				TableIndex.Module,
				TableIndex.DeclSecurity,
				TableIndex.Property,
				TableIndex.Event,
				TableIndex.StandAloneSig,
				TableIndex.ModuleRef,
				TableIndex.TypeSpec,
				TableIndex.Assembly,
				TableIndex.AssemblyRef,
				TableIndex.File,
				TableIndex.ExportedType,
				TableIndex.ManifestResource,
				TableIndex.GenericParam,
				TableIndex.GenericParamConstraint,
				TableIndex.MethodSpec
			});
			this.HasFieldMarshalCodedIndexIsSmall = this.IsReferenceSmall(1, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.Param
			});
			this.HasSemanticsCodedIndexIsSmall = this.IsReferenceSmall(1, new TableIndex[]
			{
				TableIndex.Event,
				TableIndex.Property
			});
			this.ImplementationCodedIndexIsSmall = this.IsReferenceSmall(2, new TableIndex[]
			{
				TableIndex.File,
				TableIndex.AssemblyRef,
				TableIndex.ExportedType
			});
			this.MemberForwardedCodedIndexIsSmall = this.IsReferenceSmall(1, new TableIndex[]
			{
				TableIndex.Field,
				TableIndex.MethodDef
			});
			this.MemberRefParentCodedIndexIsSmall = this.IsReferenceSmall(3, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.TypeRef,
				TableIndex.ModuleRef,
				TableIndex.MethodDef,
				TableIndex.TypeSpec
			});
			this.MethodDefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.MethodDef });
			this.MethodDefOrRefCodedIndexIsSmall = this.IsReferenceSmall(1, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.MemberRef
			});
			this.ModuleRefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.ModuleRef });
			this.ParameterReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.Param });
			this.PropertyDefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.Property });
			this.ResolutionScopeCodedIndexIsSmall = this.IsReferenceSmall(2, new TableIndex[]
			{
				TableIndex.Module,
				TableIndex.ModuleRef,
				TableIndex.AssemblyRef,
				TableIndex.TypeRef
			});
			this.TypeDefReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.TypeDef });
			this.TypeDefOrRefCodedIndexIsSmall = this.IsReferenceSmall(2, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.TypeRef,
				TableIndex.TypeSpec
			});
			this.TypeOrMethodDefCodedIndexIsSmall = this.IsReferenceSmall(1, new TableIndex[]
			{
				TableIndex.TypeDef,
				TableIndex.MethodDef
			});
			this.DocumentReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.Document });
			this.LocalVariableReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.LocalVariable });
			this.LocalConstantReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.LocalConstant });
			this.ImportScopeReferenceIsSmall = this.IsReferenceSmall(0, new TableIndex[] { TableIndex.ImportScope });
			this.HasCustomDebugInformationCodedIndexIsSmall = this.IsReferenceSmall(5, new TableIndex[]
			{
				TableIndex.MethodDef,
				TableIndex.Field,
				TableIndex.TypeRef,
				TableIndex.TypeDef,
				TableIndex.Param,
				TableIndex.InterfaceImpl,
				TableIndex.MemberRef,
				TableIndex.Module,
				TableIndex.DeclSecurity,
				TableIndex.Property,
				TableIndex.Event,
				TableIndex.StandAloneSig,
				TableIndex.ModuleRef,
				TableIndex.TypeSpec,
				TableIndex.Assembly,
				TableIndex.AssemblyRef,
				TableIndex.File,
				TableIndex.ExportedType,
				TableIndex.ManifestResource,
				TableIndex.GenericParam,
				TableIndex.GenericParamConstraint,
				TableIndex.MethodSpec,
				TableIndex.Document,
				TableIndex.LocalScope,
				TableIndex.LocalVariable,
				TableIndex.LocalConstant,
				TableIndex.ImportScope
			});
			int num = this.CalculateTableStreamHeaderSize();
			byte b = (this.BlobReferenceIsSmall ? 2 : 4);
			byte b2 = (this.StringReferenceIsSmall ? 2 : 4);
			byte b3 = (this.GuidReferenceIsSmall ? 2 : 4);
			byte b4 = (this.CustomAttributeTypeCodedIndexIsSmall ? 2 : 4);
			byte b5 = (this.DeclSecurityCodedIndexIsSmall ? 2 : 4);
			byte b6 = (this.EventDefReferenceIsSmall ? 2 : 4);
			byte b7 = (this.FieldDefReferenceIsSmall ? 2 : 4);
			byte b8 = (this.GenericParamReferenceIsSmall ? 2 : 4);
			byte b9 = (this.HasConstantCodedIndexIsSmall ? 2 : 4);
			byte b10 = (this.HasCustomAttributeCodedIndexIsSmall ? 2 : 4);
			byte b11 = (this.HasFieldMarshalCodedIndexIsSmall ? 2 : 4);
			byte b12 = (this.HasSemanticsCodedIndexIsSmall ? 2 : 4);
			byte b13 = (this.ImplementationCodedIndexIsSmall ? 2 : 4);
			byte b14 = (this.MemberForwardedCodedIndexIsSmall ? 2 : 4);
			byte b15 = (this.MemberRefParentCodedIndexIsSmall ? 2 : 4);
			byte b16 = (this.MethodDefReferenceIsSmall ? 2 : 4);
			byte b17 = (this.MethodDefOrRefCodedIndexIsSmall ? 2 : 4);
			byte b18 = (this.ModuleRefReferenceIsSmall ? 2 : 4);
			byte b19 = (this.ParameterReferenceIsSmall ? 2 : 4);
			byte b20 = (this.PropertyDefReferenceIsSmall ? 2 : 4);
			byte b21 = (this.ResolutionScopeCodedIndexIsSmall ? 2 : 4);
			byte b22 = (this.TypeDefReferenceIsSmall ? 2 : 4);
			byte b23 = (this.TypeDefOrRefCodedIndexIsSmall ? 2 : 4);
			byte b24 = (this.TypeOrMethodDefCodedIndexIsSmall ? 2 : 4);
			byte b25 = (this.DocumentReferenceIsSmall ? 2 : 4);
			byte b26 = (this.LocalVariableReferenceIsSmall ? 2 : 4);
			byte b27 = (this.LocalConstantReferenceIsSmall ? 2 : 4);
			byte b28 = (this.ImportScopeReferenceIsSmall ? 2 : 4);
			byte b29 = (this.HasCustomDebugInformationCodedIndexIsSmall ? 2 : 4);
			num += this.GetTableSize(TableIndex.Module, (int)(2 + 3 * b3 + b2));
			num += this.GetTableSize(TableIndex.TypeRef, (int)(b21 + b2 + b2));
			num += this.GetTableSize(TableIndex.TypeDef, (int)(4 + b2 + b2 + b23 + b7 + b16));
			num += this.GetTableSize(TableIndex.Field, (int)(2 + b2 + b));
			num += this.GetTableSize(TableIndex.MethodDef, (int)(8 + b2 + b + b19));
			num += this.GetTableSize(TableIndex.Param, (int)(4 + b2));
			num += this.GetTableSize(TableIndex.InterfaceImpl, (int)(b22 + b23));
			num += this.GetTableSize(TableIndex.MemberRef, (int)(b15 + b2 + b));
			num += this.GetTableSize(TableIndex.Constant, (int)(2 + b9 + b));
			num += this.GetTableSize(TableIndex.CustomAttribute, (int)(b10 + b4 + b));
			num += this.GetTableSize(TableIndex.FieldMarshal, (int)(b11 + b));
			num += this.GetTableSize(TableIndex.DeclSecurity, (int)(2 + b5 + b));
			num += this.GetTableSize(TableIndex.ClassLayout, (int)(6 + b22));
			num += this.GetTableSize(TableIndex.FieldLayout, (int)(4 + b7));
			num += this.GetTableSize(TableIndex.StandAloneSig, (int)b);
			num += this.GetTableSize(TableIndex.EventMap, (int)(b22 + b6));
			num += this.GetTableSize(TableIndex.Event, (int)(2 + b2 + b23));
			num += this.GetTableSize(TableIndex.PropertyMap, (int)(b22 + b20));
			num += this.GetTableSize(TableIndex.Property, (int)(2 + b2 + b));
			num += this.GetTableSize(TableIndex.MethodSemantics, (int)(2 + b16 + b12));
			num += this.GetTableSize(TableIndex.MethodImpl, (int)(b22 + b17 + b17));
			num += this.GetTableSize(TableIndex.ModuleRef, (int)b2);
			num += this.GetTableSize(TableIndex.TypeSpec, (int)b);
			num += this.GetTableSize(TableIndex.ImplMap, (int)(2 + b14 + b2 + b18));
			num += this.GetTableSize(TableIndex.FieldRva, (int)(4 + b7));
			num += this.GetTableSize(TableIndex.EncLog, 8);
			num += this.GetTableSize(TableIndex.EncMap, 4);
			num += this.GetTableSize(TableIndex.Assembly, (int)(16 + b + b2 + b2));
			num += this.GetTableSize(TableIndex.AssemblyRef, (int)(12 + b + b2 + b2 + b));
			num += this.GetTableSize(TableIndex.File, (int)(4 + b2 + b));
			num += this.GetTableSize(TableIndex.ExportedType, (int)(8 + b2 + b2 + b13));
			num += this.GetTableSize(TableIndex.ManifestResource, (int)(8 + b2 + b13));
			num += this.GetTableSize(TableIndex.NestedClass, (int)(b22 + b22));
			num += this.GetTableSize(TableIndex.GenericParam, (int)(4 + b24 + b2));
			num += this.GetTableSize(TableIndex.MethodSpec, (int)(b17 + b));
			num += this.GetTableSize(TableIndex.GenericParamConstraint, (int)(b8 + b23));
			num += this.GetTableSize(TableIndex.Document, (int)(b + b3 + b + b3));
			num += this.GetTableSize(TableIndex.MethodDebugInformation, (int)(b25 + b));
			num += this.GetTableSize(TableIndex.LocalScope, (int)(b16 + b28 + b26 + b27 + 4 + 4));
			num += this.GetTableSize(TableIndex.LocalVariable, (int)(4 + b2));
			num += this.GetTableSize(TableIndex.LocalConstant, (int)(b2 + b));
			num += this.GetTableSize(TableIndex.ImportScope, (int)(b28 + b));
			num += this.GetTableSize(TableIndex.StateMachineMethod, (int)(b16 + b16));
			num += this.GetTableSize(TableIndex.CustomDebugInformation, (int)(b29 + b3 + b));
			num = BitArithmetic.Align(num + 1, 4);
			this.MetadataTableStreamSize = num;
			num += this.GetAlignedHeapSize(HeapIndex.String);
			num += this.GetAlignedHeapSize(HeapIndex.UserString);
			num += this.GetAlignedHeapSize(HeapIndex.Guid);
			num += this.GetAlignedHeapSize(HeapIndex.Blob);
			this.StandalonePdbStreamSize = (isStandaloneDebugMetadata ? this.CalculateStandalonePdbStreamSize() : 0);
			num += this.StandalonePdbStreamSize;
			this.MetadataStreamStorageSize = num;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001BC09 File Offset: 0x00019E09
		internal bool IsStandaloneDebugMetadata
		{
			get
			{
				return this.StandalonePdbStreamSize > 0;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001BC14 File Offset: 0x00019E14
		internal bool IsPresent(TableIndex table)
		{
			return (this.PresentTablesMask & (1UL << (int)table)) > 0UL;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001BC28 File Offset: 0x00019E28
		internal int MetadataHeaderSize
		{
			get
			{
				return 16 + this.MetadataVersionPaddedLength + 2 + 2 + (this.IsStandaloneDebugMetadata ? 16 : 0) + 76 + (this.IsEncDelta ? 16 : 0);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001BC56 File Offset: 0x00019E56
		[NullableContext(1)]
		internal static int GetMetadataStreamHeaderSize(string streamName)
		{
			return 8 + BitArithmetic.Align(streamName.Length + 1, 4);
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001BC68 File Offset: 0x00019E68
		internal int MetadataSize
		{
			get
			{
				return this.MetadataHeaderSize + this.MetadataStreamStorageSize;
			}
		}

		/// <summary>Returns aligned size of the specified heap.</summary>
		/// <param name="index" />
		// Token: 0x060009CA RID: 2506 RVA: 0x0001BC78 File Offset: 0x00019E78
		public int GetAlignedHeapSize(HeapIndex index)
		{
			if (index < HeapIndex.UserString || index > (HeapIndex)this.HeapSizes.Length)
			{
				Throw.ArgumentOutOfRange("index");
			}
			return BitArithmetic.Align(this.HeapSizes[(int)index], 4);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001BCBC File Offset: 0x00019EBC
		internal int CalculateTableStreamHeaderSize()
		{
			int num = 24;
			for (int i = 0; i < this.RowCounts.Length; i++)
			{
				if (((1UL << i) & this.PresentTablesMask) != 0UL)
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001BCFC File Offset: 0x00019EFC
		internal int CalculateStandalonePdbStreamSize()
		{
			return 32 + BitArithmetic.CountBits(this.ExternalTablesMask) * 4;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001BD1C File Offset: 0x00019F1C
		private static ulong ComputeNonEmptyTableMask(ImmutableArray<int> rowCounts)
		{
			ulong num = 0UL;
			for (int i = 0; i < rowCounts.Length; i++)
			{
				if (rowCounts[i] > 0)
				{
					num |= 1UL << i;
				}
			}
			return num;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001BD54 File Offset: 0x00019F54
		private int GetTableSize(TableIndex index, int rowSize)
		{
			return this.RowCounts[(int)index] * rowSize;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001BD72 File Offset: 0x00019F72
		private bool IsReferenceSmall(int tagBitSize, params TableIndex[] tables)
		{
			return this.IsCompressed && this.ReferenceFits(16 - tagBitSize, tables);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001BD8C File Offset: 0x00019F8C
		private bool ReferenceFits(int bitCount, TableIndex[] tables)
		{
			int num = (1 << bitCount) - 1;
			foreach (TableIndex tableIndex in tables)
			{
				if (this.RowCounts[(int)tableIndex] + this.ExternalRowCounts[(int)tableIndex] > num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000574 RID: 1396
		private const int StreamAlignment = 4;

		// Token: 0x04000575 RID: 1397
		internal const int MaxMetadataVersionByteCount = 254;

		// Token: 0x04000576 RID: 1398
		internal readonly int MetadataVersionPaddedLength;

		// Token: 0x04000577 RID: 1399
		internal const ulong SortedDebugTables = 55169095435288576UL;

		// Token: 0x04000578 RID: 1400
		internal readonly bool IsEncDelta;

		// Token: 0x04000579 RID: 1401
		internal readonly bool IsCompressed;

		// Token: 0x0400057A RID: 1402
		internal readonly bool BlobReferenceIsSmall;

		// Token: 0x0400057B RID: 1403
		internal readonly bool StringReferenceIsSmall;

		// Token: 0x0400057C RID: 1404
		internal readonly bool GuidReferenceIsSmall;

		// Token: 0x0400057D RID: 1405
		internal readonly bool CustomAttributeTypeCodedIndexIsSmall;

		// Token: 0x0400057E RID: 1406
		internal readonly bool DeclSecurityCodedIndexIsSmall;

		// Token: 0x0400057F RID: 1407
		internal readonly bool EventDefReferenceIsSmall;

		// Token: 0x04000580 RID: 1408
		internal readonly bool FieldDefReferenceIsSmall;

		// Token: 0x04000581 RID: 1409
		internal readonly bool GenericParamReferenceIsSmall;

		// Token: 0x04000582 RID: 1410
		internal readonly bool HasConstantCodedIndexIsSmall;

		// Token: 0x04000583 RID: 1411
		internal readonly bool HasCustomAttributeCodedIndexIsSmall;

		// Token: 0x04000584 RID: 1412
		internal readonly bool HasFieldMarshalCodedIndexIsSmall;

		// Token: 0x04000585 RID: 1413
		internal readonly bool HasSemanticsCodedIndexIsSmall;

		// Token: 0x04000586 RID: 1414
		internal readonly bool ImplementationCodedIndexIsSmall;

		// Token: 0x04000587 RID: 1415
		internal readonly bool MemberForwardedCodedIndexIsSmall;

		// Token: 0x04000588 RID: 1416
		internal readonly bool MemberRefParentCodedIndexIsSmall;

		// Token: 0x04000589 RID: 1417
		internal readonly bool MethodDefReferenceIsSmall;

		// Token: 0x0400058A RID: 1418
		internal readonly bool MethodDefOrRefCodedIndexIsSmall;

		// Token: 0x0400058B RID: 1419
		internal readonly bool ModuleRefReferenceIsSmall;

		// Token: 0x0400058C RID: 1420
		internal readonly bool ParameterReferenceIsSmall;

		// Token: 0x0400058D RID: 1421
		internal readonly bool PropertyDefReferenceIsSmall;

		// Token: 0x0400058E RID: 1422
		internal readonly bool ResolutionScopeCodedIndexIsSmall;

		// Token: 0x0400058F RID: 1423
		internal readonly bool TypeDefReferenceIsSmall;

		// Token: 0x04000590 RID: 1424
		internal readonly bool TypeDefOrRefCodedIndexIsSmall;

		// Token: 0x04000591 RID: 1425
		internal readonly bool TypeOrMethodDefCodedIndexIsSmall;

		// Token: 0x04000592 RID: 1426
		internal readonly bool DocumentReferenceIsSmall;

		// Token: 0x04000593 RID: 1427
		internal readonly bool LocalVariableReferenceIsSmall;

		// Token: 0x04000594 RID: 1428
		internal readonly bool LocalConstantReferenceIsSmall;

		// Token: 0x04000595 RID: 1429
		internal readonly bool ImportScopeReferenceIsSmall;

		// Token: 0x04000596 RID: 1430
		internal readonly bool HasCustomDebugInformationCodedIndexIsSmall;

		// Token: 0x0400059A RID: 1434
		internal readonly ulong PresentTablesMask;

		// Token: 0x0400059B RID: 1435
		internal readonly ulong ExternalTablesMask;

		// Token: 0x0400059C RID: 1436
		internal readonly int MetadataStreamStorageSize;

		// Token: 0x0400059D RID: 1437
		internal readonly int MetadataTableStreamSize;

		// Token: 0x0400059E RID: 1438
		internal readonly int StandalonePdbStreamSize;

		// Token: 0x0400059F RID: 1439
		internal const int PdbIdSize = 20;
	}
}
