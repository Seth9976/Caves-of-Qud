using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.AI
{
	// Token: 0x02000B40 RID: 2880
	[GenerateSerializationPartial]
	public class OpinionBeguile : IOpinionSubject
	{
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x002BE202 File Offset: 0x002BC402
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600744E RID: 29774 RVA: 0x002BE205 File Offset: 0x002BC405
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x002BE21F File Offset: 0x002BC41F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06007450 RID: 29776 RVA: 0x002BE239 File Offset: 0x002BC439
		public override int BaseValue
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06007451 RID: 29777 RVA: 0x002BE23C File Offset: 0x002BC43C
		public override float Limit
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x002BE243 File Offset: 0x002BC443
		public override string GetText(GameObject Actor)
		{
			return "Beguiled me.";
		}
	}
}
