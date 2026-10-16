using System;

namespace XRL.World.Parts
{
	// Token: 0x02001247 RID: 4679
	[Serializable]
	public class StringPropertyChangeDuringEffect : IPart
	{
		// Token: 0x0600BA0D RID: 47629 RVA: 0x0043F344 File Offset: 0x0043D544
		public override bool SameAs(IPart p)
		{
			StringPropertyChangeDuringEffect stringPropertyChangeDuringEffect = p as StringPropertyChangeDuringEffect;
			return !(stringPropertyChangeDuringEffect.PropertyName != this.PropertyName) && !(stringPropertyChangeDuringEffect.EffectName != this.EffectName) && !(stringPropertyChangeDuringEffect.Value != this.Value) && stringPropertyChangeDuringEffect.TriggerRepaint == this.TriggerRepaint && stringPropertyChangeDuringEffect.Active == this.Active && stringPropertyChangeDuringEffect.HadProperty == this.HadProperty && !(stringPropertyChangeDuringEffect.OriginalValue != this.OriginalValue) && !(stringPropertyChangeDuringEffect.RemoveStringPropertyOnTransition != this.RemoveStringPropertyOnTransition) && base.SameAs(p);
		}

		// Token: 0x0600BA0E RID: 47630 RVA: 0x0043F3F8 File Offset: 0x0043D5F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x0600BA0F RID: 47631 RVA: 0x0043F416 File Offset: 0x0043D616
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.Sync();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA10 RID: 47632 RVA: 0x0043F425 File Offset: 0x0043D625
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.Sync();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA11 RID: 47633 RVA: 0x0043F434 File Offset: 0x0043D634
		public void Sync()
		{
			bool flag = false;
			if (this.Active)
			{
				if (!this.ParentObject.HasEffect(this.EffectName))
				{
					if (this.HadProperty)
					{
						this.ParentObject.SetStringProperty(this.PropertyName, this.OriginalValue, false);
					}
					else
					{
						this.ParentObject.RemoveStringProperty(this.PropertyName);
					}
					this.HadProperty = false;
					this.OriginalValue = null;
					this.Active = false;
					flag = true;
				}
			}
			else if (this.ParentObject.HasEffect(this.EffectName))
			{
				this.HadProperty = this.ParentObject.HasStringProperty(this.PropertyName);
				this.OriginalValue = (this.HadProperty ? this.ParentObject.GetStringProperty(this.PropertyName, null) : null);
				this.ParentObject.SetStringProperty(this.PropertyName, this.Value, false);
				this.Active = true;
				flag = true;
			}
			if (flag)
			{
				if (!this.RemoveStringPropertyOnTransition.IsNullOrEmpty())
				{
					this.ParentObject.RemoveStringProperty(this.RemoveStringPropertyOnTransition);
				}
				if (this.TriggerRepaint)
				{
					Zone currentZone = this.ParentObject.CurrentZone;
					if (currentZone != null && currentZone.Built)
					{
						this.ParentObject.CurrentCell.PaintWallsAround();
					}
				}
			}
		}

		// Token: 0x0600BA12 RID: 47634 RVA: 0x0043F56D File Offset: 0x0043D76D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004ABF RID: 19135
		public string PropertyName;

		// Token: 0x04004AC0 RID: 19136
		public string EffectName;

		// Token: 0x04004AC1 RID: 19137
		public string Value;

		// Token: 0x04004AC2 RID: 19138
		public bool TriggerRepaint;

		// Token: 0x04004AC3 RID: 19139
		public bool Active;

		// Token: 0x04004AC4 RID: 19140
		public bool HadProperty;

		// Token: 0x04004AC5 RID: 19141
		public string OriginalValue;

		// Token: 0x04004AC6 RID: 19142
		public string RemoveStringPropertyOnTransition;
	}
}
