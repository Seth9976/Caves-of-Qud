using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000F29 RID: 3881
	[Serializable]
	public class CyberneticsFistReplacement : IPart
	{
		// Token: 0x06009ABF RID: 39615 RVA: 0x0038200A File Offset: 0x0038020A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AC0 RID: 39616 RVA: 0x0038200D File Offset: 0x0038020D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AC1 RID: 39617 RVA: 0x00382010 File Offset: 0x00380210
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (Reader.FileVersion <= 400)
			{
				this.ReplacedFists.Clear();
				this.OriginalFistBlueprints.Clear();
			}
		}

		// Token: 0x06009AC2 RID: 39618 RVA: 0x0038203D File Offset: 0x0038023D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == PooledEvent<RegenerateDefaultEquipmentEvent>.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009AC3 RID: 39619 RVA: 0x00382063 File Offset: 0x00380263
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ImplantDependency = E.Part.DependsOn;
			this.ApplyFists(E.Part.ParentBody);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AC4 RID: 39620 RVA: 0x0038208E File Offset: 0x0038028E
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.ImplantDependency = null;
			BodyPart part = E.Part;
			if (part != null)
			{
				Body parentBody = part.ParentBody;
				if (parentBody != null)
				{
					parentBody.UpdateBodyParts(0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009AC5 RID: 39621 RVA: 0x003820BB File Offset: 0x003802BB
		public override bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			this.ApplyFists(E.Body);
			return base.HandleEvent(E);
		}

		// Token: 0x06009AC6 RID: 39622 RVA: 0x003820D0 File Offset: 0x003802D0
		public void ApplyFists(Body Body)
		{
			if (Body == null || this.ImplantDependency.IsNullOrEmpty())
			{
				return;
			}
			foreach (BodyPart bodyPart in Body.GetPart("Hand", true))
			{
				if (!bodyPart.Extrinsic && !(bodyPart.SupportsDependent != this.ImplantDependency))
				{
					if (!this.ReplacedFists.Contains(bodyPart.ID))
					{
						this.ReplacedFists.Add(bodyPart.ID);
						List<string> originalFistBlueprints = this.OriginalFistBlueprints;
						GameObject defaultBehavior = bodyPart.DefaultBehavior;
						originalFistBlueprints.Add((defaultBehavior != null) ? defaultBehavior.Blueprint : null);
					}
					else
					{
						GameObject defaultBehavior2 = bodyPart.DefaultBehavior;
						if (((defaultBehavior2 != null) ? defaultBehavior2.Blueprint : null) == this.FistObject)
						{
							continue;
						}
					}
					GameObject defaultBehavior3 = bodyPart.DefaultBehavior;
					if (defaultBehavior3 != null)
					{
						defaultBehavior3.Obliterate(null, false, null);
					}
					bodyPart.DefaultBehavior = GameObject.Create(this.FistObject);
					bodyPart.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "CyberneticsFistReplacement", false);
				}
			}
		}

		// Token: 0x04004051 RID: 16465
		public string FistObject;

		// Token: 0x04004052 RID: 16466
		public List<int> ReplacedFists = new List<int>();

		// Token: 0x04004053 RID: 16467
		public List<string> OriginalFistBlueprints = new List<string>();

		// Token: 0x04004054 RID: 16468
		public string ImplantDependency;
	}
}
