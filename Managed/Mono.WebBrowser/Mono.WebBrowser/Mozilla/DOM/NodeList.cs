using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000140 RID: 320
	internal class NodeList : DOMObject, INodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x000095F4 File Offset: 0x000077F4
		public NodeList(WebBrowser control, nsIDOMNodeList nodeList)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedNodes = nsDOMNodeList.GetProxy(control, nodeList);
				return;
			}
			this.unmanagedNodes = nodeList;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00009620 File Offset: 0x00007820
		public NodeList(WebBrowser control)
			: base(control)
		{
			INode[] array = new Node[0];
			this.nodes = array;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00009642 File Offset: 0x00007842
		public NodeList(WebBrowser control, bool loaded)
			: base(control)
		{
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000964B File Offset: 0x0000784B
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00009668 File Offset: 0x00007868
		protected void Clear()
		{
			if (this.nodes != null)
			{
				for (int i = 0; i < this.nodeCount; i++)
				{
					this.nodes[i] = null;
				}
				this.nodeCount = 0;
				this.unmanagedNodes = null;
				this.nodes = null;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000096AC File Offset: 0x000078AC
		internal virtual void Load()
		{
			if (this.unmanagedNodes == null)
			{
				return;
			}
			this.Clear();
			uint num;
			this.unmanagedNodes.getLength(out num);
			this.nodeCount = (int)num;
			INode[] array = new Node[this.nodeCount];
			this.nodes = array;
			for (int i = 0; i < this.nodeCount; i++)
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.item((uint)i, out nsIDOMNode);
				ushort num2;
				nsIDOMNode.getNodeType(out num2);
				this.nodes[i] = base.GetTypedNode(nsIDOMNode);
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00009728 File Offset: 0x00007928
		public IEnumerator GetEnumerator()
		{
			return new NodeList.NodeListEnumerator(this);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00009730 File Offset: 0x00007930
		public void CopyTo(Array dest, int index)
		{
			if (this.nodes != null)
			{
				Array.Copy(this.nodes, 0, dest, index, this.Count);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000974E File Offset: 0x0000794E
		public virtual int Count
		{
			get
			{
				if (this.unmanagedNodes != null && this.nodes == null)
				{
					this.Load();
				}
				return this.nodeCount;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0000976C File Offset: 0x0000796C
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0000976F File Offset: 0x0000796F
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00009772 File Offset: 0x00007972
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00009775 File Offset: 0x00007975
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00009778 File Offset: 0x00007978
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00009784 File Offset: 0x00007984
		public void RemoveAt(int index)
		{
			if (index > this.Count || index < 0)
			{
				return;
			}
			Array.Copy(this.nodes, index + 1, this.nodes, index, this.nodeCount - index - 1);
			this.nodeCount--;
			this.nodes[this.nodeCount] = null;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000097DA File Offset: 0x000079DA
		public void Remove(INode node)
		{
			this.RemoveAt(this.IndexOf(node));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000097E9 File Offset: 0x000079E9
		void IList.Remove(object node)
		{
			this.Remove(node as INode);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000097F8 File Offset: 0x000079F8
		public void Insert(int index, INode value)
		{
			if (index > this.Count)
			{
				index = this.nodeCount;
			}
			INode[] array = new Node[this.nodeCount + 1];
			INode[] array2 = array;
			if (index > 0)
			{
				Array.Copy(this.nodes, 0, array2, 0, index);
			}
			array2[index] = value;
			if (index < this.nodeCount)
			{
				Array.Copy(this.nodes, index, array2, index + 1, this.nodeCount - index);
			}
			this.nodes = array2;
			this.nodeCount++;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00009873 File Offset: 0x00007A73
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as INode);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00009882 File Offset: 0x00007A82
		public int IndexOf(INode node)
		{
			return Array.IndexOf<INode>(this.nodes, node);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00009890 File Offset: 0x00007A90
		int IList.IndexOf(object node)
		{
			return this.IndexOf(node as INode);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0000989E File Offset: 0x00007A9E
		public bool Contains(INode node)
		{
			return this.IndexOf(node) != -1;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000098AD File Offset: 0x00007AAD
		bool IList.Contains(object node)
		{
			return this.Contains(node as INode);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000098BB File Offset: 0x00007ABB
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000098C3 File Offset: 0x00007AC3
		public int Add(INode node)
		{
			this.Insert(this.Count + 1, node);
			return this.nodeCount - 1;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000098DC File Offset: 0x00007ADC
		int IList.Add(object node)
		{
			return this.Add(node as INode);
		}

		// Token: 0x17000103 RID: 259
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value as INode;
			}
		}

		// Token: 0x17000104 RID: 260
		public INode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.nodes[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.nodes[index] = value;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00009947 File Offset: 0x00007B47
		public override int GetHashCode()
		{
			if (this.unmanagedNodes != null)
			{
				return this.unmanagedNodes.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x0400012A RID: 298
		protected nsIDOMNodeList unmanagedNodes;

		// Token: 0x0400012B RID: 299
		protected INode[] nodes;

		// Token: 0x0400012C RID: 300
		protected int nodeCount;

		// Token: 0x0200014C RID: 332
		internal class NodeListEnumerator : IEnumerator
		{
			// Token: 0x06000A75 RID: 2677 RVA: 0x0000A3BC File Offset: 0x000085BC
			public NodeListEnumerator(NodeList collection)
			{
				this.collection = collection;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0000A3D2 File Offset: 0x000085D2
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x0000A3F0 File Offset: 0x000085F0
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x0000A418 File Offset: 0x00008618
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x0400016F RID: 367
			private NodeList collection;

			// Token: 0x04000170 RID: 368
			private int index = -1;
		}
	}
}
