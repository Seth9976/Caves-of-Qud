using System;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C8 RID: 456
	internal static class PrimitiveTypeCodeExtensions
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x0003A11B File Offset: 0x0003831B
		public static bool Is64BitIntegral(this PrimitiveTypeCode kind)
		{
			return kind == PrimitiveTypeCode.Int64 || kind == PrimitiveTypeCode.UInt64;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0003A129 File Offset: 0x00038329
		public static bool IsSigned(this PrimitiveTypeCode kind)
		{
			return kind - PrimitiveTypeCode.Int8 <= 6;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0003A134 File Offset: 0x00038334
		public static bool IsUnsigned(this PrimitiveTypeCode kind)
		{
			if (kind != PrimitiveTypeCode.Char)
			{
				switch (kind)
				{
				case PrimitiveTypeCode.Pointer:
				case PrimitiveTypeCode.UInt8:
				case PrimitiveTypeCode.UInt16:
				case PrimitiveTypeCode.UInt32:
				case PrimitiveTypeCode.UInt64:
				case PrimitiveTypeCode.UIntPtr:
				case PrimitiveTypeCode.FunctionPointer:
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0003A174 File Offset: 0x00038374
		public static bool IsFloatingPoint(this PrimitiveTypeCode kind)
		{
			return kind - PrimitiveTypeCode.Float32 <= 1;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003A180 File Offset: 0x00038380
		public static ConstantValueTypeDiscriminator GetConstantValueTypeDiscriminator(this PrimitiveTypeCode type)
		{
			switch (type)
			{
			case PrimitiveTypeCode.Boolean:
				return ConstantValueTypeDiscriminator.Boolean;
			case PrimitiveTypeCode.Char:
				return ConstantValueTypeDiscriminator.Char;
			case PrimitiveTypeCode.Int8:
				return ConstantValueTypeDiscriminator.SByte;
			case PrimitiveTypeCode.Float32:
				return ConstantValueTypeDiscriminator.Single;
			case PrimitiveTypeCode.Float64:
				return ConstantValueTypeDiscriminator.Double;
			case PrimitiveTypeCode.Int16:
				return ConstantValueTypeDiscriminator.Int16;
			case PrimitiveTypeCode.Int32:
				return ConstantValueTypeDiscriminator.Int32;
			case PrimitiveTypeCode.Int64:
				return ConstantValueTypeDiscriminator.Int64;
			case PrimitiveTypeCode.String:
				return ConstantValueTypeDiscriminator.String;
			case PrimitiveTypeCode.UInt8:
				return ConstantValueTypeDiscriminator.Byte;
			case PrimitiveTypeCode.UInt16:
				return ConstantValueTypeDiscriminator.UInt16;
			case PrimitiveTypeCode.UInt32:
				return ConstantValueTypeDiscriminator.UInt32;
			case PrimitiveTypeCode.UInt64:
				return ConstantValueTypeDiscriminator.UInt64;
			}
			throw ExceptionUtilities.UnexpectedValue(type);
		}
	}
}
