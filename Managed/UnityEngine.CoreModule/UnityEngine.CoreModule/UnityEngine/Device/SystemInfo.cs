using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine.Device
{
	// Token: 0x02000464 RID: 1124
	public static class SystemInfo
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000535A2 File Offset: 0x000517A2
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.batteryLevel;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000535A9 File Offset: 0x000517A9
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.batteryStatus;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000535B0 File Offset: 0x000517B0
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.operatingSystem;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000535B7 File Offset: 0x000517B7
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.operatingSystemFamily;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000535BE File Offset: 0x000517BE
		public static string processorType
		{
			get
			{
				return SystemInfo.processorType;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000535C5 File Offset: 0x000517C5
		public static string ProcessorModel
		{
			get
			{
				return SystemInfo.processorModel;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000535CC File Offset: 0x000517CC
		public static string ProcessorManufacturer
		{
			get
			{
				return SystemInfo.processorManufacturer;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000535D3 File Offset: 0x000517D3
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.processorFrequency;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000535DA File Offset: 0x000517DA
		public static int processorCount
		{
			get
			{
				return SystemInfo.processorCount;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000535E1 File Offset: 0x000517E1
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.systemMemorySize;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000535E8 File Offset: 0x000517E8
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000535EF File Offset: 0x000517EF
		public static string deviceName
		{
			get
			{
				return SystemInfo.deviceName;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000535F6 File Offset: 0x000517F6
		public static string deviceModel
		{
			get
			{
				return SystemInfo.deviceModel;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000535FD File Offset: 0x000517FD
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.supportsAccelerometer;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x00053604 File Offset: 0x00051804
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.supportsGyroscope;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x0005360B File Offset: 0x0005180B
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.supportsLocationService;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x00053612 File Offset: 0x00051812
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.supportsVibration;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x00053619 File Offset: 0x00051819
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.supportsAudio;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x00053620 File Offset: 0x00051820
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.deviceType;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x00053627 File Offset: 0x00051827
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.graphicsMemorySize;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x0005362E File Offset: 0x0005182E
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.graphicsDeviceName;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x00053635 File Offset: 0x00051835
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.graphicsDeviceVendor;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x0005363C File Offset: 0x0005183C
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.graphicsDeviceID;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x00053643 File Offset: 0x00051843
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.graphicsDeviceVendorID;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0005364A File Offset: 0x0005184A
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.graphicsDeviceType;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x00053651 File Offset: 0x00051851
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.graphicsUVStartsAtTop;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x00053658 File Offset: 0x00051858
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.graphicsDeviceVersion;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x0005365F File Offset: 0x0005185F
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.graphicsShaderLevel;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x00053666 File Offset: 0x00051866
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.graphicsMultiThreaded;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x0005366D File Offset: 0x0005186D
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.renderingThreadingMode;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x00053674 File Offset: 0x00051874
		public static FoveatedRenderingCaps foveatedRenderingCaps
		{
			get
			{
				return SystemInfo.foveatedRenderingCaps;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x0005367B File Offset: 0x0005187B
		public static int maxTiledPixelStorageSize
		{
			get
			{
				return SystemInfo.maxTiledPixelStorageSize;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00053682 File Offset: 0x00051882
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.hasHiddenSurfaceRemovalOnGPU;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x00053689 File Offset: 0x00051889
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x00053690 File Offset: 0x00051890
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.supportsShadows;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x00053697 File Offset: 0x00051897
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.supportsRawShadowDepthSampling;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x0005369E File Offset: 0x0005189E
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.supportsMotionVectors;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000536A5 File Offset: 0x000518A5
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.supports3DTextures;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000536AC File Offset: 0x000518AC
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.supportsCompressed3DTextures;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000536B3 File Offset: 0x000518B3
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.supports2DArrayTextures;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000536BA File Offset: 0x000518BA
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.supports3DRenderTextures;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000536C1 File Offset: 0x000518C1
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.supportsCubemapArrayTextures;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000536C8 File Offset: 0x000518C8
		public static bool supportsAnisotropicFilter
		{
			get
			{
				return SystemInfo.supportsAnisotropicFilter;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000536CF File Offset: 0x000518CF
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.copyTextureSupport;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000536D6 File Offset: 0x000518D6
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000536DD File Offset: 0x000518DD
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.supportsGeometryShaders;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000536E4 File Offset: 0x000518E4
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.supportsTessellationShaders;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000536EB File Offset: 0x000518EB
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.supportsRenderTargetArrayIndexFromVertexShader;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000536F2 File Offset: 0x000518F2
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.supportsInstancing;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000536F9 File Offset: 0x000518F9
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.supportsHardwareQuadTopology;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x00053700 File Offset: 0x00051900
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.supports32bitsIndexBuffer;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x00053707 File Offset: 0x00051907
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.supportsSparseTextures;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x0005370E File Offset: 0x0005190E
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.supportedRenderTargetCount;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x00053715 File Offset: 0x00051915
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.supportsSeparatedRenderTargetsBlend;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x0005371C File Offset: 0x0005191C
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.supportedRandomWriteTargetCount;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x00053723 File Offset: 0x00051923
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.supportsMultisampledTextures;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x0005372A File Offset: 0x0005192A
		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return SystemInfo.supportsMultisampled2DArrayTextures;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x00053731 File Offset: 0x00051931
		public static bool supportsMultisampledBackBuffer
		{
			get
			{
				return SystemInfo.supportsMultisampledBackBuffer;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x00053738 File Offset: 0x00051938
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.supportsMultisampleAutoResolve;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x0005373F File Offset: 0x0005193F
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.supportsTextureWrapMirrorOnce;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00053746 File Offset: 0x00051946
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.usesReversedZBuffer;
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x00053750 File Offset: 0x00051950
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRenderTextureFormat(format);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00053768 File Offset: 0x00051968
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsBlendingOnRenderTextureFormat(format);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00053780 File Offset: 0x00051980
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			return SystemInfo.SupportsTextureFormat(format);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x00053798 File Offset: 0x00051998
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			return SystemInfo.SupportsVertexAttributeFormat(format, dimension);
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000537B1 File Offset: 0x000519B1
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.npotSupport;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000537B8 File Offset: 0x000519B8
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.maxTextureSize;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000537BF File Offset: 0x000519BF
		public static int maxTexture3DSize
		{
			get
			{
				return SystemInfo.maxTexture3DSize;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000537C6 File Offset: 0x000519C6
		public static int maxTextureArraySlices
		{
			get
			{
				return SystemInfo.maxTextureArraySlices;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000537CD File Offset: 0x000519CD
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.maxCubemapSize;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000537D4 File Offset: 0x000519D4
		public static int maxAnisotropyLevel
		{
			get
			{
				return SystemInfo.maxAnisotropyLevel;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000537DB File Offset: 0x000519DB
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsVertex;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000537E2 File Offset: 0x000519E2
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsFragment;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000537E9 File Offset: 0x000519E9
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsGeometry;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000537F0 File Offset: 0x000519F0
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsDomain;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x000537F7 File Offset: 0x000519F7
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsHull;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000537FE File Offset: 0x000519FE
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsCompute;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x00053805 File Offset: 0x00051A05
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSize;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x0005380C File Offset: 0x00051A0C
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeX;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x00053813 File Offset: 0x00051A13
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeY;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x0005381A File Offset: 0x00051A1A
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeZ;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x00053821 File Offset: 0x00051A21
		public static int computeSubGroupSize
		{
			get
			{
				return SystemInfo.computeSubGroupSize;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x00053828 File Offset: 0x00051A28
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.supportsAsyncCompute;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x0005382F File Offset: 0x00051A2F
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.supportsGpuRecorder;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x00053836 File Offset: 0x00051A36
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.supportsGraphicsFence;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x0005383D File Offset: 0x00051A3D
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.supportsAsyncGPUReadback;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x00053844 File Offset: 0x00051A44
		public static bool supportsParallelPSOCreation
		{
			get
			{
				return SystemInfo.supportsParallelPSOCreation;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x0005384B File Offset: 0x00051A4B
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.supportsRayTracing;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x00053852 File Offset: 0x00051A52
		public static bool supportsRayTracingShaders
		{
			get
			{
				return SystemInfo.supportsRayTracingShaders;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x00053859 File Offset: 0x00051A59
		public static bool supportsInlineRayTracing
		{
			get
			{
				return SystemInfo.supportsInlineRayTracing;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x00053860 File Offset: 0x00051A60
		public static bool supportsIndirectDispatchRays
		{
			get
			{
				return SystemInfo.supportsIndirectDispatchRays;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x00053867 File Offset: 0x00051A67
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.supportsSetConstantBuffer;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x0005386E File Offset: 0x00051A6E
		public static int constantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.constantBufferOffsetAlignment;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x00053875 File Offset: 0x00051A75
		public static int maxConstantBufferSize
		{
			get
			{
				return SystemInfo.maxConstantBufferSize;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x0005387C File Offset: 0x00051A7C
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.maxGraphicsBufferSize;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x00053883 File Offset: 0x00051A83
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.hasMipMaxLevel;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x0005388A File Offset: 0x00051A8A
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.supportsMipStreaming;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x00053891 File Offset: 0x00051A91
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.usesLoadStoreActions;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x00053898 File Offset: 0x00051A98
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.hdrDisplaySupportFlags;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x0005389F File Offset: 0x00051A9F
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.supportsConservativeRaster;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000538A6 File Offset: 0x00051AA6
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.supportsMultiview;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000538AD File Offset: 0x00051AAD
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.supportsStoreAndResolveAction;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000538B4 File Offset: 0x00051AB4
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.supportsMultisampleResolveDepth;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000538BB File Offset: 0x00051ABB
		public static bool supportsMultisampleResolveStencil
		{
			get
			{
				return SystemInfo.supportsMultisampleResolveStencil;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000538C2 File Offset: 0x00051AC2
		public static bool supportsIndirectArgumentsBuffer
		{
			get
			{
				return SystemInfo.supportsIndirectArgumentsBuffer;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000538C9 File Offset: 0x00051AC9
		public static bool supportsDepthFetchInRenderPass
		{
			get
			{
				return SystemInfo.supportsDepthFetchInRenderPass;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000538D0 File Offset: 0x00051AD0
		public static bool supportsDynamicResolution
		{
			get
			{
				return SystemInfo.supportsDynamicResolution;
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000538D8 File Offset: 0x00051AD8
		[Obsolete("Use overload with a GraphicsFormatUsage parameter instead", false)]
		public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			GraphicsFormatUsage graphicsFormatUsage = (GraphicsFormatUsage)(1 << (int)usage);
			return SystemInfo.IsFormatSupported(format, graphicsFormatUsage);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000538F8 File Offset: 0x00051AF8
		public static bool IsFormatSupported(GraphicsFormat format, GraphicsFormatUsage usage)
		{
			return SystemInfo.IsFormatSupported(format, usage);
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x00053914 File Offset: 0x00051B14
		[Obsolete("Use overload with a GraphicsFormatUsage parameter instead", false)]
		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			GraphicsFormatUsage graphicsFormatUsage = (GraphicsFormatUsage)(1 << (int)usage);
			return SystemInfo.GetCompatibleFormat(format, graphicsFormatUsage);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x00053934 File Offset: 0x00051B34
		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, GraphicsFormatUsage usage)
		{
			return SystemInfo.GetCompatibleFormat(format, usage);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x00053950 File Offset: 0x00051B50
		public static GraphicsFormat GetGraphicsFormat(DefaultFormat format)
		{
			return SystemInfo.GetGraphicsFormat(format);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x00053968 File Offset: 0x00051B68
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount(desc);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x00053980 File Offset: 0x00051B80
		public static bool SupportsRandomWriteOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRandomWriteOnRenderTextureFormat(format);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x00053998 File Offset: 0x00051B98
		public static bool SupportsTypedUAVShaderLoadStoreOnGraphicsFormat(GraphicsFormat graphicsFormat, bool isLoad)
		{
			return SystemInfo.SupportsTypedUAVShaderLoadStoreOnGraphicsFormat(graphicsFormat, isLoad);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000539B4 File Offset: 0x00051BB4
		public static int GetTiledRenderTargetStorageSize(GraphicsFormat format, int sampleCount)
		{
			return SystemInfo.GetTiledRenderTargetStorageSize(format, sampleCount);
		}

		// Token: 0x04000C46 RID: 3142
		public const string unsupportedIdentifier = "n/a";
	}
}
