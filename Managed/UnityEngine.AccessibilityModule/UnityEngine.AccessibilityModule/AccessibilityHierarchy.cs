using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000010 RID: 16
	public class AccessibilityHierarchy
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000329E File Offset: 0x0000149E
		public IReadOnlyList<AccessibilityNode> rootNodes
		{
			get
			{
				return this.m_RootNodes;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000B4 RID: 180 RVA: 0x000032A8 File Offset: 0x000014A8
		// (remove) Token: 0x060000B5 RID: 181 RVA: 0x000032E0 File Offset: 0x000014E0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event Action<AccessibilityHierarchy> m_Changed;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000B6 RID: 182 RVA: 0x00003315 File Offset: 0x00001515
		// (remove) Token: 0x060000B7 RID: 183 RVA: 0x0000331F File Offset: 0x0000151F
		internal event Action<AccessibilityHierarchy> changed
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
			add
			{
				this.m_Changed += value;
			}
			[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
			remove
			{
				this.m_Changed -= value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000332C File Offset: 0x0000152C
		protected override void Finalize()
		{
			try
			{
				HashSet<int> hashSet = AccessibilityHierarchy.usedNodeIds;
				lock (hashSet)
				{
					foreach (int num in this.nodes.Keys)
					{
						AccessibilityHierarchy.usedNodeIds.Remove(num);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000033D4 File Offset: 0x000015D4
		internal void NotifyHierarchyChanged()
		{
			Action<AccessibilityHierarchy> changed = this.m_Changed;
			if (changed != null)
			{
				changed(this);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000033EC File Offset: 0x000015EC
		public void Clear()
		{
			for (int i = this.m_RootNodes.Count - 1; i >= 0; i--)
			{
				this.RemoveNode(this.m_RootNodes[i], true);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003430 File Offset: 0x00001630
		public bool TryGetNode(int id, out AccessibilityNode node)
		{
			return this.nodes.TryGetValue(id, out node);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003450 File Offset: 0x00001650
		public AccessibilityNode AddNode(string label = null, AccessibilityNode parent = null)
		{
			return this.InsertNode(-1, label, parent);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000346C File Offset: 0x0000166C
		public AccessibilityNode InsertNode(int childIndex, string label = null, AccessibilityNode parent = null)
		{
			bool flag = parent != null;
			if (flag)
			{
				this.ValidateNodeInHierarchy(parent);
			}
			AccessibilityNode accessibilityNode = this.CreateNode();
			this.nodes[accessibilityNode.id] = accessibilityNode;
			bool flag2 = label != null;
			if (flag2)
			{
				accessibilityNode.label = label;
			}
			AccessibilityNode accessibilityNode2 = accessibilityNode;
			IList<AccessibilityNode> list = null;
			IList<AccessibilityNode> list2;
			if (parent != null)
			{
				list2 = parent.childList;
			}
			else
			{
				IList<AccessibilityNode> rootNodes = this.m_RootNodes;
				list2 = rootNodes;
			}
			this.SetParent(accessibilityNode2, parent, list, list2, childIndex);
			this.NotifyHierarchyChanged();
			return accessibilityNode;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000034E4 File Offset: 0x000016E4
		public bool MoveNode(AccessibilityNode node, AccessibilityNode newParent, int newChildIndex = -1)
		{
			this.ValidateNodeInHierarchy(node);
			bool flag = node == newParent;
			if (flag)
			{
				throw new ArgumentException(string.Format("Attempting to move the node {0} under itself.", node));
			}
			bool flag2 = node.parent == newParent;
			bool flag4;
			if (flag2)
			{
				IList<AccessibilityNode> list;
				if (newParent != null)
				{
					list = newParent.childList;
				}
				else
				{
					IList<AccessibilityNode> rootNodes = this.m_RootNodes;
					list = rootNodes;
				}
				IList<AccessibilityNode> list2 = list;
				bool flag3 = newChildIndex == list2.IndexOf(node);
				if (flag3)
				{
					flag4 = false;
				}
				else
				{
					this.CheckForLoopsAndSetParent(node, newParent, newChildIndex);
					flag4 = true;
				}
			}
			else
			{
				bool flag5 = newParent == null;
				if (flag5)
				{
					bool flag6 = node.parent == null;
					if (flag6)
					{
						flag4 = false;
					}
					else
					{
						this.CheckForLoopsAndSetParent(node, null, newChildIndex);
						flag4 = true;
					}
				}
				else
				{
					this.ValidateNodeInHierarchy(newParent);
					this.CheckForLoopsAndSetParent(node, newParent, newChildIndex);
					this.NotifyHierarchyChanged();
					flag4 = true;
				}
			}
			return flag4;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000035AC File Offset: 0x000017AC
		public void RemoveNode(AccessibilityNode node, bool removeChildren = true)
		{
			this.ValidateNodeInHierarchy(node);
			if (removeChildren)
			{
				AccessibilityHierarchy.<>c__DisplayClass21_0 CS$<>8__locals1;
				CS$<>8__locals1.nodeIdsToRemove = new List<int>();
				this.<RemoveNode>g__RemoveFromNodes|21_0(node, ref CS$<>8__locals1);
				HashSet<int> hashSet = AccessibilityHierarchy.usedNodeIds;
				lock (hashSet)
				{
					foreach (int num in CS$<>8__locals1.nodeIdsToRemove)
					{
						AccessibilityHierarchy.usedNodeIds.Remove(num);
					}
				}
			}
			else
			{
				this.nodes.Remove(node.id);
				HashSet<int> hashSet2 = AccessibilityHierarchy.usedNodeIds;
				lock (hashSet2)
				{
					AccessibilityHierarchy.usedNodeIds.Remove(node.id);
				}
			}
			bool flag3 = this.m_RootNodes.Contains(node);
			if (flag3)
			{
				this.m_RootNodes.Remove(node);
				bool flag4 = !removeChildren;
				if (flag4)
				{
					this.m_RootNodes.AddRange(node.childList);
				}
			}
			node.Destroy(removeChildren);
			this.NotifyHierarchyChanged();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003708 File Offset: 0x00001908
		public bool ContainsNode(AccessibilityNode node)
		{
			AccessibilityNode accessibilityNode;
			return node != null && this.nodes.TryGetValue(node.id, out accessibilityNode) && accessibilityNode == node;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000373C File Offset: 0x0000193C
		private void CheckForLoopsAndSetParent(AccessibilityNode node, AccessibilityNode parent, int newChildIndex = -1)
		{
			bool flag = parent == null;
			if (flag)
			{
				AccessibilityNode accessibilityNode = null;
				AccessibilityNode parent2 = node.parent;
				this.SetParent(node, accessibilityNode, ((parent2 != null) ? parent2.childList : null) ?? this.m_RootNodes, this.m_RootNodes, newChildIndex);
			}
			else
			{
				bool flag2 = node.parent == parent;
				if (flag2)
				{
					this.SetParent(node, parent, parent.childList, parent.childList, newChildIndex);
				}
				else
				{
					bool flag3 = node.parent == null && parent.parent == null;
					if (flag3)
					{
						this.SetParent(node, parent, this.m_RootNodes, parent.childList, newChildIndex);
					}
					else
					{
						for (AccessibilityNode accessibilityNode2 = parent.parent; accessibilityNode2 != null; accessibilityNode2 = accessibilityNode2.parent)
						{
							bool flag4 = accessibilityNode2 == node;
							if (flag4)
							{
								throw new ArgumentException(string.Format("Trying to set the node {0} to have parent {1}, but this would create a loop.", node, parent));
							}
						}
						AccessibilityNode parent3 = node.parent;
						this.SetParent(node, parent, ((parent3 != null) ? parent3.childList : null) ?? this.m_RootNodes, parent.childList, newChildIndex);
					}
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003844 File Offset: 0x00001A44
		private void SetParent(AccessibilityNode node, AccessibilityNode parent, IList<AccessibilityNode> previousParentChildren, IList<AccessibilityNode> newParentChildren, int newChildIndex = -1)
		{
			if (previousParentChildren != null)
			{
				previousParentChildren.Remove(node);
			}
			node.SetParent(parent, newChildIndex);
			bool flag = newChildIndex < 0 || newChildIndex > newParentChildren.Count;
			if (flag)
			{
				newParentChildren.Add(node);
			}
			else
			{
				newParentChildren.Insert(newChildIndex, node);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000389C File Offset: 0x00001A9C
		internal void AllocateNative()
		{
			foreach (AccessibilityNode accessibilityNode in this.m_RootNodes)
			{
				accessibilityNode.AllocateNative();
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000038F4 File Offset: 0x00001AF4
		internal void FreeNative()
		{
			foreach (AccessibilityNode accessibilityNode in this.m_RootNodes)
			{
				accessibilityNode.FreeNative(true);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003950 File Offset: 0x00001B50
		public void RefreshNodeFrames()
		{
			foreach (AccessibilityNode accessibilityNode in this.nodes.Values)
			{
				accessibilityNode.CalculateFrame();
			}
			AssistiveSupport.OnHierarchyNodeFramesRefreshed(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000039B4 File Offset: 0x00001BB4
		public bool TryGetNodeAt(float horizontalPosition, float verticalPosition, out AccessibilityNode node)
		{
			Vector2 vector = new Vector2(horizontalPosition, verticalPosition);
			node = AccessibilityHierarchy.<TryGetNodeAt>g__FindNodeContainingPoint|28_0(this.m_RootNodes, vector);
			return node != null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000039E4 File Offset: 0x00001BE4
		public AccessibilityNode GetLowestCommonAncestor(AccessibilityNode firstNode, AccessibilityNode secondNode)
		{
			bool flag = firstNode == null || secondNode == null;
			AccessibilityNode accessibilityNode;
			if (flag)
			{
				accessibilityNode = null;
			}
			else
			{
				bool flag2 = firstNode.parent == null && secondNode.parent == null;
				if (flag2)
				{
					accessibilityNode = null;
				}
				else
				{
					bool flag3 = !this.ContainsNode(firstNode) || !this.ContainsNode(secondNode);
					if (flag3)
					{
						accessibilityNode = null;
					}
					else
					{
						this.m_FirstLowestCommonAncestorChain.Clear();
						this.m_SecondLowestCommonAncestorChain.Clear();
						this.<GetLowestCommonAncestor>g__buildNodeIdStack|29_0(firstNode, ref this.m_FirstLowestCommonAncestorChain);
						this.<GetLowestCommonAncestor>g__buildNodeIdStack|29_0(secondNode, ref this.m_SecondLowestCommonAncestorChain);
						AccessibilityNode accessibilityNode2 = null;
						for (int i = Mathf.Min(this.m_FirstLowestCommonAncestorChain.Count, this.m_SecondLowestCommonAncestorChain.Count); i > 0; i--)
						{
							AccessibilityNode accessibilityNode3 = this.m_FirstLowestCommonAncestorChain.Pop();
							AccessibilityNode accessibilityNode4 = this.m_SecondLowestCommonAncestorChain.Pop();
							bool flag4 = accessibilityNode3 != accessibilityNode4;
							if (flag4)
							{
								break;
							}
							accessibilityNode2 = accessibilityNode3;
						}
						accessibilityNode = accessibilityNode2;
					}
				}
			}
			return accessibilityNode;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private AccessibilityNode CreateNode()
		{
			HashSet<int> hashSet = AccessibilityHierarchy.usedNodeIds;
			int num;
			lock (hashSet)
			{
				bool flag2 = AccessibilityHierarchy.usedNodeIds.Count >= int.MaxValue;
				if (flag2)
				{
					throw new InvalidOperationException("Could not create a new accessibility node. A maximum of " + string.Format("{0} nodes can exist at a time across all hierarchies. Try clearing unused ", int.MaxValue) + "hierarchies or removing unused nodes.");
				}
				while (AccessibilityHierarchy.usedNodeIds.Contains(AccessibilityHierarchy.nextUniqueNodeId))
				{
					AccessibilityHierarchy.nextUniqueNodeId = ((AccessibilityHierarchy.nextUniqueNodeId == int.MaxValue) ? 0 : (AccessibilityHierarchy.nextUniqueNodeId + 1));
				}
				num = AccessibilityHierarchy.nextUniqueNodeId;
				AccessibilityHierarchy.usedNodeIds.Add(num);
				AccessibilityHierarchy.nextUniqueNodeId = ((num == int.MaxValue) ? 0 : (num + 1));
			}
			return new AccessibilityNode(num, this);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void ValidateNodeInHierarchy(AccessibilityNode node)
		{
			bool flag = node != null;
			if (!flag)
			{
				throw new ArgumentNullException("node");
			}
			bool flag2 = this.ContainsNode(node);
			if (flag2)
			{
				return;
			}
			throw new ArgumentException(string.Format("Trying to use an AccessibilityNode with ID {0} that is not part of this hierarchy.", node.id));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C60 File Offset: 0x00001E60
		[CompilerGenerated]
		private void <RemoveNode>g__RemoveFromNodes|21_0(AccessibilityNode child, ref AccessibilityHierarchy.<>c__DisplayClass21_0 A_2)
		{
			this.nodes.Remove(child.id);
			A_2.nodeIdsToRemove.Add(child.id);
			foreach (AccessibilityNode accessibilityNode in child.children)
			{
				this.<RemoveNode>g__RemoveFromNodes|21_0(accessibilityNode, ref A_2);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[CompilerGenerated]
		internal static AccessibilityNode <TryGetNodeAt>g__FindNodeContainingPoint|28_0(IList<AccessibilityNode> nodes, Vector2 pos)
		{
			int i = nodes.Count - 1;
			while (i >= 0)
			{
				AccessibilityNode accessibilityNode = nodes[i];
				AccessibilityNode accessibilityNode2 = AccessibilityHierarchy.<TryGetNodeAt>g__FindNodeContainingPoint|28_0(accessibilityNode.childList, pos);
				bool flag = accessibilityNode2 != null;
				AccessibilityNode accessibilityNode3;
				if (flag)
				{
					accessibilityNode3 = accessibilityNode2;
				}
				else
				{
					bool flag2 = accessibilityNode.isActive && accessibilityNode.frame.Contains(pos);
					if (!flag2)
					{
						i--;
						continue;
					}
					accessibilityNode3 = accessibilityNode;
				}
				return accessibilityNode3;
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003D54 File Offset: 0x00001F54
		[CompilerGenerated]
		private void <GetLowestCommonAncestor>g__buildNodeIdStack|29_0(AccessibilityNode node, ref Stack<AccessibilityNode> nodeStack)
		{
			while (node != null)
			{
				nodeStack.Push(node);
				node = this.nodes[node.id].parent;
			}
		}

		// Token: 0x0400004C RID: 76
		internal readonly Dictionary<int, AccessibilityNode> nodes = new Dictionary<int, AccessibilityNode>();

		// Token: 0x0400004D RID: 77
		internal List<AccessibilityNode> m_RootNodes = new List<AccessibilityNode>();

		// Token: 0x0400004E RID: 78
		private Stack<AccessibilityNode> m_FirstLowestCommonAncestorChain = new Stack<AccessibilityNode>();

		// Token: 0x0400004F RID: 79
		private Stack<AccessibilityNode> m_SecondLowestCommonAncestorChain = new Stack<AccessibilityNode>();

		// Token: 0x04000050 RID: 80
		internal static int nextUniqueNodeId;

		// Token: 0x04000051 RID: 81
		internal static readonly HashSet<int> usedNodeIds = new HashSet<int>();
	}
}
