using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001FD RID: 509
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SwitchConstantValueHelper
	{
		// Token: 0x06001480 RID: 5248 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		public static bool IsValidSwitchCaseLabelConstant(ConstantValue constant)
		{
			switch (constant.Discriminator)
			{
			case ConstantValueTypeDiscriminator.Nothing:
			case ConstantValueTypeDiscriminator.SByte:
			case ConstantValueTypeDiscriminator.Byte:
			case ConstantValueTypeDiscriminator.Int16:
			case ConstantValueTypeDiscriminator.UInt16:
			case ConstantValueTypeDiscriminator.Int32:
			case ConstantValueTypeDiscriminator.UInt32:
			case ConstantValueTypeDiscriminator.Int64:
			case ConstantValueTypeDiscriminator.UInt64:
			case ConstantValueTypeDiscriminator.Char:
			case ConstantValueTypeDiscriminator.Boolean:
			case ConstantValueTypeDiscriminator.String:
				return true;
			}
			return false;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00040C14 File Offset: 0x0003EE14
		public static int CompareSwitchCaseLabelConstants(ConstantValue first, ConstantValue second)
		{
			if (first.IsNull)
			{
				if (!second.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (second.IsNull)
				{
					return 1;
				}
				switch (first.Discriminator)
				{
				case ConstantValueTypeDiscriminator.SByte:
				case ConstantValueTypeDiscriminator.Int16:
				case ConstantValueTypeDiscriminator.Int32:
				case ConstantValueTypeDiscriminator.Int64:
					return first.Int64Value.CompareTo(second.Int64Value);
				case ConstantValueTypeDiscriminator.Byte:
				case ConstantValueTypeDiscriminator.UInt16:
				case ConstantValueTypeDiscriminator.UInt32:
				case ConstantValueTypeDiscriminator.UInt64:
				case ConstantValueTypeDiscriminator.Char:
				case ConstantValueTypeDiscriminator.Boolean:
					return first.UInt64Value.CompareTo(second.UInt64Value);
				case ConstantValueTypeDiscriminator.String:
					return string.CompareOrdinal(first.StringValue, second.StringValue);
				}
				throw ExceptionUtilities.UnexpectedValue(first.Discriminator);
			}
		}

		// Token: 0x020005E9 RID: 1513
		[Nullable(new byte[] { 0, 1 })]
		public class SwitchLabelsComparer : EqualityComparer<object>
		{
			// Token: 0x060036E7 RID: 14055 RVA: 0x0009A07C File Offset: 0x0009827C
			[NullableContext(2)]
			public override bool Equals(object first, object second)
			{
				ConstantValue constantValue = first as ConstantValue;
				if (constantValue != null)
				{
					ConstantValue constantValue2 = second as ConstantValue;
					if (constantValue2 != null)
					{
						if (!SwitchConstantValueHelper.IsValidSwitchCaseLabelConstant(constantValue) || !SwitchConstantValueHelper.IsValidSwitchCaseLabelConstant(constantValue2))
						{
							return constantValue.Equals(constantValue2);
						}
						return SwitchConstantValueHelper.CompareSwitchCaseLabelConstants(constantValue, constantValue2) == 0;
					}
				}
				string text = first as string;
				if (text != null)
				{
					return string.Equals(text, second as string, 4);
				}
				return first.Equals(second);
			}

			// Token: 0x060036E8 RID: 14056 RVA: 0x0009A0EC File Offset: 0x000982EC
			public override int GetHashCode(object obj)
			{
				ConstantValue constantValue = obj as ConstantValue;
				if (constantValue != null)
				{
					switch (constantValue.Discriminator)
					{
					case ConstantValueTypeDiscriminator.SByte:
					case ConstantValueTypeDiscriminator.Int16:
					case ConstantValueTypeDiscriminator.Int32:
					case ConstantValueTypeDiscriminator.Int64:
						return constantValue.Int64Value.GetHashCode();
					case ConstantValueTypeDiscriminator.Byte:
					case ConstantValueTypeDiscriminator.UInt16:
					case ConstantValueTypeDiscriminator.UInt32:
					case ConstantValueTypeDiscriminator.UInt64:
					case ConstantValueTypeDiscriminator.Char:
					case ConstantValueTypeDiscriminator.Boolean:
						return constantValue.UInt64Value.GetHashCode();
					case ConstantValueTypeDiscriminator.String:
						return constantValue.RopeValue.GetHashCode();
					}
				}
				return obj.GetHashCode();
			}
		}
	}
}
