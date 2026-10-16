using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D8 RID: 1240
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleValueKeywordExtension
	{
		// Token: 0x060025EE RID: 9710 RVA: 0x0008980C File Offset: 0x00087A0C
		public static string ToUssString(this StyleValueKeyword svk)
		{
			string text;
			switch (svk)
			{
			case StyleValueKeyword.Inherit:
				text = "inherit";
				break;
			case StyleValueKeyword.Initial:
				text = "initial";
				break;
			case StyleValueKeyword.Auto:
				text = "auto";
				break;
			case StyleValueKeyword.Unset:
				text = "unset";
				break;
			case StyleValueKeyword.True:
				text = "true";
				break;
			case StyleValueKeyword.False:
				text = "false";
				break;
			case StyleValueKeyword.None:
				text = "none";
				break;
			case StyleValueKeyword.Cover:
				text = "cover";
				break;
			case StyleValueKeyword.Contain:
				text = "contain";
				break;
			default:
				throw new ArgumentOutOfRangeException("svk", svk, "Unknown StyleValueKeyword");
			}
			return text;
		}
	}
}
