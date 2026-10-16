using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000043 RID: 67
	internal struct DependencyTreeInitializeOrderSorter
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00003487 File Offset: 0x00001687
		public DependencyTreeInitializeOrderSorter(DependencyTree tree, ICollection<int> target)
		{
			this.Tree = tree;
			this.Target = target;
			this.m_PackageTypeHashExplorationHistory = null;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000034A0 File Offset: 0x000016A0
		public void SortRegisteredPackagesIntoTarget()
		{
			this.Target.Clear();
			this.RemoveUnprovidedOptionalDependenciesFromTree();
			IReadOnlyCollection<int> packageTypeHashes = this.GetPackageTypeHashes();
			this.m_PackageTypeHashExplorationHistory = new Dictionary<int, DependencyTreeInitializeOrderSorter.ExplorationMark>(packageTypeHashes.Count);
			try
			{
				foreach (int num in packageTypeHashes)
				{
					this.SortTreeThrough(num);
				}
			}
			catch (HashException ex)
			{
				throw new DependencyTreeSortFailedException(this.Tree, this.Target, ex);
			}
			this.m_PackageTypeHashExplorationHistory = null;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000353C File Offset: 0x0000173C
		private void RemoveUnprovidedOptionalDependenciesFromTree()
		{
			foreach (List<int> list in this.Tree.PackageTypeHashToComponentTypeHashDependencies.Values)
			{
				this.RemoveUnprovidedOptionalDependencies(list);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000359C File Offset: 0x0000179C
		private void RemoveUnprovidedOptionalDependencies(IList<int> dependencyTypeHashes)
		{
			for (int i = dependencyTypeHashes.Count - 1; i >= 0; i--)
			{
				int num = dependencyTypeHashes[i];
				if (this.Tree.IsOptional(num) && !this.Tree.IsProvided(num))
				{
					dependencyTypeHashes.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000035E8 File Offset: 0x000017E8
		private void SortTreeThrough(int packageTypeHash)
		{
			DependencyTreeInitializeOrderSorter.ExplorationMark explorationMark;
			this.m_PackageTypeHashExplorationHistory.TryGetValue(packageTypeHash, out explorationMark);
			if (explorationMark == DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed)
			{
				throw new CircularDependencyException();
			}
			if (explorationMark != DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted)
			{
				this.MarkPackage(packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark.Viewed);
				IEnumerable<int> dependencyTypeHashesFor = this.GetDependencyTypeHashesFor(packageTypeHash);
				try
				{
					this.SortTreeThrough(dependencyTypeHashesFor);
				}
				catch (DependencyTreeComponentHashException ex)
				{
					throw new DependencyTreePackageHashException(packageTypeHash, string.Format("Component with hash[{0}] threw exception when sorting package[{1}][{2}]", ex.Hash, packageTypeHash, this.Tree.PackageTypeHashToInstance[packageTypeHash].GetType().FullName), ex);
				}
				this.Target.Add(packageTypeHash);
				this.MarkPackage(packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark.Sorted);
				return;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003694 File Offset: 0x00001894
		private void SortTreeThrough(IEnumerable<int> dependencyTypeHashes)
		{
			foreach (int num in dependencyTypeHashes)
			{
				int packageTypeHashFor = this.GetPackageTypeHashFor(num);
				this.SortTreeThrough(packageTypeHashFor);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000036E4 File Offset: 0x000018E4
		private void MarkPackage(int packageTypeHash, DependencyTreeInitializeOrderSorter.ExplorationMark mark)
		{
			this.m_PackageTypeHashExplorationHistory[packageTypeHash] = mark;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000036F3 File Offset: 0x000018F3
		private IReadOnlyCollection<int> GetPackageTypeHashes()
		{
			return this.Tree.PackageTypeHashToInstance.Keys;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003708 File Offset: 0x00001908
		private int GetPackageTypeHashFor(int componentTypeHash)
		{
			int num;
			if (!this.Tree.ComponentTypeHashToPackageTypeHash.TryGetValue(componentTypeHash, out num))
			{
				throw new DependencyTreeComponentHashException(componentTypeHash, string.Format("Component with hash[{0}] does not exist!", componentTypeHash));
			}
			return num;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003744 File Offset: 0x00001944
		private IEnumerable<int> GetDependencyTypeHashesFor(int packageTypeHash)
		{
			List<int> list;
			if (!this.Tree.PackageTypeHashToComponentTypeHashDependencies.TryGetValue(packageTypeHash, out list))
			{
				throw new DependencyTreePackageHashException(packageTypeHash, string.Format("Package with hash[{0}] does not exist!", packageTypeHash));
			}
			return list;
		}

		// Token: 0x0400004B RID: 75
		public readonly DependencyTree Tree;

		// Token: 0x0400004C RID: 76
		public readonly ICollection<int> Target;

		// Token: 0x0400004D RID: 77
		private Dictionary<int, DependencyTreeInitializeOrderSorter.ExplorationMark> m_PackageTypeHashExplorationHistory;

		// Token: 0x02000069 RID: 105
		private enum ExplorationMark
		{
			// Token: 0x0400009A RID: 154
			None,
			// Token: 0x0400009B RID: 155
			Viewed,
			// Token: 0x0400009C RID: 156
			Sorted
		}
	}
}
