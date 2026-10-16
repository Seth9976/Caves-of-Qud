using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group. </summary>
	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x0003596A File Offset: 0x00033B6A
		internal Group(string text, int[] caps, int capcount, string name)
			: base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this.Name = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>true if the match is successful; otherwise, false.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000359A3 File Offset: 0x00033BA3
		public bool Success
		{
			get
			{
				return this._capcount != 0;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x000359AE File Offset: 0x00033BAE
		public string Name { get; }

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000359B6 File Offset: 0x00033BB6
		public CaptureCollection Captures
		{
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		/// <summary>Returns a Group object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <returns>A regular expression Group object. </returns>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06000D00 RID: 3328 RVA: 0x000359D4 File Offset: 0x00033BD4
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner.Success)
			{
				captures.ForceInitialized();
			}
			return inner;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal Group()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000803 RID: 2051
		internal static readonly Group s_emptyGroup = new Group(string.Empty, Array.Empty<int>(), 0, string.Empty);

		// Token: 0x04000804 RID: 2052
		internal readonly int[] _caps;

		// Token: 0x04000805 RID: 2053
		internal int _capcount;

		// Token: 0x04000806 RID: 2054
		internal CaptureCollection _capcoll;
	}
}
