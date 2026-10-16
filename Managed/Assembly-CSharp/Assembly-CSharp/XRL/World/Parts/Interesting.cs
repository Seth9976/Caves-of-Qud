using System;
using ConsoleLib.Console;
using Genkit;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F70 RID: 3952
	[Serializable]
	public class Interesting : IPart
	{
		// Token: 0x06009D35 RID: 40245 RVA: 0x0038EE1A File Offset: 0x0038D01A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D36 RID: 40246 RVA: 0x0038EE1D File Offset: 0x0038D01D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || (ID == ReplicaCreatedEvent.ID && !this.KeepOnReplicas);
		}

		// Token: 0x06009D37 RID: 40247 RVA: 0x0038EE50 File Offset: 0x0038D050
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "DisplayName", this.DisplayName);
			E.AddEntry(this, "Explanation", this.Explanation);
			E.AddEntry(this, "IconTile", this.IconTile);
			E.AddEntry(this, "IconRenderString", this.IconRenderString);
			E.AddEntry(this, "IconColorString", this.IconColorString);
			E.AddEntry(this, "IconTileColor", this.IconTileColor);
			E.AddEntry(this, "IconDetailColor", this.IconDetailColor);
			E.AddEntry(this, "IfPartOperational", this.IfPartOperational);
			E.AddEntry(this, "Key", this.Key);
			E.AddEntry(this, "Preposition", this.Preposition);
			E.AddEntry(this, "Order", this.Order);
			E.AddEntry(this, "X", this.X);
			E.AddEntry(this, "Y", this.Y);
			E.AddEntry(this, "Radius", this.Radius);
			E.AddEntry(this, "EvenIfUnknown", this.EvenIfUnknown);
			E.AddEntry(this, "EvenIfHostile", this.EvenIfHostile);
			E.AddEntry(this, "EvenIfCompanion", this.EvenIfCompanion);
			E.AddEntry(this, "EvenIfInvisible", this.EvenIfInvisible);
			E.AddEntry(this, "EvenIfUnexplored", this.EvenIfUnexplored);
			E.AddEntry(this, "NearestKeyOnly", this.NearestKeyOnly);
			E.AddEntry(this, "TranslateToLocation", this.TranslateToLocation);
			E.AddEntry(this, "UseBaseDisplayNameAsKey", this.UseBaseDisplayNameAsKey);
			E.AddEntry(this, "KeepOnReplicas", this.KeepOnReplicas);
			return base.HandleEvent(E);
		}

		// Token: 0x06009D38 RID: 40248 RVA: 0x0038F004 File Offset: 0x0038D204
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (this.RequirementsMet(E.Actor))
			{
				bool flag = true;
				string text = this.Explanation;
				string text2 = (this.UseBaseDisplayNameAsKey ? ("Interesting " + this.ParentObject.BaseDisplayName) : this.Key);
				if (this.NearestKeyOnly && !string.IsNullOrEmpty(text2))
				{
					PointOfInterest pointOfInterest = E.Find(text2);
					if (pointOfInterest != null)
					{
						if (pointOfInterest.Object == this.ParentObject)
						{
							E.Remove(pointOfInterest);
						}
						else if (this.ParentObject.DistanceTo(E.Actor) < pointOfInterest.GetDistanceTo(E.Actor))
						{
							E.Remove(pointOfInterest);
							if (!string.IsNullOrEmpty(text))
							{
								text += ", nearest";
							}
							else
							{
								text = "nearest";
							}
						}
						else
						{
							flag = false;
							if (!string.IsNullOrEmpty(pointOfInterest.Explanation))
							{
								if (!pointOfInterest.Explanation.Contains("nearest"))
								{
									PointOfInterest pointOfInterest2 = pointOfInterest;
									pointOfInterest2.Explanation += ", nearest";
								}
							}
							else
							{
								pointOfInterest.Explanation = "nearest";
							}
						}
					}
				}
				else
				{
					PointOfInterest pointOfInterest3 = E.Find(this.ParentObject);
					if (pointOfInterest3 != null)
					{
						E.Remove(pointOfInterest3);
					}
				}
				if (flag)
				{
					Renderable renderable = null;
					if (!string.IsNullOrEmpty(this.IconTile))
					{
						renderable = new Renderable();
						renderable.Tile = this.IconTile;
						renderable.RenderString = this.IconRenderString;
						renderable.ColorString = this.IconColorString;
						if (!string.IsNullOrEmpty(this.IconTileColor))
						{
							renderable.TileColor = this.IconTileColor;
						}
						if (!string.IsNullOrEmpty(this.IconDetailColor))
						{
							renderable.DetailColor = this.IconDetailColor[0];
						}
					}
					GameObject gameObject = this.ParentObject;
					Location2D location2D = null;
					if (this.TranslateToLocation)
					{
						location2D = this.ParentObject.CurrentCell.Location;
						gameObject = null;
					}
					else if (this.X != 0 && this.Y != 0)
					{
						location2D = Location2D.Get(this.X, this.Y);
					}
					E.Add(gameObject, this.DisplayName ?? this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), text, text2, this.Preposition, location2D, null, this.Radius, renderable, this.Order);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D39 RID: 40249 RVA: 0x0038F24D File Offset: 0x0038D44D
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (!this.KeepOnReplicas && E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D3A RID: 40250 RVA: 0x0038F274 File Offset: 0x0038D474
		public bool RequirementsMet(GameObject who)
		{
			if (this.ParentObject == who)
			{
				return false;
			}
			if (!this.EvenIfUnexplored && !this.ParentObject.CurrentCell.IsExplored())
			{
				return false;
			}
			if (!this.EvenIfInvisible)
			{
				Render render = this.ParentObject.Render;
				if (render == null || !render.Visible)
				{
					return false;
				}
			}
			if (!this.EvenIfUnknown && !this.ParentObject.Understood())
			{
				return false;
			}
			if (!this.EvenIfHostile && this.ParentObject.IsHostileTowards(who))
			{
				return false;
			}
			if (!this.EvenIfCompanion && this.ParentObject.IsLedBy(who))
			{
				return false;
			}
			if (this.ParentObject.HasPart<FungalVision>() && FungalVisionary.VisionLevel <= 0)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.IfPartOperational))
			{
				IActivePart activePart = this.ParentObject.GetPart(this.IfPartOperational) as IActivePart;
				if (activePart == null || activePart.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040040F4 RID: 16628
		public string DisplayName;

		// Token: 0x040040F5 RID: 16629
		public string Explanation;

		// Token: 0x040040F6 RID: 16630
		public string IconTile;

		// Token: 0x040040F7 RID: 16631
		public string IconRenderString = " ";

		// Token: 0x040040F8 RID: 16632
		public string IconColorString = "";

		// Token: 0x040040F9 RID: 16633
		public string IconTileColor;

		// Token: 0x040040FA RID: 16634
		public string IconDetailColor;

		// Token: 0x040040FB RID: 16635
		public string IfPartOperational;

		// Token: 0x040040FC RID: 16636
		public string Key;

		// Token: 0x040040FD RID: 16637
		public string Preposition;

		// Token: 0x040040FE RID: 16638
		public int Order;

		// Token: 0x040040FF RID: 16639
		public int X;

		// Token: 0x04004100 RID: 16640
		public int Y;

		// Token: 0x04004101 RID: 16641
		public int Radius = -1;

		// Token: 0x04004102 RID: 16642
		public bool EvenIfUnknown;

		// Token: 0x04004103 RID: 16643
		public bool EvenIfHostile;

		// Token: 0x04004104 RID: 16644
		public bool EvenIfCompanion;

		// Token: 0x04004105 RID: 16645
		public bool EvenIfInvisible;

		// Token: 0x04004106 RID: 16646
		public bool EvenIfUnexplored;

		// Token: 0x04004107 RID: 16647
		public bool NearestKeyOnly = true;

		// Token: 0x04004108 RID: 16648
		public bool TranslateToLocation;

		// Token: 0x04004109 RID: 16649
		public bool UseBaseDisplayNameAsKey;

		// Token: 0x0400410A RID: 16650
		public bool KeepOnReplicas;
	}
}
