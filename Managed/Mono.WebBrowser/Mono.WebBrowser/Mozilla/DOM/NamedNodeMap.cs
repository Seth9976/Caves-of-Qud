using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x0200013D RID: 317
	internal class NamedNodeMap : NodeList, INamedNodeMap, IList, ICollection, IEnumerable
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00008119 File Offset: 0x00006319
		public NamedNodeMap(WebBrowser control, nsIDOMNamedNodeMap nodeMap)
			: base(control, true)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedNodes = nsDOMNamedNodeMap.GetProxy(control, nodeMap);
				return;
			}
			this.unmanagedNodes = nodeMap;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00008148 File Offset: 0x00006348
		internal override void Load()
		{
			base.Clear();
			uint num;
			this.unmanagedNodes.getLength(out num);
			this.nodeCount = (int)num;
			INode[] array = new Node[num];
			this.nodes = array;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.item((uint)num2, out nsIDOMNode);
				this.nodes[num2] = new Attribute(this.control, nsIDOMNode as nsIDOMAttr);
				num2++;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000081B4 File Offset: 0x000063B4
		public override int Count
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

		// Token: 0x170000E6 RID: 230
		public new INode this[int index]
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
			}
		}

		// Token: 0x170000E7 RID: 231
		public INode this[string name]
		{
			get
			{
				Base.StringSet(this.storage, name);
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.getNamedItem(this.storage, out nsIDOMNode);
				for (int i = 0; i < this.Count; i++)
				{
					if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
					{
						return this.nodes[i];
					}
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00008260 File Offset: 0x00006460
		public INode RemoveNamedItem(string name)
		{
			Base.StringSet(this.storage, name);
			nsIDOMNode nsIDOMNode;
			this.unmanagedNodes.removeNamedItem(this.storage, out nsIDOMNode);
			for (int i = 0; i < this.Count; i++)
			{
				if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
				{
					INode node = this.nodes[i];
					base.Remove(this.nodes[i]);
					return node;
				}
			}
			return null;
		}

		// Token: 0x170000E8 RID: 232
		public INode this[string namespaceURI, string localName]
		{
			get
			{
				Base.StringSet(this.storage, namespaceURI);
				UniString uniString = new UniString(localName);
				nsIDOMNode nsIDOMNode;
				this.unmanagedNodes.getNamedItemNS(this.storage, uniString.Handle, out nsIDOMNode);
				for (int i = 0; i < this.Count; i++)
				{
					if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
					{
						return this.nodes[i];
					}
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00008348 File Offset: 0x00006548
		public INode RemoveNamedItemNS(string namespaceURI, string localName)
		{
			Base.StringSet(this.storage, namespaceURI);
			UniString uniString = new UniString(localName);
			nsIDOMNode nsIDOMNode;
			this.unmanagedNodes.removeNamedItemNS(this.storage, uniString.Handle, out nsIDOMNode);
			for (int i = 0; i < this.Count; i++)
			{
				if (this.nodes[i].GetHashCode().Equals(nsIDOMNode.GetHashCode()))
				{
					INode node = this.nodes[i];
					base.Remove(this.nodes[i]);
					return node;
				}
			}
			return null;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000083C8 File Offset: 0x000065C8
		public override int GetHashCode()
		{
			return this.unmanagedNodes.GetHashCode();
		}

		// Token: 0x04000122 RID: 290
		protected new nsIDOMNamedNodeMap unmanagedNodes;
	}
}
