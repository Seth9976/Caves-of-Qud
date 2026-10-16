using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200012D RID: 301
	public sealed class StateMachineScopeDebugInformation : CustomDebugInformation
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00025940 File Offset: 0x00023B40
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00025948 File Offset: 0x00023B48
		public InstructionOffset Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00025951 File Offset: 0x00023B51
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00025959 File Offset: 0x00023B59
		public InstructionOffset End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.StateMachineScope;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00025962 File Offset: 0x00023B62
		internal StateMachineScopeDebugInformation(int start, int end)
			: base(StateMachineScopeDebugInformation.KindIdentifier)
		{
			this.start = new InstructionOffset(start);
			this.end = new InstructionOffset(end);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00025988 File Offset: 0x00023B88
		public StateMachineScopeDebugInformation(Instruction start, Instruction end)
			: base(StateMachineScopeDebugInformation.KindIdentifier)
		{
			this.start = new InstructionOffset(start);
			this.end = ((end != null) ? new InstructionOffset(end) : default(InstructionOffset));
		}

		// Token: 0x040006CB RID: 1739
		internal InstructionOffset start;

		// Token: 0x040006CC RID: 1740
		internal InstructionOffset end;

		// Token: 0x040006CD RID: 1741
		public static Guid KindIdentifier = new Guid("{6DA9A61E-F8C7-4874-BE62-68BC5630DF71}");
	}
}
