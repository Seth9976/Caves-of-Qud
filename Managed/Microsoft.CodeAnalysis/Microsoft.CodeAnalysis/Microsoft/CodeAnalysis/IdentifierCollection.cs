using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011D RID: 285
	[NullableContext(1)]
	[Nullable(0)]
	internal class IdentifierCollection
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0001C09B File Offset: 0x0001A29B
		public IdentifierCollection()
		{
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001C0B3 File Offset: 0x0001A2B3
		public IdentifierCollection(IEnumerable<string> identifiers)
		{
			this.AddIdentifiers(identifiers);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		public void AddIdentifiers(IEnumerable<string> identifiers)
		{
			foreach (string text in identifiers)
			{
				this.AddIdentifier(text);
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001C11C File Offset: 0x0001A31C
		public void AddIdentifier(string identifier)
		{
			object obj;
			if (!this._map.TryGetValue(identifier, ref obj))
			{
				this.AddInitialSpelling(identifier);
				return;
			}
			this.AddAdditionalSpelling(identifier, obj);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001C14C File Offset: 0x0001A34C
		private void AddAdditionalSpelling(string identifier, object value)
		{
			string text = value as string;
			if (text != null)
			{
				if (!string.Equals(identifier, text, 4))
				{
					Dictionary<string, object> map = this._map;
					HashSet<string> hashSet = new HashSet<string>();
					hashSet.Add(identifier);
					hashSet.Add(text);
					map[identifier] = hashSet;
					return;
				}
			}
			else
			{
				((HashSet<string>)value).Add(identifier);
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001C19C File Offset: 0x0001A39C
		private void AddInitialSpelling(string identifier)
		{
			this._map.Add(identifier, identifier);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001C1AB File Offset: 0x0001A3AB
		public bool ContainsIdentifier(string identifier, bool caseSensitive)
		{
			if (caseSensitive)
			{
				return this.CaseSensitiveContains(identifier);
			}
			return this.CaseInsensitiveContains(identifier);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001C1BF File Offset: 0x0001A3BF
		private bool CaseInsensitiveContains(string identifier)
		{
			return this._map.ContainsKey(identifier);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		private bool CaseSensitiveContains(string identifier)
		{
			object obj;
			if (!this._map.TryGetValue(identifier, ref obj))
			{
				return false;
			}
			string text = obj as string;
			if (text != null)
			{
				return string.Equals(identifier, text, 4);
			}
			return ((HashSet<string>)obj).Contains(identifier);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001C20E File Offset: 0x0001A40E
		public ICollection<string> AsCaseSensitiveCollection()
		{
			return new IdentifierCollection.CaseSensitiveCollection(this);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001C216 File Offset: 0x0001A416
		public ICollection<string> AsCaseInsensitiveCollection()
		{
			return new IdentifierCollection.CaseInsensitiveCollection(this);
		}

		// Token: 0x040002C8 RID: 712
		private readonly Dictionary<string, object> _map = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000542 RID: 1346
		[Nullable(0)]
		private abstract class CollectionBase : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x060033C0 RID: 13248 RVA: 0x00092428 File Offset: 0x00090628
			protected CollectionBase(IdentifierCollection identifierCollection)
			{
				this.IdentifierCollection = identifierCollection;
			}

			// Token: 0x060033C1 RID: 13249
			public abstract bool Contains(string item);

			// Token: 0x060033C2 RID: 13250 RVA: 0x00092440 File Offset: 0x00090640
			public void CopyTo(string[] array, int arrayIndex)
			{
				using (IEnumerator<string> enumerator = this.GetEnumerator())
				{
					while (arrayIndex < array.Length && enumerator.MoveNext())
					{
						array[arrayIndex] = enumerator.Current;
						arrayIndex++;
					}
				}
			}

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x060033C3 RID: 13251 RVA: 0x00092490 File Offset: 0x00090690
			public int Count
			{
				get
				{
					if (this._count == -1)
					{
						this._count = Enumerable.Sum<object>(this.IdentifierCollection._map.Values, delegate(object o)
						{
							if (!(o is string))
							{
								return ((ISet<string>)o).Count;
							}
							return 1;
						});
					}
					return this._count;
				}
			}

			// Token: 0x17000EC9 RID: 3785
			// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000924E6 File Offset: 0x000906E6
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060033C5 RID: 13253 RVA: 0x000924E9 File Offset: 0x000906E9
			public IEnumerator<string> GetEnumerator()
			{
				foreach (object obj in this.IdentifierCollection._map.Values)
				{
					HashSet<string> hashSet = obj as HashSet<string>;
					if (hashSet != null)
					{
						foreach (string text in hashSet)
						{
							yield return text;
						}
						HashSet<string>.Enumerator enumerator2 = default(HashSet<string>.Enumerator);
					}
					else
					{
						yield return (string)obj;
					}
				}
				Dictionary<string, object>.ValueCollection.Enumerator enumerator = default(Dictionary<string, object>.ValueCollection.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x060033C6 RID: 13254 RVA: 0x000924F8 File Offset: 0x000906F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060033C7 RID: 13255 RVA: 0x00092500 File Offset: 0x00090700
			public void Add(string item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060033C8 RID: 13256 RVA: 0x00092507 File Offset: 0x00090707
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060033C9 RID: 13257 RVA: 0x0009250E File Offset: 0x0009070E
			public bool Remove(string item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040014EF RID: 5359
			protected readonly IdentifierCollection IdentifierCollection;

			// Token: 0x040014F0 RID: 5360
			private int _count = -1;
		}

		// Token: 0x02000543 RID: 1347
		[Nullable(0)]
		private sealed class CaseSensitiveCollection : IdentifierCollection.CollectionBase
		{
			// Token: 0x060033CA RID: 13258 RVA: 0x00092515 File Offset: 0x00090715
			public CaseSensitiveCollection(IdentifierCollection identifierCollection)
				: base(identifierCollection)
			{
			}

			// Token: 0x060033CB RID: 13259 RVA: 0x0009251E File Offset: 0x0009071E
			public override bool Contains(string item)
			{
				return this.IdentifierCollection.CaseSensitiveContains(item);
			}
		}

		// Token: 0x02000544 RID: 1348
		[Nullable(0)]
		private sealed class CaseInsensitiveCollection : IdentifierCollection.CollectionBase
		{
			// Token: 0x060033CC RID: 13260 RVA: 0x0009252C File Offset: 0x0009072C
			public CaseInsensitiveCollection(IdentifierCollection identifierCollection)
				: base(identifierCollection)
			{
			}

			// Token: 0x060033CD RID: 13261 RVA: 0x00092535 File Offset: 0x00090735
			public override bool Contains(string item)
			{
				return this.IdentifierCollection.CaseInsensitiveContains(item);
			}
		}
	}
}
