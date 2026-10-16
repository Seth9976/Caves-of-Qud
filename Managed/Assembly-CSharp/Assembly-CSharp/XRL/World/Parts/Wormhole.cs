using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012A6 RID: 4774
	[Serializable]
	public class Wormhole : IPart
	{
		// Token: 0x0600BC95 RID: 48277 RVA: 0x0044F0DF File Offset: 0x0044D2DF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BC96 RID: 48278 RVA: 0x0044F0E2 File Offset: 0x0044D2E2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("ObjectEnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC97 RID: 48279 RVA: 0x0044F104 File Offset: 0x0044D304
		public override bool Render(RenderEvent E)
		{
			if (!this.bSeen)
			{
				if (this.ParentObject.CurrentCell != null && this.ParentObject.CurrentCell.IsVisible())
				{
					this.ParentObject.DilationSplat();
					this.bSeen = true;
				}
				return true;
			}
			if (Stat.RandomCosmetic(1, 60) < 3)
			{
				string text = "&C";
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&W";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&R";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&B";
				}
				XRLCore.ParticleManager.AddRadial(text + "ù", (float)this.ParentObject.CurrentCell.X, (float)this.ParentObject.CurrentCell.Y, (float)Stat.RandomCosmetic(0, 7), (float)Stat.RandomCosmetic(5, 10), 0.01f * (float)Stat.RandomCosmetic(4, 6), -0.01f * (float)Stat.RandomCosmetic(3, 7));
			}
			int num = Stat.RandomCosmetic(0, 4);
			if (num == 0)
			{
				E.ColorString = "&B^k";
			}
			else if (num == 1)
			{
				E.ColorString = "&R^k";
			}
			else if (num == 2)
			{
				E.ColorString = "&C^k";
			}
			else if (num == 3)
			{
				E.ColorString = "&W^k";
			}
			else if (num == 4)
			{
				E.ColorString = "&K^k";
			}
			int num2 = Stat.RandomCosmetic(0, 3);
			if (num2 == 0)
			{
				E.RenderString = "\t";
			}
			else if (num2 == 1)
			{
				E.RenderString = "é";
			}
			else if (num2 == 2)
			{
				E.RenderString = "\u0015";
			}
			else if (num2 == 3)
			{
				E.RenderString = "\u000f";
			}
			return true;
		}

		// Token: 0x0600BC98 RID: 48280 RVA: 0x0044F298 File Offset: 0x0044D498
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectEnteredCell")
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_spacetimeVortex_interact_in", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			else if (E.ID == "EnteredCell")
			{
				GameObject parentObject2 = this.ParentObject;
				if (parentObject2 != null)
				{
					parentObject2.PlayWorldSound("Sounds/Abilities/sfx_ability_spacetimeVortex_interact_out", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B97 RID: 19351
		public bool bSeen;
	}
}
