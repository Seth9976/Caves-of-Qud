using System;
using Trivial.Mono.Cecil;
using UnityEngine;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class CodeSecurityRestrictions : ICodeSecurityValidator
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A17 File Offset: 0x00000C17
		public CodeSecurityRestrictionsEntry AssemblyReferences
		{
			get
			{
				return this.assemblyReferences;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A1F File Offset: 0x00000C1F
		public CodeSecurityRestrictionsEntry NamespaceReferences
		{
			get
			{
				return this.namespaceReferences;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A27 File Offset: 0x00000C27
		public CodeSecurityRestrictionsEntry TypeReferences
		{
			get
			{
				return this.typeReferences;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A2F File Offset: 0x00000C2F
		public CodeSecurityRestrictionsEntry MemberReferences
		{
			get
			{
				return this.memberReferences;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A37 File Offset: 0x00000C37
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002A3F File Offset: 0x00000C3F
		public bool IgnoreCase
		{
			get
			{
				return this.ignoreCase;
			}
			set
			{
				this.ignoreCase = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A48 File Offset: 0x00000C48
		public int RestrictionsHash
		{
			get
			{
				int num = 6779;
				int num2 = num + (num ^ this.assemblyReferences.RestrictionsHash);
				int num3 = num2 * (num2 ^ this.namespaceReferences.RestrictionsHash);
				int num4 = num3 / (num3 ^ this.typeReferences.RestrictionsHash);
				int num5 = num4 - (num4 ^ this.memberReferences.RestrictionsHash);
				return num5 | (num5 ^ this.ignoreCase.GetHashCode());
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public bool IsAssemblyReferenceAllowed(string assemblyReference)
		{
			return this.assemblyReferences.IsAllowable(assemblyReference, this.ignoreCase, false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public bool IsNamespaceReferenceAllowed(string namespaceName)
		{
			return this.namespaceReferences.IsAllowable(namespaceName, this.ignoreCase, true);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002ACA File Offset: 0x00000CCA
		public bool IsTypeReferenceAllowed(TypeReference reference)
		{
			return this.typeReferences.IsAllowable(reference.FullName, this.ignoreCase, false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public bool IsMemberReferenceAllowed(MemberReference reference)
		{
			string text = reference.Name;
			if (text.StartsWith("get_"))
			{
				text = text.Remove(0, 4);
			}
			else if (text.StartsWith("set_"))
			{
				text = text.Remove(0, 4);
			}
			string text2 = string.Format("{0}.{1}", reference.DeclaringType.FullName, text);
			return this.memberReferences.IsAllowable(text2, this.ignoreCase, false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B50 File Offset: 0x00000D50
		public static string SystemToCecilTypeString(string fullTypeName)
		{
			return fullTypeName.Replace("+", "/");
		}

		// Token: 0x0400001A RID: 26
		[SerializeField]
		[HideInInspector]
		private CodeSecurityRestrictionsEntry assemblyReferences = new CodeSecurityRestrictionsEntry();

		// Token: 0x0400001B RID: 27
		[SerializeField]
		[HideInInspector]
		private CodeSecurityRestrictionsEntry namespaceReferences = new CodeSecurityRestrictionsEntry();

		// Token: 0x0400001C RID: 28
		[SerializeField]
		[HideInInspector]
		private CodeSecurityRestrictionsEntry typeReferences = new CodeSecurityRestrictionsEntry();

		// Token: 0x0400001D RID: 29
		[SerializeField]
		[HideInInspector]
		public CodeSecurityRestrictionsEntry memberReferences = new CodeSecurityRestrictionsEntry();

		// Token: 0x0400001E RID: 30
		[SerializeField]
		[HideInInspector]
		private bool ignoreCase;

		// Token: 0x02000020 RID: 32
		[Serializable]
		public enum CodeSecurityBehaviour
		{
			// Token: 0x04000070 RID: 112
			Allow,
			// Token: 0x04000071 RID: 113
			Deny
		}
	}
}
