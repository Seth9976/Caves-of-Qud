using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200014F RID: 335
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class ScriptCompilationInfo
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002741A File Offset: 0x0002561A
		internal Type ReturnTypeOpt { get; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00027422 File Offset: 0x00025622
		[Nullable(1)]
		public Type ReturnType
		{
			[NullableContext(1)]
			get
			{
				return this.ReturnTypeOpt ?? typeof(object);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00027438 File Offset: 0x00025638
		public Type GlobalsType { get; }

		// Token: 0x06000D57 RID: 3415 RVA: 0x00027440 File Offset: 0x00025640
		internal ScriptCompilationInfo(Type returnType, Type globalsType)
		{
			this.ReturnTypeOpt = returnType;
			this.GlobalsType = globalsType;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00027456 File Offset: 0x00025656
		public Compilation PreviousScriptCompilation
		{
			get
			{
				return this.CommonPreviousScriptCompilation;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D59 RID: 3417
		internal abstract Compilation CommonPreviousScriptCompilation { get; }

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002745E File Offset: 0x0002565E
		[NullableContext(1)]
		public ScriptCompilationInfo WithPreviousScriptCompilation([Nullable(2)] Compilation compilation)
		{
			return this.CommonWithPreviousScriptCompilation(compilation);
		}

		// Token: 0x06000D5B RID: 3419
		[NullableContext(1)]
		internal abstract ScriptCompilationInfo CommonWithPreviousScriptCompilation([Nullable(2)] Compilation compilation);
	}
}
