using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000F4 RID: 244
	public sealed class AsyncMethodBodyDebugInformation : CustomDebugInformation
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000204C9 File Offset: 0x0001E6C9
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x000204D1 File Offset: 0x0001E6D1
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000204DC File Offset: 0x0001E6DC
		public Collection<InstructionOffset> Yields
		{
			get
			{
				Collection<InstructionOffset> collection;
				if ((collection = this.yields) == null)
				{
					collection = (this.yields = new Collection<InstructionOffset>());
				}
				return collection;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00020504 File Offset: 0x0001E704
		public Collection<InstructionOffset> Resumes
		{
			get
			{
				Collection<InstructionOffset> collection;
				if ((collection = this.resumes) == null)
				{
					collection = (this.resumes = new Collection<InstructionOffset>());
				}
				return collection;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00020529 File Offset: 0x0001E729
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00020531 File Offset: 0x0001E731
		public MethodDefinition MoveNextMethod
		{
			get
			{
				return this.move_next;
			}
			set
			{
				this.move_next = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002053A File Offset: 0x0001E73A
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.AsyncMethodBody;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002053D File Offset: 0x0001E73D
		internal AsyncMethodBodyDebugInformation(int catchHandler)
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(catchHandler);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00020556 File Offset: 0x0001E756
		public AsyncMethodBodyDebugInformation(Instruction catchHandler)
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(catchHandler);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002056F File Offset: 0x0001E76F
		public AsyncMethodBodyDebugInformation()
			: base(AsyncMethodBodyDebugInformation.KindIdentifier)
		{
			this.catch_handler = new InstructionOffset(-1);
		}

		// Token: 0x0400041D RID: 1053
		internal InstructionOffset catch_handler;

		// Token: 0x0400041E RID: 1054
		internal Collection<InstructionOffset> yields;

		// Token: 0x0400041F RID: 1055
		internal Collection<InstructionOffset> resumes;

		// Token: 0x04000420 RID: 1056
		internal MethodDefinition move_next;

		// Token: 0x04000421 RID: 1057
		public static Guid KindIdentifier = new Guid("{54FD2AC5-E925-401A-9C2A-F94F171072F8}");
	}
}
