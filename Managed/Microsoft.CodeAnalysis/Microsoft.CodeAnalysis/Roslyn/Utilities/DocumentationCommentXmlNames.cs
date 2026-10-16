using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DocumentationCommentXmlNames
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004C87 File Offset: 0x00002E87
		public static bool ElementEquals(string name1, string name2, bool fromVb = false)
		{
			return string.Equals(name1, name2, fromVb ? 4 : 5);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004C97 File Offset: 0x00002E97
		public static bool AttributeEquals(string name1, string name2)
		{
			return string.Equals(name1, name2, 4);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004CA1 File Offset: 0x00002EA1
		public static bool Equals(object left, object right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0400003E RID: 62
		public const string CElementName = "c";

		// Token: 0x0400003F RID: 63
		public const string CodeElementName = "code";

		// Token: 0x04000040 RID: 64
		public const string CompletionListElementName = "completionlist";

		// Token: 0x04000041 RID: 65
		public const string DescriptionElementName = "description";

		// Token: 0x04000042 RID: 66
		public const string ExampleElementName = "example";

		// Token: 0x04000043 RID: 67
		public const string ExceptionElementName = "exception";

		// Token: 0x04000044 RID: 68
		public const string IncludeElementName = "include";

		// Token: 0x04000045 RID: 69
		public const string InheritdocElementName = "inheritdoc";

		// Token: 0x04000046 RID: 70
		public const string ItemElementName = "item";

		// Token: 0x04000047 RID: 71
		public const string ListElementName = "list";

		// Token: 0x04000048 RID: 72
		public const string ListHeaderElementName = "listheader";

		// Token: 0x04000049 RID: 73
		public const string ParaElementName = "para";

		// Token: 0x0400004A RID: 74
		public const string ParameterElementName = "param";

		// Token: 0x0400004B RID: 75
		public const string ParameterReferenceElementName = "paramref";

		// Token: 0x0400004C RID: 76
		public const string PermissionElementName = "permission";

		// Token: 0x0400004D RID: 77
		public const string PlaceholderElementName = "placeholder";

		// Token: 0x0400004E RID: 78
		public const string PreliminaryElementName = "preliminary";

		// Token: 0x0400004F RID: 79
		public const string RemarksElementName = "remarks";

		// Token: 0x04000050 RID: 80
		public const string ReturnsElementName = "returns";

		// Token: 0x04000051 RID: 81
		public const string SeeElementName = "see";

		// Token: 0x04000052 RID: 82
		public const string SeeAlsoElementName = "seealso";

		// Token: 0x04000053 RID: 83
		public const string SummaryElementName = "summary";

		// Token: 0x04000054 RID: 84
		public const string TermElementName = "term";

		// Token: 0x04000055 RID: 85
		public const string ThreadSafetyElementName = "threadsafety";

		// Token: 0x04000056 RID: 86
		public const string TypeParameterElementName = "typeparam";

		// Token: 0x04000057 RID: 87
		public const string TypeParameterReferenceElementName = "typeparamref";

		// Token: 0x04000058 RID: 88
		public const string ValueElementName = "value";

		// Token: 0x04000059 RID: 89
		public const string CrefAttributeName = "cref";

		// Token: 0x0400005A RID: 90
		public const string HrefAttributeName = "href";

		// Token: 0x0400005B RID: 91
		public const string FileAttributeName = "file";

		// Token: 0x0400005C RID: 92
		public const string InstanceAttributeName = "instance";

		// Token: 0x0400005D RID: 93
		public const string LangwordAttributeName = "langword";

		// Token: 0x0400005E RID: 94
		public const string NameAttributeName = "name";

		// Token: 0x0400005F RID: 95
		public const string PathAttributeName = "path";

		// Token: 0x04000060 RID: 96
		public const string StaticAttributeName = "static";

		// Token: 0x04000061 RID: 97
		public const string TypeAttributeName = "type";
	}
}
