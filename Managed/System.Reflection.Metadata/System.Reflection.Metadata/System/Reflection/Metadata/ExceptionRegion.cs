using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000082 RID: 130
	public readonly struct ExceptionRegion
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0000C45F File Offset: 0x0000A65F
		internal ExceptionRegion(ExceptionRegionKind kind, int tryOffset, int tryLength, int handlerOffset, int handlerLength, int classTokenOrFilterOffset)
		{
			this._kind = kind;
			this._tryOffset = tryOffset;
			this._tryLength = tryLength;
			this._handlerOffset = handlerOffset;
			this._handlerLength = handlerLength;
			this._classTokenOrFilterOffset = classTokenOrFilterOffset;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000C48E File Offset: 0x0000A68E
		public ExceptionRegionKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		/// <summary>Gets the starting IL offset of the try block.</summary>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000C496 File Offset: 0x0000A696
		public int TryOffset
		{
			get
			{
				return this._tryOffset;
			}
		}

		/// <summary>Gets the length in bytes of the try block.</summary>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000C49E File Offset: 0x0000A69E
		public int TryLength
		{
			get
			{
				return this._tryLength;
			}
		}

		/// <summary>Gets the starting IL offset of the exception handler.</summary>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		public int HandlerOffset
		{
			get
			{
				return this._handlerOffset;
			}
		}

		/// <summary>Gets the length in bytes of the exception handler.</summary>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000C4AE File Offset: 0x0000A6AE
		public int HandlerLength
		{
			get
			{
				return this._handlerLength;
			}
		}

		/// <summary>Gets the IL offset of the start of the filter block, or -1 if the region is not a filter.</summary>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
		public int FilterOffset
		{
			get
			{
				if (this.Kind != ExceptionRegionKind.Filter)
				{
					return -1;
				}
				return this._classTokenOrFilterOffset;
			}
		}

		/// <summary>Gets a TypeRef, TypeDef, or TypeSpec handle if the region represents a catch, or a nil token otherwise (<see langword="default" />(<see cref="T:System.Reflection.Metadata.EntityHandle" />)).</summary>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		public EntityHandle CatchType
		{
			get
			{
				if (this.Kind != ExceptionRegionKind.Catch)
				{
					return default(EntityHandle);
				}
				return new EntityHandle((uint)this._classTokenOrFilterOffset);
			}
		}

		// Token: 0x0400034F RID: 847
		private readonly ExceptionRegionKind _kind;

		// Token: 0x04000350 RID: 848
		private readonly int _tryOffset;

		// Token: 0x04000351 RID: 849
		private readonly int _tryLength;

		// Token: 0x04000352 RID: 850
		private readonly int _handlerOffset;

		// Token: 0x04000353 RID: 851
		private readonly int _handlerLength;

		// Token: 0x04000354 RID: 852
		private readonly int _classTokenOrFilterOffset;
	}
}
