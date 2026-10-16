using System;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x0200062E RID: 1582
	public struct ActionCommandEntry : IComposite
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x0015498A File Offset: 0x00152B8A
		public ActionCommandEntry(Guid ID, IActionCommand Command, int SegmentDelay)
		{
			this.ID = ID;
			this.Command = Command;
			this.SegmentDelay = SegmentDelay;
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x001549A1 File Offset: 0x00152BA1
		public ActionCommandEntry(IActionCommand Action, int SegmentDelay)
		{
			this = new ActionCommandEntry(Guid.NewGuid(), Action, SegmentDelay);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x001549B0 File Offset: 0x00152BB0
		public void Write(SerializationWriter Writer)
		{
			Writer.Write(this.ID);
			Writer.Write(this.Command);
			Writer.WriteOptimized(this.SegmentDelay);
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x001549D6 File Offset: 0x00152BD6
		public void Read(SerializationReader Reader)
		{
			this.ID = Reader.ReadGuid();
			this.Command = Reader.ReadComposite() as IActionCommand;
			this.SegmentDelay = Reader.ReadOptimizedInt32();
		}

		// Token: 0x04002071 RID: 8305
		public Guid ID;

		// Token: 0x04002072 RID: 8306
		public IActionCommand Command;

		// Token: 0x04002073 RID: 8307
		public int SegmentDelay;
	}
}
