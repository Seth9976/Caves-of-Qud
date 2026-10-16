using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL;
using XRL.Core;
using XRL.UI;
using XRL.World;

// Token: 0x02000061 RID: 97
public static class ImposterManager
{
	// Token: 0x06000358 RID: 856 RVA: 0x00016868 File Offset: 0x00014A68
	public static QudScreenBufferExtra getNewExtra()
	{
		Queue<QudScreenBufferExtra> queue = ImposterManager.extraPool;
		lock (queue)
		{
			while (ImposterManager.extraPool.Count > 0)
			{
				QudScreenBufferExtra qudScreenBufferExtra = ImposterManager.extraPool.Dequeue();
				if (qudScreenBufferExtra != null)
				{
					return qudScreenBufferExtra;
				}
			}
			for (int i = 0; i < 100; i++)
			{
				ImposterManager.extraPool.Enqueue(new QudScreenBufferExtra());
			}
		}
		return new QudScreenBufferExtra();
	}

	// Token: 0x06000359 RID: 857 RVA: 0x000168EC File Offset: 0x00014AEC
	public static void freeExtra(QudScreenBufferExtra extra)
	{
		try
		{
			Queue<QudScreenBufferExtra> queue = ImposterManager.extraPool;
			lock (queue)
			{
				extra.Clear();
				ImposterManager.extraPool.Enqueue(extra);
			}
		}
		catch (Exception ex)
		{
			ImposterManager.extraPool = new Queue<QudScreenBufferExtra>();
			MetricsManager.LogException("ImposterManager::freeExtra", ex, "game_exception");
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00016964 File Offset: 0x00014B64
	public static QudScreenBufferExtra getImposterUpdateFrame(ScreenBuffer Buffer)
	{
		if (Globals.RenderMode != RenderModeType.Text && !Options.DisableImposters)
		{
			ImposterManager.currentImposterFrame += 1L;
		}
		QudScreenBufferExtra newExtra = ImposterManager.getNewExtra();
		GameObject player = The.Player;
		Cell cell = ((player != null) ? player.CurrentCell : null);
		if (cell != null)
		{
			newExtra.playerPosition = cell.Pos2D;
		}
		return newExtra;
	}

	// Token: 0x040002A7 RID: 679
	private static Queue<QudScreenBufferExtra> extraPool = new Queue<QudScreenBufferExtra>();

	// Token: 0x040002A8 RID: 680
	public static long currentImposterFrame = 0L;
}
