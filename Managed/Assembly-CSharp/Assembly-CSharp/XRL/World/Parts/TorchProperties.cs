using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010B7 RID: 4279
	[Serializable]
	public class TorchProperties : IPart
	{
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600AA72 RID: 43634 RVA: 0x003E5864 File Offset: 0x003E3A64
		private LightSource pLight
		{
			get
			{
				LightSource lightSource;
				if ((lightSource = this._pLight) == null)
				{
					lightSource = (this._pLight = this.ParentObject.GetPart<LightSource>());
				}
				return lightSource;
			}
		}

		// Token: 0x0600AA73 RID: 43635 RVA: 0x003E588F File Offset: 0x003E3A8F
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			GameObject.Validate(ref this.LastThrower);
			base.Write(Basis, Writer);
		}

		// Token: 0x0600AA74 RID: 43636 RVA: 0x003E58A8 File Offset: 0x003E3AA8
		public override bool SameAs(IPart p)
		{
			TorchProperties torchProperties = p as TorchProperties;
			return torchProperties.Fuel == this.Fuel && torchProperties.ChangeColorString == this.ChangeColorString && torchProperties.ChangeDetailColor == this.ChangeDetailColor && torchProperties.LiquidExtinguishes == this.LiquidExtinguishes && base.SameAs(p);
		}

		// Token: 0x0600AA75 RID: 43637 RVA: 0x003E5904 File Offset: 0x003E3B04
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == AfterThrownEvent.ID || (ID == EffectAppliedEvent.ID && this.LiquidExtinguishes) || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || (ID == EnteredCellEvent.ID && this.LiquidExtinguishes) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == SingletonEvent<RadiatesHeatEvent>.ID || ID == SuspendingEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AA76 RID: 43638 RVA: 0x003E598D File Offset: 0x003E3B8D
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.Extinguish();
			this.InHand = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA77 RID: 43639 RVA: 0x003E59A3 File Offset: 0x003E3BA3
		public override bool HandleEvent(AfterThrownEvent E)
		{
			this.LastThrower = E.Actor;
			this.InHand = false;
			if (!this.IsUnlightableBecauseOfSubmersion(null) && !this.IsUnlightableBecauseOfLiquidCovering(null))
			{
				this.Light();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA78 RID: 43640 RVA: 0x003E59D7 File Offset: 0x003E3BD7
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.LiquidExtinguishes)
			{
				LightSource pLight = this.pLight;
				if (pLight != null && pLight.Lit && this.IsUnlightableBecauseOfSubmersion(E.Cell))
				{
					this.Extinguish();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA79 RID: 43641 RVA: 0x003E5A10 File Offset: 0x003E3C10
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.LiquidExtinguishes)
			{
				LiquidCovered liquidCovered = E.Effect as LiquidCovered;
				if (liquidCovered != null)
				{
					LightSource pLight = this.pLight;
					if (pLight != null && pLight.Lit && this.IsUnlightableBecauseOfLiquidCovering(liquidCovered))
					{
						this.Extinguish();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA7A RID: 43642 RVA: 0x003E5A5E File Offset: 0x003E3C5E
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return !this.pLight.Lit && base.HandleEvent(E);
		}

		// Token: 0x0600AA7B RID: 43643 RVA: 0x003E5A78 File Offset: 0x003E3C78
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.ParentObject.HasPart<Flame>())
			{
				if (this.pLight.Lit)
				{
					E.AddAdjective("lit", -40);
					E.AddTag("(" + this.getLitDescription() + ")", -40);
				}
				else
				{
					E.AddTag("(" + this.getUnlitDescription() + ")", -40);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA7C RID: 43644 RVA: 0x003E5AEF File Offset: 0x003E3CEF
		public override bool HandleEvent(SuspendingEvent E)
		{
			this.LastThrower = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA7D RID: 43645 RVA: 0x003E5B00 File Offset: 0x003E3D00
		public override bool HandleEvent(EquippedEvent E)
		{
			if (E.Part != null && E.Part.Type == "Hand")
			{
				if (!this.IsUnlightableBecauseOfLiquidCovering(null))
				{
					this.Light();
				}
				this.InHand = true;
			}
			else
			{
				this.Extinguish();
				this.InHand = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA7E RID: 43646 RVA: 0x003E5B58 File Offset: 0x003E3D58
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.InHand = false;
			this.Extinguish();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA7F RID: 43647 RVA: 0x003E5B70 File Offset: 0x003E3D70
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.LastThrower);
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && equipped.OnWorldMap())
			{
				return true;
			}
			if (Options.AutoTorch && this.InHand)
			{
				if (this.pLight.Lit)
				{
					if (equipped != null && equipped.IsPlayer() && equipped.IsUnderSky() && base.IsDay())
					{
						this.Extinguish();
					}
				}
				else if (equipped != null && equipped.IsPlayer())
				{
					if (equipped.IsUnderSky() && base.IsNight())
					{
						if (!this.IsUnlightableBecauseOfLiquidCovering(null))
						{
							this.Light();
						}
					}
					else if (equipped.CurrentZone != null && equipped.CurrentZone.Z > 10 && !this.IsUnlightableBecauseOfLiquidCovering(null))
					{
						this.Light();
					}
				}
			}
			if (this.pLight.Lit)
			{
				if (this.Fuel > 0)
				{
					this.Fuel--;
				}
				if (this.Fuel <= 0)
				{
					this.Extinguish();
					if (equipped != null && equipped.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Your torch burns out!", null, true);
						AutoAct.Interrupt(null, null, null, false);
					}
					this.ParentObject.Destroy(null, true, false, null);
				}
				else
				{
					if (this.ParentObject.Physics.CurrentCell != null)
					{
						int phase = this.ParentObject.GetPhase();
						foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsWithPartReadonly("Physics"))
						{
							if (gameObject != this.ParentObject)
							{
								if (this.ParentObject.Physics.IsReal)
								{
									if (gameObject.Physics.Temperature < 400 && Stat.Random(1, 4) == 1)
									{
										gameObject.TemperatureChange(400, this.LastThrower, false, false, false, false, phase, null, null);
									}
								}
								else if (gameObject.Physics.Temperature < 400 && Stat.Random(1, 4) == 1)
								{
									gameObject.TemperatureChange(100, this.LastThrower, false, false, false, false, phase, null, null);
								}
							}
						}
					}
					if (this.ParentObject.Physics.IsReal && this.ParentObject.CurrentCell != null && 8.in100())
					{
						this.ParentObject.Smoke();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA80 RID: 43648 RVA: 0x003E5E00 File Offset: 0x003E4000
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.InHand && this.ParentObject.Equipped != null)
			{
				if (this.pLight.Lit)
				{
					E.AddAction("Extinguish", "extinguish", "TorchExtinguish", null, 'x', false, 0, 0, false, false, true, false, true, null, false);
				}
				else
				{
					E.AddAction("Light", "light", "TorchLight", null, 'i', false, 0, 0, false, false, true, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA81 RID: 43649 RVA: 0x003E5E80 File Offset: 0x003E4080
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "TorchLight")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (this.IsUnlightableBecauseOfLiquidCovering(null))
				{
					return E.Actor.Fail(string.Concat(new string[]
					{
						"You cannot light ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" because of the liquid ",
						this.ParentObject.itis,
						" covered in."
					}));
				}
				if (this.IsUnlightableBecauseOfSubmersion(null))
				{
					return E.Actor.Fail(string.Concat(new string[]
					{
						"You cannot light ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" because of the liquid ",
						this.ParentObject.itis,
						" submerged in."
					}));
				}
				E.Actor.UseEnergy(1000);
				this.Light();
			}
			else if (E.Command == "TorchExtinguish")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				E.Actor.UseEnergy(1000);
				this.Extinguish();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA82 RID: 43650 RVA: 0x003E5FF8 File Offset: 0x003E41F8
		public void Light()
		{
			if (this.Fuel > 0)
			{
				base.PlayWorldSound(this.LightSound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				this.pLight.Lit = true;
				this.ParentObject.Render.Tile = "Items/sw_torch_lit.png";
				this.ParentObject.Render.DetailColor = "W";
			}
		}

		// Token: 0x0600AA83 RID: 43651 RVA: 0x003E6070 File Offset: 0x003E4270
		private string getLitDescription()
		{
			if (this.Fuel > 3500)
			{
				return "{{Y|blazing}}";
			}
			if (this.Fuel > 2000)
			{
				return "{{W|bright}}";
			}
			if (this.Fuel > 500)
			{
				return "{{w|dim}}";
			}
			return "{{r|smouldering}}";
		}

		// Token: 0x0600AA84 RID: 43652 RVA: 0x003E60B0 File Offset: 0x003E42B0
		private string getUnlitDescription()
		{
			if (this.Fuel > 3500)
			{
				return "unburnt";
			}
			if (this.Fuel > 2000)
			{
				return "half-burnt";
			}
			if (this.Fuel > 500)
			{
				return "mostly burnt";
			}
			return "nearly extinguished";
		}

		// Token: 0x0600AA85 RID: 43653 RVA: 0x003E60F0 File Offset: 0x003E42F0
		public void Extinguish()
		{
			base.PlayWorldSound(this.ExtinguishSound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			this.pLight.Lit = false;
			this.ParentObject.Render.Tile = "Items/sw_torch.png";
			this.ParentObject.Render.DetailColor = "r";
		}

		// Token: 0x0600AA86 RID: 43654 RVA: 0x003E6160 File Offset: 0x003E4360
		public override bool Render(RenderEvent E)
		{
			if ((this.ChangeColorString || this.ChangeDetailColor) && this.pLight.Lit)
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += Stat.Random(1, 5);
				}
				char c = 'W';
				if (num < 15)
				{
					c = 'R';
				}
				else if (num >= 30 && num < 45)
				{
					c = 'r';
				}
				if (this.ChangeColorString || (this.ChangeDetailColor && !Options.UseTiles))
				{
					E.ColorString = "&" + c.ToString();
				}
				if (this.ChangeDetailColor)
				{
					E.DetailColor = c.ToString() ?? "";
				}
			}
			return true;
		}

		// Token: 0x0600AA87 RID: 43655 RVA: 0x003E6222 File Offset: 0x003E4422
		public bool IsUnlightableBecauseOfSubmersion(Cell Cell = null)
		{
			if (!this.LiquidExtinguishes)
			{
				return false;
			}
			if (Cell == null)
			{
				Cell = this.ParentObject.CurrentCell;
			}
			return Cell != null && Cell.HasWadingDepthLiquid();
		}

		// Token: 0x0600AA88 RID: 43656 RVA: 0x003E624C File Offset: 0x003E444C
		public bool IsUnlightableBecauseOfLiquidCovering(LiquidCovered FX = null)
		{
			if (!this.LiquidExtinguishes)
			{
				return false;
			}
			if (FX == null)
			{
				FX = this.ParentObject.GetEffect<LiquidCovered>();
			}
			return ((FX != null) ? FX.Liquid : null) != null && FX.Liquid.GetLiquidCombustibility() < 30 && FX.Liquid.GetLiquidTemperature() < 100;
		}

		// Token: 0x0400463A RID: 17978
		public int Fuel = 5000;

		// Token: 0x0400463B RID: 17979
		public bool InHand;

		// Token: 0x0400463C RID: 17980
		public GameObject LastThrower;

		// Token: 0x0400463D RID: 17981
		public string LightSound;

		// Token: 0x0400463E RID: 17982
		public string ExtinguishSound;

		// Token: 0x0400463F RID: 17983
		public int FrameOffset;

		// Token: 0x04004640 RID: 17984
		public bool ChangeColorString;

		// Token: 0x04004641 RID: 17985
		public bool ChangeDetailColor;

		// Token: 0x04004642 RID: 17986
		public bool LiquidExtinguishes = true;

		// Token: 0x04004643 RID: 17987
		private LightSource _pLight;
	}
}
