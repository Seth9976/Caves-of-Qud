using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Occult.Engine.CodeGeneration;
using XRL.Liquids;
using XRL.Rules;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000DCC RID: 3532
	[GenerateSerializationPartial]
	[Serializable]
	public class DismemberedProperties : IPart
	{
		// Token: 0x06008F9B RID: 36763 RVA: 0x0033C988 File Offset: 0x0033AB88
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.SourceID);
			Writer.WriteOptimized(this.SourceBlueprint);
			Writer.WriteOptimized(this.SourceGenotype);
			Writer.WriteOptimized(this.SourceBlood);
			Writer.Write(this.BodyPart);
		}

		// Token: 0x06008F9C RID: 36764 RVA: 0x0033C9C8 File Offset: 0x0033ABC8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.SourceID = Reader.ReadOptimizedString();
			this.SourceBlueprint = Reader.ReadOptimizedString();
			this.SourceGenotype = Reader.ReadOptimizedString();
			this.SourceBlood = Reader.ReadOptimizedString();
			this.BodyPart = (BodyPart)Reader.ReadComposite();
		}

		// Token: 0x06008F9D RID: 36765 RVA: 0x0033CA18 File Offset: 0x0033AC18
		public void SetFrom(BodyPart BodyPart)
		{
			Body parentBody = BodyPart.ParentBody;
			GameObject gameObject = ((parentBody != null) ? parentBody.ParentObject : null);
			if (gameObject != null)
			{
				this.SourceID = gameObject.ID;
				this.SourceBlueprint = gameObject.Blueprint;
				this.SourceGenotype = gameObject.GetGenotype();
				this.SourceBlood = gameObject.GetBleedLiquid("blood-1000");
			}
			this.BodyPart = BodyPart.DeepCopy(null, null, null, null, false);
		}

		// Token: 0x06008F9E RID: 36766 RVA: 0x0033CA81 File Offset: 0x0033AC81
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<ConfigureMissileVisualEffectEvent>.ID;
		}

		// Token: 0x06008F9F RID: 36767 RVA: 0x0033CA98 File Offset: 0x0033AC98
		public override bool HandleEvent(ConfigureMissileVisualEffectEvent E)
		{
			if (E.Projectile == this.ParentObject && !this.SourceBlood.IsNullOrEmpty())
			{
				BaseLiquid liquid = LiquidVolume.GetLiquid(this.SourceBlood.AsSpan(0, this.SourceBlood.IndexOf('-')));
				List<string> colors = liquid.GetColors();
				string text = ((colors.Count > 0) ? colors[0] : liquid.GetColor());
				string text2 = ((colors.Count > 1) ? colors[Stat.Rnd.Next(1, colors.Count)] : text);
				E.Path.SetParameter("ParticleStartColor", text);
				E.Path.SetParameter("ParticleEndColor", text2);
			}
			return base.HandleEvent(E);
		}

		/// <summary>The ID of the source game object.</summary>
		// Token: 0x04003BF1 RID: 15345
		public string SourceID;

		/// <summary>The blueprint of the source game object.</summary>
		// Token: 0x04003BF2 RID: 15346
		public string SourceBlueprint;

		/// <summary>The genotype of the source game object.</summary>
		// Token: 0x04003BF3 RID: 15347
		public string SourceGenotype;

		/// <summary>The bleed liquid of the source game object.</summary>
		// Token: 0x04003BF4 RID: 15348
		public string SourceBlood;

		/// <summary>A copy of the body part at time of dismemberment.</summary>
		/// <remarks>Game object fields are excluded by default.</remarks>
		// Token: 0x04003BF5 RID: 15349
		public BodyPart BodyPart;
	}
}
