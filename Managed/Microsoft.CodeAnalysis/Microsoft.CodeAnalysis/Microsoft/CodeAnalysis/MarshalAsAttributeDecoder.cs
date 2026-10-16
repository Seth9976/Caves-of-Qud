using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200022F RID: 559
	internal static class MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation> where TWellKnownAttributeData : WellKnownAttributeData, IMarshalAsAttributeTarget, new() where TAttributeSyntax : SyntaxNode where TAttributeData : AttributeData
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x00044684 File Offset: 0x00042884
		internal static void Decode(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, AttributeTargets target, CommonMessageProvider messageProvider)
		{
			UnmanagedType unmanagedType = MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsType(arguments.Attribute);
			switch (unmanagedType)
			{
			case 23:
				if (target != 256)
				{
					messageProvider.ReportMarshalUnmanagedTypeOnlyValidForFields(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, "ByValTStr", arguments.Attribute);
					return;
				}
				MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsFixedString(ref arguments, messageProvider);
				return;
			case 24:
			case 27:
			case 31:
			case 32:
			case 33:
				break;
			case 25:
			case 26:
			case 28:
				MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsComInterface(ref arguments, unmanagedType, messageProvider);
				return;
			case 29:
				MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsSafeArray(ref arguments, messageProvider);
				return;
			case 30:
				if (target != 256)
				{
					messageProvider.ReportMarshalUnmanagedTypeOnlyValidForFields(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, "ByValArray", arguments.Attribute);
					return;
				}
				MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsArray(ref arguments, messageProvider, true);
				return;
			case 34:
				if (target == 256)
				{
					messageProvider.ReportMarshalUnmanagedTypeNotValidForFields(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, "VBByRefStr", arguments.Attribute);
					return;
				}
				arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsSimpleType(unmanagedType);
				return;
			default:
				if (unmanagedType == 42)
				{
					MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsArray(ref arguments, messageProvider, false);
					return;
				}
				if (unmanagedType == 44)
				{
					MarshalAsAttributeDecoder<TWellKnownAttributeData, TAttributeSyntax, TAttributeData, TAttributeLocation>.DecodeMarshalAsCustom(ref arguments, messageProvider);
					return;
				}
				break;
			}
			if (unmanagedType < 0 || unmanagedType > 536870911)
			{
				messageProvider.ReportInvalidAttributeArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, 0, arguments.Attribute);
				return;
			}
			arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsSimpleType(unmanagedType);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0004480C File Offset: 0x00042A0C
		private static UnmanagedType DecodeMarshalAsType(AttributeData attribute)
		{
			UnmanagedType unmanagedType;
			if (attribute.AttributeConstructor.Parameters[0].Type.SpecialType == SpecialType.System_Int16)
			{
				unmanagedType = attribute.CommonConstructorArguments[0].DecodeValue<short>(SpecialType.System_Int16);
			}
			else
			{
				unmanagedType = attribute.CommonConstructorArguments[0].DecodeValue<UnmanagedType>(SpecialType.System_Enum);
			}
			return unmanagedType;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00044874 File Offset: 0x00042A74
		private static void DecodeMarshalAsCustom(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, CommonMessageProvider messageProvider)
		{
			ITypeSymbolInternal typeSymbolInternal = null;
			string text = null;
			string text2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in arguments.Attribute.NamedArguments)
			{
				string key = keyValuePair.Key;
				if (!(key == "MarshalType"))
				{
					if (!(key == "MarshalTypeRef"))
					{
						if (key == "MarshalCookie")
						{
							text2 = keyValuePair.Value.DecodeValue<string>(SpecialType.System_String);
							if (!MetadataHelpers.IsValidUnicodeString(text2))
							{
								messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num, arguments.Attribute.AttributeClass, keyValuePair.Key);
								flag3 = true;
							}
						}
					}
					else
					{
						typeSymbolInternal = keyValuePair.Value.DecodeValue<ITypeSymbolInternal>(SpecialType.None);
						flag2 = true;
					}
				}
				else
				{
					text = keyValuePair.Value.DecodeValue<string>(SpecialType.System_String);
					if (!MetadataHelpers.IsValidUnicodeString(text))
					{
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num, arguments.Attribute.AttributeClass, keyValuePair.Key);
						flag3 = true;
					}
					flag = true;
				}
				num++;
			}
			if (!flag && !flag2)
			{
				messageProvider.ReportAttributeParameterRequired(arguments.Diagnostics, arguments.AttributeSyntaxOpt, "MarshalType", "MarshalTypeRef");
				flag3 = true;
			}
			if (!flag3)
			{
				arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsCustom(flag ? text : typeSymbolInternal, text2);
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00044A08 File Offset: 0x00042C08
		private static void DecodeMarshalAsComInterface(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, UnmanagedType unmanagedType, CommonMessageProvider messageProvider)
		{
			int? num = default(int?);
			int num2 = 1;
			bool flag = false;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in arguments.Attribute.NamedArguments)
			{
				if (keyValuePair.Key == "IidParameterIndex")
				{
					num = new int?(keyValuePair.Value.DecodeValue<int>(SpecialType.System_Int32));
					int? num3 = num;
					int num4 = 0;
					if (!((num3.GetValueOrDefault() < num4) & (num3 != null)))
					{
						num3 = num;
						num4 = 536870911;
						if (!((num3.GetValueOrDefault() > num4) & (num3 != null)))
						{
							goto IL_00CE;
						}
					}
					messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num2, arguments.Attribute.AttributeClass, keyValuePair.Key);
					flag = true;
				}
				IL_00CE:
				num2++;
			}
			if (!flag)
			{
				arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsComInterface(unmanagedType, num);
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00044B10 File Offset: 0x00042D10
		private static void DecodeMarshalAsArray(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, CommonMessageProvider messageProvider, bool isFixed)
		{
			UnmanagedType? unmanagedType = default(UnmanagedType?);
			int? num = (isFixed ? new int?(1) : default(int?));
			short? num2 = default(short?);
			bool flag = false;
			int num3 = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in arguments.Attribute.NamedArguments)
			{
				string key = keyValuePair.Key;
				if (!(key == "ArraySubType"))
				{
					if (!(key == "SizeConst"))
					{
						if (!(key == "SizeParamIndex"))
						{
							if (!(key == "SafeArraySubType"))
							{
								goto IL_0265;
							}
						}
						else if (!isFixed)
						{
							num2 = new short?(keyValuePair.Value.DecodeValue<short>(SpecialType.System_Int16));
							short? num4 = num2;
							int? num5 = ((num4 != null) ? new int?((int)num4.GetValueOrDefault()) : default(int?));
							int num6 = 0;
							if ((num5.GetValueOrDefault() < num6) & (num5 != null))
							{
								messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, arguments.Attribute.AttributeClass, keyValuePair.Key);
								flag = true;
								goto IL_0265;
							}
							goto IL_0265;
						}
						messageProvider.ReportParameterNotValidForType(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3);
						flag = true;
					}
					else
					{
						num = new int?(keyValuePair.Value.DecodeValue<int>(SpecialType.System_Int32));
						int? num5 = num;
						int num6 = 0;
						if (!((num5.GetValueOrDefault() < num6) & (num5 != null)))
						{
							num5 = num;
							num6 = 536870911;
							if (!((num5.GetValueOrDefault() > num6) & (num5 != null)))
							{
								goto IL_0265;
							}
						}
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, arguments.Attribute.AttributeClass, keyValuePair.Key);
						flag = true;
					}
				}
				else
				{
					unmanagedType = new UnmanagedType?(keyValuePair.Value.DecodeValue<UnmanagedType>(SpecialType.System_Enum));
					if (!isFixed)
					{
						UnmanagedType? unmanagedType2 = unmanagedType;
						UnmanagedType unmanagedType3 = 44;
						if ((unmanagedType2.GetValueOrDefault() == unmanagedType3) & (unmanagedType2 != null))
						{
							goto IL_00F5;
						}
					}
					if (unmanagedType.Value >= 0 && unmanagedType.Value <= 536870911)
					{
						goto IL_0265;
					}
					IL_00F5:
					messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num3, arguments.Attribute.AttributeClass, keyValuePair.Key);
					flag = true;
				}
				IL_0265:
				num3++;
			}
			if (!flag)
			{
				MarshalPseudoCustomAttributeData orCreateData = arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData();
				if (isFixed)
				{
					orCreateData.SetMarshalAsFixedArray(unmanagedType, num);
					return;
				}
				orCreateData.SetMarshalAsArray(unmanagedType, num, num2);
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00044DC0 File Offset: 0x00042FC0
		private static void DecodeMarshalAsSafeArray(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, CommonMessageProvider messageProvider)
		{
			VarEnum? varEnum = default(VarEnum?);
			ITypeSymbolInternal typeSymbolInternal = null;
			int num = -1;
			bool flag = false;
			int num2 = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in arguments.Attribute.NamedArguments)
			{
				string key = keyValuePair.Key;
				if (!(key == "SafeArraySubType"))
				{
					if (!(key == "SafeArrayUserDefinedSubType"))
					{
						if (key == "ArraySubType" || key == "SizeConst" || key == "SizeParamIndex")
						{
							messageProvider.ReportParameterNotValidForType(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num2);
							flag = true;
						}
					}
					else
					{
						typeSymbolInternal = keyValuePair.Value.DecodeValue<ITypeSymbolInternal>(SpecialType.None);
						num = num2;
					}
				}
				else
				{
					varEnum = new VarEnum?(keyValuePair.Value.DecodeValue<VarEnum>(SpecialType.System_Enum));
					VarEnum? varEnum2 = varEnum;
					VarEnum varEnum3 = VarEnum.VT_EMPTY;
					if (((varEnum2.GetValueOrDefault() < varEnum3) & (varEnum2 != null)) || varEnum.Value > (VarEnum)536870911)
					{
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num2, arguments.Attribute.AttributeClass, keyValuePair.Key);
						flag = true;
					}
				}
				num2++;
			}
			if (varEnum != null)
			{
				VarEnum varEnum3 = varEnum.GetValueOrDefault();
				if (varEnum3 == VarEnum.VT_DISPATCH || varEnum3 == VarEnum.VT_UNKNOWN || varEnum3 == VarEnum.VT_RECORD)
				{
					goto IL_01A2;
				}
			}
			if (varEnum != null && num >= 0)
			{
				messageProvider.ReportParameterNotValidForType(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num);
				flag = true;
			}
			else
			{
				typeSymbolInternal = null;
			}
			IL_01A2:
			if (!flag)
			{
				arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsSafeArray(varEnum, typeSymbolInternal);
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00044F8C File Offset: 0x0004318C
		private static void DecodeMarshalAsFixedString(ref DecodeWellKnownAttributeArguments<TAttributeSyntax, TAttributeData, TAttributeLocation> arguments, CommonMessageProvider messageProvider)
		{
			int num = -1;
			int num2 = 1;
			bool flag = false;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in arguments.Attribute.NamedArguments)
			{
				string key = keyValuePair.Key;
				if (!(key == "SizeConst"))
				{
					if (key == "ArraySubType" || key == "SizeParamIndex")
					{
						messageProvider.ReportParameterNotValidForType(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num2);
						flag = true;
					}
				}
				else
				{
					num = keyValuePair.Value.DecodeValue<int>(SpecialType.System_Int32);
					if (num < 0 || num > 536870911)
					{
						messageProvider.ReportInvalidNamedArgument(arguments.Diagnostics, arguments.AttributeSyntaxOpt, num2, arguments.Attribute.AttributeClass, keyValuePair.Key);
						flag = true;
					}
				}
				num2++;
			}
			if (num < 0)
			{
				messageProvider.ReportAttributeParameterRequired(arguments.Diagnostics, arguments.AttributeSyntaxOpt, "SizeConst");
				flag = true;
			}
			if (!flag)
			{
				arguments.GetOrCreateData<TWellKnownAttributeData>().GetOrCreateData().SetMarshalAsFixedString(num);
			}
		}
	}
}
