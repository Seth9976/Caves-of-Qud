using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000553 RID: 1363
	public struct ScopedSubPass : IDisposable
	{
		// Token: 0x06003446 RID: 13382 RVA: 0x000668BC File Offset: 0x00064ABC
		internal ScopedSubPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000668C8 File Offset: 0x00064AC8
		public void Dispose()
		{
			try
			{
				this.m_Context.EndSubPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedSubPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x040011A1 RID: 4513
		private ScriptableRenderContext m_Context;
	}
}
