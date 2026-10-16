using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000552 RID: 1362
	public struct ScopedRenderPass : IDisposable
	{
		// Token: 0x06003444 RID: 13380 RVA: 0x00066875 File Offset: 0x00064A75
		internal ScopedRenderPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x00066880 File Offset: 0x00064A80
		public void Dispose()
		{
			try
			{
				this.m_Context.EndRenderPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedRenderPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x040011A0 RID: 4512
		private ScriptableRenderContext m_Context;
	}
}
