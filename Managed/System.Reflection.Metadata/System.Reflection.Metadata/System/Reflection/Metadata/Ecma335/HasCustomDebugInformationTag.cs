using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000163 RID: 355
	internal static class HasCustomDebugInformationTag
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x00021CD4 File Offset: 0x0001FED4
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint taggedReference)
		{
			uint num = HasCustomDebugInformationTag.TagToTokenTypeArray[(int)(taggedReference & 31U)];
			uint num2 = taggedReference >> 5;
			if (num == 4294967295U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00021D0C File Offset: 0x0001FF0C
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
				case 26U:
					return (rowId << 5) | 12U;
				case 27U:
					return (rowId << 5) | 13U;
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
				case 48U:
					return (rowId << 5) | 22U;
				case 50U:
					return (rowId << 5) | 23U;
				case 51U:
					return (rowId << 5) | 24U;
				case 52U:
					return (rowId << 5) | 25U;
				case 53U:
					return (rowId << 5) | 26U;
				}
				break;
			}
			return 0U;
		}

		// Token: 0x040008DE RID: 2270
		internal const int NumberOfBits = 5;

		// Token: 0x040008DF RID: 2271
		internal const int LargeRowSize = 2048;

		// Token: 0x040008E0 RID: 2272
		internal const uint MethodDef = 0U;

		// Token: 0x040008E1 RID: 2273
		internal const uint Field = 1U;

		// Token: 0x040008E2 RID: 2274
		internal const uint TypeRef = 2U;

		// Token: 0x040008E3 RID: 2275
		internal const uint TypeDef = 3U;

		// Token: 0x040008E4 RID: 2276
		internal const uint Param = 4U;

		// Token: 0x040008E5 RID: 2277
		internal const uint InterfaceImpl = 5U;

		// Token: 0x040008E6 RID: 2278
		internal const uint MemberRef = 6U;

		// Token: 0x040008E7 RID: 2279
		internal const uint Module = 7U;

		// Token: 0x040008E8 RID: 2280
		internal const uint DeclSecurity = 8U;

		// Token: 0x040008E9 RID: 2281
		internal const uint Property = 9U;

		// Token: 0x040008EA RID: 2282
		internal const uint Event = 10U;

		// Token: 0x040008EB RID: 2283
		internal const uint StandAloneSig = 11U;

		// Token: 0x040008EC RID: 2284
		internal const uint ModuleRef = 12U;

		// Token: 0x040008ED RID: 2285
		internal const uint TypeSpec = 13U;

		// Token: 0x040008EE RID: 2286
		internal const uint Assembly = 14U;

		// Token: 0x040008EF RID: 2287
		internal const uint AssemblyRef = 15U;

		// Token: 0x040008F0 RID: 2288
		internal const uint File = 16U;

		// Token: 0x040008F1 RID: 2289
		internal const uint ExportedType = 17U;

		// Token: 0x040008F2 RID: 2290
		internal const uint ManifestResource = 18U;

		// Token: 0x040008F3 RID: 2291
		internal const uint GenericParam = 19U;

		// Token: 0x040008F4 RID: 2292
		internal const uint GenericParamConstraint = 20U;

		// Token: 0x040008F5 RID: 2293
		internal const uint MethodSpec = 21U;

		// Token: 0x040008F6 RID: 2294
		internal const uint Document = 22U;

		// Token: 0x040008F7 RID: 2295
		internal const uint LocalScope = 23U;

		// Token: 0x040008F8 RID: 2296
		internal const uint LocalVariable = 24U;

		// Token: 0x040008F9 RID: 2297
		internal const uint LocalConstant = 25U;

		// Token: 0x040008FA RID: 2298
		internal const uint Import = 26U;

		// Token: 0x040008FB RID: 2299
		internal const uint TagMask = 31U;

		// Token: 0x040008FC RID: 2300
		internal const uint InvalidTokenType = 4294967295U;

		// Token: 0x040008FD RID: 2301
		[Nullable(1)]
		internal static uint[] TagToTokenTypeArray = new uint[]
		{
			100663296U, 67108864U, 16777216U, 33554432U, 134217728U, 150994944U, 167772160U, 0U, 234881024U, 385875968U,
			335544320U, 285212672U, 436207616U, 452984832U, 536870912U, 587202560U, 637534208U, 654311424U, 671088640U, 704643072U,
			738197504U, 721420288U, 805306368U, 838860800U, 855638016U, 872415232U, 889192448U, uint.MaxValue, uint.MaxValue, uint.MaxValue,
			uint.MaxValue, uint.MaxValue
		};

		// Token: 0x040008FE RID: 2302
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.TypeDef | TableMask.Field | TableMask.MethodDef | TableMask.Param | TableMask.InterfaceImpl | TableMask.MemberRef | TableMask.DeclSecurity | TableMask.StandAloneSig | TableMask.Event | TableMask.Property | TableMask.ModuleRef | TableMask.TypeSpec | TableMask.Assembly | TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType | TableMask.ManifestResource | TableMask.GenericParam | TableMask.MethodSpec | TableMask.GenericParamConstraint | TableMask.Document | TableMask.LocalScope | TableMask.LocalVariable | TableMask.LocalConstant | TableMask.ImportScope;
	}
}
