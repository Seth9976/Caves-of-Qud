using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000109 RID: 265
	public sealed class ExceptionHandler
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x000229B5 File Offset: 0x00020BB5
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x000229BD File Offset: 0x00020BBD
		public Instruction TryStart
		{
			get
			{
				return this.try_start;
			}
			set
			{
				this.try_start = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000229C6 File Offset: 0x00020BC6
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x000229CE File Offset: 0x00020BCE
		public Instruction TryEnd
		{
			get
			{
				return this.try_end;
			}
			set
			{
				this.try_end = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000229D7 File Offset: 0x00020BD7
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x000229DF File Offset: 0x00020BDF
		public Instruction FilterStart
		{
			get
			{
				return this.filter_start;
			}
			set
			{
				this.filter_start = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000229E8 File Offset: 0x00020BE8
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000229F0 File Offset: 0x00020BF0
		public Instruction HandlerStart
		{
			get
			{
				return this.handler_start;
			}
			set
			{
				this.handler_start = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000229F9 File Offset: 0x00020BF9
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00022A01 File Offset: 0x00020C01
		public Instruction HandlerEnd
		{
			get
			{
				return this.handler_end;
			}
			set
			{
				this.handler_end = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00022A0A File Offset: 0x00020C0A
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00022A12 File Offset: 0x00020C12
		public TypeReference CatchType
		{
			get
			{
				return this.catch_type;
			}
			set
			{
				this.catch_type = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00022A1B File Offset: 0x00020C1B
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00022A23 File Offset: 0x00020C23
		public ExceptionHandlerType HandlerType
		{
			get
			{
				return this.handler_type;
			}
			set
			{
				this.handler_type = value;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00022A2C File Offset: 0x00020C2C
		public ExceptionHandler(ExceptionHandlerType handlerType)
		{
			this.handler_type = handlerType;
		}

		// Token: 0x0400053C RID: 1340
		private Instruction try_start;

		// Token: 0x0400053D RID: 1341
		private Instruction try_end;

		// Token: 0x0400053E RID: 1342
		private Instruction filter_start;

		// Token: 0x0400053F RID: 1343
		private Instruction handler_start;

		// Token: 0x04000540 RID: 1344
		private Instruction handler_end;

		// Token: 0x04000541 RID: 1345
		private TypeReference catch_type;

		// Token: 0x04000542 RID: 1346
		private ExceptionHandlerType handler_type;
	}
}
