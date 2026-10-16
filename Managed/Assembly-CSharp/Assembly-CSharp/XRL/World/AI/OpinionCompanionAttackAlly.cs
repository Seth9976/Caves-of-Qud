using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B43 RID: 2883
	[GenerateSerializationPartial]
	public class OpinionCompanionAttackAlly : IOpinionCombat
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06007461 RID: 29793 RVA: 0x002BE2E8 File Offset: 0x002BC4E8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007462 RID: 29794 RVA: 0x002BE2EB File Offset: 0x002BC4EB
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Ally);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x002BE311 File Offset: 0x002BC511
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Ally = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06007464 RID: 29796 RVA: 0x002BE337 File Offset: 0x002BC537
		public override int BaseValue
		{
			get
			{
				return -75;
			}
		}

		// Token: 0x06007465 RID: 29797 RVA: 0x002BE33B File Offset: 0x002BC53B
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			if (Object.HasProperName)
			{
				this.Ally = Object.Render.DisplayName;
				return;
			}
			this.Ally = null;
		}

		// Token: 0x06007466 RID: 29798 RVA: 0x002BE35E File Offset: 0x002BC55E
		public override string GetText(GameObject Actor)
		{
			return "Companion attacked " + (this.Ally ?? "my ally") + ".";
		}

		// Token: 0x04003588 RID: 13704
		public string Ally;
	}
}
