using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class CodeSecurityRestrictionsEntry
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B96 File Offset: 0x00000D96
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002B9E File Offset: 0x00000D9E
		public CodeSecurityRestrictions.CodeSecurityBehaviour DefaultBehaviour
		{
			get
			{
				return this.defaultBehaviour;
			}
			set
			{
				this.defaultBehaviour = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public IList<string> AllowEntries
		{
			get
			{
				return this.allowEntries;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002BAF File Offset: 0x00000DAF
		public IList<string> DenyEntries
		{
			get
			{
				return this.denyEntries;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public int RestrictionsHash
		{
			get
			{
				int num = 16205 ^ this.defaultBehaviour.GetHashCode();
				int num2 = num | (num ^ this.allowEntries.GetHashCode());
				return num2 & (num2 ^ this.denyEntries.GetHashCode());
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void AddNestedEntryName(string typeName, string entryName, CodeSecurityRestrictions.CodeSecurityBehaviour behaviour)
		{
			this.AddEntryName(string.Format("{0}.{1}", typeName, entryName), behaviour);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C01 File Offset: 0x00000E01
		public void AddEntryName(string entryName, CodeSecurityRestrictions.CodeSecurityBehaviour behaviour)
		{
			if (behaviour == CodeSecurityRestrictions.CodeSecurityBehaviour.Allow)
			{
				if (!this.allowEntries.Contains(entryName))
				{
					this.allowEntries.Add(entryName);
					return;
				}
			}
			else if (behaviour == CodeSecurityRestrictions.CodeSecurityBehaviour.Deny && !this.denyEntries.Contains(entryName))
			{
				this.denyEntries.Add(entryName);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C40 File Offset: 0x00000E40
		public bool IsAllowable(string entryName, bool ignoreCase, bool allowWildcard = false)
		{
			if (allowWildcard)
			{
				foreach (string text in this.allowEntries)
				{
					if (text.EndsWith(".*"))
					{
						if (string.Compare(text.Remove(text.Length - 2, 2), entryName, ignoreCase) == 0)
						{
							return true;
						}
					}
					else if (string.Compare(text, entryName, ignoreCase) == 0)
					{
						return true;
					}
				}
				using (List<string>.Enumerator enumerator = this.denyEntries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text2 = enumerator.Current;
						if (text2.EndsWith(".*"))
						{
							if (string.Compare(text2.Remove(text2.Length - 2, 2), entryName, ignoreCase) == 0)
							{
								return false;
							}
						}
						else if (string.Compare(text2, entryName, ignoreCase) == 0)
						{
							return false;
						}
					}
					goto IL_0102;
				}
			}
			if (Enumerable.Contains<string>(this.allowEntries, entryName, StringComparer.OrdinalIgnoreCase))
			{
				return true;
			}
			if (Enumerable.Contains<string>(this.denyEntries, entryName, StringComparer.OrdinalIgnoreCase))
			{
				return false;
			}
			IL_0102:
			return this.defaultBehaviour == CodeSecurityRestrictions.CodeSecurityBehaviour.Allow;
		}

		// Token: 0x0400001F RID: 31
		[SerializeField]
		[HideInInspector]
		private CodeSecurityRestrictions.CodeSecurityBehaviour defaultBehaviour;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		[HideInInspector]
		private List<string> allowEntries = new List<string>();

		// Token: 0x04000021 RID: 33
		[SerializeField]
		[HideInInspector]
		private List<string> denyEntries = new List<string>();
	}
}
