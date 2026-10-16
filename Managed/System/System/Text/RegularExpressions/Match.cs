using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single regular expression match.</summary>
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public class Match : Group
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00035D50 File Offset: 0x00033F50
		internal Match(Regex regex, int capcount, string text, int begpos, int len, int startpos)
			: base(text, new int[2], 0, "0")
		{
			this._regex = regex;
			this._matchcount = new int[capcount];
			this._matches = new int[capcount][];
			this._matches[0] = this._caps;
			this._textbeg = begpos;
			this._textend = begpos + len;
			this._textstart = startpos;
			this._balancing = false;
		}

		/// <summary>Gets the empty group. All failed matches return this empty match.</summary>
		/// <returns>An empty match.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00035DBF File Offset: 0x00033FBF
		public static Match Empty { get; } = new Match(null, 1, string.Empty, 0, 0, 0);

		// Token: 0x06000D2C RID: 3372 RVA: 0x00035DC8 File Offset: 0x00033FC8
		internal virtual void Reset(Regex regex, string text, int textbeg, int textend, int textstart)
		{
			this._regex = regex;
			base.Text = text;
			this._textbeg = textbeg;
			this._textend = textend;
			this._textstart = textstart;
			for (int i = 0; i < this._matchcount.Length; i++)
			{
				this._matchcount[i] = 0;
			}
			this._balancing = false;
		}

		/// <summary>Gets a collection of groups matched by the regular expression.</summary>
		/// <returns>The character groups matched by the pattern.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00035E1D File Offset: 0x0003401D
		public virtual GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, null);
				}
				return this._groupcoll;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Text.RegularExpressions.Match" /> object with the results for the next match, starting at the position at which the last match ended (at the character after the last matched character).</summary>
		/// <returns>The next regular expression match.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00035E3A File Offset: 0x0003403A
		public Match NextMatch()
		{
			if (this._regex == null)
			{
				return this;
			}
			return this._regex.Run(false, base.Length, base.Text, this._textbeg, this._textend - this._textbeg, this._textpos);
		}

		/// <summary>Returns the expansion of the specified replacement pattern. </summary>
		/// <returns>The expanded version of the <paramref name="replacement" /> parameter.</returns>
		/// <param name="replacement">The replacement pattern to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">Expansion is not allowed for this pattern.</exception>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00035E78 File Offset: 0x00034078
		public virtual string Result(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (this._regex == null)
			{
				throw new NotSupportedException("Result cannot be called on a failed Match.");
			}
			return RegexReplacement.GetOrCreate(this._regex._replref, replacement, this._regex.caps, this._regex.capsize, this._regex.capnames, this._regex.roptions).Replacement(this);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00035EEC File Offset: 0x000340EC
		internal virtual ReadOnlySpan<char> GroupToStringImpl(int groupnum)
		{
			int num = this._matchcount[groupnum];
			if (num == 0)
			{
				return string.Empty;
			}
			int[] array = this._matches[groupnum];
			return base.Text.AsSpan(array[(num - 1) * 2], array[num * 2 - 1]);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00035F32 File Offset: 0x00034132
		internal ReadOnlySpan<char> LastGroupToStringImpl()
		{
			return this.GroupToStringImpl(this._matchcount.Length - 1);
		}

		/// <summary>Returns a <see cref="T:System.Text.RegularExpressions.Match" /> instance equivalent to the one supplied that is suitable to share between multiple threads.</summary>
		/// <returns>A regular expression match that is suitable to share between multiple threads. </returns>
		/// <param name="inner">A regular expression match equivalent to the one expected. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00035F44 File Offset: 0x00034144
		public static Match Synchronized(Match inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			int num = inner._matchcount.Length;
			for (int i = 0; i < num; i++)
			{
				Group.Synchronized(inner.Groups[i]);
			}
			return inner;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00035F88 File Offset: 0x00034188
		internal virtual void AddMatch(int cap, int start, int len)
		{
			if (this._matches[cap] == null)
			{
				this._matches[cap] = new int[2];
			}
			int num = this._matchcount[cap];
			if (num * 2 + 2 > this._matches[cap].Length)
			{
				int[] array = this._matches[cap];
				int[] array2 = new int[num * 8];
				for (int i = 0; i < num * 2; i++)
				{
					array2[i] = array[i];
				}
				this._matches[cap] = array2;
			}
			this._matches[cap][num * 2] = start;
			this._matches[cap][num * 2 + 1] = len;
			this._matchcount[cap] = num + 1;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00036020 File Offset: 0x00034220
		internal virtual void BalanceMatch(int cap)
		{
			this._balancing = true;
			int num = this._matchcount[cap] * 2 - 2;
			if (this._matches[cap][num] < 0)
			{
				num = -3 - this._matches[cap][num];
			}
			num -= 2;
			if (num >= 0 && this._matches[cap][num] < 0)
			{
				this.AddMatch(cap, this._matches[cap][num], this._matches[cap][num + 1]);
				return;
			}
			this.AddMatch(cap, -3 - num, -4 - num);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003609E File Offset: 0x0003429E
		internal virtual void RemoveMatch(int cap)
		{
			this._matchcount[cap]--;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000360B1 File Offset: 0x000342B1
		internal virtual bool IsMatched(int cap)
		{
			return cap < this._matchcount.Length && this._matchcount[cap] > 0 && this._matches[cap][this._matchcount[cap] * 2 - 1] != -2;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000360E8 File Offset: 0x000342E8
		internal virtual int MatchIndex(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 2];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00036120 File Offset: 0x00034320
		internal virtual int MatchLength(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 1];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00036158 File Offset: 0x00034358
		internal virtual void Tidy(int textpos)
		{
			int[] array = this._matches[0];
			base.Index = array[0];
			base.Length = array[1];
			this._textpos = textpos;
			this._capcount = this._matchcount[0];
			if (this._balancing)
			{
				for (int i = 0; i < this._matchcount.Length; i++)
				{
					int num = this._matchcount[i] * 2;
					int[] array2 = this._matches[i];
					int j = 0;
					while (j < num && array2[j] >= 0)
					{
						j++;
					}
					int num2 = j;
					while (j < num)
					{
						if (array2[j] < 0)
						{
							num2--;
						}
						else
						{
							if (j != num2)
							{
								array2[num2] = array2[j];
							}
							num2++;
						}
						j++;
					}
					this._matchcount[i] = num2 / 2;
				}
				this._balancing = false;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal Match()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400080D RID: 2061
		internal GroupCollection _groupcoll;

		// Token: 0x0400080E RID: 2062
		internal Regex _regex;

		// Token: 0x0400080F RID: 2063
		internal int _textbeg;

		// Token: 0x04000810 RID: 2064
		internal int _textpos;

		// Token: 0x04000811 RID: 2065
		internal int _textend;

		// Token: 0x04000812 RID: 2066
		internal int _textstart;

		// Token: 0x04000813 RID: 2067
		internal int[][] _matches;

		// Token: 0x04000814 RID: 2068
		internal int[] _matchcount;

		// Token: 0x04000815 RID: 2069
		internal bool _balancing;
	}
}
