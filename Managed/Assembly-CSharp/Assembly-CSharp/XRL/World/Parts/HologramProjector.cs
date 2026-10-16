using System;

namespace XRL.World.Parts
{
	// Token: 0x0200104C RID: 4172
	[Serializable]
	public class HologramProjector : IPoweredPart
	{
		// Token: 0x0600A614 RID: 42516 RVA: 0x003CCB05 File Offset: 0x003CAD05
		public HologramProjector()
		{
			this.WorksOnSelf = true;
			this.IsPowerSwitchSensitive = true;
			this.NameForStatus = "Holography";
		}

		// Token: 0x0600A615 RID: 42517 RVA: 0x003CCB3C File Offset: 0x003CAD3C
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Blueprint);
			Writer.WriteOptimized(this.Direction);
			Writer.WriteGameObject(this.Hologram);
		}

		// Token: 0x0600A616 RID: 42518 RVA: 0x003CCB62 File Offset: 0x003CAD62
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Blueprint = Reader.ReadOptimizedString();
			this.Direction = Reader.ReadOptimizedString();
			this.Hologram = Reader.ReadGameObject(null);
		}

		// Token: 0x0600A617 RID: 42519 RVA: 0x003CCB89 File Offset: 0x003CAD89
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A618 RID: 42520 RVA: 0x003CCB8C File Offset: 0x003CAD8C
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckStatus(true, Amount);
		}

		// Token: 0x0600A619 RID: 42521 RVA: 0x003CCB98 File Offset: 0x003CAD98
		public void CheckStatus(bool UseCharge = false, int MultipleCharge = 1)
		{
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				this.Enable(false);
				return;
			}
			if (GameObject.Validate(ref this.Hologram))
			{
				this.Disable(false);
			}
		}

		// Token: 0x0600A61A RID: 42522 RVA: 0x003CCBEC File Offset: 0x003CADEC
		public void Enable(bool Silent = false)
		{
			if (GameObject.Validate(ref this.Hologram) || this.Blueprint.IsNullOrEmpty())
			{
				return;
			}
			GameObject projectionSurface = this.GetProjectionSurface();
			Cell cell = ((projectionSurface != null) ? projectionSurface.CurrentCell : null);
			if (cell != null)
			{
				GameObject gameObject = (this.Hologram = cell.AddObject(this.Blueprint, null, null, null, null));
				gameObject.RequirePart<ExistenceSupport>(false).SupportedBy = this.ParentObject;
				HologramMaterial hologramMaterial;
				if (gameObject.TryGetPart<HologramMaterial>(out hologramMaterial))
				{
					hologramMaterial.FlickerFrame = 2;
				}
				if (!Silent)
				{
					gameObject.Physics.DidX("appear", null, null, null, null, null, null, false, true, null, null, false, true, false, false, false, null);
				}
			}
		}

		// Token: 0x0600A61B RID: 42523 RVA: 0x003CCC8C File Offset: 0x003CAE8C
		public void Disable(bool Silent = false)
		{
			if (!GameObject.Validate(ref this.Hologram))
			{
				return;
			}
			if (!Silent)
			{
				this.Hologram.Physics.DidX("disappear", null, null, null, null, null, null, false, true, null, null, false, true, false, false, false, null);
			}
			this.Hologram.Destroy(null, Silent, false, null);
		}

		// Token: 0x0600A61C RID: 42524 RVA: 0x003CCCDF File Offset: 0x003CAEDF
		public void SetBlueprint(string Blueprint, bool Silent = false)
		{
			if (Blueprint == this.Blueprint)
			{
				return;
			}
			this.Blueprint = Blueprint;
			if (GameObject.Validate(ref this.Hologram))
			{
				this.Disable(Silent);
				this.Enable(Silent);
			}
		}

		// Token: 0x0600A61D RID: 42525 RVA: 0x003CCD12 File Offset: 0x003CAF12
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == PowerSwitchFlippedEvent.ID;
		}

		// Token: 0x0600A61E RID: 42526 RVA: 0x003CCD26 File Offset: 0x003CAF26
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckStatus(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A61F RID: 42527 RVA: 0x003CCD38 File Offset: 0x003CAF38
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (this.Hologram != E.Object || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A620 RID: 42528 RVA: 0x003CCD84 File Offset: 0x003CAF84
		public GameObject GetProjectionSurface()
		{
			if (GameObject.Validate(this.Surface) && this.ParentObject.GetDirectionToward(this.Surface, false) == this.Direction)
			{
				return this.Surface;
			}
			this.Surface = null;
			for (Cell cell = this.ParentObject.CurrentCell.GetLocalCellFromDirection(this.Direction, true); cell != null; cell = cell.GetLocalCellFromDirection(this.Direction, true))
			{
				GameObject firstObjectWithPropertyOrTag = cell.GetFirstObjectWithPropertyOrTag("ProjectionSurface");
				if (firstObjectWithPropertyOrTag != null)
				{
					return this.Surface = firstObjectWithPropertyOrTag;
				}
			}
			return null;
		}

		// Token: 0x0600A621 RID: 42529 RVA: 0x003CCE11 File Offset: 0x003CB011
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.Blueprint.IsNullOrEmpty() || !GameObject.Validate(this.GetProjectionSurface());
		}

		// Token: 0x0600A622 RID: 42530 RVA: 0x003CCE30 File Offset: 0x003CB030
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			if (this.Blueprint.IsNullOrEmpty())
			{
				return "ProjectionGraphMissing";
			}
			return "ProjectionSurfaceMissing";
		}

		// Token: 0x04004499 RID: 17561
		public string ACT_CMD = "ActivateHologramProjector";

		// Token: 0x0400449A RID: 17562
		public string Blueprint;

		// Token: 0x0400449B RID: 17563
		public string Direction = "S";

		// Token: 0x0400449C RID: 17564
		public GameObject Hologram;

		// Token: 0x0400449D RID: 17565
		[NonSerialized]
		public GameObject Surface;
	}
}
