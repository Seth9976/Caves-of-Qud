using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000030 RID: 48
	internal static class PatchFunctions
	{
		// Token: 0x0600010E RID: 270 RVA: 0x0000953D File Offset: 0x0000773D
		internal static List<MethodInfo> GetSortedPatchMethods(MethodBase original, Patch[] patches, bool debug)
		{
			return new PatchSorter(patches, debug).Sort(original);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000954C File Offset: 0x0000774C
		internal static MethodInfo UpdateWrapper(MethodBase original, PatchInfo patchInfo)
		{
			bool flag = patchInfo.Debugging || Harmony.DEBUG;
			List<MethodInfo> sortedPatchMethods = PatchFunctions.GetSortedPatchMethods(original, patchInfo.prefixes, flag);
			List<MethodInfo> sortedPatchMethods2 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.postfixes, flag);
			List<MethodInfo> sortedPatchMethods3 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.transpilers, flag);
			List<MethodInfo> sortedPatchMethods4 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.finalizers, flag);
			Dictionary<int, CodeInstruction> dictionary;
			MethodInfo methodInfo = new MethodPatcher(original, null, sortedPatchMethods, sortedPatchMethods2, sortedPatchMethods3, sortedPatchMethods4, flag).CreateReplacement(out dictionary);
			if (methodInfo == null)
			{
				throw new MissingMethodException("Cannot create replacement for " + original.FullDescription());
			}
			try
			{
				Memory.DetourMethodAndPersist(original, methodInfo);
			}
			catch (Exception ex)
			{
				throw HarmonyException.Create(ex, dictionary);
			}
			return methodInfo;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000095F8 File Offset: 0x000077F8
		internal static void UpdateRecompiledMethod(MethodBase original, IntPtr codeStart, PatchInfo patchInfo)
		{
			try
			{
				List<MethodInfo> sortedPatchMethods = PatchFunctions.GetSortedPatchMethods(original, patchInfo.prefixes, false);
				List<MethodInfo> sortedPatchMethods2 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.postfixes, false);
				List<MethodInfo> sortedPatchMethods3 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.transpilers, false);
				List<MethodInfo> sortedPatchMethods4 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.finalizers, false);
				Dictionary<int, CodeInstruction> dictionary;
				MethodInfo methodInfo = new MethodPatcher(original, null, sortedPatchMethods, sortedPatchMethods2, sortedPatchMethods3, sortedPatchMethods4, false).CreateReplacement(out dictionary);
				if (methodInfo == null)
				{
					throw new MissingMethodException("Cannot create replacement for " + original.FullDescription());
				}
				Memory.DetourCompiledMethod(codeStart, methodInfo);
			}
			catch
			{
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000968C File Offset: 0x0000788C
		internal static MethodInfo ReversePatch(HarmonyMethod standin, MethodBase original, MethodInfo postTranspiler)
		{
			if (standin == null)
			{
				throw new ArgumentNullException("standin");
			}
			if (standin.method == null)
			{
				throw new ArgumentNullException("standin", "standin.method is NULL");
			}
			bool flag = standin.debug.GetValueOrDefault() || Harmony.DEBUG;
			List<MethodInfo> list = new List<MethodInfo>();
			HarmonyReversePatchType? reversePatchType = standin.reversePatchType;
			HarmonyReversePatchType harmonyReversePatchType = HarmonyReversePatchType.Snapshot;
			if ((reversePatchType.GetValueOrDefault() == harmonyReversePatchType) & (reversePatchType != null))
			{
				Patches patchInfo = Harmony.GetPatchInfo(original);
				list.AddRange(PatchFunctions.GetSortedPatchMethods(original, patchInfo.Transpilers.ToArray<Patch>(), flag));
			}
			if (postTranspiler != null)
			{
				list.Add(postTranspiler);
			}
			List<MethodInfo> list2 = new List<MethodInfo>();
			Dictionary<int, CodeInstruction> dictionary;
			MethodInfo methodInfo = new MethodPatcher(standin.method, original, list2, list2, list, list2, flag).CreateReplacement(out dictionary);
			if (methodInfo == null)
			{
				throw new MissingMethodException("Cannot create replacement for " + standin.method.FullDescription());
			}
			try
			{
				string text = Memory.DetourMethod(standin.method, methodInfo);
				if (text != null)
				{
					throw new FormatException("Method " + standin.method.FullDescription() + " cannot be patched. Reason: " + text);
				}
			}
			catch (Exception ex)
			{
				throw HarmonyException.Create(ex, dictionary);
			}
			PatchTools.RememberObject(standin.method, methodInfo);
			return methodInfo;
		}
	}
}
