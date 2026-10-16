using System;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200061B RID: 1563
	internal class BuilderLocationStack : Stack
	{
		// Token: 0x0600430B RID: 17163 RVA: 0x000B10A3 File Offset: 0x000AF2A3
		public override void Push(object o)
		{
			if (!(o is BuilderLocation))
			{
				throw new InvalidOperationException();
			}
			base.Push(o);
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000B10BC File Offset: 0x000AF2BC
		public virtual void Push(ControlBuilder builder, ILocation location)
		{
			BuilderLocation builderLocation = new BuilderLocation(builder, location);
			this.Push(builderLocation);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000B10D8 File Offset: 0x000AF2D8
		public new BuilderLocation Peek()
		{
			return (BuilderLocation)base.Peek();
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000B10E5 File Offset: 0x000AF2E5
		public new BuilderLocation Pop()
		{
			return (BuilderLocation)base.Pop();
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x000B10F2 File Offset: 0x000AF2F2
		public ControlBuilder Builder
		{
			get
			{
				return this.Peek().Builder;
			}
		}
	}
}
