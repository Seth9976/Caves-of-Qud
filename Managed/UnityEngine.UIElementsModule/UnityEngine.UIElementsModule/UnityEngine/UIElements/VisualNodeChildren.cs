using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020005E2 RID: 1506
	internal readonly struct VisualNodeChildren : IEnumerable<VisualNode>, IEnumerable
	{
		// Token: 0x06002C91 RID: 11409 RVA: 0x000A0580 File Offset: 0x0009E780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe VisualNodeChildrenData* GetDataPtr()
		{
			return (VisualNodeChildrenData*)this.m_Manager.GetChildrenPtr(in this.m_Handle).ToPointer();
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000A05A6 File Offset: 0x0009E7A6
		public int Count
		{
			get
			{
				return this.m_Manager.GetChildrenCount(in this.m_Handle);
			}
		}

		// Token: 0x17000B0D RID: 2829
		public unsafe VisualNode this[int index]
		{
			get
			{
				VisualNodeChildrenData* dataPtr = this.GetDataPtr();
				bool flag = (ulong)index >= (ulong)((long)dataPtr->Count);
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return new VisualNode(this.m_Manager, dataPtr->ElementAt(index));
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000A05FF File Offset: 0x0009E7FF
		public VisualNodeChildren(VisualManager manager, VisualNodeHandle handle)
		{
			this.m_Manager = manager;
			this.m_Handle = handle;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000A0610 File Offset: 0x0009E810
		public void Add(in VisualNode child)
		{
			VisualManager manager = this.m_Manager;
			VisualNodeHandle handle = child.Handle;
			manager.AddChild(in this.m_Handle, in handle);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000A063C File Offset: 0x0009E83C
		public bool Remove(in VisualNode child)
		{
			VisualManager manager = this.m_Manager;
			VisualNodeHandle handle = child.Handle;
			return manager.RemoveChild(in this.m_Handle, in handle);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000A0668 File Offset: 0x0009E868
		public unsafe VisualNodeChildren.Enumerator GetEnumerator()
		{
			return new VisualNodeChildren.Enumerator(this.m_Manager, in *this.GetDataPtr());
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000A067B File Offset: 0x0009E87B
		IEnumerator<VisualNode> IEnumerable<VisualNode>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000A067B File Offset: 0x0009E87B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001447 RID: 5191
		private readonly VisualManager m_Manager;

		// Token: 0x04001448 RID: 5192
		private readonly VisualNodeHandle m_Handle;

		// Token: 0x020005E3 RID: 1507
		public struct Enumerator : IEnumerator<VisualNode>, IEnumerator, IDisposable
		{
			// Token: 0x06002C9A RID: 11418 RVA: 0x000A0688 File Offset: 0x0009E888
			internal Enumerator(VisualManager manager, in VisualNodeChildrenData children)
			{
				this.m_Manager = manager;
				this.m_Children = children;
				this.m_Position = -1;
			}

			// Token: 0x06002C9B RID: 11419 RVA: 0x000A06A8 File Offset: 0x0009E8A8
			public bool MoveNext()
			{
				int num = this.m_Position + 1;
				this.m_Position = num;
				return num < this.m_Children.Count;
			}

			// Token: 0x06002C9C RID: 11420 RVA: 0x000A06D3 File Offset: 0x0009E8D3
			public void Reset()
			{
				this.m_Position = -1;
			}

			// Token: 0x17000B0E RID: 2830
			// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000A06DC File Offset: 0x0009E8DC
			public VisualNode Current
			{
				get
				{
					return new VisualNode(this.m_Manager, this.m_Children[this.m_Position]);
				}
			}

			// Token: 0x17000B0F RID: 2831
			// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000A06FA File Offset: 0x0009E8FA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002C9F RID: 11423 RVA: 0x00002152 File Offset: 0x00000352
			public void Dispose()
			{
			}

			// Token: 0x04001449 RID: 5193
			private readonly VisualManager m_Manager;

			// Token: 0x0400144A RID: 5194
			private readonly VisualNodeChildrenData m_Children;

			// Token: 0x0400144B RID: 5195
			private int m_Position;
		}
	}
}
