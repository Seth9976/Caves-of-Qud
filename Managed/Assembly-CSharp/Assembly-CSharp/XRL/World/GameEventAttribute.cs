using System;

namespace XRL.World
{
	/// <summary>Marks a class for generation of min event partials.</summary>
	// Token: 0x02000937 RID: 2359
	[AttributeUsage(AttributeTargets.Class)]
	public class GameEventAttribute : Attribute
	{
		/// <summary>The cascading depth of this event, defaults to the parent event's cascade level.</summary>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x001B8E00 File Offset: 0x001B7000
		// (set) Token: 0x06005B20 RID: 23328 RVA: 0x001B8E0D File Offset: 0x001B700D
		public int Cascade
		{
			get
			{
				return this._Cascade.GetValueOrDefault();
			}
			set
			{
				this._Cascade = new int?(value);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x001B8E1B File Offset: 0x001B701B
		public bool HasCascade
		{
			get
			{
				return this._Cascade != null;
			}
		}

		/// <summary>Seed for event's integer ID, defaults to class name.</summary>
		/// <remarks>This will be serialized for registrations and needs to be consistent across versions.</remarks>
		// Token: 0x04002CF3 RID: 11507
		public string Seed;

		// Token: 0x04002CF4 RID: 11508
		private int? _Cascade;

		/// <summary>Mark event as a base type for derived events.</summary>
		// Token: 0x04002CF5 RID: 11509
		public bool Base;

		/// <summary>How to create instances of this event.</summary>
		// Token: 0x04002CF6 RID: 11510
		public MinEvent.Cache Cache;
	}
}
