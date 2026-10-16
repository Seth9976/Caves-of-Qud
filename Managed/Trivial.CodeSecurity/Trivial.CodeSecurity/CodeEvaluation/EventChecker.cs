using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000014 RID: 20
	internal class EventChecker : CodeChecker<EventDefinition>, ICodeUsageProvider
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003F4D File Offset: 0x0000214D
		public EventChecker()
		{
			this.typeReferenceChecker = new TypeReferenceChecker(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F6C File Offset: 0x0000216C
		public override void SecurityCheckCode(CodeSecurityContext context, EventDefinition eventDefinition)
		{
			this.currentEvent = eventDefinition;
			this.typeReferenceChecker.SecurityCheckCode(context, eventDefinition.EventType);
			base.SecurityCheckAndReportMemberReference(context, eventDefinition, this);
			if (eventDefinition.AddMethod != null)
			{
				this.accessorChecker.SecurityCheckCode(context, eventDefinition.AddMethod);
			}
			if (eventDefinition.RemoveMethod != null)
			{
				this.accessorChecker.SecurityCheckCode(context, eventDefinition.RemoveMethod);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FCF File Offset: 0x000021CF
		public IllegalReferenceUsage GetIllegalUsage()
		{
			return new IllegalReferenceUsage(this.currentEvent);
		}

		// Token: 0x04000040 RID: 64
		private EventDefinition currentEvent;

		// Token: 0x04000041 RID: 65
		private TypeReferenceChecker typeReferenceChecker;

		// Token: 0x04000042 RID: 66
		private MethodChecker accessorChecker = new MethodChecker();
	}
}
