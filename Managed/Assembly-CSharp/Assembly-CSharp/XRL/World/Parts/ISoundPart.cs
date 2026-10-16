using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200123C RID: 4668
	[Serializable]
	public abstract class ISoundPart : IPart
	{
		// Token: 0x0600B9A6 RID: 47526 RVA: 0x0043CAE0 File Offset: 0x0043ACE0
		public virtual void Trigger()
		{
			if (this.Sounds != null)
			{
				this.Played = true;
				if (this.SoundOptions == null)
				{
					this.SoundOptions = this.Sounds.Split(',', StringSplitOptions.None);
				}
				base.PlayWorldSound(this.SoundOptions[Stat.Random5(0, this.SoundOptions.Length - 1)], this.Volume, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x04004AA4 RID: 19108
		[NonSerialized]
		public string[] SoundOptions;

		// Token: 0x04004AA5 RID: 19109
		public string Sounds;

		// Token: 0x04004AA6 RID: 19110
		public bool Played;

		// Token: 0x04004AA7 RID: 19111
		public float Volume = 1f;
	}
}
