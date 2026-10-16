using System;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class PatchInfo
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public bool Debugging
		{
			get
			{
				if (!this.prefixes.Any<Patch>((Patch p) => p.debug))
				{
					if (!this.postfixes.Any<Patch>((Patch p) => p.debug))
					{
						if (!this.transpilers.Any<Patch>((Patch p) => p.debug))
						{
							return this.finalizers.Any<Patch>((Patch p) => p.debug);
						}
					}
				}
				return true;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CA68 File Offset: 0x0000AC68
		internal void AddPrefixes(string owner, params HarmonyMethod[] methods)
		{
			this.prefixes = PatchInfo.Add(owner, methods, this.prefixes);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CA80 File Offset: 0x0000AC80
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddPrefix(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddPrefixes(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000CAAF File Offset: 0x0000ACAF
		public void RemovePrefix(string owner)
		{
			this.prefixes = PatchInfo.Remove(owner, this.prefixes);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CAC3 File Offset: 0x0000ACC3
		internal void AddPostfixes(string owner, params HarmonyMethod[] methods)
		{
			this.postfixes = PatchInfo.Add(owner, methods, this.postfixes);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddPostfix(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddPostfixes(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000CB07 File Offset: 0x0000AD07
		public void RemovePostfix(string owner)
		{
			this.postfixes = PatchInfo.Remove(owner, this.postfixes);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000CB1B File Offset: 0x0000AD1B
		internal void AddTranspilers(string owner, params HarmonyMethod[] methods)
		{
			this.transpilers = PatchInfo.Add(owner, methods, this.transpilers);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CB30 File Offset: 0x0000AD30
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddTranspiler(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddTranspilers(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CB5F File Offset: 0x0000AD5F
		public void RemoveTranspiler(string owner)
		{
			this.transpilers = PatchInfo.Remove(owner, this.transpilers);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CB73 File Offset: 0x0000AD73
		internal void AddFinalizers(string owner, params HarmonyMethod[] methods)
		{
			this.finalizers = PatchInfo.Add(owner, methods, this.finalizers);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000CB88 File Offset: 0x0000AD88
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddFinalizer(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddFinalizers(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CBB7 File Offset: 0x0000ADB7
		public void RemoveFinalizer(string owner)
		{
			this.finalizers = PatchInfo.Remove(owner, this.finalizers);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public void RemovePatch(MethodInfo patch)
		{
			this.prefixes = this.prefixes.Where<Patch>((Patch p) => p.PatchMethod != patch).ToArray<Patch>();
			this.postfixes = this.postfixes.Where<Patch>((Patch p) => p.PatchMethod != patch).ToArray<Patch>();
			this.transpilers = this.transpilers.Where<Patch>((Patch p) => p.PatchMethod != patch).ToArray<Patch>();
			this.finalizers = this.finalizers.Where<Patch>((Patch p) => p.PatchMethod != patch).ToArray<Patch>();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CC70 File Offset: 0x0000AE70
		private static Patch[] Add(string owner, HarmonyMethod[] add, Patch[] current)
		{
			if (add.Length == 0)
			{
				return current;
			}
			int initialIndex = current.Length;
			return current.Concat<Patch>(add.Where<HarmonyMethod>((HarmonyMethod method) => method != null).Select<HarmonyMethod, Patch>((HarmonyMethod method, int i) => new Patch(method, i + initialIndex, owner))).ToArray<Patch>();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		private static Patch[] Remove(string owner, Patch[] current)
		{
			if (!(owner == "*"))
			{
				return current.Where<Patch>((Patch patch) => patch.owner != owner).ToArray<Patch>();
			}
			return new Patch[0];
		}

		// Token: 0x0400015B RID: 347
		public Patch[] prefixes = new Patch[0];

		// Token: 0x0400015C RID: 348
		public Patch[] postfixes = new Patch[0];

		// Token: 0x0400015D RID: 349
		public Patch[] transpilers = new Patch[0];

		// Token: 0x0400015E RID: 350
		public Patch[] finalizers = new Patch[0];
	}
}
