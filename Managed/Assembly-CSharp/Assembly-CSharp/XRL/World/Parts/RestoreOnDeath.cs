using System;
using System.Text;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001091 RID: 4241
	[Serializable]
	public class RestoreOnDeath : IPart
	{
		// Token: 0x0600A8BE RID: 43198 RVA: 0x003DBCCC File Offset: 0x003D9ECC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDieEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A8BF RID: 43199 RVA: 0x003DBCEC File Offset: 0x003D9EEC
		public override bool HandleEvent(BeforeDieEvent E)
		{
			if (this.CurrentCooldown <= 0)
			{
				this.CurrentCooldown = this.Cooldown;
				if (base.Visible())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.Show("Just before your demise, your health is restored!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						base.DidX("swim", "before your eyes", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
				}
				float num = (float)this.ParentObject.GetStat("Hitpoints").BaseValue * ((float)this.Health / 100f);
				this.ParentObject.Heal((int)num, true, true, false);
				if (this.Amount != -1)
				{
					int num2 = this.Amount - 1;
					this.Amount = num2;
					if (num2 <= 0)
					{
						this.ParentObject.RemovePart(this);
					}
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8C0 RID: 43200 RVA: 0x003DBDCD File Offset: 0x003D9FCD
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8C1 RID: 43201 RVA: 0x003DBDF0 File Offset: 0x003D9FF0
		public void AppendEffect(StringBuilder SB)
		{
			SB.Append("Respawns ");
			if (this.Amount > 0)
			{
				SB.Append(Grammar.Multiplicative(this.Amount)).Append(' ');
			}
			SB.Append("at ").Append(this.Health).Append("% health");
		}

		// Token: 0x0600A8C2 RID: 43202 RVA: 0x003DBE4C File Offset: 0x003DA04C
		public override bool WantTurnTick()
		{
			return this.Cooldown > 0;
		}

		// Token: 0x0600A8C3 RID: 43203 RVA: 0x003DBE57 File Offset: 0x003DA057
		public override void TurnTick(long TimeTick, int Amount1)
		{
			if (this.CurrentCooldown > 0)
			{
				this.CurrentCooldown--;
			}
		}

		// Token: 0x040045BF RID: 17855
		public int Cooldown;

		// Token: 0x040045C0 RID: 17856
		public int Health = 100;

		// Token: 0x040045C1 RID: 17857
		public int Amount = -1;

		// Token: 0x040045C2 RID: 17858
		public int CurrentCooldown;
	}
}
