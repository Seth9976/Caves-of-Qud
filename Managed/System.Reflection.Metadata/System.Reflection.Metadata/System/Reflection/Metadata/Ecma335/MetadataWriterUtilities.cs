using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000107 RID: 263
	internal static class MetadataWriterUtilities
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		[NullableContext(2)]
		public static SignatureTypeCode GetConstantTypeCode(object value)
		{
			if (value == null)
			{
				return (SignatureTypeCode)18;
			}
			if (value.GetType() == typeof(int))
			{
				return SignatureTypeCode.Int32;
			}
			if (value.GetType() == typeof(string))
			{
				return SignatureTypeCode.String;
			}
			if (value.GetType() == typeof(bool))
			{
				return SignatureTypeCode.Boolean;
			}
			if (value.GetType() == typeof(char))
			{
				return SignatureTypeCode.Char;
			}
			if (value.GetType() == typeof(byte))
			{
				return SignatureTypeCode.Byte;
			}
			if (value.GetType() == typeof(long))
			{
				return SignatureTypeCode.Int64;
			}
			if (value.GetType() == typeof(double))
			{
				return SignatureTypeCode.Double;
			}
			if (value.GetType() == typeof(short))
			{
				return SignatureTypeCode.Int16;
			}
			if (value.GetType() == typeof(ushort))
			{
				return SignatureTypeCode.UInt16;
			}
			if (value.GetType() == typeof(uint))
			{
				return SignatureTypeCode.UInt32;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return SignatureTypeCode.SByte;
			}
			if (value.GetType() == typeof(ulong))
			{
				return SignatureTypeCode.UInt64;
			}
			if (value.GetType() == typeof(float))
			{
				return SignatureTypeCode.Single;
			}
			throw new ArgumentException(SR.Format(SR.InvalidConstantValueOfType, value.GetType()), "value");
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001BF58 File Offset: 0x0001A158
		internal static void SerializeRowCounts([Nullable(1)] BlobBuilder writer, ImmutableArray<int> rowCounts)
		{
			for (int i = 0; i < rowCounts.Length; i++)
			{
				int num = rowCounts[i];
				if (num > 0)
				{
					writer.WriteInt32(num);
				}
			}
		}
	}
}
