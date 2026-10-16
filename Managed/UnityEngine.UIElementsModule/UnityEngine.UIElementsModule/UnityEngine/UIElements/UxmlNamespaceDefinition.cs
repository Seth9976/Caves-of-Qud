using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000581 RID: 1409
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct UxmlNamespaceDefinition : IEquatable<UxmlNamespaceDefinition>
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000984A0 File Offset: 0x000966A0
		public static UxmlNamespaceDefinition Empty { get; } = default(UxmlNamespaceDefinition);

		// Token: 0x06002A46 RID: 10822 RVA: 0x000984A8 File Offset: 0x000966A8
		public string Export()
		{
			bool flag = string.IsNullOrEmpty(this.prefix);
			string text;
			if (flag)
			{
				text = "xmlns=\"" + this.resolvedNamespace + "\"";
			}
			else
			{
				text = string.Concat(new string[] { "xmlns:", this.prefix, "=\"", this.resolvedNamespace, "\"" });
			}
			return text;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00098518 File Offset: 0x00096718
		public static bool operator ==(UxmlNamespaceDefinition lhs, UxmlNamespaceDefinition rhs)
		{
			return string.Compare(lhs.prefix, rhs.prefix, StringComparison.Ordinal) == 0 && string.Compare(lhs.resolvedNamespace, rhs.resolvedNamespace, StringComparison.Ordinal) == 0;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00098558 File Offset: 0x00096758
		public static bool operator !=(UxmlNamespaceDefinition lhs, UxmlNamespaceDefinition rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00098574 File Offset: 0x00096774
		public bool Equals(UxmlNamespaceDefinition other)
		{
			return this == other;
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00098594 File Offset: 0x00096794
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is UxmlNamespaceDefinition)
			{
				UxmlNamespaceDefinition uxmlNamespaceDefinition = (UxmlNamespaceDefinition)obj;
				flag = this.Equals(uxmlNamespaceDefinition);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000985C0 File Offset: 0x000967C0
		public override int GetHashCode()
		{
			return HashCode.Combine<string, string>(this.prefix, this.resolvedNamespace);
		}

		// Token: 0x040012F9 RID: 4857
		public string prefix;

		// Token: 0x040012FA RID: 4858
		public string resolvedNamespace;
	}
}
