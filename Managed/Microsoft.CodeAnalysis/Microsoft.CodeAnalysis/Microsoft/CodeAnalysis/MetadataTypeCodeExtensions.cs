using System;
using System.Reflection.Metadata;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A7 RID: 423
	internal static class MetadataTypeCodeExtensions
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x000331E0 File Offset: 0x000313E0
		internal static SpecialType ToSpecialType(this SignatureTypeCode typeCode)
		{
			switch (typeCode)
			{
			case SignatureTypeCode.Void:
				return SpecialType.System_Void;
			case SignatureTypeCode.Boolean:
				return SpecialType.System_Boolean;
			case SignatureTypeCode.Char:
				return SpecialType.System_Char;
			case SignatureTypeCode.SByte:
				return SpecialType.System_SByte;
			case SignatureTypeCode.Byte:
				return SpecialType.System_Byte;
			case SignatureTypeCode.Int16:
				return SpecialType.System_Int16;
			case SignatureTypeCode.UInt16:
				return SpecialType.System_UInt16;
			case SignatureTypeCode.Int32:
				return SpecialType.System_Int32;
			case SignatureTypeCode.UInt32:
				return SpecialType.System_UInt32;
			case SignatureTypeCode.Int64:
				return SpecialType.System_Int64;
			case SignatureTypeCode.UInt64:
				return SpecialType.System_UInt64;
			case SignatureTypeCode.Single:
				return SpecialType.System_Single;
			case SignatureTypeCode.Double:
				return SpecialType.System_Double;
			case SignatureTypeCode.String:
				return SpecialType.System_String;
			case SignatureTypeCode.TypedReference:
				return SpecialType.System_TypedReference;
			case SignatureTypeCode.IntPtr:
				return SpecialType.System_IntPtr;
			case SignatureTypeCode.UIntPtr:
				return SpecialType.System_UIntPtr;
			case SignatureTypeCode.Object:
				return SpecialType.System_Object;
			}
			throw ExceptionUtilities.UnexpectedValue(typeCode);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000332A4 File Offset: 0x000314A4
		internal static bool HasShortFormSignatureEncoding(this SpecialType type)
		{
			switch (type)
			{
			case SpecialType.System_Object:
			case SpecialType.System_Void:
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_String:
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
				break;
			case SpecialType.System_Enum:
			case SpecialType.System_MulticastDelegate:
			case SpecialType.System_Delegate:
			case SpecialType.System_ValueType:
			case SpecialType.System_Decimal:
				return false;
			default:
				if (type != SpecialType.System_TypedReference)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003331C File Offset: 0x0003151C
		internal static SerializationTypeCode ToSerializationType(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_Object:
				return SerializationTypeCode.TaggedObject;
			case SpecialType.System_Boolean:
				return SerializationTypeCode.Boolean;
			case SpecialType.System_Char:
				return SerializationTypeCode.Char;
			case SpecialType.System_SByte:
				return SerializationTypeCode.SByte;
			case SpecialType.System_Byte:
				return SerializationTypeCode.Byte;
			case SpecialType.System_Int16:
				return SerializationTypeCode.Int16;
			case SpecialType.System_UInt16:
				return SerializationTypeCode.UInt16;
			case SpecialType.System_Int32:
				return SerializationTypeCode.Int32;
			case SpecialType.System_UInt32:
				return SerializationTypeCode.UInt32;
			case SpecialType.System_Int64:
				return SerializationTypeCode.Int64;
			case SpecialType.System_UInt64:
				return SerializationTypeCode.UInt64;
			case SpecialType.System_Single:
				return SerializationTypeCode.Single;
			case SpecialType.System_Double:
				return SerializationTypeCode.Double;
			case SpecialType.System_String:
				return SerializationTypeCode.String;
			}
			throw ExceptionUtilities.UnexpectedValue(specialType);
		}
	}
}
