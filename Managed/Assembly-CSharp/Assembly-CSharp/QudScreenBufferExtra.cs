using System;
using ConsoleLib.Console;
using Genkit;

// Token: 0x02000062 RID: 98
public class QudScreenBufferExtra : IScreenBufferExtra
{
	// Token: 0x0600035C RID: 860 RVA: 0x000169C7 File Offset: 0x00014BC7
	public QudScreenBufferExtra setPlayerPosition(Point2D pos)
	{
		this.playerPosition = pos;
		return this;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x000169D1 File Offset: 0x00014BD1
	public void Clear()
	{
		this.playerPosition = Point2D.invalid;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x000169DE File Offset: 0x00014BDE
	public void Free()
	{
		ImposterManager.freeExtra(this);
	}

	// Token: 0x040002A9 RID: 681
	public Point2D playerPosition = Point2D.invalid;
}
