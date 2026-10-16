using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarmonyLib
{
	// Token: 0x02000081 RID: 129
	public class Patches
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000DD48 File Offset: 0x0000BF48
		public ReadOnlyCollection<string> Owners
		{
			get
			{
				HashSet<string> hashSet = new HashSet<string>();
				hashSet.UnionWith(this.Prefixes.Select<Patch, string>((Patch p) => p.owner));
				hashSet.UnionWith(this.Postfixes.Select<Patch, string>((Patch p) => p.owner));
				hashSet.UnionWith(this.Transpilers.Select<Patch, string>((Patch p) => p.owner));
				hashSet.UnionWith(this.Finalizers.Select<Patch, string>((Patch p) => p.owner));
				return hashSet.ToList<string>().AsReadOnly();
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DE24 File Offset: 0x0000C024
		public Patches(Patch[] prefixes, Patch[] postfixes, Patch[] transpilers, Patch[] finalizers)
		{
			if (prefixes == null)
			{
				prefixes = new Patch[0];
			}
			if (postfixes == null)
			{
				postfixes = new Patch[0];
			}
			if (transpilers == null)
			{
				transpilers = new Patch[0];
			}
			if (finalizers == null)
			{
				finalizers = new Patch[0];
			}
			this.Prefixes = prefixes.ToList<Patch>().AsReadOnly();
			this.Postfixes = postfixes.ToList<Patch>().AsReadOnly();
			this.Transpilers = transpilers.ToList<Patch>().AsReadOnly();
			this.Finalizers = finalizers.ToList<Patch>().AsReadOnly();
		}

		// Token: 0x04000185 RID: 389
		public readonly ReadOnlyCollection<Patch> Prefixes;

		// Token: 0x04000186 RID: 390
		public readonly ReadOnlyCollection<Patch> Postfixes;

		// Token: 0x04000187 RID: 391
		public readonly ReadOnlyCollection<Patch> Transpilers;

		// Token: 0x04000188 RID: 392
		public readonly ReadOnlyCollection<Patch> Finalizers;
	}
}
