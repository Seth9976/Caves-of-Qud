using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x020001F5 RID: 501
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Match>))]
	[Serializable]
	public class MatchCollection : IList<Match>, ICollection<Match>, IEnumerable<Match>, IEnumerable, IReadOnlyList<Match>, IReadOnlyCollection<Match>, IList, ICollection
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x0003627C File Offset: 0x0003447C
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new List<Match>();
			this._done = false;
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases. </returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x000362EC File Offset: 0x000344EC
		public int Count
		{
			get
			{
				this.EnsureInitialized();
				return this._matches.Count;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the <see cref="T:System.Text.RegularExpressions.Match" /> collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />. </exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x1700024D RID: 589
		public virtual Match this[int i]
		{
			get
			{
				if (i < 0)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Match" /> objects within the <see cref="T:System.Text.RegularExpressions.MatchCollection" />.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000D42 RID: 3394 RVA: 0x00036325 File Offset: 0x00034525
		public IEnumerator GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00036325 File Offset: 0x00034525
		IEnumerator<Match> IEnumerable<Match>.GetEnumerator()
		{
			return new MatchCollection.Enumerator(this);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00036330 File Offset: 0x00034530
		private Match GetMatch(int i)
		{
			if (this._matches.Count > i)
			{
				return this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match.Length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000363D1 File Offset: 0x000345D1
		private void EnsureInitialized()
		{
			if (!this._done)
			{
				this.GetMatch(int.MaxValue);
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x000075E1 File Offset: 0x000057E1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000D48 RID: 3400 RVA: 0x000363E7 File Offset: 0x000345E7
		public void CopyTo(Array array, int arrayIndex)
		{
			this.EnsureInitialized();
			((ICollection)this._matches).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000363FC File Offset: 0x000345FC
		public void CopyTo(Match[] array, int arrayIndex)
		{
			this.EnsureInitialized();
			this._matches.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00036411 File Offset: 0x00034611
		int IList<Match>.IndexOf(Match item)
		{
			this.EnsureInitialized();
			return this._matches.IndexOf(item);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00035761 File Offset: 0x00033961
		void IList<Match>.Insert(int index, Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00035761 File Offset: 0x00033961
		void IList<Match>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000250 RID: 592
		Match IList<Match>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00035761 File Offset: 0x00033961
		void ICollection<Match>.Add(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00035761 File Offset: 0x00033961
		void ICollection<Match>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003642E File Offset: 0x0003462E
		bool ICollection<Match>.Contains(Match item)
		{
			this.EnsureInitialized();
			return this._matches.Contains(item);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00035761 File Offset: 0x00033961
		bool ICollection<Match>.Remove(Match item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00035761 File Offset: 0x00033961
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00035761 File Offset: 0x00033961
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00036442 File Offset: 0x00034642
		bool IList.Contains(object value)
		{
			return value is Match && ((ICollection<Match>)this).Contains((Match)value);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003645A File Offset: 0x0003465A
		int IList.IndexOf(object value)
		{
			if (!(value is Match))
			{
				return -1;
			}
			return ((IList<Match>)this).IndexOf((Match)value);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00035761 File Offset: 0x00033961
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00035761 File Offset: 0x00033961
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00035761 File Offset: 0x00033961
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000252 RID: 594
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal MatchCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000818 RID: 2072
		private readonly Regex _regex;

		// Token: 0x04000819 RID: 2073
		private readonly List<Match> _matches;

		// Token: 0x0400081A RID: 2074
		private bool _done;

		// Token: 0x0400081B RID: 2075
		private readonly string _input;

		// Token: 0x0400081C RID: 2076
		private readonly int _beginning;

		// Token: 0x0400081D RID: 2077
		private readonly int _length;

		// Token: 0x0400081E RID: 2078
		private int _startat;

		// Token: 0x0400081F RID: 2079
		private int _prevlen;

		// Token: 0x020001F6 RID: 502
		[Serializable]
		private sealed class Enumerator : IEnumerator<Match>, IDisposable, IEnumerator
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x00036472 File Offset: 0x00034672
			internal Enumerator(MatchCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x00036488 File Offset: 0x00034688
			public bool MoveNext()
			{
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._collection.GetMatch(this._index) == null)
				{
					this._index = -2;
					return false;
				}
				return true;
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000364C2 File Offset: 0x000346C2
			public Match Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection.GetMatch(this._index);
				}
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000364E9 File Offset: 0x000346E9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000364F1 File Offset: 0x000346F1
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x00003917 File Offset: 0x00001B17
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000820 RID: 2080
			private readonly MatchCollection _collection;

			// Token: 0x04000821 RID: 2081
			private int _index;
		}
	}
}
