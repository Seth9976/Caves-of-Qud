using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000013 RID: 19
	internal abstract class CodeChecker<T> where T : MemberReference
	{
		// Token: 0x06000094 RID: 148
		public abstract void SecurityCheckCode(CodeSecurityContext context, T definition);

		// Token: 0x06000095 RID: 149 RVA: 0x00003E7C File Offset: 0x0000207C
		protected void SecurityCheckAndReportTypeReferenceAndNamespace(CodeSecurityContext context, TypeReference reference, ICodeUsageProvider usageProvider)
		{
			if (this.CheckForIllegalNamespaceReference(context, reference.Namespace))
			{
				this.ReportIllegalNamespaceReferenceOccurence(context, reference.Namespace, reference, usageProvider.GetIllegalUsage());
			}
			if (this.CheckForIllegalTypeReference(context, reference))
			{
				this.ReportIllegalTypeReferenceOccurence(context, reference, usageProvider.GetIllegalUsage());
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003EBC File Offset: 0x000020BC
		protected void SecurityCheckAndReportMemberReference(CodeSecurityContext context, MemberReference reference, ICodeUsageProvider usageProvider)
		{
			if (this.CheckForIllegalMemberReference(context, reference))
			{
				bool flag = this.CheckForIllegalTypeReference(context, reference.DeclaringType);
				this.ReportIllegalMemberReferenceOccurence(context, reference, usageProvider.GetIllegalUsage(), flag);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003EF0 File Offset: 0x000020F0
		protected bool CheckForIllegalNamespaceReference(CodeSecurityContext context, string potentialIllegalNamespace)
		{
			return !context.IsNamespaceReferenceAllowedCached(potentialIllegalNamespace);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003EFC File Offset: 0x000020FC
		protected bool CheckForIllegalTypeReference(CodeSecurityContext context, TypeReference potentialIllegalType)
		{
			return !context.IsTypeReferenceAllowedCached(potentialIllegalType);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003F08 File Offset: 0x00002108
		protected bool CheckForIllegalMemberReference(CodeSecurityContext context, MemberReference potentialIllegalMember)
		{
			return !context.IsMemberReferenceAllowedCached(potentialIllegalMember);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003F14 File Offset: 0x00002114
		protected void ReportIllegalNamespaceReferenceOccurence(CodeSecurityContext context, string illegalNamespace, TypeReference indirectIllegalTypeReference, IllegalReferenceUsage illegalReferenceUsage)
		{
			context.Report.AddIllegalNamespaceReferenceOccurence(illegalNamespace, indirectIllegalTypeReference, illegalReferenceUsage);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F25 File Offset: 0x00002125
		protected void ReportIllegalTypeReferenceOccurence(CodeSecurityContext context, TypeReference illegalType, IllegalReferenceUsage illegalReferenceUsage)
		{
			context.Report.AddIllegalTypeReferenceOccurence(illegalType, illegalReferenceUsage);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F34 File Offset: 0x00002134
		protected void ReportIllegalMemberReferenceOccurence(CodeSecurityContext context, MemberReference illegalMember, IllegalReferenceUsage illegalReferenceUsage, bool indirect)
		{
			context.Report.AddIllegalMemberReferenceOccurence(illegalMember, illegalReferenceUsage, indirect);
		}
	}
}
