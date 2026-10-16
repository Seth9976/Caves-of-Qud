using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000036 RID: 54
	internal class PatchSorter
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00009BD0 File Offset: 0x00007DD0
		internal PatchSorter(Patch[] patches, bool debug)
		{
			this.patches = patches.Select<Patch, PatchSorter.PatchSortingWrapper>((Patch x) => new PatchSorter.PatchSortingWrapper(x)).ToList<PatchSorter.PatchSortingWrapper>();
			this.debug = debug;
			using (List<PatchSorter.PatchSortingWrapper>.Enumerator enumerator = this.patches.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PatchSorter.PatchSortingWrapper node = enumerator.Current;
					node.AddBeforeDependency(this.patches.Where<PatchSorter.PatchSortingWrapper>((PatchSorter.PatchSortingWrapper x) => node.innerPatch.before.Contains(x.innerPatch.owner)));
					node.AddAfterDependency(this.patches.Where<PatchSorter.PatchSortingWrapper>((PatchSorter.PatchSortingWrapper x) => node.innerPatch.after.Contains(x.innerPatch.owner)));
				}
			}
			this.patches.Sort();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009CB4 File Offset: 0x00007EB4
		internal List<MethodInfo> Sort(MethodBase original)
		{
			if (this.sortedPatchArray != null)
			{
				return this.sortedPatchArray.Select<Patch, MethodInfo>((Patch x) => x.GetMethod(original)).ToList<MethodInfo>();
			}
			this.handledPatches = new HashSet<PatchSorter.PatchSortingWrapper>();
			this.waitingList = new List<PatchSorter.PatchSortingWrapper>();
			this.result = new List<PatchSorter.PatchSortingWrapper>(this.patches.Count);
			Queue<PatchSorter.PatchSortingWrapper> queue = new Queue<PatchSorter.PatchSortingWrapper>(this.patches);
			Func<PatchSorter.PatchSortingWrapper, bool> <>9__3;
			while (queue.Count != 0)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in queue)
				{
					IEnumerable<PatchSorter.PatchSortingWrapper> after = patchSortingWrapper.after;
					Func<PatchSorter.PatchSortingWrapper, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (PatchSorter.PatchSortingWrapper x) => this.handledPatches.Contains(x));
					}
					if (after.All<PatchSorter.PatchSortingWrapper>(func))
					{
						this.AddNodeToResult(patchSortingWrapper);
						if (patchSortingWrapper.before.Count != 0)
						{
							this.ProcessWaitingList();
						}
					}
					else
					{
						this.waitingList.Add(patchSortingWrapper);
					}
				}
				this.CullDependency();
				queue = new Queue<PatchSorter.PatchSortingWrapper>(this.waitingList);
				this.waitingList.Clear();
			}
			this.sortedPatchArray = this.result.Select<PatchSorter.PatchSortingWrapper, Patch>((PatchSorter.PatchSortingWrapper x) => x.innerPatch).ToArray<Patch>();
			this.handledPatches = null;
			this.waitingList = null;
			this.patches = null;
			return this.sortedPatchArray.Select<Patch, MethodInfo>((Patch x) => x.GetMethod(original)).ToList<MethodInfo>();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00009E58 File Offset: 0x00008058
		internal bool ComparePatchLists(Patch[] patches)
		{
			if (this.sortedPatchArray == null)
			{
				this.Sort(null);
			}
			return patches != null && this.sortedPatchArray.Length == patches.Length && this.sortedPatchArray.All<Patch>((Patch x) => patches.Contains(x, new PatchSorter.PatchDetailedComparer()));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009EB8 File Offset: 0x000080B8
		private void CullDependency()
		{
			for (int i = this.waitingList.Count - 1; i >= 0; i--)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in this.waitingList[i].after)
				{
					if (!this.handledPatches.Contains(patchSortingWrapper))
					{
						this.waitingList[i].RemoveAfterDependency(patchSortingWrapper);
						if (this.debug)
						{
							string text = patchSortingWrapper.innerPatch.PatchMethod.FullDescription();
							string text2 = this.waitingList[i].innerPatch.PatchMethod.FullDescription();
							FileLog.LogBuffered("Breaking dependance between " + text + " and " + text2);
						}
						return;
					}
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00009F9C File Offset: 0x0000819C
		private void ProcessWaitingList()
		{
			int num = this.waitingList.Count;
			int i = 0;
			while (i < num)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = this.waitingList[i];
				if (patchSortingWrapper.after.All<PatchSorter.PatchSortingWrapper>(new Func<PatchSorter.PatchSortingWrapper, bool>(this.handledPatches.Contains)))
				{
					this.waitingList.Remove(patchSortingWrapper);
					this.AddNodeToResult(patchSortingWrapper);
					num--;
					i = 0;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A009 File Offset: 0x00008209
		private void AddNodeToResult(PatchSorter.PatchSortingWrapper node)
		{
			this.result.Add(node);
			this.handledPatches.Add(node);
		}

		// Token: 0x040000CC RID: 204
		private List<PatchSorter.PatchSortingWrapper> patches;

		// Token: 0x040000CD RID: 205
		private HashSet<PatchSorter.PatchSortingWrapper> handledPatches;

		// Token: 0x040000CE RID: 206
		private List<PatchSorter.PatchSortingWrapper> result;

		// Token: 0x040000CF RID: 207
		private List<PatchSorter.PatchSortingWrapper> waitingList;

		// Token: 0x040000D0 RID: 208
		internal Patch[] sortedPatchArray;

		// Token: 0x040000D1 RID: 209
		private readonly bool debug;

		// Token: 0x02000037 RID: 55
		private class PatchSortingWrapper : IComparable
		{
			// Token: 0x0600012D RID: 301 RVA: 0x0000A024 File Offset: 0x00008224
			internal PatchSortingWrapper(Patch patch)
			{
				this.innerPatch = patch;
				this.before = new HashSet<PatchSorter.PatchSortingWrapper>();
				this.after = new HashSet<PatchSorter.PatchSortingWrapper>();
			}

			// Token: 0x0600012E RID: 302 RVA: 0x0000A049 File Offset: 0x00008249
			public int CompareTo(object obj)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = obj as PatchSorter.PatchSortingWrapper;
				return PatchInfoSerialization.PriorityComparer((patchSortingWrapper != null) ? patchSortingWrapper.innerPatch : null, this.innerPatch.index, this.innerPatch.priority);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x0000A078 File Offset: 0x00008278
			public override bool Equals(object obj)
			{
				PatchSorter.PatchSortingWrapper patchSortingWrapper = obj as PatchSorter.PatchSortingWrapper;
				return patchSortingWrapper != null && this.innerPatch.PatchMethod == patchSortingWrapper.innerPatch.PatchMethod;
			}

			// Token: 0x06000130 RID: 304 RVA: 0x0000A0AC File Offset: 0x000082AC
			public override int GetHashCode()
			{
				return this.innerPatch.PatchMethod.GetHashCode();
			}

			// Token: 0x06000131 RID: 305 RVA: 0x0000A0C0 File Offset: 0x000082C0
			internal void AddBeforeDependency(IEnumerable<PatchSorter.PatchSortingWrapper> dependencies)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in dependencies)
				{
					this.before.Add(patchSortingWrapper);
					patchSortingWrapper.after.Add(this);
				}
			}

			// Token: 0x06000132 RID: 306 RVA: 0x0000A11C File Offset: 0x0000831C
			internal void AddAfterDependency(IEnumerable<PatchSorter.PatchSortingWrapper> dependencies)
			{
				foreach (PatchSorter.PatchSortingWrapper patchSortingWrapper in dependencies)
				{
					this.after.Add(patchSortingWrapper);
					patchSortingWrapper.before.Add(this);
				}
			}

			// Token: 0x06000133 RID: 307 RVA: 0x0000A178 File Offset: 0x00008378
			internal void RemoveAfterDependency(PatchSorter.PatchSortingWrapper afterNode)
			{
				this.after.Remove(afterNode);
				afterNode.before.Remove(this);
			}

			// Token: 0x06000134 RID: 308 RVA: 0x0000A194 File Offset: 0x00008394
			internal void RemoveBeforeDependency(PatchSorter.PatchSortingWrapper beforeNode)
			{
				this.before.Remove(beforeNode);
				beforeNode.after.Remove(this);
			}

			// Token: 0x040000D2 RID: 210
			internal readonly HashSet<PatchSorter.PatchSortingWrapper> after;

			// Token: 0x040000D3 RID: 211
			internal readonly HashSet<PatchSorter.PatchSortingWrapper> before;

			// Token: 0x040000D4 RID: 212
			internal readonly Patch innerPatch;
		}

		// Token: 0x02000038 RID: 56
		internal class PatchDetailedComparer : IEqualityComparer<Patch>
		{
			// Token: 0x06000135 RID: 309 RVA: 0x0000A1B0 File Offset: 0x000083B0
			public bool Equals(Patch x, Patch y)
			{
				return y != null && x != null && x.owner == y.owner && x.PatchMethod == y.PatchMethod && x.index == y.index && x.priority == y.priority && x.before.Length == y.before.Length && x.after.Length == y.after.Length && x.before.All<string>(new Func<string, bool>(y.before.Contains<string>)) && x.after.All<string>(new Func<string, bool>(y.after.Contains<string>));
			}

			// Token: 0x06000136 RID: 310 RVA: 0x0000A26E File Offset: 0x0000846E
			public int GetHashCode(Patch obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}
