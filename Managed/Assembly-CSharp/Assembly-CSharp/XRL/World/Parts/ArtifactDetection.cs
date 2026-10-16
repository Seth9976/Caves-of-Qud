using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FFB RID: 4091
	[Serializable]
	public class ArtifactDetection : IActivePart
	{
		// Token: 0x0600A307 RID: 41735 RVA: 0x003B96B2 File Offset: 0x003B78B2
		public ArtifactDetection()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A308 RID: 41736 RVA: 0x003B96D3 File Offset: 0x003B78D3
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A309 RID: 41737 RVA: 0x003B96D6 File Offset: 0x003B78D6
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.DetectArtifacts(true, Amount);
		}

		// Token: 0x0600A30A RID: 41738 RVA: 0x003B96E0 File Offset: 0x003B78E0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == TakenEvent.ID || ID == DroppedEvent.ID;
		}

		// Token: 0x0600A30B RID: 41739 RVA: 0x003B9708 File Offset: 0x003B7908
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Radius < 80)
			{
				E.Postfix.Compound("{{rules|You detect unidentified artifacts within a radius of ", '\n').Append(this.Radius).Append(".}}");
			}
			else
			{
				E.Postfix.Compound("{{rules|You detect all unidentified artifacts on the local map.}}", '\n');
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A30C RID: 41740 RVA: 0x003B9763 File Offset: 0x003B7963
		public override bool HandleEvent(TakenEvent E)
		{
			this.DetectArtifacts(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A30D RID: 41741 RVA: 0x003B9774 File Offset: 0x003B7974
		public override bool HandleEvent(DroppedEvent E)
		{
			this.DetectArtifacts(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A30E RID: 41742 RVA: 0x003B9788 File Offset: 0x003B7988
		public List<Examiner> GetNearbyArtifacts(Cell C)
		{
			if (this.Radius >= 80)
			{
				using (IEnumerator<GameObject> enumerator = C.ParentZone.YieldObjects().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Examiner examiner;
						if (enumerator.Current.TryGetPart<Examiner>(out examiner) && this.IsDetectable(examiner))
						{
							this.Artifacts.Add(examiner);
						}
					}
					goto IL_00D5;
				}
			}
			foreach (Cell cell in C.YieldAdjacentCells(this.Radius, true, true))
			{
				using (Container<GameObject>.Enumerator enumerator3 = cell.Objects.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Examiner examiner2;
						if (enumerator3.Current.TryGetPart<Examiner>(out examiner2) && this.IsDetectable(examiner2))
						{
							this.Artifacts.Add(examiner2);
						}
					}
				}
			}
			IL_00D5:
			return this.Artifacts;
		}

		// Token: 0x0600A30F RID: 41743 RVA: 0x003B9898 File Offset: 0x003B7A98
		public bool IsDetectable(Examiner Artifact)
		{
			return Artifact.Complexity > 0 && !Artifact.KeepTile && (this.DetectKnown || Artifact.GetEpistemicStatus() != 2) && Artifact.ParentObject.CanHypersensesDetect();
		}

		// Token: 0x0600A310 RID: 41744 RVA: 0x003B98CC File Offset: 0x003B7ACC
		public void DetectArtifacts(bool UseCharge = false, int MultipleCharge = 1)
		{
			this.Artifacts.Clear();
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			Cell cell = ((activePartFirstSubject != null) ? activePartFirstSubject.CurrentCell : null);
			if (cell == null || !activePartFirstSubject.IsPlayer() || cell.OnWorldMap())
			{
				this.LastStatus = new ActivePartStatus?(ActivePartStatus.NeedsSubject);
				return;
			}
			if (base.IsDisabled(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				return;
			}
			foreach (Examiner examiner in this.GetNearbyArtifacts(cell))
			{
				if (!examiner.ParentObject.HasEffect<ArtifactDetectionEffect>())
				{
					ArtifactDetectionEffect artifactDetectionEffect = new ArtifactDetectionEffect(this, examiner)
					{
						Source = this.ParentObject,
						Radius = ((this.Radius < 80) ? this.Radius : 9999),
						IdentifyChance = (this.Identify ? this.IdentifyChance : 0)
					};
					examiner.ParentObject.ApplyEffect(artifactDetectionEffect, null);
				}
			}
		}

		// Token: 0x04004388 RID: 17288
		public const int RADIUS_ZONE = 80;

		// Token: 0x04004389 RID: 17289
		public int Radius;

		// Token: 0x0400438A RID: 17290
		public int IdentifyChance;

		// Token: 0x0400438B RID: 17291
		public bool Identify;

		// Token: 0x0400438C RID: 17292
		public bool DetectKnown;

		// Token: 0x0400438D RID: 17293
		[NonSerialized]
		private List<Examiner> Artifacts = new List<Examiner>();
	}
}
