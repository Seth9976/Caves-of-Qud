using System;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x0200003F RID: 63
	[VisibleToOtherModules]
	internal interface IHandle<HandleType> : IValidatable, IEquatable<HandleType> where HandleType : struct, IHandle<HandleType>
	{
	}
}
