using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E4 RID: 740
	internal sealed class AsyncMethodBodyDebugInformation : CustomDebugInformation
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0003CBC3 File Offset: 0x0003ADC3
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x0003CBCB File Offset: 0x0003ADCB
		public InstructionOffset CatchHandler
		{
			get
			{
				return this.catch_handler;
			}
			set
			{
				this.catch_handler = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		public Collection<InstructionOffset> Yields
		{
			get
			{
				if (this.yields == null)
				{
					Interlocked.CompareExchange<Collection<InstructionOffset>>(ref this.yields, new Collection<InstructionOffset>(), null);
				}
				return this.yields;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x0003CBF6 File Offset: 0x0003ADF6
		public Collection<InstructionOffset> Resumes
		{
			get
			{
				if (this.resumes == null)
				{
					Interlocked.CompareExchange<Collection<InstructionOffset>>(ref this.resumes, new Collection<InstructionOffset>(), null);
				}
				return this.resumes;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0003CC18 File Offset: 0x0003AE18
		public Collection<MethodDefinition> ResumeMethods
		{
			get
			{
				Collection<MethodDefinition> collection;
				if ((collection = this.resume_methods) == null)
				{
					collection = (this.resume_methods = new Collection<MethodDefinition>());
				}
				return collection;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0003CC3D File Offset: 0x0003AE3D
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.AsyncMethodBody;
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003CC40 File Offset: 0x0003AE40
		internal AsyncMethodBodyDebugInformation(int catchHandler)
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(catchHandler);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0003CC59 File Offset: 0x0003AE59
		public AsyncMethodBodyDebugInformation(Instruction catchHandler)
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(catchHandler);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003CC72 File Offset: 0x0003AE72
		public AsyncMethodBodyDebugInformation()
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(-1);
		}

		// Token: 0x0400098C RID: 2444
		internal InstructionOffset catch_handler;

		// Token: 0x0400098D RID: 2445
		internal Collection<InstructionOffset> yields;

		// Token: 0x0400098E RID: 2446
		internal Collection<InstructionOffset> resumes;

		// Token: 0x0400098F RID: 2447
		internal Collection<MethodDefinition> resume_methods;

		// Token: 0x04000990 RID: 2448
		public static Guid KindIdentifier = new Guid("{54FD2AC5-E925-401A-9C2A-F94F171072F8}");
	}
}
