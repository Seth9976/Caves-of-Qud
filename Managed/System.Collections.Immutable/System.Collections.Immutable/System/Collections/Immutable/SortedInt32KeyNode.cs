using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<[Nullable(2)] TValue> : IBinaryTree
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000936D File Offset: 0x0000756D
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000937C File Offset: 0x0000757C
		private SortedInt32KeyNode(int key, TValue value, SortedInt32KeyNode<TValue> left, SortedInt32KeyNode<TValue> right, bool frozen = false)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(left, "left");
			Requires.NotNull<SortedInt32KeyNode<TValue>>(right, "right");
			this._key = key;
			this._value = value;
			this._left = left;
			this._right = right;
			this._frozen = frozen;
			this._height = checked(1 + Math.Max(left._height, right._height));
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000093E6 File Offset: 0x000075E6
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000093F1 File Offset: 0x000075F1
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000093F9 File Offset: 0x000075F9
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Left
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00009401 File Offset: 0x00007601
		[Nullable(new byte[] { 2, 1 })]
		public SortedInt32KeyNode<TValue> Right
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._right;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00009409 File Offset: 0x00007609
		[Nullable(2)]
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00009411 File Offset: 0x00007611
		[Nullable(2)]
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00009419 File Offset: 0x00007619
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00009420 File Offset: 0x00007620
		[Nullable(new byte[] { 0, 1 })]
		public KeyValuePair<int, TValue> Value
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00009434 File Offset: 0x00007634
		internal IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, TValue> keyValuePair in this)
				{
					yield return keyValuePair.Value;
				}
				SortedInt32KeyNode<TValue>.Enumerator enumerator = default(SortedInt32KeyNode<TValue>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00009451 File Offset: 0x00007651
		[NullableContext(0)]
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009459 File Offset: 0x00007659
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00009474 File Offset: 0x00007674
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00009480 File Offset: 0x00007680
		[NullableContext(2)]
		internal TValue GetValueOrDefault(int key)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					return sortedInt32KeyNode._value;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			return default(TValue);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000094CC File Offset: 0x000076CC
		internal bool TryGetValue(int key, [MaybeNullWhen(false)] out TValue value)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					value = sortedInt32KeyNode._value;
					return true;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00009520 File Offset: 0x00007720
		internal void Freeze([Nullable(new byte[] { 2, 0, 1 })] Action<KeyValuePair<int, TValue>> freezeAction = null)
		{
			if (!this._frozen)
			{
				if (freezeAction != null)
				{
					freezeAction.Invoke(new KeyValuePair<int, TValue>(this._key, this._value));
				}
				this._left.Freeze(freezeAction);
				this._right.Freeze(freezeAction);
				this._frozen = true;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00009570 File Offset: 0x00007770
		private static SortedInt32KeyNode<TValue> RotateLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> right = tree._right;
			return right.Mutate(tree.Mutate(null, right._left), null);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000095B4 File Offset: 0x000077B4
		private static SortedInt32KeyNode<TValue> RotateRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> left = tree._left;
			return left.Mutate(null, tree.Mutate(left._right, null));
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000095F8 File Offset: 0x000077F8
		private static SortedInt32KeyNode<TValue> DoubleLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = tree.Mutate(null, SortedInt32KeyNode<TValue>.RotateRight(tree._right));
			return SortedInt32KeyNode<TValue>.RotateLeft(sortedInt32KeyNode);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00009638 File Offset: 0x00007838
		private static SortedInt32KeyNode<TValue> DoubleRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = tree.Mutate(SortedInt32KeyNode<TValue>.RotateLeft(tree._left), null);
			return SortedInt32KeyNode<TValue>.RotateRight(sortedInt32KeyNode);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00009678 File Offset: 0x00007878
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000969C File Offset: 0x0000789C
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000096B5 File Offset: 0x000078B5
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000096D0 File Offset: 0x000078D0
		private static SortedInt32KeyNode<TValue> MakeBalanced(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (SortedInt32KeyNode<TValue>.IsRightHeavy(tree))
			{
				if (SortedInt32KeyNode<TValue>.Balance(tree._right) >= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateLeft(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleLeft(tree);
			}
			else
			{
				if (!SortedInt32KeyNode<TValue>.IsLeftHeavy(tree))
				{
					return tree;
				}
				if (SortedInt32KeyNode<TValue>.Balance(tree._left) <= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateRight(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleRight(tree);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009734 File Offset: 0x00007934
		private SortedInt32KeyNode<TValue> SetOrAdd(int key, TValue value, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
		{
			replacedExistingValue = false;
			if (this.IsEmpty)
			{
				mutated = true;
				return new SortedInt32KeyNode<TValue>(key, value, this, this, false);
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key > this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, sortedInt32KeyNode2);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode3 = this._left.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(sortedInt32KeyNode3, null);
				}
			}
			else
			{
				if (valueComparer.Equals(this._value, value))
				{
					mutated = false;
					return this;
				}
				if (!overwriteExistingValue)
				{
					throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
				}
				mutated = true;
				replacedExistingValue = true;
				sortedInt32KeyNode = new SortedInt32KeyNode<TValue>(key, value, this._left, this._right, false);
			}
			if (!mutated)
			{
				return sortedInt32KeyNode;
			}
			return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00009818 File Offset: 0x00007A18
		private SortedInt32KeyNode<TValue> RemoveRecursive(int key, out bool mutated)
		{
			if (this.IsEmpty)
			{
				mutated = false;
				return this;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key == this._key)
			{
				mutated = true;
				if (this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = SortedInt32KeyNode<TValue>.EmptyNode;
				}
				else if (this._right.IsEmpty && !this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._left;
				}
				else if (!this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._right;
				}
				else
				{
					SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right;
					while (!sortedInt32KeyNode2._left.IsEmpty)
					{
						sortedInt32KeyNode2 = sortedInt32KeyNode2._left;
					}
					bool flag;
					SortedInt32KeyNode<TValue> sortedInt32KeyNode3 = this._right.Remove(sortedInt32KeyNode2._key, out flag);
					sortedInt32KeyNode = sortedInt32KeyNode2.Mutate(this._left, sortedInt32KeyNode3);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode4 = this._left.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(sortedInt32KeyNode4, null);
				}
			}
			else
			{
				SortedInt32KeyNode<TValue> sortedInt32KeyNode5 = this._right.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, sortedInt32KeyNode5);
				}
			}
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
			}
			return sortedInt32KeyNode;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000994C File Offset: 0x00007B4C
		private SortedInt32KeyNode<TValue> Mutate(SortedInt32KeyNode<TValue> left = null, SortedInt32KeyNode<TValue> right = null)
		{
			if (this._frozen)
			{
				return new SortedInt32KeyNode<TValue>(this._key, this._value, left ?? this._left, right ?? this._right, false);
			}
			if (left != null)
			{
				this._left = left;
			}
			if (right != null)
			{
				this._right = right;
			}
			this._height = checked(1 + Math.Max(this._left._height, this._right._height));
			return this;
		}

		// Token: 0x04000044 RID: 68
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x04000045 RID: 69
		private readonly int _key;

		// Token: 0x04000046 RID: 70
		private readonly TValue _value;

		// Token: 0x04000047 RID: 71
		private bool _frozen;

		// Token: 0x04000048 RID: 72
		private byte _height;

		// Token: 0x04000049 RID: 73
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x0400004A RID: 74
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x0200007B RID: 123
		[NullableContext(0)]
		[EditorBrowsable(2)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x00010BD4 File Offset: 0x0000EDD4
			[NullableContext(1)]
			internal Enumerator(SortedInt32KeyNode<TValue> root)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(root, "root");
				this._root = root;
				this._current = null;
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryTake(this, out this._stack))
					{
						this._stack = SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x00010C5E File Offset: 0x0000EE5E
			[Nullable(new byte[] { 0, 1 })]
			public KeyValuePair<int, TValue> Current
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600063F RID: 1599 RVA: 0x00010C7F File Offset: 0x0000EE7F
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000640 RID: 1600 RVA: 0x00010C87 File Offset: 0x0000EE87
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x00010C94 File Offset: 0x0000EE94
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack;
				if (this._stack != null && this._stack.TryUse<SortedInt32KeyNode<TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					SortedInt32KeyNode<TValue>.Enumerator.s_enumeratingStacks.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x06000642 RID: 1602 RVA: 0x00010CEC File Offset: 0x0000EEEC
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						SortedInt32KeyNode<TValue> value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x06000643 RID: 1603 RVA: 0x00010D48 File Offset: 0x0000EF48
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._current = null;
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x06000644 RID: 1604 RVA: 0x00010D89 File Offset: 0x0000EF89
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x00010DB4 File Offset: 0x0000EFB4
			private void PushLeft(SortedInt32KeyNode<TValue> node)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(node, "node");
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<SortedInt32KeyNode<TValue>>(node));
					node = node.Left;
				}
			}

			// Token: 0x040000FD RID: 253
			private static readonly SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator> s_enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>();

			// Token: 0x040000FE RID: 254
			private readonly int _poolUserId;

			// Token: 0x040000FF RID: 255
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x04000100 RID: 256
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x04000101 RID: 257
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
