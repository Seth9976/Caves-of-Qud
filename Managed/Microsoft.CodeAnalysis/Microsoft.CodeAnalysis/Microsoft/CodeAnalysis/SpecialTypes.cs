using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F7 RID: 503
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SpecialTypes
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0003FC38 File Offset: 0x0003DE38
		static SpecialTypes()
		{
			for (int i = 1; i < SpecialTypes.s_emittedNames.Length; i++)
			{
				string text = SpecialTypes.s_emittedNames[i];
				SpecialTypes.s_nameToTypeIdMap.Add(text, (SpecialType)i);
			}
			SpecialTypes.s_typeIdToTypeCodeMap = new PrimitiveTypeCode[46];
			for (int i = 0; i < SpecialTypes.s_typeIdToTypeCodeMap.Length; i++)
			{
				SpecialTypes.s_typeIdToTypeCodeMap[i] = PrimitiveTypeCode.NotPrimitive;
			}
			SpecialTypes.s_typeIdToTypeCodeMap[7] = PrimitiveTypeCode.Boolean;
			SpecialTypes.s_typeIdToTypeCodeMap[8] = PrimitiveTypeCode.Char;
			SpecialTypes.s_typeIdToTypeCodeMap[6] = PrimitiveTypeCode.Void;
			SpecialTypes.s_typeIdToTypeCodeMap[20] = PrimitiveTypeCode.String;
			SpecialTypes.s_typeIdToTypeCodeMap[15] = PrimitiveTypeCode.Int64;
			SpecialTypes.s_typeIdToTypeCodeMap[13] = PrimitiveTypeCode.Int32;
			SpecialTypes.s_typeIdToTypeCodeMap[11] = PrimitiveTypeCode.Int16;
			SpecialTypes.s_typeIdToTypeCodeMap[9] = PrimitiveTypeCode.Int8;
			SpecialTypes.s_typeIdToTypeCodeMap[16] = PrimitiveTypeCode.UInt64;
			SpecialTypes.s_typeIdToTypeCodeMap[14] = PrimitiveTypeCode.UInt32;
			SpecialTypes.s_typeIdToTypeCodeMap[12] = PrimitiveTypeCode.UInt16;
			SpecialTypes.s_typeIdToTypeCodeMap[10] = PrimitiveTypeCode.UInt8;
			SpecialTypes.s_typeIdToTypeCodeMap[18] = PrimitiveTypeCode.Float32;
			SpecialTypes.s_typeIdToTypeCodeMap[19] = PrimitiveTypeCode.Float64;
			SpecialTypes.s_typeIdToTypeCodeMap[21] = PrimitiveTypeCode.IntPtr;
			SpecialTypes.s_typeIdToTypeCodeMap[22] = PrimitiveTypeCode.UIntPtr;
			SpecialTypes.s_typeCodeToTypeIdMap = new SpecialType[21];
			for (int i = 0; i < SpecialTypes.s_typeCodeToTypeIdMap.Length; i++)
			{
				SpecialTypes.s_typeCodeToTypeIdMap[i] = SpecialType.None;
			}
			SpecialTypes.s_typeCodeToTypeIdMap[0] = SpecialType.System_Boolean;
			SpecialTypes.s_typeCodeToTypeIdMap[1] = SpecialType.System_Char;
			SpecialTypes.s_typeCodeToTypeIdMap[17] = SpecialType.System_Void;
			SpecialTypes.s_typeCodeToTypeIdMap[11] = SpecialType.System_String;
			SpecialTypes.s_typeCodeToTypeIdMap[7] = SpecialType.System_Int64;
			SpecialTypes.s_typeCodeToTypeIdMap[6] = SpecialType.System_Int32;
			SpecialTypes.s_typeCodeToTypeIdMap[5] = SpecialType.System_Int16;
			SpecialTypes.s_typeCodeToTypeIdMap[2] = SpecialType.System_SByte;
			SpecialTypes.s_typeCodeToTypeIdMap[15] = SpecialType.System_UInt64;
			SpecialTypes.s_typeCodeToTypeIdMap[14] = SpecialType.System_UInt32;
			SpecialTypes.s_typeCodeToTypeIdMap[13] = SpecialType.System_UInt16;
			SpecialTypes.s_typeCodeToTypeIdMap[12] = SpecialType.System_Byte;
			SpecialTypes.s_typeCodeToTypeIdMap[3] = SpecialType.System_Single;
			SpecialTypes.s_typeCodeToTypeIdMap[4] = SpecialType.System_Double;
			SpecialTypes.s_typeCodeToTypeIdMap[8] = SpecialType.System_IntPtr;
			SpecialTypes.s_typeCodeToTypeIdMap[16] = SpecialType.System_UIntPtr;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0003FF86 File Offset: 0x0003E186
		[NullableContext(2)]
		public static string GetMetadataName(this SpecialType id)
		{
			return SpecialTypes.s_emittedNames[(int)id];
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0003FF90 File Offset: 0x0003E190
		public static SpecialType GetTypeFromMetadataName(string metadataName)
		{
			SpecialType specialType;
			if (SpecialTypes.s_nameToTypeIdMap.TryGetValue(metadataName, ref specialType))
			{
				return specialType;
			}
			return SpecialType.None;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0003FFAF File Offset: 0x0003E1AF
		public static SpecialType GetTypeFromMetadataName(PrimitiveTypeCode typeCode)
		{
			return SpecialTypes.s_typeCodeToTypeIdMap[(int)typeCode];
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		public static PrimitiveTypeCode GetTypeCode(SpecialType typeId)
		{
			return SpecialTypes.s_typeIdToTypeCodeMap[(int)typeId];
		}

		// Token: 0x040006E1 RID: 1761
		[Nullable(new byte[] { 1, 2 })]
		private static readonly string[] s_emittedNames = new string[]
		{
			default(string),
			"System.Object",
			"System.Enum",
			"System.MulticastDelegate",
			"System.Delegate",
			"System.ValueType",
			"System.Void",
			"System.Boolean",
			"System.Char",
			"System.SByte",
			"System.Byte",
			"System.Int16",
			"System.UInt16",
			"System.Int32",
			"System.UInt32",
			"System.Int64",
			"System.UInt64",
			"System.Decimal",
			"System.Single",
			"System.Double",
			"System.String",
			"System.IntPtr",
			"System.UIntPtr",
			"System.Array",
			"System.Collections.IEnumerable",
			"System.Collections.Generic.IEnumerable`1",
			"System.Collections.Generic.IList`1",
			"System.Collections.Generic.ICollection`1",
			"System.Collections.IEnumerator",
			"System.Collections.Generic.IEnumerator`1",
			"System.Collections.Generic.IReadOnlyList`1",
			"System.Collections.Generic.IReadOnlyCollection`1",
			"System.Nullable`1",
			"System.DateTime",
			"System.Runtime.CompilerServices.IsVolatile",
			"System.IDisposable",
			"System.TypedReference",
			"System.ArgIterator",
			"System.RuntimeArgumentHandle",
			"System.RuntimeFieldHandle",
			"System.RuntimeMethodHandle",
			"System.RuntimeTypeHandle",
			"System.IAsyncResult",
			"System.AsyncCallback",
			"System.Runtime.CompilerServices.RuntimeFeature",
			"System.Runtime.CompilerServices.PreserveBaseOverridesAttribute"
		};

		// Token: 0x040006E2 RID: 1762
		private static readonly Dictionary<string, SpecialType> s_nameToTypeIdMap = new Dictionary<string, SpecialType>(45);

		// Token: 0x040006E3 RID: 1763
		private static readonly PrimitiveTypeCode[] s_typeIdToTypeCodeMap;

		// Token: 0x040006E4 RID: 1764
		private static readonly SpecialType[] s_typeCodeToTypeIdMap;
	}
}
