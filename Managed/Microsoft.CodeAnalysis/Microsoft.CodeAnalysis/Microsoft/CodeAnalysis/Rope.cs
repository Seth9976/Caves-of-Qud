using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011F RID: 287
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class Rope
	{
		// Token: 0x06000A07 RID: 2567
		public abstract override string ToString();

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A08 RID: 2568
		public abstract int Length { get; }

		// Token: 0x06000A09 RID: 2569
		protected abstract IEnumerable<char> GetChars();

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001CD87 File Offset: 0x0001AF87
		private Rope()
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001CD8F File Offset: 0x0001AF8F
		public static Rope ForString(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return new Rope.StringRope(s);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		public static Rope Concat(Rope r1, Rope r2)
		{
			if (r1 == null)
			{
				throw new ArgumentNullException("r1");
			}
			if (r2 == null)
			{
				throw new ArgumentNullException("r2");
			}
			if (r1.Length == 0)
			{
				return r2;
			}
			if (r2.Length == 0)
			{
				return r1;
			}
			if (checked(r1.Length + r2.Length) >= 32)
			{
				return new Rope.ConcatRope(r1, r2);
			}
			return Rope.ForString(r1.ToString() + r2.ToString());
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001CE14 File Offset: 0x0001B014
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			Rope rope = obj as Rope;
			if (rope == null || this.Length != rope.Length)
			{
				return false;
			}
			if (this.Length == 0)
			{
				return true;
			}
			IEnumerator<char> enumerator = this.GetChars().GetEnumerator();
			IEnumerator<char> enumerator2 = rope.GetChars().GetEnumerator();
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				if (enumerator.Current != enumerator2.Current)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001CE80 File Offset: 0x0001B080
		public override int GetHashCode()
		{
			int num = this.Length;
			foreach (char c in this.GetChars())
			{
				num = Hash.Combine((int)c, num);
			}
			return num;
		}

		// Token: 0x040002C9 RID: 713
		public static readonly Rope Empty = Rope.ForString("");

		// Token: 0x0200054A RID: 1354
		[Nullable(0)]
		private sealed class StringRope : Rope
		{
			// Token: 0x060033D6 RID: 13270 RVA: 0x00092A66 File Offset: 0x00090C66
			public StringRope(string value)
			{
				this._value = value;
			}

			// Token: 0x060033D7 RID: 13271 RVA: 0x00092A75 File Offset: 0x00090C75
			public override string ToString()
			{
				return this._value;
			}

			// Token: 0x17000ECA RID: 3786
			// (get) Token: 0x060033D8 RID: 13272 RVA: 0x00092A7D File Offset: 0x00090C7D
			public override int Length
			{
				get
				{
					return this._value.Length;
				}
			}

			// Token: 0x060033D9 RID: 13273 RVA: 0x00092A8A File Offset: 0x00090C8A
			protected override IEnumerable<char> GetChars()
			{
				return this._value;
			}

			// Token: 0x04001512 RID: 5394
			private readonly string _value;
		}

		// Token: 0x0200054B RID: 1355
		[Nullable(0)]
		private sealed class ConcatRope : Rope
		{
			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x060033DA RID: 13274 RVA: 0x00092A92 File Offset: 0x00090C92
			public override int Length { get; }

			// Token: 0x060033DB RID: 13275 RVA: 0x00092A9A File Offset: 0x00090C9A
			public ConcatRope(Rope left, Rope right)
			{
				this._left = left;
				this._right = right;
				this.Length = checked(left.Length + right.Length);
			}

			// Token: 0x060033DC RID: 13276 RVA: 0x00092AC4 File Offset: 0x00090CC4
			public override string ToString()
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				Stack<Rope> stack = new Stack<Rope>();
				stack.Push(this);
				while (stack.Count != 0)
				{
					Rope rope = stack.Pop();
					Rope.StringRope stringRope = rope as Rope.StringRope;
					if (stringRope == null)
					{
						Rope.ConcatRope concatRope = rope as Rope.ConcatRope;
						if (concatRope == null)
						{
							throw ExceptionUtilities.UnexpectedValue(rope.GetType().Name);
						}
						stack.Push(concatRope._right);
						stack.Push(concatRope._left);
					}
					else
					{
						instance.Builder.Append(stringRope.ToString());
					}
				}
				return instance.ToStringAndFree();
			}

			// Token: 0x060033DD RID: 13277 RVA: 0x00092B54 File Offset: 0x00090D54
			protected override IEnumerable<char> GetChars()
			{
				Stack<Rope> stack = new Stack<Rope>();
				stack.Push(this);
				while (stack.Count != 0)
				{
					Rope rope = stack.Pop();
					Rope.StringRope stringRope = rope as Rope.StringRope;
					if (stringRope == null)
					{
						Rope.ConcatRope concatRope = rope as Rope.ConcatRope;
						if (concatRope == null)
						{
							throw ExceptionUtilities.UnexpectedValue(rope.GetType().Name);
						}
						stack.Push(concatRope._right);
						stack.Push(concatRope._left);
					}
					else
					{
						string text = stringRope.ToString();
						for (int i = 0; i < text.Length; i++)
						{
							char c = text.get_Chars(i);
							yield return c;
						}
						text = null;
					}
				}
				yield break;
			}

			// Token: 0x04001513 RID: 5395
			private readonly Rope _left;

			// Token: 0x04001514 RID: 5396
			private readonly Rope _right;
		}
	}
}
