using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C43 RID: 3139
	[Serializable]
	public class Burning : SoundRendererEffect
	{
		// Token: 0x06008018 RID: 32792 RVA: 0x002F2EA6 File Offset: 0x002F10A6
		public Burning()
		{
			this.DisplayName = "{{R|burning}}";
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x002F2EB9 File Offset: 0x002F10B9
		public Burning(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x002F2EC8 File Offset: 0x002F10C8
		public override string GetDescription()
		{
			if (base.Object == null || !base.Object.IsPlayer())
			{
				return base.GetDescription();
			}
			return null;
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x002F2EE7 File Offset: 0x002F10E7
		public override int GetEffectType()
		{
			return 33554944;
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x002F2EEE File Offset: 0x002F10EE
		public override string GetStateDescription()
		{
			return "{{R|on fire}}";
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x002F2EF5 File Offset: 0x002F10F5
		public static bool CanApply(GameObject Object)
		{
			return CanApplyEffectEvent.Check<Burning>(Object, 0);
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x002F2F03 File Offset: 0x002F1103
		public static bool ApplyTo(GameObject Object)
		{
			return Burning.CanApply(Object) && !Object.HasEffect<Burning>() && Object.ApplyEffect(new Burning(1), null);
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x002F2F28 File Offset: 0x002F1128
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("sfx_statusEffect_ignited", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (AutoAct.IsActive(false))
			{
				if (Object.IsPlayer())
				{
					AutoAct.Interrupt("you caught fire", null, null, false);
				}
				else if (Object.IsPlayerLedAndPerceptible() && !Object.IsTrifling)
				{
					AutoAct.Interrupt(string.Concat(new string[]
					{
						"you ",
						Object.GetPerceptionVerb(),
						" ",
						Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false),
						" catch fire",
						Object.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(Object, false, false))
					}), null, Object, true);
				}
			}
			return base.Apply(Object);
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x002F3028 File Offset: 0x002F1228
		public override void Remove(GameObject Object)
		{
			Object.PlayWorldSound("sfx_statusEffect_fireExtinguished", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x002F305F File Offset: 0x002F125F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == BeforeRenderEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x002F3085 File Offset: 0x002F1285
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			base.AddLight(3, LightLevel.Light, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x002F309B File Offset: 0x002F129B
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("on fire", 50);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x002F30C0 File Offset: 0x002F12C0
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject @object = base.Object;
			Physics physics = @object.Physics;
			if (!physics.IsAflame())
			{
				@object.RemoveEffect(this, true);
			}
			else
			{
				GameObject object2 = base.Object;
				int num = Burning.GetBurningAmount(@object).RollCached();
				string text = "from the fire%S!";
				string text2 = "Fire";
				string text3 = null;
				string text4 = null;
				bool flag = physics.AmbientTemperature >= physics.FlameTemperature || RadiatesHeatEvent.Check(physics.CurrentCell) || RadiatesHeatAdjacentEvent.Check(physics.CurrentCell);
				object2.TakeDamage(num, text, text2, text3, text4, physics.InflamedBy, null, null, null, null, true, flag, true, false, false, false, true, false, false, 5, null);
				int num2;
				if (!@object.GetTag("HotBurn", null).IsNullOrEmpty() && int.TryParse(@object.GetTag("HotBurn", null), out num2))
				{
					physics.Temperature += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x002F3190 File Offset: 0x002F1390
		public static string GetBurningAmount(GameObject Object)
		{
			if (!Object.IsValid())
			{
				return "1";
			}
			int num = Object.Physics.Temperature - Object.Physics.FlameTemperature;
			string text;
			if (num <= 300)
			{
				if (num >= 0 && num > 100)
				{
					text = "1-2";
				}
				else
				{
					text = "1";
				}
			}
			else if (num <= 700)
			{
				if (num > 500)
				{
					text = "3-4";
				}
				else
				{
					text = "2-3";
				}
			}
			else if (num > 900)
			{
				text = "5-6";
			}
			else
			{
				text = "4-5";
			}
			return text;
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x002F3221 File Offset: 0x002F1421
		public override string GetDetails()
		{
			return Burning.GetBurningAmount(base.Object) + " damage per turn.";
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x002F3238 File Offset: 0x002F1438
		public override bool Render(RenderEvent E)
		{
			int num = Stat.RandomCosmetic(1, 4);
			if (base.Object.HasEffect(typeof(CoatedInPlasma)))
			{
				if (num == 1)
				{
					E.ColorString = "&G";
					E.BackgroundString = "^g";
				}
				else if (num == 2)
				{
					E.ColorString = "&g";
					E.BackgroundString = "^Y";
				}
				else if (num == 3)
				{
					E.ColorString = "&G";
					E.BackgroundString = "^k";
				}
				else if (num == 4)
				{
					E.ColorString = "&Y";
					E.BackgroundString = "^k";
				}
			}
			else if (num == 1)
			{
				E.ColorString = "&R";
				E.BackgroundString = "^W";
			}
			else if (num == 2)
			{
				E.ColorString = "&r";
				E.BackgroundString = "^W";
			}
			else if (num == 3)
			{
				E.ColorString = "&R";
				E.BackgroundString = "^k";
			}
			else if (num == 4)
			{
				E.ColorString = "&Y";
				E.BackgroundString = "^k";
			}
			if (Stat.RandomCosmetic(1, 20) == 1)
			{
				base.Object.Smoke();
			}
			return true;
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x002F3368 File Offset: 0x002F1568
		public override void Render(RenderSoundEvent E)
		{
			if (Options.UseFireSounds && base.Object.Physics.IsReal)
			{
				if (this.FlameSoundID == null)
				{
					this.FlameSoundID = Guid.NewGuid().ToString();
				}
				E.Char.soundExtra.Add(this.FlameSoundID, "sfx_statusEffect_onFire_lp", 1f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x040038EC RID: 14572
		[NonSerialized]
		private string FlameSoundID;
	}
}
