using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E6 RID: 742
	internal sealed class StateMachineScopeDebugInformation : CustomDebugInformation
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public Collection<StateMachineScope> Scopes
		{
			get
			{
				Collection<StateMachineScope> collection;
				if ((collection = this.scopes) == null)
				{
					collection = (this.scopes = new Collection<StateMachineScope>());
				}
				return collection;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00018941 File Offset: 0x00016B41
		public override CustomDebugInformationKind Kind
		{
			get
			{
				return CustomDebugInformationKind.StateMachineScope;
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003CD41 File Offset: 0x0003AF41
		public StateMachineScopeDebugInformation()
			: base(StateMachineScopeDebugInformation.KindIdentifier)
		{
		}

		// Token: 0x04000993 RID: 2451
		internal Collection<StateMachineScope> scopes;

		// Token: 0x04000994 RID: 2452
		public static Guid KindIdentifier = new Guid("{6DA9A61E-F8C7-4874-BE62-68BC5630DF71}");
	}
}
