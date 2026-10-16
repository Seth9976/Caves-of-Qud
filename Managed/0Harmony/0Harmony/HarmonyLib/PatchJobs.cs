using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000031 RID: 49
	internal class PatchJobs<T>
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000097C0 File Offset: 0x000079C0
		internal PatchJobs<T>.Job GetJob(MethodBase method)
		{
			if (method == null)
			{
				return null;
			}
			PatchJobs<T>.Job job;
			if (!this.state.TryGetValue(method, out job))
			{
				job = new PatchJobs<T>.Job
				{
					original = method
				};
				this.state[method] = job;
			}
			return job;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000097FD File Offset: 0x000079FD
		internal List<PatchJobs<T>.Job> GetJobs()
		{
			return this.state.Values.Where<PatchJobs<T>.Job>((PatchJobs<T>.Job job) => job.prefixes.Count + job.postfixes.Count + job.transpilers.Count + job.finalizers.Count > 0).ToList<PatchJobs<T>.Job>();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009833 File Offset: 0x00007A33
		internal List<T> GetReplacements()
		{
			return this.state.Values.Select<PatchJobs<T>.Job, T>((PatchJobs<T>.Job job) => job.replacement).ToList<T>();
		}

		// Token: 0x040000B8 RID: 184
		internal Dictionary<MethodBase, PatchJobs<T>.Job> state = new Dictionary<MethodBase, PatchJobs<T>.Job>();

		// Token: 0x02000032 RID: 50
		internal class Job
		{
			// Token: 0x06000116 RID: 278 RVA: 0x0000987C File Offset: 0x00007A7C
			internal void AddPatch(AttributePatch patch)
			{
				HarmonyPatchType? type = patch.type;
				if (type != null)
				{
					switch (type.GetValueOrDefault())
					{
					case HarmonyPatchType.Prefix:
						this.prefixes.Add(patch.info);
						return;
					case HarmonyPatchType.Postfix:
						this.postfixes.Add(patch.info);
						return;
					case HarmonyPatchType.Transpiler:
						this.transpilers.Add(patch.info);
						return;
					case HarmonyPatchType.Finalizer:
						this.finalizers.Add(patch.info);
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x040000B9 RID: 185
			internal MethodBase original;

			// Token: 0x040000BA RID: 186
			internal T replacement;

			// Token: 0x040000BB RID: 187
			internal List<HarmonyMethod> prefixes = new List<HarmonyMethod>();

			// Token: 0x040000BC RID: 188
			internal List<HarmonyMethod> postfixes = new List<HarmonyMethod>();

			// Token: 0x040000BD RID: 189
			internal List<HarmonyMethod> transpilers = new List<HarmonyMethod>();

			// Token: 0x040000BE RID: 190
			internal List<HarmonyMethod> finalizers = new List<HarmonyMethod>();
		}
	}
}
