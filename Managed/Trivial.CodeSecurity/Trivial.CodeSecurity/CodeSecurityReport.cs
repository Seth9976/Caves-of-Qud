using System;
using System.Collections.Generic;
using System.Text;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000005 RID: 5
	public class CodeSecurityReport
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025C2 File Offset: 0x000007C2
		public AssemblyDefinition CheckAssembly
		{
			get
			{
				return this.checkAssembly;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025CA File Offset: 0x000007CA
		public CodeSecurityRestrictions RestrictionsUsed
		{
			get
			{
				return this.restrictionsUsed;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025D2 File Offset: 0x000007D2
		public ICollection<IllegalAssemblyReference> IllegalAssemblyReferences
		{
			get
			{
				return this.illegalAssemblyReferences;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025DA File Offset: 0x000007DA
		public ICollection<IllegalNamespaceReference> IllegalNamespaceReferences
		{
			get
			{
				return this.illegalNamespaceReferences;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025E2 File Offset: 0x000007E2
		public ICollection<IllegalTypeReference> IllegalTypeReferences
		{
			get
			{
				return this.illegalTypeReferences;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025EA File Offset: 0x000007EA
		public ICollection<IllegalMemberReference> IllegalMemberReferences
		{
			get
			{
				return this.illegalMemberReferences;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025F2 File Offset: 0x000007F2
		public bool IsAssemblyReferencesSecurityVerified
		{
			get
			{
				return this.illegalAssemblyReferences.Count == 0;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002602 File Offset: 0x00000802
		public bool IsNamespaceReferencesSecurityVerified
		{
			get
			{
				return this.illegalNamespaceReferences.Count == 0;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002612 File Offset: 0x00000812
		public bool IsTypeReferencesSecurityVerified
		{
			get
			{
				return this.illegalTypeReferences.Count == 0;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002622 File Offset: 0x00000822
		public bool IsMemberReferencesSecurityVerified
		{
			get
			{
				return this.illegalMemberReferences.Count == 0;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002632 File Offset: 0x00000832
		public bool IsSecurityVerified
		{
			get
			{
				return this.illegalAssemblyReferences.Count == 0 && this.illegalNamespaceReferences.Count == 0 && this.illegalTypeReferences.Count == 0 && this.illegalMemberReferences.Count == 0;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000266C File Offset: 0x0000086C
		internal CodeSecurityReport(AssemblyDefinition definition, CodeSecurityRestrictions restrictions)
		{
			this.checkAssembly = definition;
			this.restrictionsUsed = restrictions;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026B9 File Offset: 0x000008B9
		public IEnumerable<CodeSecurityReportEntry> GetAllEntries(bool reportAllOccurences)
		{
			if (!this.IsAssemblyReferencesSecurityVerified)
			{
				foreach (IllegalAssemblyReference assemblyReference in this.illegalAssemblyReferences)
				{
					yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalAssembly, CodeSecurityLocation.defaultLocation, assemblyReference.ToString());
					if (reportAllOccurences)
					{
						yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalOccurence, CodeSecurityLocation.defaultLocation, assemblyReference.IllegalUsage.ToString());
					}
					assemblyReference = null;
				}
				List<IllegalAssemblyReference>.Enumerator enumerator = default(List<IllegalAssemblyReference>.Enumerator);
			}
			if (!this.IsNamespaceReferencesSecurityVerified)
			{
				foreach (IllegalNamespaceReference namespaceReference in this.illegalNamespaceReferences)
				{
					yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalNamespace, CodeSecurityLocation.defaultLocation, namespaceReference.ToString());
					if (reportAllOccurences)
					{
						foreach (IllegalReferenceUsage illegalReferenceUsage in namespaceReference.ReferenceUsages)
						{
							yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalOccurence, illegalReferenceUsage.GetUsageLocation(), illegalReferenceUsage.ToString());
						}
						IEnumerator<IllegalReferenceUsage> enumerator3 = null;
					}
					namespaceReference = null;
				}
				List<IllegalNamespaceReference>.Enumerator enumerator2 = default(List<IllegalNamespaceReference>.Enumerator);
			}
			if (!this.IsTypeReferencesSecurityVerified)
			{
				foreach (IllegalTypeReference typeReference in this.illegalTypeReferences)
				{
					yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalType, CodeSecurityLocation.defaultLocation, typeReference.ToString());
					if (reportAllOccurences)
					{
						foreach (IllegalReferenceUsage illegalReferenceUsage2 in typeReference.ReferenceUsages)
						{
							yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalOccurence, illegalReferenceUsage2.GetUsageLocation(), illegalReferenceUsage2.ToString());
						}
						IEnumerator<IllegalReferenceUsage> enumerator3 = null;
					}
					typeReference = null;
				}
				List<IllegalTypeReference>.Enumerator enumerator4 = default(List<IllegalTypeReference>.Enumerator);
			}
			if (!this.IsMemberReferencesSecurityVerified)
			{
				foreach (IllegalMemberReference memberReference in this.illegalMemberReferences)
				{
					yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalMember, CodeSecurityLocation.defaultLocation, memberReference.ToString());
					if (reportAllOccurences)
					{
						foreach (IllegalReferenceUsage illegalReferenceUsage3 in memberReference.ReferenceUsages)
						{
							yield return new CodeSecurityReportEntry(CodeSecurityReportEntry.ReportEntryType.IllegalOccurence, illegalReferenceUsage3.GetUsageLocation(), illegalReferenceUsage3.ToString());
						}
						IEnumerator<IllegalReferenceUsage> enumerator3 = null;
					}
					memberReference = null;
				}
				List<IllegalMemberReference>.Enumerator enumerator5 = default(List<IllegalMemberReference>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026D0 File Offset: 0x000008D0
		public string[] GetAllLines(bool reportAllOccurences)
		{
			List<string> list = new List<string>();
			foreach (CodeSecurityReportEntry codeSecurityReportEntry in this.GetAllEntries(reportAllOccurences))
			{
				if (codeSecurityReportEntry.EntryType == CodeSecurityReportEntry.ReportEntryType.IllegalOccurence && reportAllOccurences)
				{
					list.Add("\t" + codeSecurityReportEntry.Message);
				}
				else
				{
					list.Add(codeSecurityReportEntry.Message);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002758 File Offset: 0x00000958
		public string GetAllText(bool reportAllOccurences)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CodeSecurityReportEntry codeSecurityReportEntry in this.GetAllEntries(reportAllOccurences))
			{
				if (codeSecurityReportEntry.EntryType == CodeSecurityReportEntry.ReportEntryType.IllegalOccurence && reportAllOccurences)
				{
					stringBuilder.Append('\t');
					stringBuilder.AppendLine(codeSecurityReportEntry.Message);
				}
				else
				{
					stringBuilder.AppendLine(codeSecurityReportEntry.Message);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027E0 File Offset: 0x000009E0
		public string GetSummaryText()
		{
			if (!this.IsSecurityVerified)
			{
				return string.Format("Assembly '{0}' has failed code security verification. Illegal Assembly Reference = '{1}', Illegal Namespace References = '{2}', Illegal Type References = '{3}', Illegal Member References = '{4}'", new object[]
				{
					this.checkAssembly,
					this.illegalAssemblyReferences.Count,
					this.illegalNamespaceReferences.Count,
					this.illegalTypeReferences.Count,
					this.illegalMemberReferences.Count
				});
			}
			return string.Format("Assembly '{0}' has passed code security verification", this.checkAssembly);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000286B File Offset: 0x00000A6B
		internal void AddIllegalAssemblyReferenceOccurence(AssemblyNameReference assemblyReference, ModuleDefinition module)
		{
			this.illegalAssemblyReferences.Add(new IllegalAssemblyReference(assemblyReference, module));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002880 File Offset: 0x00000A80
		internal void AddIllegalNamespaceReferenceOccurence(string namespaceReference, TypeReference illegalIndirectType, IllegalReferenceUsage illegalReferenceUsage)
		{
			foreach (IllegalNamespaceReference illegalNamespaceReference in this.illegalNamespaceReferences)
			{
				if (illegalNamespaceReference.ReferencedNamespace == namespaceReference)
				{
					illegalNamespaceReference.AddIllegalNamespaceUsage(illegalIndirectType, illegalReferenceUsage);
					return;
				}
			}
			this.illegalNamespaceReferences.Add(new IllegalNamespaceReference(namespaceReference, illegalIndirectType, illegalReferenceUsage));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028F8 File Offset: 0x00000AF8
		internal void AddIllegalTypeReferenceOccurence(TypeReference typeReference, IllegalReferenceUsage illegalReferenceUsage)
		{
			foreach (IllegalTypeReference illegalTypeReference in this.illegalTypeReferences)
			{
				if (illegalTypeReference.ReferencedType == typeReference)
				{
					illegalTypeReference.AddIllegalReferenceUsage(illegalReferenceUsage);
					return;
				}
			}
			this.illegalTypeReferences.Add(new IllegalTypeReference(typeReference, illegalReferenceUsage, false));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000296C File Offset: 0x00000B6C
		internal void AddIllegalMemberReferenceOccurence(MemberReference memberReference, IllegalReferenceUsage illegalReferenceUsage, bool indirect)
		{
			foreach (IllegalMemberReference illegalMemberReference in this.illegalMemberReferences)
			{
				if (illegalMemberReference.ReferencedMember == memberReference)
				{
					illegalMemberReference.AddIllegalReferenceUsage(illegalReferenceUsage);
					return;
				}
			}
			this.illegalMemberReferences.Add(new IllegalMemberReference(memberReference, illegalReferenceUsage, indirect));
		}

		// Token: 0x04000011 RID: 17
		private AssemblyDefinition checkAssembly;

		// Token: 0x04000012 RID: 18
		private CodeSecurityRestrictions restrictionsUsed;

		// Token: 0x04000013 RID: 19
		private List<IllegalAssemblyReference> illegalAssemblyReferences = new List<IllegalAssemblyReference>();

		// Token: 0x04000014 RID: 20
		private List<IllegalNamespaceReference> illegalNamespaceReferences = new List<IllegalNamespaceReference>();

		// Token: 0x04000015 RID: 21
		private List<IllegalTypeReference> illegalTypeReferences = new List<IllegalTypeReference>();

		// Token: 0x04000016 RID: 22
		private List<IllegalMemberReference> illegalMemberReferences = new List<IllegalMemberReference>();
	}
}
