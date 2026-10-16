using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020002E3 RID: 739
	[NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h")]
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h")]
	[NativeHeader("Runtime/Misc/SystemInfoMemory.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	public sealed class SystemInfo
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0004595C File Offset: 0x00043B5C
		[NativeProperty]
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.GetBatteryLevel();
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x00045974 File Offset: 0x00043B74
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.GetBatteryStatus();
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0004598C File Offset: 0x00043B8C
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.GetOperatingSystem();
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000459A4 File Offset: 0x00043BA4
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.GetOperatingSystemFamily();
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000459BC File Offset: 0x00043BBC
		public static string processorType
		{
			get
			{
				return SystemInfo.GetProcessorType();
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000459D4 File Offset: 0x00043BD4
		public static string processorModel
		{
			get
			{
				return SystemInfo.GetProcessorModel();
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000459EC File Offset: 0x00043BEC
		public static string processorManufacturer
		{
			get
			{
				return SystemInfo.GetProcessorManufacturer();
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00045A04 File Offset: 0x00043C04
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.GetProcessorFrequencyMHz();
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x00045A1C File Offset: 0x00043C1C
		public static int processorCount
		{
			get
			{
				return SystemInfo.GetProcessorCount();
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00045A34 File Offset: 0x00043C34
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.GetPhysicalMemoryMB();
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x00045A4C File Offset: 0x00043C4C
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.GetDeviceUniqueIdentifier();
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x00045A64 File Offset: 0x00043C64
		public static string deviceName
		{
			get
			{
				return SystemInfo.GetDeviceName();
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x00045A7C File Offset: 0x00043C7C
		public static string deviceModel
		{
			get
			{
				return SystemInfo.GetDeviceModel();
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00045A94 File Offset: 0x00043C94
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.SupportsAccelerometer();
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00045AAC File Offset: 0x00043CAC
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.IsGyroAvailable();
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00045AC4 File Offset: 0x00043CC4
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.SupportsLocationService();
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x00045ADC File Offset: 0x00043CDC
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.SupportsVibration();
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00045AF4 File Offset: 0x00043CF4
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.SupportsAudio();
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00045B0C File Offset: 0x00043D0C
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.GetDeviceType();
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x00045B24 File Offset: 0x00043D24
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.GetGraphicsMemorySize();
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00045B3C File Offset: 0x00043D3C
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceName();
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x00045B54 File Offset: 0x00043D54
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendor();
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x00045B6C File Offset: 0x00043D6C
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceID();
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x00045B84 File Offset: 0x00043D84
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVendorID();
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x00045B9C File Offset: 0x00043D9C
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceType();
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00045BB4 File Offset: 0x00043DB4
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.GetGraphicsUVStartsAtTop();
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00045BCC File Offset: 0x00043DCC
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.GetGraphicsDeviceVersion();
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x00045BE4 File Offset: 0x00043DE4
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.GetGraphicsShaderLevel();
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x00045BFC File Offset: 0x00043DFC
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.GetGraphicsMultiThreaded();
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00045C14 File Offset: 0x00043E14
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.GetRenderingThreadingMode();
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00045C2C File Offset: 0x00043E2C
		public static FoveatedRenderingCaps foveatedRenderingCaps
		{
			get
			{
				return SystemInfo.GetFoveatedRenderingCaps();
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x00045C44 File Offset: 0x00043E44
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.HasHiddenSurfaceRemovalOnGPU();
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00045C5C File Offset: 0x00043E5C
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.HasDynamicUniformArrayIndexingInFragmentShaders();
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x00045C74 File Offset: 0x00043E74
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.SupportsShadows();
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x00045C8C File Offset: 0x00043E8C
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.SupportsRawShadowDepthSampling();
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00045CA4 File Offset: 0x00043EA4
		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00045CB8 File Offset: 0x00043EB8
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.SupportsMotionVectors();
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x00045CD0 File Offset: 0x00043ED0
		[Obsolete("supportsRenderToCubemap always returns true, no need to call it")]
		public static bool supportsRenderToCubemap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00045CE4 File Offset: 0x00043EE4
		[Obsolete("supportsImageEffects always returns true, no need to call it")]
		public static bool supportsImageEffects
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x00045CF8 File Offset: 0x00043EF8
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.Supports3DTextures();
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x00045D10 File Offset: 0x00043F10
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.SupportsCompressed3DTextures();
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00045D28 File Offset: 0x00043F28
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.Supports2DArrayTextures();
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x00045D40 File Offset: 0x00043F40
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.Supports3DRenderTextures();
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00045D58 File Offset: 0x00043F58
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.SupportsCubemapArrayTextures();
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x00045D70 File Offset: 0x00043F70
		public static bool supportsAnisotropicFilter
		{
			get
			{
				return SystemInfo.SupportsAnisotropicFilter();
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x00045D88 File Offset: 0x00043F88
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.GetCopyTextureSupport();
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.SupportsComputeShaders();
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x00045DB8 File Offset: 0x00043FB8
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.SupportsGeometryShaders();
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x00045DD0 File Offset: 0x00043FD0
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.SupportsTessellationShaders();
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x00045DE8 File Offset: 0x00043FE8
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.SupportsRenderTargetArrayIndexFromVertexShader();
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x00045E00 File Offset: 0x00044000
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.SupportsInstancing();
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x00045E18 File Offset: 0x00044018
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.SupportsHardwareQuadTopology();
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x00045E30 File Offset: 0x00044030
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.Supports32bitsIndexBuffer();
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x00045E48 File Offset: 0x00044048
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.SupportsSparseTextures();
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00045E60 File Offset: 0x00044060
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.SupportedRenderTargetCount();
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00045E78 File Offset: 0x00044078
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.SupportsSeparatedRenderTargetsBlend();
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00045E90 File Offset: 0x00044090
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.SupportedRandomWriteTargetCount();
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x00045EA8 File Offset: 0x000440A8
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampledTextures();
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00045EC0 File Offset: 0x000440C0
		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return SystemInfo.SupportsMultisampled2DArrayTextures();
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x00045ED8 File Offset: 0x000440D8
		public static bool supportsMultisampledBackBuffer
		{
			get
			{
				return SystemInfo.SupportsMultisampledBackBuffer();
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00045EF0 File Offset: 0x000440F0
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.SupportsMultisampleAutoResolve();
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x00045F08 File Offset: 0x00044108
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.SupportsTextureWrapMirrorOnce();
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x00045F20 File Offset: 0x00044120
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.UsesReversedZBuffer();
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00045F38 File Offset: 0x00044138
		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00045F4C File Offset: 0x0004414C
		public static int maxTiledPixelStorageSize
		{
			get
			{
				return SystemInfo.MaxTiledPixelStorageSize();
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x00045F64 File Offset: 0x00044164
		public static bool supportsDynamicResolution
		{
			get
			{
				return SystemInfo.SupportsDynamicResolution();
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00045F7C File Offset: 0x0004417C
		private static bool IsValidEnumValue(Enum value)
		{
			bool flag = !Enum.IsDefined(value.GetType(), value);
			return !flag;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00045FA8 File Offset: 0x000441A8
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.HasRenderTextureNative(format);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00045FE0 File Offset: 0x000441E0
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsBlendingOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.SupportsBlendingOnRenderTextureFormatNative(format);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00046018 File Offset: 0x00044218
		public static bool SupportsRandomWriteOnRenderTextureFormat(RenderTextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsRandomWriteOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SystemInfo.SupportsRandomWriteOnRenderTextureFormatNative(format);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00046050 File Offset: 0x00044250
		public static bool SupportsTypedUAVShaderLoadStoreOnGraphicsFormat(GraphicsFormat graphicsFormat, bool isLoad)
		{
			bool flag = !SystemInfo.IsValidEnumValue(graphicsFormat);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTypedUAVShaderLoadStoreOnGraphicsFormat; format is not a valid GraphicsFormat");
			}
			return SystemInfo.SupportsTypedUAVShaderLoadStoreOnGraphicsFormatNative(graphicsFormat, isLoad);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00046088 File Offset: 0x00044288
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return SystemInfo.SupportsTextureFormatNative(format);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000460C0 File Offset: 0x000442C0
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			bool flag = !SystemInfo.IsValidEnumValue(format);
			if (flag)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; format is not a valid VertexAttributeFormat");
			}
			bool flag2 = dimension < 1 || dimension > 4;
			if (flag2)
			{
				throw new ArgumentException("Failed SupportsVertexAttributeFormat; dimension must be in 1..4 range");
			}
			return SystemInfo.SupportsVertexAttributeFormatNative(format, dimension);
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x00046110 File Offset: 0x00044310
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.GetNPOTSupport();
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00046128 File Offset: 0x00044328
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.GetMaxTextureSize();
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x00046140 File Offset: 0x00044340
		public static int maxTexture3DSize
		{
			get
			{
				return SystemInfo.GetMaxTexture3DSize();
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x00046158 File Offset: 0x00044358
		public static int maxTextureArraySlices
		{
			get
			{
				return SystemInfo.GetMaxTextureArraySlices();
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00046170 File Offset: 0x00044370
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.GetMaxCubemapSize();
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x00046188 File Offset: 0x00044388
		public static int maxAnisotropyLevel
		{
			get
			{
				return SystemInfo.GetMaxAnisotropyLevel();
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000461A0 File Offset: 0x000443A0
		internal static int maxRenderTextureSize
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				return SystemInfo.GetMaxRenderTextureSize();
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000461B8 File Offset: 0x000443B8
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsVertex();
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000461D0 File Offset: 0x000443D0
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsFragment();
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000461E8 File Offset: 0x000443E8
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsGeometry();
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00046200 File Offset: 0x00044400
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsDomain();
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x00046218 File Offset: 0x00044418
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsHull();
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x00046230 File Offset: 0x00044430
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.MaxComputeBufferInputsCompute();
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x00046248 File Offset: 0x00044448
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSize();
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x00046260 File Offset: 0x00044460
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeX();
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00046278 File Offset: 0x00044478
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeY();
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x00046290 File Offset: 0x00044490
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.GetMaxComputeWorkGroupSizeZ();
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000462A8 File Offset: 0x000444A8
		public static int computeSubGroupSize
		{
			get
			{
				return SystemInfo.GetComputeSubGroupSize();
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000462C0 File Offset: 0x000444C0
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.SupportsAsyncCompute();
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000462D8 File Offset: 0x000444D8
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.SupportsGpuRecorder();
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x000462F0 File Offset: 0x000444F0
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.SupportsGPUFence();
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00046308 File Offset: 0x00044508
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.SupportsAsyncGPUReadback();
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00046320 File Offset: 0x00044520
		public static bool supportsParallelPSOCreation
		{
			get
			{
				return SystemInfo.SupportsParallelPSOCreation();
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x00046338 File Offset: 0x00044538
		public static bool supportsRayTracingShaders
		{
			get
			{
				return SystemInfo.SupportsRayTracingShaders();
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x00046350 File Offset: 0x00044550
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.SupportsRayTracing();
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x00046368 File Offset: 0x00044568
		public static bool supportsInlineRayTracing
		{
			get
			{
				return SystemInfo.SupportsInlineRayTracing();
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00046380 File Offset: 0x00044580
		public static bool supportsIndirectDispatchRays
		{
			get
			{
				return SystemInfo.SupportsIndirectDispatchRays();
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x00046398 File Offset: 0x00044598
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.SupportsSetConstantBuffer();
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000463B0 File Offset: 0x000445B0
		public static int constantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.MinConstantBufferOffsetAlignment();
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000463C8 File Offset: 0x000445C8
		public static int maxConstantBufferSize
		{
			get
			{
				return SystemInfo.MaxConstantBufferSize();
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000463E0 File Offset: 0x000445E0
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.MaxGraphicsBufferSize();
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000463F8 File Offset: 0x000445F8
		[Obsolete("Use SystemInfo.constantBufferOffsetAlignment instead.")]
		public static bool minConstantBufferOffsetAlignment
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x0004640C File Offset: 0x0004460C
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.HasMipMaxLevel();
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x00046424 File Offset: 0x00044624
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.SupportsMipStreaming();
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0004643C File Offset: 0x0004463C
		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x00046450 File Offset: 0x00044650
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.UsesLoadStoreActions();
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00046468 File Offset: 0x00044668
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.GetHDRDisplaySupportFlags();
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x00046480 File Offset: 0x00044680
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.SupportsConservativeRaster();
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x00046498 File Offset: 0x00044698
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.SupportsMultiview();
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000464B0 File Offset: 0x000446B0
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.SupportsStoreAndResolveAction();
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000464C8 File Offset: 0x000446C8
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.SupportsMultisampleResolveDepth();
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000464E0 File Offset: 0x000446E0
		public static bool supportsMultisampleResolveStencil
		{
			get
			{
				return SystemInfo.SupportsMultisampleResolveStencil();
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000464F8 File Offset: 0x000446F8
		public static bool supportsIndirectArgumentsBuffer
		{
			get
			{
				return SystemInfo.SupportsIndirectArgumentsBuffer();
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00046510 File Offset: 0x00044710
		public static bool supportsDepthFetchInRenderPass
		{
			get
			{
				return SystemInfo.SupportsDepthFetchInRenderPass();
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00046528 File Offset: 0x00044728
		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060023E3 RID: 9187
		[FreeFunction("systeminfo::GetBatteryLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetBatteryLevel();

		// Token: 0x060023E4 RID: 9188
		[FreeFunction("systeminfo::GetBatteryStatus")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern BatteryStatus GetBatteryStatus();

		// Token: 0x060023E5 RID: 9189 RVA: 0x0004653C File Offset: 0x0004473C
		[FreeFunction("systeminfo::GetOperatingSystem")]
		private static string GetOperatingSystem()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetOperatingSystem_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023E6 RID: 9190
		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		// Token: 0x060023E7 RID: 9191 RVA: 0x0004656C File Offset: 0x0004476C
		[FreeFunction("systeminfo::GetProcessorType")]
		private static string GetProcessorType()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetProcessorType_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0004659C File Offset: 0x0004479C
		[FreeFunction("systeminfo::GetProcessorModel")]
		private static string GetProcessorModel()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetProcessorModel_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000465CC File Offset: 0x000447CC
		[FreeFunction("systeminfo::GetProcessorManufacturer")]
		private static string GetProcessorManufacturer()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetProcessorManufacturer_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023EA RID: 9194
		[FreeFunction("systeminfo::GetProcessorFrequencyMHz")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetProcessorFrequencyMHz();

		// Token: 0x060023EB RID: 9195
		[FreeFunction("systeminfo::GetProcessorCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetProcessorCount();

		// Token: 0x060023EC RID: 9196
		[FreeFunction("systeminfo::GetPhysicalMemoryMB")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPhysicalMemoryMB();

		// Token: 0x060023ED RID: 9197 RVA: 0x000465FC File Offset: 0x000447FC
		[FreeFunction("systeminfo::GetDeviceUniqueIdentifier")]
		private static string GetDeviceUniqueIdentifier()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetDeviceUniqueIdentifier_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x0004662C File Offset: 0x0004482C
		[FreeFunction("systeminfo::GetDeviceName")]
		private static string GetDeviceName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetDeviceName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x0004665C File Offset: 0x0004485C
		[FreeFunction("systeminfo::GetDeviceModel")]
		private static string GetDeviceModel()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetDeviceModel_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023F0 RID: 9200
		[FreeFunction("systeminfo::SupportsAccelerometer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAccelerometer();

		// Token: 0x060023F1 RID: 9201
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsGyroAvailable();

		// Token: 0x060023F2 RID: 9202
		[FreeFunction("systeminfo::SupportsLocationService")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsLocationService();

		// Token: 0x060023F3 RID: 9203
		[FreeFunction("systeminfo::SupportsVibration")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsVibration();

		// Token: 0x060023F4 RID: 9204
		[FreeFunction("systeminfo::SupportsAudio")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAudio();

		// Token: 0x060023F5 RID: 9205
		[FreeFunction("systeminfo::GetDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DeviceType GetDeviceType();

		// Token: 0x060023F6 RID: 9206
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMemorySize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsMemorySize();

		// Token: 0x060023F7 RID: 9207 RVA: 0x0004668C File Offset: 0x0004488C
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		private static string GetGraphicsDeviceName()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetGraphicsDeviceName_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000466BC File Offset: 0x000448BC
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		private static string GetGraphicsDeviceVendor()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetGraphicsDeviceVendor_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023F9 RID: 9209
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsDeviceID();

		// Token: 0x060023FA RID: 9210
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsDeviceVendorID();

		// Token: 0x060023FB RID: 9211
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		// Token: 0x060023FC RID: 9212
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGraphicsUVStartsAtTop();

		// Token: 0x060023FD RID: 9213 RVA: 0x000466EC File Offset: 0x000448EC
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVersion")]
		private static string GetGraphicsDeviceVersion()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SystemInfo.GetGraphicsDeviceVersion_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060023FE RID: 9214
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsShaderLevel();

		// Token: 0x060023FF RID: 9215
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMultiThreaded")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetGraphicsMultiThreaded();

		// Token: 0x06002400 RID: 9216
		[FreeFunction("ScriptingGraphicsCaps::GetRenderingThreadingMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderingThreadingMode GetRenderingThreadingMode();

		// Token: 0x06002401 RID: 9217
		[FreeFunction("ScriptingGraphicsCaps::GetFoveatedRenderingCaps")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FoveatedRenderingCaps GetFoveatedRenderingCaps();

		// Token: 0x06002402 RID: 9218
		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		// Token: 0x06002403 RID: 9219
		[FreeFunction("ScriptingGraphicsCaps::HasDynamicUniformArrayIndexingInFragmentShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasDynamicUniformArrayIndexingInFragmentShaders();

		// Token: 0x06002404 RID: 9220
		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsShadows();

		// Token: 0x06002405 RID: 9221
		[FreeFunction("ScriptingGraphicsCaps::SupportsRawShadowDepthSampling")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRawShadowDepthSampling();

		// Token: 0x06002406 RID: 9222
		[FreeFunction("SupportsMotionVectors")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMotionVectors();

		// Token: 0x06002407 RID: 9223
		[FreeFunction("ScriptingGraphicsCaps::Supports3DTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports3DTextures();

		// Token: 0x06002408 RID: 9224
		[FreeFunction("ScriptingGraphicsCaps::SupportsCompressed3DTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsCompressed3DTextures();

		// Token: 0x06002409 RID: 9225
		[FreeFunction("ScriptingGraphicsCaps::Supports2DArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports2DArrayTextures();

		// Token: 0x0600240A RID: 9226
		[FreeFunction("ScriptingGraphicsCaps::Supports3DRenderTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports3DRenderTextures();

		// Token: 0x0600240B RID: 9227
		[FreeFunction("ScriptingGraphicsCaps::SupportsCubemapArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsCubemapArrayTextures();

		// Token: 0x0600240C RID: 9228
		[FreeFunction("ScriptingGraphicsCaps::SupportsAnisotropicFilter")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAnisotropicFilter();

		// Token: 0x0600240D RID: 9229
		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		// Token: 0x0600240E RID: 9230
		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsComputeShaders();

		// Token: 0x0600240F RID: 9231
		[FreeFunction("ScriptingGraphicsCaps::SupportsGeometryShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGeometryShaders();

		// Token: 0x06002410 RID: 9232
		[FreeFunction("ScriptingGraphicsCaps::SupportsTessellationShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTessellationShaders();

		// Token: 0x06002411 RID: 9233
		[FreeFunction("ScriptingGraphicsCaps::SupportsRenderTargetArrayIndexFromVertexShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRenderTargetArrayIndexFromVertexShader();

		// Token: 0x06002412 RID: 9234
		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsInstancing();

		// Token: 0x06002413 RID: 9235
		[FreeFunction("ScriptingGraphicsCaps::SupportsHardwareQuadTopology")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsHardwareQuadTopology();

		// Token: 0x06002414 RID: 9236
		[FreeFunction("ScriptingGraphicsCaps::Supports32bitsIndexBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Supports32bitsIndexBuffer();

		// Token: 0x06002415 RID: 9237
		[FreeFunction("ScriptingGraphicsCaps::SupportsSparseTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSparseTextures();

		// Token: 0x06002416 RID: 9238
		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportedRenderTargetCount();

		// Token: 0x06002417 RID: 9239
		[FreeFunction("ScriptingGraphicsCaps::SupportsSeparatedRenderTargetsBlend")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSeparatedRenderTargetsBlend();

		// Token: 0x06002418 RID: 9240
		[FreeFunction("ScriptingGraphicsCaps::SupportedRandomWriteTargetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportedRandomWriteTargetCount();

		// Token: 0x06002419 RID: 9241
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsVertex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsVertex();

		// Token: 0x0600241A RID: 9242
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsFragment")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsFragment();

		// Token: 0x0600241B RID: 9243
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsGeometry")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsGeometry();

		// Token: 0x0600241C RID: 9244
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsDomain")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsDomain();

		// Token: 0x0600241D RID: 9245
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsHull")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsHull();

		// Token: 0x0600241E RID: 9246
		[FreeFunction("ScriptingGraphicsCaps::MaxComputeBufferInputsCompute")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxComputeBufferInputsCompute();

		// Token: 0x0600241F RID: 9247
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportsMultisampledTextures();

		// Token: 0x06002420 RID: 9248
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampled2DArrayTextures")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampled2DArrayTextures();

		// Token: 0x06002421 RID: 9249
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledBackBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampledBackBuffer();

		// Token: 0x06002422 RID: 9250
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleAutoResolve();

		// Token: 0x06002423 RID: 9251
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureWrapMirrorOnce")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SupportsTextureWrapMirrorOnce();

		// Token: 0x06002424 RID: 9252
		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesReversedZBuffer();

		// Token: 0x06002425 RID: 9253
		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		// Token: 0x06002426 RID: 9254
		[FreeFunction("ScriptingGraphicsCaps::SupportsBlendingOnRenderTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsBlendingOnRenderTextureFormatNative(RenderTextureFormat format);

		// Token: 0x06002427 RID: 9255
		[FreeFunction("ScriptingGraphicsCaps::SupportsTypedUAVShaderLoadStoreOnGraphicsFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTypedUAVShaderLoadStoreOnGraphicsFormatNative(GraphicsFormat graphicsFormat, bool isLoad);

		// Token: 0x06002428 RID: 9256
		[FreeFunction("ScriptingGraphicsCaps::SupportsRandomWriteOnRenderTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRandomWriteOnRenderTextureFormatNative(RenderTextureFormat format);

		// Token: 0x06002429 RID: 9257
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		// Token: 0x0600242A RID: 9258
		[FreeFunction("ScriptingGraphicsCaps::SupportsVertexAttributeFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsVertexAttributeFormatNative(VertexAttributeFormat format, int dimension);

		// Token: 0x0600242B RID: 9259
		[FreeFunction("ScriptingGraphicsCaps::GetNPOTSupport")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NPOTSupport GetNPOTSupport();

		// Token: 0x0600242C RID: 9260
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxTextureSize();

		// Token: 0x0600242D RID: 9261
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTexture3DSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxTexture3DSize();

		// Token: 0x0600242E RID: 9262
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureArraySlices")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxTextureArraySlices();

		// Token: 0x0600242F RID: 9263
		[FreeFunction("ScriptingGraphicsCaps::GetMaxCubemapSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxCubemapSize();

		// Token: 0x06002430 RID: 9264
		[FreeFunction("ScriptingGraphicsCaps::GetMaxAnisotropyLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxAnisotropyLevel();

		// Token: 0x06002431 RID: 9265
		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxRenderTextureSize();

		// Token: 0x06002432 RID: 9266
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSize();

		// Token: 0x06002433 RID: 9267
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeX")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeX();

		// Token: 0x06002434 RID: 9268
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeY")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeY();

		// Token: 0x06002435 RID: 9269
		[FreeFunction("ScriptingGraphicsCaps::GetMaxComputeWorkGroupSizeZ")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxComputeWorkGroupSizeZ();

		// Token: 0x06002436 RID: 9270
		[FreeFunction("ScriptingGraphicsCaps::GetComputeSubGroupSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComputeSubGroupSize();

		// Token: 0x06002437 RID: 9271
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncCompute")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAsyncCompute();

		// Token: 0x06002438 RID: 9272
		[FreeFunction("ScriptingGraphicsCaps::SupportsGpuRecorder")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGpuRecorder();

		// Token: 0x06002439 RID: 9273
		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsGPUFence();

		// Token: 0x0600243A RID: 9274
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncGPUReadback")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsAsyncGPUReadback();

		// Token: 0x0600243B RID: 9275
		[FreeFunction("ScriptingGraphicsCaps::SupportsParallelPSOCreation")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsParallelPSOCreation();

		// Token: 0x0600243C RID: 9276
		[FreeFunction("ScriptingGraphicsCaps::SupportsRayTracing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRayTracing();

		// Token: 0x0600243D RID: 9277
		[FreeFunction("ScriptingGraphicsCaps::SupportsRayTracingShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsRayTracingShaders();

		// Token: 0x0600243E RID: 9278
		[FreeFunction("ScriptingGraphicsCaps::SupportsInlineRayTracing")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsInlineRayTracing();

		// Token: 0x0600243F RID: 9279
		[FreeFunction("ScriptingGraphicsCaps::SupportsIndirectDispatchRays")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsIndirectDispatchRays();

		// Token: 0x06002440 RID: 9280
		[FreeFunction("ScriptingGraphicsCaps::SupportsSetConstantBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsSetConstantBuffer();

		// Token: 0x06002441 RID: 9281
		[FreeFunction("ScriptingGraphicsCaps::MinConstantBufferOffsetAlignment")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MinConstantBufferOffsetAlignment();

		// Token: 0x06002442 RID: 9282
		[FreeFunction("ScriptingGraphicsCaps::MaxConstantBufferSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxConstantBufferSize();

		// Token: 0x06002443 RID: 9283
		[FreeFunction("ScriptingGraphicsCaps::MaxGraphicsBufferSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long MaxGraphicsBufferSize();

		// Token: 0x06002444 RID: 9284
		[FreeFunction("ScriptingGraphicsCaps::HasMipMaxLevel")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMipMaxLevel();

		// Token: 0x06002445 RID: 9285
		[FreeFunction("ScriptingGraphicsCaps::SupportsMipStreaming")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMipStreaming();

		// Token: 0x06002446 RID: 9286 RVA: 0x0004671C File Offset: 0x0004491C
		[Obsolete("Use overload with a GraphicsFormatUsage parameter instead", false)]
		public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			GraphicsFormatUsage graphicsFormatUsage = (GraphicsFormatUsage)(1 << (int)usage);
			return SystemInfo.IsFormatSupported(format, graphicsFormatUsage);
		}

		// Token: 0x06002447 RID: 9287
		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsFormatSupported(GraphicsFormat format, GraphicsFormatUsage usage);

		// Token: 0x06002448 RID: 9288 RVA: 0x0004673C File Offset: 0x0004493C
		[Obsolete("Use overload with a GraphicsFormatUsage parameter instead", false)]
		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			GraphicsFormatUsage graphicsFormatUsage = (GraphicsFormatUsage)(1 << (int)usage);
			return SystemInfo.GetCompatibleFormat(format, graphicsFormatUsage);
		}

		// Token: 0x06002449 RID: 9289
		[FreeFunction("ScriptingGraphicsCaps::GetCompatibleFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetCompatibleFormat(GraphicsFormat format, GraphicsFormatUsage usage);

		// Token: 0x0600244A RID: 9290
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsFormat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GraphicsFormat GetGraphicsFormat(DefaultFormat format);

		// Token: 0x0600244B RID: 9291 RVA: 0x0004675C File Offset: 0x0004495C
		[FreeFunction("ScriptingGraphicsCaps::GetRenderTextureSupportedMSAASampleCount")]
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount_Injected(ref desc);
		}

		// Token: 0x0600244C RID: 9292
		[FreeFunction("ScriptingGraphicsCaps::GetTiledRenderTargetStorageSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetTiledRenderTargetStorageSize(GraphicsFormat format, int sampleCount);

		// Token: 0x0600244D RID: 9293
		[FreeFunction("ScriptingGraphicsCaps::UsesLoadStoreActions")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UsesLoadStoreActions();

		// Token: 0x0600244E RID: 9294
		[FreeFunction("ScriptingGraphicsCaps::GetHDRDisplaySupportFlags")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HDRDisplaySupportFlags GetHDRDisplaySupportFlags();

		// Token: 0x0600244F RID: 9295
		[FreeFunction("ScriptingGraphicsCaps::SupportsConservativeRaster")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsConservativeRaster();

		// Token: 0x06002450 RID: 9296
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultiview")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultiview();

		// Token: 0x06002451 RID: 9297
		[FreeFunction("ScriptingGraphicsCaps::SupportsStoreAndResolveAction")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsStoreAndResolveAction();

		// Token: 0x06002452 RID: 9298
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleResolveDepth")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleResolveDepth();

		// Token: 0x06002453 RID: 9299
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleResolveStencil")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsMultisampleResolveStencil();

		// Token: 0x06002454 RID: 9300
		[FreeFunction("ScriptingGraphicsCaps::SupportsIndirectArgumentsBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsIndirectArgumentsBuffer();

		// Token: 0x06002455 RID: 9301
		[FreeFunction("ScriptingGraphicsCaps::SupportsDepthFetchInRenderPass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsDepthFetchInRenderPass();

		// Token: 0x06002456 RID: 9302
		[FreeFunction("ScriptingGraphicsCaps::MaxTiledPixelStorageSize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int MaxTiledPixelStorageSize();

		// Token: 0x06002457 RID: 9303
		[FreeFunction("ScriptingGraphicsCaps::SupportsDynamicResolution")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SupportsDynamicResolution();

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x00046770 File Offset: 0x00044970
		[Obsolete("SystemInfo.supportsGPUFence has been deprecated, use SystemInfo.supportsGraphicsFence instead (UnityUpgradable) ->  supportsGraphicsFence", true)]
		public static bool supportsGPUFence
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600245A RID: 9306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOperatingSystem_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600245B RID: 9307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetProcessorType_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600245C RID: 9308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetProcessorModel_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600245D RID: 9309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetProcessorManufacturer_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600245E RID: 9310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceUniqueIdentifier_Injected(out ManagedSpanWrapper ret);

		// Token: 0x0600245F RID: 9311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002460 RID: 9312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceModel_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002461 RID: 9313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsDeviceName_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002462 RID: 9314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsDeviceVendor_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002463 RID: 9315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsDeviceVersion_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06002464 RID: 9316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRenderTextureSupportedMSAASampleCount_Injected([In] ref RenderTextureDescriptor desc);

		// Token: 0x04000A51 RID: 2641
		public const string unsupportedIdentifier = "n/a";
	}
}
