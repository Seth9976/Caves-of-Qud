using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B7 RID: 695
	[NullableContext(2)]
	[Nullable(0)]
	internal static class SyntaxNodeCache
	{
		// Token: 0x06001D77 RID: 7543 RVA: 0x00055D18 File Offset: 0x00053F18
		[NullableContext(1)]
		internal static void AddNode(GreenNode node, int hash)
		{
			if (SyntaxNodeCache.AllChildrenInCache(node) && !node.IsMissing)
			{
				int num = hash & 65535;
				SyntaxNodeCache.s_cache[num] = new SyntaxNodeCache.Entry(hash, node);
			}
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00055D4F File Offset: 0x00053F4F
		private static bool CanBeCached(GreenNode child1)
		{
			return child1 == null || child1.IsCacheable;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00055D5C File Offset: 0x00053F5C
		private static bool CanBeCached(GreenNode child1, GreenNode child2)
		{
			return SyntaxNodeCache.CanBeCached(child1) && SyntaxNodeCache.CanBeCached(child2);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00055D6E File Offset: 0x00053F6E
		private static bool CanBeCached(GreenNode child1, GreenNode child2, GreenNode child3)
		{
			return SyntaxNodeCache.CanBeCached(child1) && SyntaxNodeCache.CanBeCached(child2) && SyntaxNodeCache.CanBeCached(child3);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00055D88 File Offset: 0x00053F88
		private static bool ChildInCache(GreenNode child)
		{
			if (child == null || child.SlotCount == 0)
			{
				return true;
			}
			int num = child.GetCacheHash() & 65535;
			return SyntaxNodeCache.s_cache[num].node == child;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00055DC4 File Offset: 0x00053FC4
		[NullableContext(1)]
		private static bool AllChildrenInCache(GreenNode node)
		{
			int slotCount = node.SlotCount;
			for (int i = 0; i < slotCount; i++)
			{
				if (!SyntaxNodeCache.ChildInCache(node.GetSlot(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00055DF5 File Offset: 0x00053FF5
		internal static GreenNode TryGetNode(int kind, GreenNode child1, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, SyntaxNodeCache.GetDefaultNodeFlags(), out hash);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00055E04 File Offset: 0x00054004
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode.NodeFlags flags, out int hash)
		{
			if (SyntaxNodeCache.CanBeCached(child1))
			{
				int num = (hash = SyntaxNodeCache.GetCacheHash(kind, flags, child1));
				int num2 = num & 65535;
				SyntaxNodeCache.Entry entry = SyntaxNodeCache.s_cache[num2];
				if (entry.hash == num && entry.node != null && entry.node.IsCacheEquivalent(kind, flags, child1))
				{
					return entry.node;
				}
			}
			else
			{
				hash = -1;
			}
			return null;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00055E67 File Offset: 0x00054067
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, child2, SyntaxNodeCache.GetDefaultNodeFlags(), out hash);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00055E78 File Offset: 0x00054078
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, GreenNode.NodeFlags flags, out int hash)
		{
			if (SyntaxNodeCache.CanBeCached(child1, child2))
			{
				int num = (hash = SyntaxNodeCache.GetCacheHash(kind, flags, child1, child2));
				int num2 = num & 65535;
				SyntaxNodeCache.Entry entry = SyntaxNodeCache.s_cache[num2];
				if (entry.hash == num && entry.node != null && entry.node.IsCacheEquivalent(kind, flags, child1, child2))
				{
					return entry.node;
				}
			}
			else
			{
				hash = -1;
			}
			return null;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00055EE0 File Offset: 0x000540E0
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, GreenNode child3, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, child2, child3, SyntaxNodeCache.GetDefaultNodeFlags(), out hash);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00055EF4 File Offset: 0x000540F4
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, GreenNode child3, GreenNode.NodeFlags flags, out int hash)
		{
			if (SyntaxNodeCache.CanBeCached(child1, child2, child3))
			{
				int num = (hash = SyntaxNodeCache.GetCacheHash(kind, flags, child1, child2, child3));
				int num2 = num & 65535;
				SyntaxNodeCache.Entry entry = SyntaxNodeCache.s_cache[num2];
				if (entry.hash == num && entry.node != null && entry.node.IsCacheEquivalent(kind, flags, child1, child2, child3))
				{
					return entry.node;
				}
			}
			else
			{
				hash = -1;
			}
			return null;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00055F61 File Offset: 0x00054161
		public static GreenNode.NodeFlags GetDefaultNodeFlags()
		{
			return GreenNode.NodeFlags.IsNotMissing;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00055F68 File Offset: 0x00054168
		private static int GetCacheHash(int kind, GreenNode.NodeFlags flags, GreenNode child1)
		{
			int num = (int)flags ^ kind;
			num = Hash.Combine(RuntimeHelpers.GetHashCode(child1), num);
			return num & int.MaxValue;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00055F90 File Offset: 0x00054190
		private static int GetCacheHash(int kind, GreenNode.NodeFlags flags, GreenNode child1, GreenNode child2)
		{
			int num = (int)flags ^ kind;
			if (child1 != null)
			{
				num = Hash.Combine(RuntimeHelpers.GetHashCode(child1), num);
			}
			if (child2 != null)
			{
				num = Hash.Combine(RuntimeHelpers.GetHashCode(child2), num);
			}
			return num & int.MaxValue;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00055FC8 File Offset: 0x000541C8
		private static int GetCacheHash(int kind, GreenNode.NodeFlags flags, GreenNode child1, GreenNode child2, GreenNode child3)
		{
			int num = (int)flags ^ kind;
			if (child1 != null)
			{
				num = Hash.Combine(RuntimeHelpers.GetHashCode(child1), num);
			}
			if (child2 != null)
			{
				num = Hash.Combine(RuntimeHelpers.GetHashCode(child2), num);
			}
			if (child3 != null)
			{
				num = Hash.Combine(RuntimeHelpers.GetHashCode(child3), num);
			}
			return num & int.MaxValue;
		}

		// Token: 0x04000D6B RID: 3435
		private const int CacheSizeBits = 16;

		// Token: 0x04000D6C RID: 3436
		private const int CacheSize = 65536;

		// Token: 0x04000D6D RID: 3437
		private const int CacheMask = 65535;

		// Token: 0x04000D6E RID: 3438
		[Nullable(1)]
		private static readonly SyntaxNodeCache.Entry[] s_cache = new SyntaxNodeCache.Entry[65536];

		// Token: 0x0200065E RID: 1630
		[NullableContext(0)]
		private readonly struct Entry
		{
			// Token: 0x060038F0 RID: 14576 RVA: 0x0009E19D File Offset: 0x0009C39D
			[NullableContext(1)]
			internal Entry(int hash, GreenNode node)
			{
				this.hash = hash;
				this.node = node;
			}

			// Token: 0x04001874 RID: 6260
			public readonly int hash;

			// Token: 0x04001875 RID: 6261
			[Nullable(2)]
			public readonly GreenNode node;
		}
	}
}
