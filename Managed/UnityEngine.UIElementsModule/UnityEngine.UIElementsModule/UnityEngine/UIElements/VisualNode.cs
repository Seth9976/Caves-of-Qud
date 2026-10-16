using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x020005E0 RID: 1504
	internal readonly struct VisualNode : IEnumerable<VisualNode>, IEnumerable, IEquatable<VisualNode>, IEquatable<VisualNodeHandle>
	{
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000A0042 File Offset: 0x0009E242
		public static VisualNode Null
		{
			get
			{
				return new VisualNode(null, VisualNodeHandle.Null);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000A004F File Offset: 0x0009E24F
		public bool IsCreated
		{
			get
			{
				return !this.m_Handle.Equals(VisualNodeHandle.Null) && this.m_Manager.ContainsNode(in this.m_Handle);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000A0077 File Offset: 0x0009E277
		public VisualNodeHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000A007F File Offset: 0x0009E27F
		public bool IsRoot
		{
			get
			{
				return this.m_Handle.Id == 1;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000A008F File Offset: 0x0009E28F
		public int Id
		{
			get
			{
				return this.m_Handle.Id;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000A009C File Offset: 0x0009E29C
		public uint ControlId
		{
			get
			{
				return this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).ControlId;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000A00B4 File Offset: 0x0009E2B4
		public VisualNode Parent
		{
			get
			{
				return new VisualNode(this.m_Manager, this.m_Manager.GetParent(in this.m_Handle));
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000A00D2 File Offset: 0x0009E2D2
		public int ChildCount
		{
			get
			{
				return this.m_Manager.GetChildrenCount(in this.m_Handle);
			}
		}

		// Token: 0x17000B00 RID: 2816
		public VisualNode this[int index]
		{
			get
			{
				return this.GetChildren()[index];
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000A0104 File Offset: 0x0009E304
		public ref bool Enabled
		{
			get
			{
				return ref this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).Enabled;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000A011C File Offset: 0x0009E31C
		public ref VisualElementFlags Flags
		{
			get
			{
				return ref this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).Flags;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000A0134 File Offset: 0x0009E334
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000A0147 File Offset: 0x0009E347
		public PseudoStates PseudoStates
		{
			get
			{
				return this.m_Manager.GetPseudoStates(in this.m_Handle);
			}
			set
			{
				this.m_Manager.SetPseudoStates(in this.m_Handle, value);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000A015C File Offset: 0x0009E35C
		public bool EnabledInHierarchy
		{
			get
			{
				return (this.PseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x000A016E File Offset: 0x0009E36E
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x000A0181 File Offset: 0x0009E381
		public RenderHints RenderHints
		{
			get
			{
				return this.m_Manager.GetRenderHints(in this.m_Handle);
			}
			set
			{
				this.m_Manager.SetRenderHints(in this.m_Handle, value);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000A0196 File Offset: 0x0009E396
		// (set) Token: 0x06002C69 RID: 11369 RVA: 0x000A01A9 File Offset: 0x0009E3A9
		public LanguageDirection LanguageDirection
		{
			get
			{
				return this.m_Manager.GetLanguageDirection(in this.m_Handle);
			}
			set
			{
				this.m_Manager.SetLanguageDirection(in this.m_Handle, value);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000A01BE File Offset: 0x0009E3BE
		// (set) Token: 0x06002C6B RID: 11371 RVA: 0x000A01D1 File Offset: 0x0009E3D1
		public LanguageDirection LocalLanguageDirection
		{
			get
			{
				return this.m_Manager.GetLocalLanguageDirection(in this.m_Handle);
			}
			set
			{
				this.m_Manager.SetLocalLanguageDirection(in this.m_Handle, value);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000A01E6 File Offset: 0x0009E3E6
		internal unsafe bool areAncestorsAndSelfDisplayed
		{
			get
			{
				return (*this.Flags & VisualElementFlags.HierarchyDisplayed) == VisualElementFlags.HierarchyDisplayed;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000A01FC File Offset: 0x0009E3FC
		public ref VisualNodeCallbackInterest CallbackInterest
		{
			get
			{
				return ref this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).CallbackInterest;
			}
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000A0214 File Offset: 0x0009E414
		internal VisualNode(VisualManager manager, VisualNodeHandle handle)
		{
			this.m_Manager = manager;
			this.m_Handle = handle;
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000A0225 File Offset: 0x0009E425
		internal void Destroy()
		{
			this.m_Manager.RemoveNode(in this.m_Handle);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000A023A File Offset: 0x0009E43A
		public VisualPanel GetPanel()
		{
			return new VisualPanel(this.m_Manager, this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).Panel);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000A025D File Offset: 0x0009E45D
		public void SetPanel(VisualPanel panel)
		{
			this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).Panel = panel.Handle;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000A027C File Offset: 0x0009E47C
		public VisualElement GetOwner()
		{
			return this.m_Manager.GetOwner(in this.m_Handle);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000A028F File Offset: 0x0009E48F
		public void SetOwner(VisualElement owner)
		{
			this.m_Manager.SetOwner(in this.m_Handle, owner);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000A02A4 File Offset: 0x0009E4A4
		public LayoutNode GetLayout()
		{
			return this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).LayoutNode;
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000A02BC File Offset: 0x0009E4BC
		public void SetLayout(LayoutNode value)
		{
			this.m_Manager.GetProperty<VisualNodeData>(this.m_Handle).LayoutNode = value;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000A02D5 File Offset: 0x0009E4D5
		public VisualNodeChildren GetChildren()
		{
			return new VisualNodeChildren(this.m_Manager, this.m_Handle);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000A02E8 File Offset: 0x0009E4E8
		public void InsertChildAtIndex(int index, in VisualNode child)
		{
			this.m_Manager.InsertChildAtIndex(in this.m_Handle, index, in child.m_Handle);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000A0304 File Offset: 0x0009E504
		public void AddChild(in VisualNode child)
		{
			this.m_Manager.AddChild(in this.m_Handle, in child.m_Handle);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000A031F File Offset: 0x0009E51F
		public void RemoveChild(in VisualNode child)
		{
			this.m_Manager.RemoveChild(in this.m_Handle, in child.m_Handle);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000A033C File Offset: 0x0009E53C
		public int IndexOfChild(in VisualNode child)
		{
			return this.m_Manager.IndexOfChild(in this.m_Handle, in child.m_Handle);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000A0365 File Offset: 0x0009E565
		public void RemoveChildAtIndex(int index)
		{
			this.m_Manager.RemoveChildAtIndex(in this.m_Handle, index);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000A037B File Offset: 0x0009E57B
		public void ClearChildren()
		{
			this.m_Manager.ClearChildren(in this.m_Handle);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000A0390 File Offset: 0x0009E590
		public void RemoveFromParent()
		{
			this.m_Manager.RemoveFromParent(in this.m_Handle);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000A03A5 File Offset: 0x0009E5A5
		public VisualNodeClassList GetClassList()
		{
			return new VisualNodeClassList(this.m_Manager, this.m_Handle);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000A03B8 File Offset: 0x0009E5B8
		public void AddToClassList(string className)
		{
			bool flag = string.IsNullOrEmpty(className);
			if (!flag)
			{
				this.m_Manager.AddToClassList(in this.m_Handle, className);
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		public bool RemoveFromClassList(string className)
		{
			bool flag = string.IsNullOrEmpty(className);
			return !flag && this.m_Manager.RemoveFromClassList(in this.m_Handle, className);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000A041C File Offset: 0x0009E61C
		public bool ClassListContains(string className)
		{
			bool flag = string.IsNullOrEmpty(className);
			return !flag && this.m_Manager.ClassListContains(in this.m_Handle, className);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000A0450 File Offset: 0x0009E650
		public bool ClearClassList()
		{
			return this.m_Manager.ClearClassList(in this.m_Handle);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000A0473 File Offset: 0x0009E673
		public void SetEnabled(bool value)
		{
			this.m_Manager.SetEnabled(in this.m_Handle, value);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000A0489 File Offset: 0x0009E689
		public VisualNode.Enumerator GetEnumerator()
		{
			return new VisualNode.Enumerator(in this);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000A0491 File Offset: 0x0009E691
		IEnumerator<VisualNode> IEnumerable<VisualNode>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000A0491 File Offset: 0x0009E691
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000A049E File Offset: 0x0009E69E
		public bool Equals(VisualNode other)
		{
			return this.m_Handle.Equals(other.m_Handle);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000A04B1 File Offset: 0x0009E6B1
		public bool Equals(VisualNodeHandle other)
		{
			return this.m_Handle.Equals(other);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000A04C0 File Offset: 0x0009E6C0
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is VisualNode)
			{
				VisualNode visualNode = (VisualNode)obj;
				flag = this.Equals(visualNode);
			}
			else if (obj is VisualNodeHandle)
			{
				VisualNodeHandle visualNodeHandle = (VisualNodeHandle)obj;
				flag = this.Equals(visualNodeHandle);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000A0500 File Offset: 0x0009E700
		public override int GetHashCode()
		{
			return HashCode.Combine<VisualManager, VisualNodeHandle>(this.m_Manager, this.m_Handle);
		}

		// Token: 0x04001443 RID: 5187
		private readonly VisualManager m_Manager;

		// Token: 0x04001444 RID: 5188
		private readonly VisualNodeHandle m_Handle;

		// Token: 0x020005E1 RID: 1505
		public struct Enumerator : IEnumerator<VisualNode>, IEnumerator, IDisposable
		{
			// Token: 0x06002C8B RID: 11403 RVA: 0x000A0513 File Offset: 0x0009E713
			public Enumerator(in VisualNode node)
			{
				this.m_Node = node;
				this.m_Position = -1;
			}

			// Token: 0x06002C8C RID: 11404 RVA: 0x000A052C File Offset: 0x0009E72C
			public bool MoveNext()
			{
				int num = this.m_Position + 1;
				this.m_Position = num;
				return num < this.m_Node.ChildCount;
			}

			// Token: 0x06002C8D RID: 11405 RVA: 0x000A0557 File Offset: 0x0009E757
			public void Reset()
			{
				this.m_Position = -1;
			}

			// Token: 0x17000B0A RID: 2826
			// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000A0560 File Offset: 0x0009E760
			public VisualNode Current
			{
				get
				{
					return this.m_Node[this.m_Position];
				}
			}

			// Token: 0x17000B0B RID: 2827
			// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000A0573 File Offset: 0x0009E773
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002C90 RID: 11408 RVA: 0x00002152 File Offset: 0x00000352
			public void Dispose()
			{
			}

			// Token: 0x04001445 RID: 5189
			private readonly VisualNode m_Node;

			// Token: 0x04001446 RID: 5190
			private int m_Position;
		}
	}
}
