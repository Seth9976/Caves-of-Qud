using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002AB RID: 683
	[NullableContext(1)]
	[Nullable(0)]
	internal struct ChildSyntaxList
	{
		// Token: 0x06001CED RID: 7405 RVA: 0x00054E47 File Offset: 0x00053047
		internal ChildSyntaxList(GreenNode node)
		{
			this._node = node;
			this._count = -1;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00054E57 File Offset: 0x00053057
		public int Count
		{
			get
			{
				if (this._count == -1)
				{
					this._count = this.CountNodes();
				}
				return this._count;
			}
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00054E74 File Offset: 0x00053074
		private int CountNodes()
		{
			int num = 0;
			while (this.GetEnumerator().MoveNext())
			{
				num++;
			}
			return num;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00054E9C File Offset: 0x0005309C
		private GreenNode[] Nodes
		{
			get
			{
				GreenNode[] array = new GreenNode[this.Count];
				int num = 0;
				foreach (GreenNode greenNode in this)
				{
					array[num++] = greenNode;
				}
				return array;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00054EDA File Offset: 0x000530DA
		public ChildSyntaxList.Enumerator GetEnumerator()
		{
			return new ChildSyntaxList.Enumerator(this._node);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00054EE7 File Offset: 0x000530E7
		public ChildSyntaxList.Reversed Reverse()
		{
			return new ChildSyntaxList.Reversed(this._node);
		}

		// Token: 0x04000D60 RID: 3424
		[Nullable(2)]
		private readonly GreenNode _node;

		// Token: 0x04000D61 RID: 3425
		private int _count;

		// Token: 0x02000655 RID: 1621
		[NullableContext(2)]
		[Nullable(0)]
		internal struct Enumerator
		{
			// Token: 0x060038B2 RID: 14514 RVA: 0x0009D736 File Offset: 0x0009B936
			internal Enumerator(GreenNode node)
			{
				this._node = node;
				this._childIndex = -1;
				this._listIndex = -1;
				this._list = null;
				this._currentChild = null;
			}

			// Token: 0x060038B3 RID: 14515 RVA: 0x0009D75C File Offset: 0x0009B95C
			public bool MoveNext()
			{
				if (this._node != null)
				{
					if (this._list != null)
					{
						this._listIndex++;
						if (this._listIndex < this._list.SlotCount)
						{
							this._currentChild = this._list.GetSlot(this._listIndex);
							return true;
						}
						this._list = null;
						this._listIndex = -1;
					}
					GreenNode slot;
					for (;;)
					{
						this._childIndex++;
						if (this._childIndex == this._node.SlotCount)
						{
							goto IL_00F7;
						}
						slot = this._node.GetSlot(this._childIndex);
						if (slot != null)
						{
							if (slot.RawKind != 1)
							{
								goto IL_00EE;
							}
							this._list = slot;
							this._listIndex++;
							if (this._listIndex < this._list.SlotCount)
							{
								break;
							}
							this._list = null;
							this._listIndex = -1;
						}
					}
					this._currentChild = this._list.GetSlot(this._listIndex);
					return true;
					IL_00EE:
					this._currentChild = slot;
					return true;
				}
				IL_00F7:
				this._currentChild = null;
				return false;
			}

			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x060038B4 RID: 14516 RVA: 0x0009D868 File Offset: 0x0009BA68
			[Nullable(1)]
			public GreenNode Current
			{
				[NullableContext(1)]
				get
				{
					return this._currentChild;
				}
			}

			// Token: 0x04001862 RID: 6242
			private readonly GreenNode _node;

			// Token: 0x04001863 RID: 6243
			private int _childIndex;

			// Token: 0x04001864 RID: 6244
			private GreenNode _list;

			// Token: 0x04001865 RID: 6245
			private int _listIndex;

			// Token: 0x04001866 RID: 6246
			private GreenNode _currentChild;
		}

		// Token: 0x02000656 RID: 1622
		[NullableContext(2)]
		[Nullable(0)]
		internal struct Reversed
		{
			// Token: 0x060038B5 RID: 14517 RVA: 0x0009D870 File Offset: 0x0009BA70
			internal Reversed(GreenNode node)
			{
				this._node = node;
			}

			// Token: 0x060038B6 RID: 14518 RVA: 0x0009D879 File Offset: 0x0009BA79
			public ChildSyntaxList.Reversed.Enumerator GetEnumerator()
			{
				return new ChildSyntaxList.Reversed.Enumerator(this._node);
			}

			// Token: 0x04001867 RID: 6247
			private readonly GreenNode _node;

			// Token: 0x020007CB RID: 1995
			[Nullable(0)]
			internal struct Enumerator
			{
				// Token: 0x06003FFB RID: 16379 RVA: 0x000B2A54 File Offset: 0x000B0C54
				internal Enumerator(GreenNode node)
				{
					if (node != null)
					{
						this._node = node;
						this._childIndex = node.SlotCount;
						this._listIndex = -1;
					}
					else
					{
						this._node = null;
						this._childIndex = 0;
						this._listIndex = -1;
					}
					this._list = null;
					this._currentChild = null;
				}

				// Token: 0x06003FFC RID: 16380 RVA: 0x000B2AA4 File Offset: 0x000B0CA4
				public bool MoveNext()
				{
					if (this._node != null)
					{
						if (this._list != null)
						{
							int num = this._listIndex - 1;
							this._listIndex = num;
							if (num >= 0)
							{
								this._currentChild = this._list.GetSlot(this._listIndex);
								return true;
							}
							this._list = null;
							this._listIndex = -1;
						}
						GreenNode slot;
						for (;;)
						{
							int num = this._childIndex - 1;
							this._childIndex = num;
							if (num < 0)
							{
								goto IL_00E5;
							}
							slot = this._node.GetSlot(this._childIndex);
							if (slot != null)
							{
								if (!slot.IsList)
								{
									goto IL_00C5;
								}
								this._list = slot;
								this._listIndex = this._list.SlotCount;
								num = this._listIndex - 1;
								this._listIndex = num;
								if (num >= 0)
								{
									break;
								}
								this._list = null;
								this._listIndex = -1;
							}
						}
						this._currentChild = this._list.GetSlot(this._listIndex);
						return true;
						IL_00C5:
						this._currentChild = slot;
						return true;
					}
					IL_00E5:
					this._currentChild = null;
					return false;
				}

				// Token: 0x1700118E RID: 4494
				// (get) Token: 0x06003FFD RID: 16381 RVA: 0x000B2B9E File Offset: 0x000B0D9E
				[Nullable(1)]
				public GreenNode Current
				{
					[NullableContext(1)]
					get
					{
						return this._currentChild;
					}
				}

				// Token: 0x04001DF8 RID: 7672
				private readonly GreenNode _node;

				// Token: 0x04001DF9 RID: 7673
				private int _childIndex;

				// Token: 0x04001DFA RID: 7674
				private GreenNode _list;

				// Token: 0x04001DFB RID: 7675
				private int _listIndex;

				// Token: 0x04001DFC RID: 7676
				private GreenNode _currentChild;
			}
		}
	}
}
