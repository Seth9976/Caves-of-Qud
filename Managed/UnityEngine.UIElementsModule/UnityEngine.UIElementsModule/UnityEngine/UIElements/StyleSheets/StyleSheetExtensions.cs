using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006E8 RID: 1768
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleSheetExtensions
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x000C71E4 File Offset: 0x000C53E4
		public static string ReadAsString(this StyleSheet sheet, StyleValueHandle handle)
		{
			string text = string.Empty;
			switch (handle.valueType)
			{
			case StyleValueType.Keyword:
				text = sheet.ReadKeyword(handle).ToUssString();
				break;
			case StyleValueType.Float:
				text = sheet.ReadFloat(handle).ToString(CultureInfo.InvariantCulture.NumberFormat);
				break;
			case StyleValueType.Dimension:
				text = sheet.ReadDimension(handle).ToString();
				break;
			case StyleValueType.Color:
				text = sheet.ReadColor(handle).ToString();
				break;
			case StyleValueType.ResourcePath:
				text = sheet.ReadResourcePath(handle);
				break;
			case StyleValueType.AssetReference:
				text = sheet.ReadAssetReference(handle).ToString();
				break;
			case StyleValueType.Enum:
				text = sheet.ReadEnum(handle);
				break;
			case StyleValueType.Variable:
				text = sheet.ReadVariable(handle);
				break;
			case StyleValueType.String:
				text = sheet.ReadString(handle);
				break;
			case StyleValueType.Function:
				text = sheet.ReadFunctionName(handle);
				break;
			case StyleValueType.CommaSeparator:
				text = ",";
				break;
			case StyleValueType.ScalableImage:
				text = sheet.ReadScalableImage(handle).ToString();
				break;
			default:
				text = "Error reading value type (" + handle.valueType.ToString() + ") at index " + handle.valueIndex.ToString();
				break;
			}
			return text;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000C734C File Offset: 0x000C554C
		public static bool IsVarFunction(this StyleValueHandle handle)
		{
			return handle.valueType == StyleValueType.Function && handle.valueIndex == 1;
		}
	}
}
