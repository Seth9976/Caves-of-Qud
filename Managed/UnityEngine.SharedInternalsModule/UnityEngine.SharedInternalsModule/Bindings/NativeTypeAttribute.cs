using System;

namespace UnityEngine.Bindings
{
	// Token: 0x0200001A RID: 26
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	internal class NativeTypeAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute, IBindingsGenerateMarshallingTypeAttribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000023C7 File Offset: 0x000005C7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000023CF File Offset: 0x000005CF
		public string Header { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000023E0 File Offset: 0x000005E0
		public string IntermediateScriptingStructName { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000023E9 File Offset: 0x000005E9
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000023F1 File Offset: 0x000005F1
		public CodegenOptions CodegenOptions { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x000023FA File Offset: 0x000005FA
		public NativeTypeAttribute()
		{
			this.CodegenOptions = CodegenOptions.Auto;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000240C File Offset: 0x0000060C
		public NativeTypeAttribute(CodegenOptions codegenOptions)
		{
			this.CodegenOptions = codegenOptions;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002420 File Offset: 0x00000620
		public NativeTypeAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.CodegenOptions = CodegenOptions.Auto;
			this.Header = header;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002477 File Offset: 0x00000677
		public NativeTypeAttribute(string header, CodegenOptions codegenOptions)
			: this(header)
		{
			this.CodegenOptions = codegenOptions;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000248A File Offset: 0x0000068A
		public NativeTypeAttribute(CodegenOptions codegenOptions, string intermediateStructName)
			: this(codegenOptions)
		{
			this.IntermediateScriptingStructName = intermediateStructName;
		}
	}
}
