using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200108D RID: 4237
	[Serializable]
	public class ReflectProjectiles : IPoweredPart
	{
		// Token: 0x0600A8A1 RID: 43169 RVA: 0x003DB0AE File Offset: 0x003D92AE
		public ReflectProjectiles()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A8A2 RID: 43170 RVA: 0x003DB0E9 File Offset: 0x003D92E9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A8A3 RID: 43171 RVA: 0x003DB0EC File Offset: 0x003D92EC
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.Deactivated;
		}

		// Token: 0x0600A8A4 RID: 43172 RVA: 0x003DB0F4 File Offset: 0x003D92F4
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "Deactivated";
		}

		// Token: 0x0600A8A5 RID: 43173 RVA: 0x003DB0FB File Offset: 0x003D92FB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("ApplyEMP");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A8A6 RID: 43174 RVA: 0x003DB11C File Offset: 0x003D931C
		public bool Check()
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (!this.ParentObject.HasEffect<ProjectileReflectionShield>())
				{
					this.ParentObject.ApplyEffect(new ProjectileReflectionShield(this.Chance, this.RetroVariance, this.Verb), null);
					base.DidX("activate", this.ParentObject.its + " reflective shield", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				}
				return true;
			}
			if (this.ParentObject.HasEffect<ProjectileReflectionShield>())
			{
				this.ParentObject.RemoveEffect<ProjectileReflectionShield>();
				base.DidX("have", this.ParentObject.its + " reflective shield deactivated", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
			}
			return false;
		}

		// Token: 0x0600A8A7 RID: 43175 RVA: 0x003DB208 File Offset: 0x003D9408
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyEMP" && this.IsEMPSensitive && this.ParentObject.HasEffect<ProjectileReflectionShield>())
			{
				this.ParentObject.RemoveEffect<ProjectileReflectionShield>();
				base.DidX("have", this.ParentObject.its + " reflective shield deactivated", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
			}
			if (E.ID == "BeginTakeAction")
			{
				this.Check();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040045B6 RID: 17846
		public int Chance = 100;

		// Token: 0x040045B7 RID: 17847
		public string RetroVariance = "0";

		// Token: 0x040045B8 RID: 17848
		public string Verb = "reflect";

		// Token: 0x040045B9 RID: 17849
		public bool Deactivated;
	}
}
