using System;
using ConsoleLib.Console;
using Genkit;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200110F RID: 4367
	[Serializable]
	public class ExplodeAfterTurns : IPart
	{
		// Token: 0x0600AD0A RID: 44298 RVA: 0x003F8423 File Offset: 0x003F6623
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600AD0B RID: 44299 RVA: 0x003F8441 File Offset: 0x003F6641
		public override bool FinalRender(RenderEvent E)
		{
			E.WantsToPaint = true;
			return true;
		}

		// Token: 0x0600AD0C RID: 44300 RVA: 0x003F844C File Offset: 0x003F664C
		public override void OnPaint(ScreenBuffer buffer)
		{
			int num = XRLCore.CurrentFrame % 60;
			GameObject parentObject = this.ParentObject;
			Location2D location2D;
			if (parentObject == null)
			{
				location2D = null;
			}
			else
			{
				Cell currentCell = parentObject.CurrentCell;
				location2D = ((currentCell != null) ? currentCell.Location : null);
			}
			Location2D location2D2 = location2D;
			ConsoleChar consoleChar;
			if (num < 30)
			{
				int num2 = num / 10;
				if (location2D2 != null)
				{
					for (int i = -num2; i <= num2; i++)
					{
						for (int j = -num2; j <= num2; j++)
						{
							Location2D location2D3 = Location2D.Get(location2D2.X + i, location2D2.Y + j);
							if (location2D3 != null && location2D3.Distance(location2D2) == num2)
							{
								consoleChar = buffer.get(location2D3.X, location2D3.Y);
								if (consoleChar != null)
								{
									consoleChar.Tile = null;
									consoleChar.Char = '!';
									consoleChar.Foreground = The.Color.R;
								}
							}
						}
					}
				}
			}
			consoleChar = buffer.get(location2D2.X, location2D2.Y);
			if (consoleChar != null)
			{
				consoleChar.Tile = null;
				consoleChar.Char = 'X';
				consoleChar.Foreground = The.Color.R;
			}
		}

		// Token: 0x0600AD0D RID: 44301 RVA: 0x003F855F File Offset: 0x003F675F
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Turns--;
			if (this.Turns <= 0)
			{
				this.Detonate(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD0E RID: 44302 RVA: 0x003F858C File Offset: 0x003F678C
		public void Detonate(GameObject Owner = null)
		{
			if (!this.VFX.IsNullOrEmpty() && Options.UseParticleVFX)
			{
				Physics physics = this.ParentObject.Physics;
				bool? flag;
				if (physics == null)
				{
					flag = null;
				}
				else
				{
					Zone currentZone = physics.CurrentZone;
					flag = ((currentZone != null) ? new bool?(currentZone.IsActive()) : null);
				}
				bool? flag2 = flag;
				if (flag2.GetValueOrDefault())
				{
					CombatJuice.playPrefabAnimation(this.ParentObject.Physics.CurrentCell.Location, this.VFX, null, null, null, false);
					base.CombatJuiceWait(0.5f);
				}
			}
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			Zone currentZone2 = this.ParentObject.CurrentZone;
			GameObject gameObject;
			if (currentZone2 == null)
			{
				gameObject = null;
			}
			else
			{
				gameObject = currentZone2.FindObject((GameObject o) => o.HasTagOrProperty("HasVLS"));
			}
			Owner = gameObject;
			this.ParentObject.Explode(this.Force, Owner, this.Damage, 1f, false, false, false, 0, null);
		}

		// Token: 0x04004770 RID: 18288
		public int Turns = 2;

		// Token: 0x04004771 RID: 18289
		public int Force = 10000;

		// Token: 0x04004772 RID: 18290
		public string Damage = "0";

		// Token: 0x04004773 RID: 18291
		public string VFX = "MissileWeaponsEffects/vls_impact";
	}
}
