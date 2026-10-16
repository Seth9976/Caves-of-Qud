using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000115 RID: 277
	internal static class HasCustomAttributeTag
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x0001D5C4 File Offset: 0x0001B7C4
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint hasCustomAttribute)
		{
			uint num = HasCustomAttributeTag.TagToTokenTypeArray[(int)(hasCustomAttribute & 31U)];
			uint num2 = hasCustomAttribute >> 5;
			if (num == 4294967295U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		internal static uint ConvertToTag(EntityHandle handle)
		{
			uint type = handle.Type;
			uint rowId = (uint)handle.RowId;
			uint num = type >> 24;
			switch (num)
			{
			case 0U:
				return (rowId << 5) | 7U;
			case 1U:
				return (rowId << 5) | 2U;
			case 2U:
				return (rowId << 5) | 3U;
			case 3U:
			case 5U:
			case 7U:
			case 11U:
			case 12U:
			case 13U:
			case 15U:
			case 16U:
			case 18U:
			case 19U:
				break;
			case 4U:
				return (rowId << 5) | 1U;
			case 6U:
				return (rowId << 5) | 0U;
			case 8U:
				return (rowId << 5) | 4U;
			case 9U:
				return (rowId << 5) | 5U;
			case 10U:
				return (rowId << 5) | 6U;
			case 14U:
				return (rowId << 5) | 8U;
			case 17U:
				return (rowId << 5) | 11U;
			case 20U:
				return (rowId << 5) | 10U;
			default:
				switch (num)
				{
				case 23U:
					return (rowId << 5) | 9U;
				case 24U:
				case 25U:
					break;
				case 26U:
					return (rowId << 5) | 12U;
				case 27U:
					return (rowId << 5) | 13U;
				default:
					switch (num)
					{
					case 32U:
						return (rowId << 5) | 14U;
					case 35U:
						return (rowId << 5) | 15U;
					case 38U:
						return (rowId << 5) | 16U;
					case 39U:
						return (rowId << 5) | 17U;
					case 40U:
						return (rowId << 5) | 18U;
					case 42U:
						return (rowId << 5) | 19U;
					case 43U:
						return (rowId << 5) | 21U;
					case 44U:
						return (rowId << 5) | 20U;
					}
					break;
				}
				break;
			}
			return 0U;
		}

		// Token: 0x04000630 RID: 1584
		internal const int NumberOfBits = 5;

		// Token: 0x04000631 RID: 1585
		internal const int LargeRowSize = 2048;

		// Token: 0x04000632 RID: 1586
		internal const uint MethodDef = 0U;

		// Token: 0x04000633 RID: 1587
		internal const uint Field = 1U;

		// Token: 0x04000634 RID: 1588
		internal const uint TypeRef = 2U;

		// Token: 0x04000635 RID: 1589
		internal const uint TypeDef = 3U;

		// Token: 0x04000636 RID: 1590
		internal const uint Param = 4U;

		// Token: 0x04000637 RID: 1591
		internal const uint InterfaceImpl = 5U;

		// Token: 0x04000638 RID: 1592
		internal const uint MemberRef = 6U;

		// Token: 0x04000639 RID: 1593
		internal const uint Module = 7U;

		// Token: 0x0400063A RID: 1594
		internal const uint DeclSecurity = 8U;

		// Token: 0x0400063B RID: 1595
		internal const uint Property = 9U;

		// Token: 0x0400063C RID: 1596
		internal const uint Event = 10U;

		// Token: 0x0400063D RID: 1597
		internal const uint StandAloneSig = 11U;

		// Token: 0x0400063E RID: 1598
		internal const uint ModuleRef = 12U;

		// Token: 0x0400063F RID: 1599
		internal const uint TypeSpec = 13U;

		// Token: 0x04000640 RID: 1600
		internal const uint Assembly = 14U;

		// Token: 0x04000641 RID: 1601
		internal const uint AssemblyRef = 15U;

		// Token: 0x04000642 RID: 1602
		internal const uint File = 16U;

		// Token: 0x04000643 RID: 1603
		internal const uint ExportedType = 17U;

		// Token: 0x04000644 RID: 1604
		internal const uint ManifestResource = 18U;

		// Token: 0x04000645 RID: 1605
		internal const uint GenericParam = 19U;

		// Token: 0x04000646 RID: 1606
		internal const uint GenericParamConstraint = 20U;

		// Token: 0x04000647 RID: 1607
		internal const uint MethodSpec = 21U;

		// Token: 0x04000648 RID: 1608
		internal const uint TagMask = 31U;

		// Token: 0x04000649 RID: 1609
		internal const uint InvalidTokenType = 4294967295U;

		// Token: 0x0400064A RID: 1610
		[Nullable(1)]
		internal static uint[] TagToTokenTypeArray = new uint[]
		{
			100663296U, 67108864U, 16777216U, 33554432U, 134217728U, 150994944U, 167772160U, 0U, 234881024U, 385875968U,
			335544320U, 285212672U, 436207616U, 452984832U, 536870912U, 587202560U, 637534208U, 654311424U, 671088640U, 704643072U,
			738197504U, 721420288U, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue,
			uint.MaxValue, uint.MaxValue
		};

		// Token: 0x0400064B RID: 1611
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint;
	}
}
