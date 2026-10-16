using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200127B RID: 4731
	[Serializable]
	public class PyroZone : IPart
	{
		// Token: 0x0600BB56 RID: 47958 RVA: 0x0044690B File Offset: 0x00444B0B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB57 RID: 47959 RVA: 0x0044690E File Offset: 0x00444B0E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == SingletonEvent<RadiatesHeatEvent>.ID;
		}

		// Token: 0x0600BB58 RID: 47960 RVA: 0x00446934 File Offset: 0x00444B34
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB59 RID: 47961 RVA: 0x00446944 File Offset: 0x00444B44
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return this.Duration <= 0 && base.HandleEvent(E);
		}

		// Token: 0x0600BB5A RID: 47962 RVA: 0x00446958 File Offset: 0x00444B58
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0)
			{
				DieRoll cachedDieRoll = (this.Level.ToString() + "d" + Pyrokinesis.GetDamageDieSize(this.Turn).ToString()).GetCachedDieRoll();
				int phase = this.ParentObject.GetPhase();
				foreach (GameObject gameObject in this.ParentObject.CurrentCell.GetObjectsWithPartReadonly("Physics"))
				{
					if (gameObject != this.ParentObject)
					{
						gameObject.TakeDamage((int)Math.Ceiling((double)cachedDieRoll.Resolve() / 2.0), "from %t pyrokinesis!", "Heat Fire", null, null, null, this.Owner, null, null, null, false, false, false, false, false, false, false, false, false, phase, null);
						gameObject.TemperatureChange((310 + 30 * this.Level) / 2, this.Owner, false, false, false, false, 0, null, null);
					}
				}
				this.ParentObject.TemperatureChange((310 + 30 * this.Level) / 2, this.Owner, false, false, false, false, 0, null, null);
				base.PlayWorldSound("Sounds/Abilities/sfx_ability_pyrokinesis_passive", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
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

		// Token: 0x0600BB5B RID: 47963 RVA: 0x00446BA4 File Offset: 0x00444DA4
		public override bool FinalRender(RenderEvent E)
		{
			if (!base.Visible())
			{
				return true;
			}
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
			if (num < 15)
			{
				E.BackgroundString = "^W";
				E.DetailColor = "W";
			}
			else if (num < 30)
			{
				E.BackgroundString = "^r";
				E.DetailColor = "r";
			}
			else if (num < 45)
			{
				E.BackgroundString = "^R";
				E.DetailColor = "R";
			}
			else
			{
				E.BackgroundString = "^r";
				E.DetailColor = "r";
			}
			if (Stat.RandomCosmetic(1, 5) == 1)
			{
				E.RenderString = "°";
				E.ColorString = "&W";
			}
			else if (Stat.RandomCosmetic(1, 5) == 1)
			{
				E.RenderString = "±";
				E.ColorString = "&R";
			}
			else
			{
				E.RenderString = "±";
				E.ColorString = "&r";
			}
			this.ParentObject.Render.ColorString = "&r^r";
			return base.FinalRender(E);
		}

		// Token: 0x0600BB5C RID: 47964 RVA: 0x00446CB0 File Offset: 0x00444EB0
		public override bool Render(RenderEvent E)
		{
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
			if (num < 15)
			{
				E.RenderString = "°";
				E.BackgroundString = "^W";
			}
			else if (num < 30)
			{
				E.RenderString = "±";
				E.BackgroundString = "^r";
			}
			else if (num < 45)
			{
				E.RenderString = "²";
				E.BackgroundString = "^R";
			}
			else
			{
				E.RenderString = "Û";
				E.BackgroundString = "^r";
			}
			this.ParentObject.Render.ColorString = "&r^r";
			return base.Render(E);
		}

		// Token: 0x0600BB5D RID: 47965 RVA: 0x00446D58 File Offset: 0x00444F58
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
								PyroZone pyroZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID && gameObject.IsVisible())
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

		// Token: 0x0600BB5E RID: 47966 RVA: 0x00446E1C File Offset: 0x0044501C
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
									PyroZone pyroZone;
									if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID && gameObject.InSameCellAs(Object))
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

		// Token: 0x0600BB5F RID: 47967 RVA: 0x00446EEC File Offset: 0x004450EC
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
								PyroZone pyroZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID)
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

		// Token: 0x0600BB60 RID: 47968 RVA: 0x00446FF8 File Offset: 0x004451F8
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
									PyroZone pyroZone;
									if (gameObject2.Blueprint == this.ParentObject.Blueprint && gameObject2.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID && gameObject2.IsVisible())
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

		// Token: 0x0600BB61 RID: 47969 RVA: 0x004470EC File Offset: 0x004452EC
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
						PyroZone pyroZone;
						if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID && IComponent<GameObject>.Visible(gameObject))
						{
							return gameObject;
						}
						i++;
					}
				}
			}
			return null;
		}

		// Token: 0x0600BB62 RID: 47970 RVA: 0x00447170 File Offset: 0x00445370
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
								PyroZone pyroZone;
								if (gameObject.Blueprint == this.ParentObject.Blueprint && gameObject.TryGetPart<PyroZone>(out pyroZone) && pyroZone.GroupID == this.GroupID && pyroZone.Control)
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

		// Token: 0x0600BB63 RID: 47971 RVA: 0x0044728C File Offset: 0x0044548C
		public GameObject GetControlIfVisible()
		{
			GameObject control = this.GetControl();
			if (control != null && IComponent<GameObject>.Visible(control))
			{
				return control;
			}
			return null;
		}

		// Token: 0x0600BB64 RID: 47972 RVA: 0x004472AE File Offset: 0x004454AE
		public GameObject GetDescriptionReference()
		{
			GameObject gameObject;
			if ((gameObject = this.GetVisibleGroupMemberInSameCellAs(The.Player)) == null)
			{
				gameObject = this.GetControlIfVisible() ?? this.GetVisibleInGroupClosestTo(The.Player);
			}
			return gameObject;
		}

		// Token: 0x0600BB65 RID: 47973 RVA: 0x004472D4 File Offset: 0x004454D4
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

		// Token: 0x0600BB66 RID: 47974 RVA: 0x00447318 File Offset: 0x00445518
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

		// Token: 0x0600BB67 RID: 47975 RVA: 0x0044735C File Offset: 0x0044555C
		public bool CheckDependsOn()
		{
			if (this.DependsOn.IsNullOrEmpty())
			{
				return true;
			}
			GameObject gameObject = GameObject.FindByID(this.DependsOn);
			return gameObject != null && !gameObject.IsNowhere();
		}

		// Token: 0x04004B36 RID: 19254
		public GameObject Owner;

		// Token: 0x04004B37 RID: 19255
		public int FrameOffset = Stat.RandomCosmetic(0, 60);

		// Token: 0x04004B38 RID: 19256
		public int Duration = 3;

		// Token: 0x04004B39 RID: 19257
		public int Turn = 1;

		// Token: 0x04004B3A RID: 19258
		public int Level = 1;

		// Token: 0x04004B3B RID: 19259
		public int GroupID;

		// Token: 0x04004B3C RID: 19260
		public bool Control;

		// Token: 0x04004B3D RID: 19261
		public string DependsOn;

		// Token: 0x04004B3E RID: 19262
		public string StartMessage = "The air =subject.generalDirectionIfAny= starts to shimmer with heat!";

		// Token: 0x04004B3F RID: 19263
		public string StopMessage = "The air =subject.generalDirectionIfAny= ceases shimmering with heat.";
	}
}
