using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A9 RID: 425
	internal static class ModuleExtensions
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00033634 File Offset: 0x00031834
		public static bool ShouldImportNestedType(this PEModule module, TypeDefinitionHandle typeDef)
		{
			return true;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00033638 File Offset: 0x00031838
		public static bool ShouldImportField(this PEModule module, FieldDefinitionHandle field, MetadataImportOptions importOptions)
		{
			bool flag;
			try
			{
				flag = ModuleExtensions.ShouldImportField(module.GetFieldDefFlagsOrThrow(field), importOptions);
			}
			catch (BadImageFormatException)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003366C File Offset: 0x0003186C
		public static bool ShouldImportField(FieldAttributes flags, MetadataImportOptions importOptions)
		{
			FieldAttributes fieldAttributes = flags & 7;
			if (fieldAttributes > 1)
			{
				return fieldAttributes != 3 || importOptions >= MetadataImportOptions.Internal;
			}
			return importOptions == MetadataImportOptions.All;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00033698 File Offset: 0x00031898
		public static bool ShouldImportMethod(this PEModule module, MethodDefinitionHandle methodDef, MetadataImportOptions importOptions)
		{
			try
			{
				MethodAttributes methodDefFlagsOrThrow = module.GetMethodDefFlagsOrThrow(methodDef);
				if ((methodDefFlagsOrThrow & 64) == null)
				{
					MethodAttributes methodAttributes = methodDefFlagsOrThrow & 7;
					if (methodAttributes > 1)
					{
						if (methodAttributes == 3)
						{
							if (importOptions == MetadataImportOptions.Public)
							{
								return false;
							}
						}
					}
					else if (importOptions != MetadataImportOptions.All)
					{
						return false;
					}
				}
			}
			catch (BadImageFormatException)
			{
			}
			bool flag;
			try
			{
				flag = !module.GetMethodDefNameOrThrow(methodDef).StartsWith("_VtblGap", 4);
			}
			catch (BadImageFormatException)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00033710 File Offset: 0x00031910
		public static int GetVTableGapSize(string emittedMethodName)
		{
			if (!emittedMethodName.StartsWith("_VtblGap", 4))
			{
				return 0;
			}
			int num = "_VtblGap".Length;
			while (num < emittedMethodName.Length && char.IsDigit(emittedMethodName, num))
			{
				num++;
			}
			if (num == "_VtblGap".Length || num >= emittedMethodName.Length - 1 || emittedMethodName.get_Chars(num) != '_' || !char.IsDigit(emittedMethodName, num + 1))
			{
				return 1;
			}
			int num2;
			if (int.TryParse(emittedMethodName.Substring(num + 1), 0, CultureInfo.InvariantCulture, ref num2) && num2 > 0)
			{
				return num2;
			}
			return 1;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0003379F File Offset: 0x0003199F
		public static string GetVTableGapName(int sequenceNumber, int countOfSlots)
		{
			return string.Format("_VtblGap{0}_{1}", sequenceNumber, countOfSlots);
		}

		// Token: 0x04000541 RID: 1345
		private const string VTableGapMethodNamePrefix = "_VtblGap";
	}
}
