using System;
using XRL.Collections;

namespace XRL.Sound
{
	// Token: 0x020005D5 RID: 1493
	public class MusicSourceCollection : StringMap<MusicSource>
	{
		// Token: 0x06004028 RID: 16424 RVA: 0x0013AFC0 File Offset: 0x001391C0
		public void Update()
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					this.Slots[i].Value.Update();
				}
			}
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x0013B008 File Offset: 0x00139208
		public void SetMusicBackground(bool State)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					this.Slots[i].Value.SetMusicBackground(State);
				}
			}
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x0013B050 File Offset: 0x00139250
		public async void StopFadeAsync()
		{
			await The.UiContext;
			this.StopFade();
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0013B088 File Offset: 0x00139288
		public void StopFade()
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					this.Slots[i].Value.StopFade();
				}
			}
		}
	}
}
