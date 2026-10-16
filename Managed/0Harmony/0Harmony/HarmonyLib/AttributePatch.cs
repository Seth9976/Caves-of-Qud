using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000034 RID: 52
	internal class AttributePatch
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00009980 File Offset: 0x00007B80
		internal static AttributePatch Create(MethodInfo patch)
		{
			if (patch == null)
			{
				throw new NullReferenceException("Patch method cannot be null");
			}
			object[] customAttributes = patch.GetCustomAttributes(true);
			HarmonyPatchType? patchType = AttributePatch.GetPatchType(patch.Name, customAttributes);
			if (patchType == null)
			{
				return null;
			}
			HarmonyPatchType? harmonyPatchType = patchType;
			HarmonyPatchType harmonyPatchType2 = HarmonyPatchType.ReversePatch;
			if (!((harmonyPatchType.GetValueOrDefault() == harmonyPatchType2) & (harmonyPatchType != null)) && !patch.IsStatic)
			{
				throw new ArgumentException("Patch method " + patch.FullDescription() + " must be static");
			}
			HarmonyMethod harmonyMethod = HarmonyMethod.Merge((from attr in customAttributes
				where attr.GetType().BaseType.FullName == AttributePatch.harmonyAttributeName
				select AccessTools.Field(attr.GetType(), "info").GetValue(attr) into harmonyInfo
				select AccessTools.MakeDeepCopy<HarmonyMethod>(harmonyInfo)).ToList<HarmonyMethod>());
			harmonyMethod.method = patch;
			return new AttributePatch
			{
				info = harmonyMethod,
				type = patchType
			};
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009A8C File Offset: 0x00007C8C
		private static HarmonyPatchType? GetPatchType(string methodName, object[] allAttributes)
		{
			HashSet<string> hashSet = new HashSet<string>(from attr in allAttributes
				select attr.GetType().FullName into name
				where name.StartsWith("Harmony")
				select name);
			HarmonyPatchType? harmonyPatchType = null;
			foreach (HarmonyPatchType harmonyPatchType2 in AttributePatch.allPatchTypes)
			{
				string text = harmonyPatchType2.ToString();
				if (text == methodName || hashSet.Contains("HarmonyLib.Harmony" + text))
				{
					harmonyPatchType = new HarmonyPatchType?(harmonyPatchType2);
					break;
				}
			}
			return harmonyPatchType;
		}

		// Token: 0x040000C2 RID: 194
		private static readonly HarmonyPatchType[] allPatchTypes = new HarmonyPatchType[]
		{
			HarmonyPatchType.Prefix,
			HarmonyPatchType.Postfix,
			HarmonyPatchType.Transpiler,
			HarmonyPatchType.Finalizer,
			HarmonyPatchType.ReversePatch
		};

		// Token: 0x040000C3 RID: 195
		internal HarmonyMethod info;

		// Token: 0x040000C4 RID: 196
		internal HarmonyPatchType? type;

		// Token: 0x040000C5 RID: 197
		private static readonly string harmonyAttributeName = typeof(HarmonyAttribute).FullName;
	}
}
