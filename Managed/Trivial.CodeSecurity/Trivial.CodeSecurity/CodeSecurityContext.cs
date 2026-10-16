using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000002 RID: 2
	internal class CodeSecurityContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AssemblyDefinition CheckAssembly
		{
			get
			{
				return this.checkAssembly;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public CodeSecurityReport Report
		{
			get
			{
				return this.report;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public ICodeSecurityValidator Validator
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public CodeSecurityContext(AssemblyDefinition checkAssembly, CodeSecurityReport report, ICodeSecurityValidator validator)
		{
			this.checkAssembly = checkAssembly;
			this.report = report;
			this.validator = validator;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A8 File Offset: 0x000002A8
		public bool IsNamespaceReferenceAllowedCached(string potentialIllegalNamespace)
		{
			if (potentialIllegalNamespace == null)
			{
				return true;
			}
			bool flag = false;
			if (this.namespaceReferenceCache.TryGetValue(potentialIllegalNamespace, ref flag))
			{
				return flag;
			}
			flag = this.validator.IsNamespaceReferenceAllowed(potentialIllegalNamespace);
			this.namespaceReferenceCache.Add(potentialIllegalNamespace, flag);
			return flag;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		public bool IsTypeReferenceAllowedCached(TypeReference potentialIllegalType)
		{
			if (potentialIllegalType == null)
			{
				return true;
			}
			bool flag = false;
			if (this.typeReferenceCache.TryGetValue(potentialIllegalType, ref flag))
			{
				return flag;
			}
			flag = this.validator.IsTypeReferenceAllowed(potentialIllegalType);
			this.typeReferenceCache.Add(potentialIllegalType, flag);
			return flag;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		public bool IsMemberReferenceAllowedCached(MemberReference potentialIllegalMember)
		{
			if (potentialIllegalMember == null)
			{
				return true;
			}
			bool flag = false;
			if (this.memberReferenceCache.TryGetValue(potentialIllegalMember, ref flag))
			{
				return flag;
			}
			flag = this.validator.IsMemberReferenceAllowed(potentialIllegalMember);
			this.memberReferenceCache.Add(potentialIllegalMember, flag);
			return flag;
		}

		// Token: 0x04000001 RID: 1
		private AssemblyDefinition checkAssembly;

		// Token: 0x04000002 RID: 2
		private CodeSecurityReport report;

		// Token: 0x04000003 RID: 3
		private ICodeSecurityValidator validator;

		// Token: 0x04000004 RID: 4
		private Dictionary<string, bool> namespaceReferenceCache = new Dictionary<string, bool>();

		// Token: 0x04000005 RID: 5
		private Dictionary<TypeReference, bool> typeReferenceCache = new Dictionary<TypeReference, bool>();

		// Token: 0x04000006 RID: 6
		private Dictionary<MemberReference, bool> memberReferenceCache = new Dictionary<MemberReference, bool>();
	}
}
