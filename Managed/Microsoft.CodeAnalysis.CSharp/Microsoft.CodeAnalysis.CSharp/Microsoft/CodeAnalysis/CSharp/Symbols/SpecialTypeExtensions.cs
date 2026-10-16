using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000560 RID: 1376
	internal static class SpecialTypeExtensions
	{
		// Token: 0x06007442 RID: 29762 RVA: 0x001B2628 File Offset: 0x001B0828
		public static bool CanBeConst(this SpecialType specialType)
		{
			return specialType - SpecialType.System_Boolean <= 13;
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x001B2634 File Offset: 0x001B0834
		public static bool IsValidVolatileFieldType(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Single:
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
				return true;
			}
			return false;
		}

		// Token: 0x06007444 RID: 29764 RVA: 0x001B268E File Offset: 0x001B088E
		public static int FixedBufferElementSizeInBytes(this SpecialType specialType)
		{
			if (specialType != SpecialType.System_Decimal)
			{
				return specialType.SizeInBytes();
			}
			return 0;
		}
	}
}
