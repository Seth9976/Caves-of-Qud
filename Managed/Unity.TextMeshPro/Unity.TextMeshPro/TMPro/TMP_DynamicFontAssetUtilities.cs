using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000023 RID: 35
	internal class TMP_DynamicFontAssetUtilities
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005274 File Offset: 0x00003474
		private void InitializeSystemFontReferenceCache()
		{
			if (this.s_SystemFontLookup == null)
			{
				this.s_SystemFontLookup = new Dictionary<ulong, TMP_DynamicFontAssetUtilities.FontReference>();
			}
			else
			{
				this.s_SystemFontLookup.Clear();
			}
			if (this.s_SystemFontPaths == null)
			{
				this.s_SystemFontPaths = Font.GetPathsToOSFonts();
			}
			for (int i = 0; i < this.s_SystemFontPaths.Length; i++)
			{
				FontEngineError fontEngineError = FontEngine.LoadFontFace(this.s_SystemFontPaths[i]);
				if (fontEngineError != FontEngineError.Success)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Error [",
						fontEngineError.ToString(),
						"] trying to load the font at path [",
						this.s_SystemFontPaths[i],
						"]."
					}));
				}
				else
				{
					string[] fontFaces = FontEngine.GetFontFaces();
					for (int j = 0; j < fontFaces.Length; j++)
					{
						TMP_DynamicFontAssetUtilities.FontReference fontReference = new TMP_DynamicFontAssetUtilities.FontReference(this.s_SystemFontPaths[i], fontFaces[j], j);
						if (!this.s_SystemFontLookup.ContainsKey(fontReference.hashCode))
						{
							this.s_SystemFontLookup.Add(fontReference.hashCode, fontReference);
							Debug.Log(string.Concat(new string[]
							{
								"[",
								i.ToString(),
								"] Family Name [",
								fontReference.familyName,
								"]   Style Name [",
								fontReference.styleName,
								"]   Index [",
								fontReference.faceIndex.ToString(),
								"]   HashCode [",
								fontReference.hashCode.ToString(),
								"]    Path [",
								fontReference.filePath,
								"]."
							}));
						}
					}
					FontEngine.UnloadFontFace();
				}
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005417 File Offset: 0x00003617
		public static bool TryGetSystemFontReference(string familyName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return TMP_DynamicFontAssetUtilities.s_Instance.TryGetSystemFontReferenceInternal(familyName, null, out fontRef);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005426 File Offset: 0x00003626
		public static bool TryGetSystemFontReference(string familyName, string styleName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return TMP_DynamicFontAssetUtilities.s_Instance.TryGetSystemFontReferenceInternal(familyName, styleName, out fontRef);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005438 File Offset: 0x00003638
		private bool TryGetSystemFontReferenceInternal(string familyName, string styleName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			if (this.s_SystemFontLookup == null)
			{
				this.InitializeSystemFontReferenceCache();
			}
			fontRef = default(TMP_DynamicFontAssetUtilities.FontReference);
			uint hashCodeCaseInSensitive = TMP_TextUtilities.GetHashCodeCaseInSensitive(familyName);
			uint num = (string.IsNullOrEmpty(styleName) ? this.s_RegularStyleNameHashCode : TMP_TextUtilities.GetHashCodeCaseInSensitive(styleName));
			ulong num2 = ((ulong)num << 32) | (ulong)hashCodeCaseInSensitive;
			if (this.s_SystemFontLookup.ContainsKey(num2))
			{
				fontRef = this.s_SystemFontLookup[num2];
				return true;
			}
			if (num != this.s_RegularStyleNameHashCode)
			{
				return false;
			}
			foreach (KeyValuePair<ulong, TMP_DynamicFontAssetUtilities.FontReference> keyValuePair in this.s_SystemFontLookup)
			{
				if (keyValuePair.Value.familyName == familyName)
				{
					fontRef = keyValuePair.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400009F RID: 159
		private static TMP_DynamicFontAssetUtilities s_Instance = new TMP_DynamicFontAssetUtilities();

		// Token: 0x040000A0 RID: 160
		private Dictionary<ulong, TMP_DynamicFontAssetUtilities.FontReference> s_SystemFontLookup;

		// Token: 0x040000A1 RID: 161
		private string[] s_SystemFontPaths;

		// Token: 0x040000A2 RID: 162
		private uint s_RegularStyleNameHashCode = 1291372090U;

		// Token: 0x02000095 RID: 149
		public struct FontReference
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x00043A8C File Offset: 0x00041C8C
			public FontReference(string fontFilePath, string faceNameAndStyle, int index)
			{
				this.familyName = null;
				this.styleName = null;
				this.faceIndex = index;
				uint num = 0U;
				uint num2 = 0U;
				this.filePath = fontFilePath;
				int length = faceNameAndStyle.Length;
				char[] array = new char[length];
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < length; i++)
				{
					char c = faceNameAndStyle[i];
					if (num3 == 0)
					{
						if (i + 2 < length && c == ' ' && faceNameAndStyle[i + 1] == '-' && faceNameAndStyle[i + 2] == ' ')
						{
							num3 = 1;
							this.familyName = new string(array, 0, num4);
							i += 2;
							num4 = 0;
						}
						else
						{
							num = ((num << 5) + num) ^ (uint)TMP_TextUtilities.ToUpperFast(c);
							array[num4++] = c;
						}
					}
					else if (num3 == 1)
					{
						num2 = ((num2 << 5) + num2) ^ (uint)TMP_TextUtilities.ToUpperFast(c);
						array[num4++] = c;
						if (i + 1 == length)
						{
							this.styleName = new string(array, 0, num4);
						}
					}
				}
				this.hashCode = ((ulong)num2 << 32) | (ulong)num;
			}

			// Token: 0x04000620 RID: 1568
			public string familyName;

			// Token: 0x04000621 RID: 1569
			public string styleName;

			// Token: 0x04000622 RID: 1570
			public int faceIndex;

			// Token: 0x04000623 RID: 1571
			public string filePath;

			// Token: 0x04000624 RID: 1572
			public ulong hashCode;
		}
	}
}
