using System;
using System.Diagnostics;
using Kobold;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class exButton : MonoBehaviour
{
	// Token: 0x06000E59 RID: 3673 RVA: 0x0005C447 File Offset: 0x0005A647
	private void Start()
	{
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0005C44C File Offset: 0x0005A64C
	public void Press()
	{
		try
		{
			if (this.BlinkTime == null)
			{
				SpriteManager.SetSprite(base.gameObject, this.Clicked);
				this.BlinkTime = new Stopwatch();
				this.BlinkTime.Start();
			}
		}
		catch (Exception ex)
		{
			global::UnityEngine.Debug.LogError(ex.ToString());
		}
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0005C4A8 File Offset: 0x0005A6A8
	private void Update()
	{
		if (this.BlinkTime != null && (double)this.BlinkTime.ElapsedMilliseconds > this.PulseDuration * 1000.0)
		{
			SpriteManager.SetSprite(base.gameObject, this.Normal);
			this.BlinkTime = null;
		}
	}

	// Token: 0x04000A01 RID: 2561
	public string Normal;

	// Token: 0x04000A02 RID: 2562
	public string Clicked;

	// Token: 0x04000A03 RID: 2563
	public double PulseDuration = 0.5;

	// Token: 0x04000A04 RID: 2564
	public Stopwatch BlinkTime;
}
