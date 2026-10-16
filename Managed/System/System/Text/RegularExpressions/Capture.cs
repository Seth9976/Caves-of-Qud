using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture. </summary>
	// Token: 0x020001EA RID: 490
	public class Capture
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x000354C7 File Offset: 0x000336C7
		internal Capture(string text, int index, int length)
		{
			this.Text = text;
			this.Index = index;
			this.Length = length;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000354E4 File Offset: 0x000336E4
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x000354EC File Offset: 0x000336EC
		public int Index { get; private protected set; }

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000354F5 File Offset: 0x000336F5
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x000354FD File Offset: 0x000336FD
		public int Length { get; private protected set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00035506 File Offset: 0x00033706
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0003550E File Offset: 0x0003370E
		protected internal string Text { internal get; private protected set; }

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00035517 File Offset: 0x00033717
		public string Value
		{
			get
			{
				return this.Text.Substring(this.Index, this.Length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property. </summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x00035530 File Offset: 0x00033730
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00035538 File Offset: 0x00033738
		internal ReadOnlySpan<char> GetLeftSubstring()
		{
			return this.Text.AsSpan(0, this.Index);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003554C File Offset: 0x0003374C
		internal ReadOnlySpan<char> GetRightSubstring()
		{
			return this.Text.AsSpan(this.Index + this.Length, this.Text.Length - this.Index - this.Length);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal Capture()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
