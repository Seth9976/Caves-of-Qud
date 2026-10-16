using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200054B RID: 1355
	public abstract class RenderPipelineAsset<TRenderPipeline> : RenderPipelineAsset where TRenderPipeline : RenderPipeline
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x00065632 File Offset: 0x00063832
		public sealed override Type pipelineType
		{
			get
			{
				return typeof(TRenderPipeline);
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x0006563E File Offset: 0x0006383E
		public override string renderPipelineShaderTag
		{
			get
			{
				return typeof(TRenderPipeline).Name;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x00065632 File Offset: 0x00063832
		[Obsolete("This property is obsolete. Use pipelineType instead. #from(23.2)", false)]
		protected internal sealed override Type renderPipelineType
		{
			get
			{
				return typeof(TRenderPipeline);
			}
		}
	}
}
