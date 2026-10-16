using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001251 RID: 4689
	[Serializable]
	public class CryoZone : IPart
	{
		// Token: 0x0600BA45 RID: 47685 RVA: 0x004405D4 File Offset: 0x0043E7D4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA46 RID: 47686 RVA: 0x004405D7 File Offset: 0x0043E7D7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x0600BA47 RID: 47687 RVA: 0x004405F5 File Offset: 0x0043E7F5
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA48 RID: 47688 RVA: 0x00440608 File Offset: 0x0043E808
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0)
			{
				DieRoll cachedDieRoll = (this.Level.ToString() + "d" + Cryokinesis.GetDamageDieSize(this.Turn).ToString()).GetCachedDieRoll();
				int phase = this.ParentObject.GetPhase();
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsWithPartReadonly("Physics"))
				{
					if (gameObject != this.ParentObject)
					{
						gameObject.TakeDamage((int)Math.Ceiling((double)cachedDieRoll.Resolve() / 2.0), "from %t cryokinesis!", "Ice Cold", null, null, null, this.Owner, null, null, null, false, false, false, false, false, false, false, false, false, phase, null);
						gameObject.TemperatureChange((-20 - 60 * this.Level) / 2, this.Owner, false, false, false, false, 0, null, null);
					}
				}
				this.ParentObject.TemperatureChange((-20 - 60 * this.Level) / 2, this.Owner, false, false, false, false, 0, null, null);
				base.PlayWorldSound("Sounds/Abilities/sfx_ability_cryokinesis_passive", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				this.Turn++;
			}
			if (this.Duration != 9999)
			{
				this.Duration--;
			}
			if (this.Duration <= 0 || !this.CheckDependsOn())
			{
				if (this.Control)
				{
					this.Stopped();
					foreach (GameObject gameObject2 in this.GetGroup())
					{
						gameObject2.Destroy(null, true, false, null);
					}
					return false;
				}
				if (this.GetControl() == null)
				{
					this.ParentObject.Destroy(null, true, false, null);
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA49 RID: 47689 RVA: 0x00440850 File Offset: 0x0043EA50
		public override bool FinalRender(RenderEvent E)
		{
			if (!base.Visible())
			{
				return true;
			}
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
			if (num < 15)
			{
				E.BackgroundString = "^C";
				E.DetailColor = "C";
			}
			else if (num < 30)
			{
				E.BackgroundString = "^c";
				E.DetailColor = "c";
			}
			else if (num < 45)
			{
				E.BackgroundString = "^Y";
				E.DetailColor = "Y";
			}
			else
			{
				E.BackgroundString = "^c";
				E.DetailColor = "c";
			}
			if (Stat.RandomCosmetic(1, 5) == 1)
			{
				E.RenderString = "°";
				E.ColorString = "&C";
			}
			else if (Stat.RandomCosmetic(1, 5) == 1)
			{
				E.RenderString = "±";
				E.ColorString = "&Y";
			}
			else
			{
				E.RenderString = "±";
				E.ColorString = "&c";
			}
			this.ParentObject.Render.ColorString = "&Y^C";
			return base.FinalRender(E);
		}

		// Token: 0x0600BA4A RID: 47690 RVA: 0x0044095C File Offset: 0x0043EB5C
		public override bool Render(RenderEvent E)
		{
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
			if (num < 15)
			{
				E.RenderString = "°";
				E.BackgroundString = "^C";
			}
			else if (num < 30)
			{
				E.RenderString = "±";
				E.BackgroundString = "^c";
			}
			else if (num < 45)
			{
				E.RenderString = "²";
				E.BackgroundString = "^Y";
			}
			else
			{
				E.RenderString = "Û";
				E.BackgroundString = "^C";
			}
			this.ParentObject.Render.ColorString = "&Y^C";
			return true;
		}

		// Token: 0x0600BA4B RID: 47691 RVA: 0x004409FC File Offset: 0x0043EBFC
		public bool AnyOtherVisibleInGroup()
		{
			Zone activeZone = The.ZoneManager.ActiveZone;
			if (activeZone != null)
			{
				for (int i = 0; i < activeZone.Height; i++)
				{
					for (int j = 0; j < activeZone.Width; j++)
					{
						Cell cell = activeZone.GetCell(j, i);
						if (cell != null)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject = cell.Objects[k];
								CryoZone cryoZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID && gameObject.IsVisible())
								{
									return true;
								}
								k++;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600BA4C RID: 47692 RVA: 0x00440AC0 File Offset: 0x0043ECC0
		public bool AnyOtherInGroupInSameCellAs(GameObject Object)
		{
			if (GameObject.Validate(ref Object))
			{
				Zone activeZone = The.ZoneManager.ActiveZone;
				if (activeZone != null)
				{
					for (int i = 0; i < activeZone.Height; i++)
					{
						for (int j = 0; j < activeZone.Width; j++)
						{
							Cell cell = activeZone.GetCell(j, i);
							if (cell != null)
							{
								int k = 0;
								int count = cell.Objects.Count;
								while (k < count)
								{
									GameObject gameObject = cell.Objects[k];
									CryoZone cryoZone;
									if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID && gameObject.InSameCellAs(Object))
									{
										return true;
									}
									k++;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600BA4D RID: 47693 RVA: 0x00440B90 File Offset: 0x0043ED90
		public List<GameObject> GetGroup()
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (Zone zone in The.ZoneManager.CachedZones.Values)
			{
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						Cell cell = zone.GetCell(j, i);
						if (cell != null)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject = cell.Objects[k];
								CryoZone cryoZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID)
								{
									list.Add(gameObject);
								}
								k++;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600BA4E RID: 47694 RVA: 0x00440C9C File Offset: 0x0043EE9C
		public GameObject GetVisibleInGroupClosestTo(GameObject Object)
		{
			if (GameObject.Validate(ref Object))
			{
				GameObject gameObject = null;
				int num = 0;
				Zone currentZone = Object.CurrentZone;
				if (currentZone != null)
				{
					for (int i = 0; i < currentZone.Height; i++)
					{
						for (int j = 0; j < currentZone.Width; j++)
						{
							Cell cell = currentZone.GetCell(j, i);
							if (cell != null)
							{
								int k = 0;
								int count = cell.Objects.Count;
								while (k < count)
								{
									GameObject gameObject2 = cell.Objects[k];
									CryoZone cryoZone;
									if (gameObject2.Blueprint == this.ParentObject.Blueprint && gameObject2.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID && gameObject2.IsVisible())
									{
										int num2 = gameObject2.DistanceTo(Object);
										if (gameObject == null || num2 < num)
										{
											gameObject = gameObject2;
											num = num2;
										}
									}
									k++;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600BA4F RID: 47695 RVA: 0x00440D90 File Offset: 0x0043EF90
		public GameObject GetVisibleGroupMemberInSameCellAs(GameObject Object)
		{
			if (GameObject.Validate(ref Object))
			{
				Cell currentCell = Object.CurrentCell;
				if (currentCell != null)
				{
					int i = 0;
					int count = currentCell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = currentCell.Objects[i];
						CryoZone cryoZone;
						if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID && IComponent<GameObject>.Visible(gameObject))
						{
							return gameObject;
						}
						i++;
					}
				}
			}
			return null;
		}

		// Token: 0x0600BA50 RID: 47696 RVA: 0x00440E14 File Offset: 0x0043F014
		public GameObject GetControl()
		{
			if (this.Control)
			{
				return this.ParentObject;
			}
			foreach (Zone zone in The.ZoneManager.CachedZones.Values)
			{
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						Cell cell = zone.GetCell(j, i);
						if (cell != null)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject = cell.Objects[k];
								CryoZone cryoZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<CryoZone>(out cryoZone) && cryoZone.GroupID == this.GroupID && cryoZone.Control)
								{
									return gameObject;
								}
								k++;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600BA51 RID: 47697 RVA: 0x00440F30 File Offset: 0x0043F130
		public GameObject GetControlIfVisible()
		{
			GameObject control = this.GetControl();
			if (control != null && IComponent<GameObject>.Visible(control))
			{
				return control;
			}
			return null;
		}

		// Token: 0x0600BA52 RID: 47698 RVA: 0x00440F52 File Offset: 0x0043F152
		public GameObject GetDescriptionReference()
		{
			GameObject gameObject;
			if ((gameObject = this.GetVisibleGroupMemberInSameCellAs(The.Player)) == null)
			{
				gameObject = this.GetControlIfVisible() ?? this.GetVisibleInGroupClosestTo(The.Player);
			}
			return gameObject;
		}

		// Token: 0x0600BA53 RID: 47699 RVA: 0x00440F78 File Offset: 0x0043F178
		public void Started()
		{
			if (!this.StartMessage.IsNullOrEmpty())
			{
				GameObject descriptionReference = this.GetDescriptionReference();
				if (descriptionReference != null)
				{
					string text = GameText.VariableReplace(this.StartMessage, descriptionReference, null, false);
					if (!text.IsNullOrEmpty())
					{
						IComponent<GameObject>.AddPlayerMessage(text, null, true);
					}
				}
			}
		}

		// Token: 0x0600BA54 RID: 47700 RVA: 0x00440FBC File Offset: 0x0043F1BC
		public void Stopped()
		{
			if (!this.StopMessage.IsNullOrEmpty())
			{
				GameObject descriptionReference = this.GetDescriptionReference();
				if (descriptionReference != null)
				{
					string text = GameText.VariableReplace(this.StopMessage, descriptionReference, null, false);
					if (!text.IsNullOrEmpty())
					{
						IComponent<GameObject>.AddPlayerMessage(text, null, true);
					}
				}
			}
		}

		// Token: 0x0600BA55 RID: 47701 RVA: 0x00441000 File Offset: 0x0043F200
		public bool CheckDependsOn()
		{
			if (this.DependsOn.IsNullOrEmpty())
			{
				return true;
			}
			GameObject gameObject = GameObject.FindByID(this.DependsOn);
			return gameObject != null && !gameObject.IsNowhere();
		}

		// Token: 0x04004ADF RID: 19167
		public GameObject Owner;

		// Token: 0x04004AE0 RID: 19168
		public int FrameOffset = Stat.RandomCosmetic(0, 60);

		// Token: 0x04004AE1 RID: 19169
		public int Duration = 3;

		// Token: 0x04004AE2 RID: 19170
		public int Turn = 1;

		// Token: 0x04004AE3 RID: 19171
		public int Level = 1;

		// Token: 0x04004AE4 RID: 19172
		public int GroupID;

		// Token: 0x04004AE5 RID: 19173
		public bool Control;

		// Token: 0x04004AE6 RID: 19174
		public string DependsOn;

		// Token: 0x04004AE7 RID: 19175
		public string StartMessage = "The air =subject.generalDirectionIfAny= bursts into a field of frigid mist!";

		// Token: 0x04004AE8 RID: 19176
		public string StopMessage = "The frigid mist =subject.generalDirectionIfAny= dissipates.";
	}
}
