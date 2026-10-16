using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000087 RID: 135
	public class ReversePatcher
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000E5D5 File Offset: 0x0000C7D5
		public ReversePatcher(Harmony instance, MethodBase original, HarmonyMethod standin)
		{
			this.instance = instance;
			this.original = original;
			this.standin = standin;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public MethodInfo Patch(HarmonyReversePatchType type = HarmonyReversePatchType.Original)
		{
			if (this.original == null)
			{
				throw new NullReferenceException("Null method for " + this.instance.Id);
			}
			this.standin.reversePatchType = new HarmonyReversePatchType?(type);
			MethodInfo transpiler = ReversePatcher.GetTranspiler(this.standin.method);
			return PatchFunctions.ReversePatch(this.standin, this.original, transpiler);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E658 File Offset: 0x0000C858
		internal static MethodInfo GetTranspiler(MethodInfo method)
		{
			string methodName = method.Name;
			IEnumerable<MethodInfo> declaredMethods = AccessTools.GetDeclaredMethods(method.DeclaringType);
			Type ici = typeof(IEnumerable<CodeInstruction>);
			return declaredMethods.FirstOrDefault<MethodInfo>((MethodInfo m) => !(m.ReturnType != ici) && m.Name.StartsWith("<" + methodName + ">"));
		}

		// Token: 0x040001A9 RID: 425
		private readonly Harmony instance;

		// Token: 0x040001AA RID: 426
		private readonly MethodBase original;

		// Token: 0x040001AB RID: 427
		private readonly HarmonyMethod standin;
	}
}
