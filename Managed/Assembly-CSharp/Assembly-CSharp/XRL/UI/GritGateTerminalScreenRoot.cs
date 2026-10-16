using System;
using ConsoleLib.Console;
using XRL.Core;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.ZoneParts;

namespace XRL.UI
{
	// Token: 0x02001499 RID: 5273
	public class GritGateTerminalScreenRoot : GritGateTerminalScreen
	{
		// Token: 0x0600CF28 RID: 53032 RVA: 0x004B6A1F File Offset: 0x004B4C1F
		public GritGateTerminalScreenRoot()
		{
			this.MainText = "Sensor access granted. The Thin World is open to you. What do you wish?";
			this.ClearOptions();
		}

		// Token: 0x0600CF29 RID: 53033 RVA: 0x004B6A38 File Offset: 0x004B4C38
		public override void BeforeRender(ScreenBuffer buffer, ref string footerText)
		{
			if (this.Options.Count == 0)
			{
				if (!XRLCore.Core.Game.HasGameState("GritGateAttackScanned"))
				{
					this.Options.Add("Scan surroundings, Ereshkigal.");
					this.optionActions.Add(delegate
					{
						XRLCore.Core.Game.SetIntGameState("GritGateAttackScanned", 1);
						ScriptCallToArms part = base.Terminal.Terminal.CurrentZone.GetPart<ScriptCallToArms>();
						if (part != null)
						{
							part.scan();
						}
						base.Terminal.CurrentScreen = new GritGateTerminalScreenMessage("{{y|Scanning..........................................\n..................................................\n..................................................\n[{{R|!!! {{y|Intruders detected}} !!!}}]\n..................................................\n[{{R|!!! {{y|I'm sounding alarms}} !!!}}]\n..................................................\n..................................................\n[{{R|!!! {{y|I'm calculating probable attack radii}} !!!}}]\n..................................................\n..................................................\n[{{R|!!! {{y|I'm displaying probable attack radii}} !!!}}]\n..................................................\n..................................................", this, delegate
						{
							this.UpdatePowerOptions();
						});
						this.ActionTaken = true;
					});
					this.Options.Add("Exit.");
					this.optionActions.Add(new Action(this.Exit));
				}
				else
				{
					this.UpdatePowerOptions();
				}
				base.Update();
			}
			base.BeforeRender(buffer, ref footerText);
		}

		// Token: 0x0600CF2A RID: 53034 RVA: 0x004B6ACC File Offset: 0x004B4CCC
		public void UpdatePowerOptions()
		{
			this.ClearOptions();
			GameObject powerstation = base.Terminal.Terminal.CurrentCell.ParentZone.FindObject("GritGateBroadcastPowerStation");
			if (powerstation == null)
			{
				this.Options.Add("[{{R|!!! ERROR: RODANIS Y IS OFFLINE !!!}}]");
			}
			else if (!powerstation.GetPart<PowerSwitch>().Active)
			{
				this.Options.Add("Activate Rodanis Y. [{{R|-100 amps}}]");
				this.optionActions.Add(delegate
				{
					powerstation.FireEvent("PowerSwitchActivate");
					this.UpdatePowerOptions();
					this.ActionTaken = true;
				});
			}
			else
			{
				this.Options.Add("Deactivate Rodanis Y. [{{G|+100 amps}}]");
				this.optionActions.Add(delegate
				{
					powerstation.FireEvent("PowerSwitchDeactivate");
					this.UpdatePowerOptions();
					this.ActionTaken = true;
				});
			}
			GameObject chromebeacon = base.Terminal.Terminal.CurrentCell.ParentZone.FindObject("GritGateChromeBeacon");
			if (chromebeacon == null)
			{
				this.Options.Add("[{{R|!!! ERROR: CHROMELING BROADCAST BEACON IS OFFLINE !!!}}]");
				this.optionActions.Add(delegate
				{
				});
			}
			else
			{
				int currentlevel = chromebeacon.GetIntProperty("beaconlevel", 0);
				if (currentlevel < 10)
				{
					this.Options.Add("Overclock chromelings (+10 QN, current level: {{C|" + currentlevel.ToString() + "}}). [{{R|-22.5 amps}}]");
					this.optionActions.Add(delegate
					{
						chromebeacon.ModIntProperty("beaconlevel", 1, false);
						int currentlevel3 = currentlevel;
						currentlevel = currentlevel3 + 1;
						ZoneAdjust part = chromebeacon.GetPart<ZoneAdjust>();
						part.AdjustSpec = chromebeacon.GetTag("beaconadjuststring", null).Replace("{0}", (currentlevel * 10).ToString());
						part.ChargeUse = currentlevel * 225;
						part.ParentObject.UseCharge(1, false, 0L, true, true, null);
						this.UpdatePowerOptions();
						this.ActionTaken = true;
					});
				}
				if (currentlevel > 0)
				{
					this.Options.Add("Reduce clock on chromelings (-10 QN, current level: {{C|" + currentlevel.ToString() + "}}). [{{G|+22.5 amps}}]");
					this.optionActions.Add(delegate
					{
						foreach (GameObject gameObject in this.Terminal.Terminal.CurrentCell.ParentZone.GetObjects("Chromeling"))
						{
							gameObject.RemoveEffect<Adjusted>();
						}
						chromebeacon.ModIntProperty("beaconlevel", -1, false);
						int currentlevel2 = currentlevel;
						currentlevel = currentlevel2 - 1;
						ZoneAdjust part2 = chromebeacon.GetPart<ZoneAdjust>();
						part2.ChargeUse = currentlevel * 225;
						if (currentlevel > 0)
						{
							part2.AdjustSpec = chromebeacon.GetTag("beaconadjuststring", null).Replace("{0}", (currentlevel * 10).ToString());
							part2.ParentObject.UseCharge(1, false, 0L, true, true, null);
						}
						else
						{
							part2.AdjustSpec = null;
						}
						this.ActionTaken = true;
						this.UpdatePowerOptions();
					});
				}
			}
			this.Options.Add("Activate a chain laser emplacement. [{{R|-50 amps}}]");
			this.optionActions.Add(delegate
			{
				Popup.Show("[{{R|!!! ERROR: REMOTE MANAGEMENT OFFLINE !!!}}]\n[{{R|!!! CHAIN LASER EMPLACEMENTS MUST BE ACTIVATED MANUALLY !!!}}]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.ActionTaken = true;
				this.UpdatePowerOptions();
			});
			this.Options.Add("Activate a force projector. [{{R|-9 amps/square}}]");
			this.optionActions.Add(delegate
			{
				Popup.Show("[{{R|!!! ERROR: REMOTE MANAGEMENT OFFLINE !!!}}]\n[{{R|!!! FORCE PROJECTORS MUST BE ACTIVATED MANUALLY !!!}}]", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.UpdatePowerOptions();
			});
			this.Options.Add("Exit.");
			this.optionActions.Add(new Action(this.Exit));
			base.Update();
		}

		// Token: 0x0600CF2B RID: 53035 RVA: 0x004B6D28 File Offset: 0x004B4F28
		private void Exit()
		{
			base.Terminal.CurrentScreen = null;
			if (this.ActionTaken)
			{
				The.Player.UseEnergy(1000, "Terminal Interface", null, null, false);
			}
		}

		// Token: 0x0600CF2C RID: 53036 RVA: 0x004B6D69 File Offset: 0x004B4F69
		public override void Back()
		{
			this.Exit();
		}

		// Token: 0x0600CF2D RID: 53037 RVA: 0x004B6D71 File Offset: 0x004B4F71
		public override void Activate()
		{
			this.optionActions[base.Terminal.Selected]();
		}

		// Token: 0x0400506C RID: 20588
		public bool ActionTaken;
	}
}
