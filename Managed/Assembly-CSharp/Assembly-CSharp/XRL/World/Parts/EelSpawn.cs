using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001269 RID: 4713
	[Serializable]
	public class EelSpawn : IPart
	{
		// Token: 0x0600BAD6 RID: 47830 RVA: 0x004435C0 File Offset: 0x004417C0
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600BAD7 RID: 47831 RVA: 0x004435C9 File Offset: 0x004417C9
		public override void Initialize()
		{
			base.Initialize();
			this.ParentObject.Render.CustomRender = true;
			this.ParentObject.Render.Visible = false;
		}

		// Token: 0x0600BAD8 RID: 47832 RVA: 0x004435F3 File Offset: 0x004417F3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BAD9 RID: 47833 RVA: 0x0044360C File Offset: 0x0044180C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object.Blueprint != "SewageEel" && E.Object.IsCombatObject() && this.ParentObject.CurrentCell != null && this.ParentObject.CurrentCell.HasWadingDepthLiquid() && 50.in100())
			{
				foreach (Cell cell in new List<Cell>(this.ParentObject.CurrentCell.GetAdjacentCells(true)).ShuffleInPlace<Cell>(null))
				{
					if (cell.HasWadingDepthLiquid())
					{
						GameObject gameObject = GameObject.Create("SewageEel");
						gameObject.MakeActive(false);
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (gameObject.IsHostileTowards(E.Object))
						{
							BodyPart bodyPart = E.Object.GetFirstBodyPart("Feet") ?? E.Object.GetFirstBodyPart("Roots");
							string text = ((bodyPart == null) ? " around you" : (" around your " + bodyPart.GetOrdinalName()));
							gameObject.Brain.Target = E.Object;
							if (!gameObject.FlightMatches(E.Object))
							{
								if (E.Object.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage("A sewage eel tries to wrap itself" + text + ", but cannot reach!", null, true);
								}
							}
							else if (!gameObject.PhaseMatches(E.Object))
							{
								if (E.Object.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage("A sewage eel tries to wrap itself" + text + ", but passes through you!", null, true);
								}
							}
							else
							{
								if (E.Object.IsPlayer())
								{
									Popup.Show("A sewage eel wraps itself" + text + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
								if (!E.Object.MakeSave("Agility", 16, null, null, "EelSpawn Knockdown", false, false, false, false, gameObject))
								{
									if (E.Object.IsPlayer())
									{
										E.Object.ApplyEffect(new Prone(), null);
									}
								}
								else if (E.Object.IsPlayer())
								{
									if (bodyPart != null && bodyPart.Type == "Feet" && E.Object.CanMoveExtremities(null, false, false, false))
									{
										Popup.Show("You maintain your balance and kick the eel away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									else
									{
										Popup.Show("You maintain your balance and shake the eel off.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
								}
							}
						}
						else if (IComponent<GameObject>.Visible(gameObject))
						{
							IComponent<GameObject>.XDidY(IComponent<GameObject>.ThePlayer, "spot", "a sewage eel " + IComponent<GameObject>.ThePlayer.DescribeDirectionToward(gameObject, false, false), "!", null, null, IComponent<GameObject>.ThePlayer, null, false, false, null, null, false, false, false, false, false, null);
						}
						this.ParentObject.Obliterate(null, false, null);
						break;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BADA RID: 47834 RVA: 0x00443920 File Offset: 0x00441B20
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CustomRender");
			Registrar.Register("Searched");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BADB RID: 47835 RVA: 0x00443940 File Offset: 0x00441B40
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CustomRender")
			{
				RenderEvent renderEvent = E.GetParameter("RenderEvent") as RenderEvent;
				if (renderEvent != null && (renderEvent.Lit == LightLevel.Radar || renderEvent.Lit == LightLevel.LitRadar))
				{
					this.Reveal(null);
				}
			}
			else if (E.ID == "Searched")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Searcher");
				if (gameObjectParameter.CurrentCell != this.ParentObject.CurrentCell && this.ParentObject.CurrentCell.HasWadingDepthLiquid() && E.GetIntParameter("Bonus", 0) + Stat.Random(1, gameObjectParameter.Stat("Intelligence", 0)) >= 16)
				{
					this.Reveal(gameObjectParameter);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BADC RID: 47836 RVA: 0x00443A10 File Offset: 0x00441C10
		public void Reveal(GameObject who = null)
		{
			if (who == null)
			{
				who = IComponent<GameObject>.ThePlayer;
			}
			if (who != null)
			{
				IComponent<GameObject>.XDidY(who, "spot", "a sewage eel " + who.DescribeDirectionToward(this.ParentObject, false, false), "!", null, null, who, null, false, false, null, null, false, false, false, false, false, null);
			}
			GameObject gameObject = GameObject.Create("SewageEel");
			gameObject.MakeActive(false);
			this.ParentObject.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			if (IComponent<GameObject>.Visible(gameObject) && AutoAct.IsInterruptable() && IComponent<GameObject>.ThePlayer.IsRelevantHostile(gameObject))
			{
				AutoAct.Interrupt(null, null, gameObject, true);
			}
			this.ParentObject.Destroy(null, false, false, null);
		}
	}
}
