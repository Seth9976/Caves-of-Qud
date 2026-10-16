using System;
using System.Buffers;
using System.Diagnostics;
using XRL.UI;

namespace XRL.World.Text
{
	// Token: 0x02001465 RID: 5221
	public class CompositedTextBuilder : TextBuilder
	{
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600CD2A RID: 52522 RVA: 0x004A5D63 File Offset: 0x004A3F63
		protected override bool Finalizable
		{
			get
			{
				return this.Buffer == null;
			}
		}

		// Token: 0x0600CD2B RID: 52523 RVA: 0x004A5D6E File Offset: 0x004A3F6E
		public CompositedTextBuilder(int Capacity = 64)
			: base(32)
		{
			this.Buffer = ArrayPool<char>.Shared.Rent(Capacity);
			if (Options.TextBuilderDebug)
			{
				TextBuilder.Traces[this] = new StackTrace(1, true);
			}
		}

		// Token: 0x0600CD2C RID: 52524 RVA: 0x004A5DA2 File Offset: 0x004A3FA2
		public override void Dispose(bool Destroy)
		{
			if (Destroy)
			{
				base.Dispose(true);
			}
			this.Length = 0;
		}
	}
}
