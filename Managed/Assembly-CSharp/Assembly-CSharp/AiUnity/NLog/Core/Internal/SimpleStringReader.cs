using System;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026D RID: 621
	internal class SimpleStringReader
	{
		// Token: 0x06001664 RID: 5732 RVA: 0x00078A88 File Offset: 0x00076C88
		public SimpleStringReader(string text)
		{
			this.text = text;
			this.Position = 0;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00078A9E File Offset: 0x00076C9E
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00078AA6 File Offset: 0x00076CA6
		internal int Position { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00078AAF File Offset: 0x00076CAF
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00078AB7 File Offset: 0x00076CB7
		internal int Peek()
		{
			if (this.Position < this.text.Length)
			{
				return (int)this.text[this.Position];
			}
			return -1;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00078AE0 File Offset: 0x00076CE0
		internal int Read()
		{
			if (this.Position < this.text.Length)
			{
				string text = this.text;
				int position = this.Position;
				this.Position = position + 1;
				return (int)text[position];
			}
			return -1;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00078B1E File Offset: 0x00076D1E
		internal string Substring(int p0, int p1)
		{
			return this.text.Substring(p0, p1 - p0);
		}

		// Token: 0x04000DD7 RID: 3543
		private readonly string text;
	}
}
