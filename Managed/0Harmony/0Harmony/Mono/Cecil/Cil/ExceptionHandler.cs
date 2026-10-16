using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BA RID: 698
	public sealed class ExceptionHandler
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00039545 File Offset: 0x00037745
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x0003954D File Offset: 0x0003774D
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00039556 File Offset: 0x00037756
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0003955E File Offset: 0x0003775E
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00039567 File Offset: 0x00037767
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0003956F File Offset: 0x0003776F
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00039578 File Offset: 0x00037778
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00039580 File Offset: 0x00037780
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00039589 File Offset: 0x00037789
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00039591 File Offset: 0x00037791
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

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0003959A File Offset: 0x0003779A
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x000395A2 File Offset: 0x000377A2
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

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x000395AB File Offset: 0x000377AB
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x000395B3 File Offset: 0x000377B3
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

		// Token: 0x060011A8 RID: 4520 RVA: 0x000395BC File Offset: 0x000377BC
		public ExceptionHandler(ExceptionHandlerType handlerType)
		{
			this.handler_type = handlerType;
		}

		// Token: 0x040007EE RID: 2030
		private Instruction try_start;

		// Token: 0x040007EF RID: 2031
		private Instruction try_end;

		// Token: 0x040007F0 RID: 2032
		private Instruction filter_start;

		// Token: 0x040007F1 RID: 2033
		private Instruction handler_start;

		// Token: 0x040007F2 RID: 2034
		private Instruction handler_end;

		// Token: 0x040007F3 RID: 2035
		private TypeReference catch_type;

		// Token: 0x040007F4 RID: 2036
		private ExceptionHandlerType handler_type;
	}
}
