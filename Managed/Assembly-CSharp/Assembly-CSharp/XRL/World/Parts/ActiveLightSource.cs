using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, light radius is increased by the standard
	/// power load bonus, i.e. 2 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02000FEA RID: 4074
	[Serializable]
	public class ActiveLightSource : IActivePart, ILightSource
	{
		// Token: 0x0600A258 RID: 41560 RVA: 0x003B5454 File Offset: 0x003B3654
		public ActiveLightSource()
		{
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A259 RID: 41561 RVA: 0x003B5474 File Offset: 0x003B3674
		public override bool SameAs(IPart p)
		{
			ActiveLightSource activeLightSource = p as ActiveLightSource;
			return activeLightSource.Darkvision == this.Darkvision && activeLightSource.Radius == this.Radius && activeLightSource.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A25A RID: 41562 RVA: 0x003B54BF File Offset: 0x003B36BF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A25B RID: 41563 RVA: 0x003B54DD File Offset: 0x003B36DD
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			this.RadiateLight();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A25C RID: 41564 RVA: 0x003B54F0 File Offset: 0x003B36F0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				List<string> list = new List<string>();
				bool flag = false;
				if (!this.WorksOnSelf)
				{
					if (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnImplantee)
					{
						list.Add("equipped");
					}
					else
					{
						flag = true;
					}
				}
				if (this.ChargeUse > 0 || this.ChargeMinimum > 0)
				{
					list.Add("powered");
				}
				if (flag || !string.IsNullOrEmpty(this.NeedsOtherActivePartOperational) || !string.IsNullOrEmpty(this.NeedsOtherActivePartEngaged))
				{
					list.Add("in use");
				}
				stringBuilder.Append((list.Count > 0) ? "When" : "While").Append(' ').Append((list.Count > 0) ? Grammar.MakeAndList(list, true) : "operational")
					.Append(", provides ")
					.Append(this.Darkvision ? "night vision" : "light");
				if (this.GetEffectiveRadius() != 9999)
				{
					stringBuilder.Append(" in radius ").Append(this.GetEffectiveRadius()).Append('.');
				}
				E.Postfix.AppendRules(stringBuilder.ToString());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A25D RID: 41565 RVA: 0x003B5639 File Offset: 0x003B3839
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A25E RID: 41566 RVA: 0x003B563C File Offset: 0x003B383C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, true, null);
			}
		}

		// Token: 0x0600A25F RID: 41567 RVA: 0x003B5677 File Offset: 0x003B3877
		public int GetEffectiveRadius()
		{
			if (this.Radius == 9999)
			{
				return this.Radius;
			}
			return this.Radius + this.MyPowerLoadBonus(int.MinValue, 100, 150);
		}

		// Token: 0x0600A260 RID: 41568 RVA: 0x003B56A8 File Offset: 0x003B38A8
		public bool RadiateLight()
		{
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return false;
			}
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			int effectiveRadius = this.GetEffectiveRadius();
			if (this.Darkvision)
			{
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null && equipped.IsPlayer())
				{
					if (effectiveRadius == 9999)
					{
						currentCell.ParentZone.LightAll(LightLevel.Darkvision);
					}
					else
					{
						currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, effectiveRadius, LightLevel.Darkvision, false);
					}
				}
			}
			else if (effectiveRadius == 9999)
			{
				currentCell.ParentZone.LightAll(LightLevel.Light);
			}
			else
			{
				currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, effectiveRadius, LightLevel.Light, false);
			}
			return true;
		}

		// Token: 0x0600A261 RID: 41569 RVA: 0x003B5785 File Offset: 0x003B3985
		int ILightSource.GetRadius()
		{
			return this.Radius;
		}

		// Token: 0x0600A262 RID: 41570 RVA: 0x003B5790 File Offset: 0x003B3990
		bool ILightSource.IsActive()
		{
			return base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
		}

		// Token: 0x0600A263 RID: 41571 RVA: 0x003B57C3 File Offset: 0x003B39C3
		bool ILightSource.IsDarkvision()
		{
			return this.Darkvision;
		}

		// Token: 0x0400434B RID: 17227
		public bool Darkvision;

		// Token: 0x0400434C RID: 17228
		public int Radius = 5;

		// Token: 0x0400434D RID: 17229
		public bool ShowInShortDescription = true;
	}
}
