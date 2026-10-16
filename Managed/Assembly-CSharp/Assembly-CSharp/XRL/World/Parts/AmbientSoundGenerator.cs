using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200123B RID: 4667
	[Serializable]
	public class AmbientSoundGenerator : IPart
	{
		// Token: 0x0600B9A2 RID: 47522 RVA: 0x0043C9E3 File Offset: 0x0043ABE3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B9A3 RID: 47523 RVA: 0x0043C9E6 File Offset: 0x0043ABE6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B9A4 RID: 47524 RVA: 0x0043C9FC File Offset: 0x0043ABFC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction" && !string.IsNullOrEmpty(this.Sounds) && !this.ParentObject.IsPlayerLed())
			{
				if (this.SoundOptions == null)
				{
					this.SoundOptions = this.Sounds.Split(',', StringSplitOptions.None);
				}
				if (string.IsNullOrEmpty(XRLCore.Core.PlayerWalking) && Stat.Random(1, 1000) <= this.ChancePerThousand)
				{
					base.PlayWorldSound(this.SoundOptions[Stat.Random5(0, this.SoundOptions.Length - 1)], this.Volume, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004AA0 RID: 19104
		[NonSerialized]
		public string[] SoundOptions;

		// Token: 0x04004AA1 RID: 19105
		public string Sounds;

		// Token: 0x04004AA2 RID: 19106
		public int ChancePerThousand = 150;

		// Token: 0x04004AA3 RID: 19107
		public float Volume = 0.25f;
	}
}
