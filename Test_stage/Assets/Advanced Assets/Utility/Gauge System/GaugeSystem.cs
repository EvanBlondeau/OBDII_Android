namespace AdvancedAssets.Utility.GaugeSystem
{
	using UnityEngine;
	using UnityEngine.UI;
	#if UNITY_EDITOR
	using UnityEditor;
	#endif
	using System.Collections.Generic;
	[AddComponentMenu("Advanced Assets/Utility/Gauge System/Gauge System",29),ExecuteInEditMode]
	public class GaugeSystem : MonoBehaviour
	{
		public enum UpdateMode {OnAwake,EveryFrame,ViaScripting}
		public enum DimensionType {TwoDimensional,ThreeDimensional}
		public enum GaugeType {Analog,Digital}
		public enum ThreeDimensionalType {SpriteRenderer,MeshRenderer}
		public enum DigitalType {Activation,Color}
		public DimensionType dimensionType = DimensionType.TwoDimensional;
		public GaugeType gaugeType = GaugeType.Analog;
		public ThreeDimensionalType threeDimensionalType = ThreeDimensionalType.SpriteRenderer;
		public DigitalType digitalType = DigitalType.Activation;
		public Material primaryMaterial = null;
		public Material secondaryMaterial = null;
		public Material needleMaterial = null;
		public UpdateMode linesUpdateMode = UpdateMode.EveryFrame;
		public Transform lineHandler = null;
		public int lineCount = 2;
		#if UNITY_EDITOR
		[HideInInspector] public bool applyValuesAutomatically = false;
		[HideInInspector] public byte valuesCount = 5;
		#endif
		public List<float> values = new List<float>();
		public bool useCustomLine = false;
		public GameObject bigLine = null;
		public GameObject smallLine = null;
		[Range(-360,360)] public float minimumAngle = 90;
		[Range(-360,360)] public float maximumAngle = -90;
		public float minimumValue = 0;
		public float value = 0;
		public float maximumValue = 100;
		public bool clamp = true;
		public bool useNeedle = false;
		public UpdateMode needleUpdateMode = UpdateMode.EveryFrame;
		public Transform needleHandler = null;
		public bool useCustomNeedle = false;
		public GameObject needle = null;
		public bool useText = false;
		public UpdateMode textUpdateMode = UpdateMode.EveryFrame;
		public bool createTextAutomatically = false;
		public bool absolute = true;
		public Vector2 textOffset = Vector2.zero;
		public Vector2 textSize = Vector2.one;
		public Font textFont = null;
		public Color textColor = Color.white;
		#if UNITY_5_3_OR_NEWER
		[Delayed] public string textName = string.Empty;
		#else
		public string textName = string.Empty;
		#endif
		public Transform text = null;
		public Vector2 bigLinePosition = Vector2.zero;
		public Vector2 smallLinePosition = Vector2.zero;
		public Vector2 lineTextPosition = Vector2.zero;
		public bool useNeedlePosition = false;
		public Vector2 needlePosition = Vector2.zero;
		public Vector2 bigLineSize = Vector2.one;
		public Vector2 smallLineSize = Vector2.one;
		public Vector2 lineTextSize = Vector2.one;
		public bool useNeedleSize = false;
		public Vector2 needleSize = Vector2.one;
		public bool useNeedlePivotSize = false;
		public Vector2 needlePivotSize = Vector2.one;
		public bool useBigLineColor = false;
		public Color bigLineColor = Color.white;
		public bool useSmallLineColor = false;
		public Color smallLineColor = Color.white;
		public Color lineColorOn = Color.white;
		public Color lineColorOff = Color.gray;
		public bool useNeedleColor = false;
		public Color needleColor = Color.red;
		public bool useLineTextColor = false;
		public Color lineTextColor = Color.white;
		public bool useLineText = true;
		public bool useWorldTextRotation = true;
		#if UNITY_EDITOR
		[HideInInspector] public int valuesScrollViewIndex = 0;
		[HideInInspector] public bool valuesIsExpanded = true;
		[HideInInspector] public Vector2 valuesScrollView = Vector2.zero;
		#endif
		[HideInInspector,SerializeField] private bool onTwoDimensionalSwitch = false;
		[HideInInspector,SerializeField] private bool onThreeDimensionalSwitch = false;
		[HideInInspector,SerializeField] private bool onAnalogSwitch = false;
		[HideInInspector,SerializeField] private bool onDigitalSwitch = false;
		[HideInInspector,SerializeField] private bool onCustomLineSwitch = false;
		[HideInInspector,SerializeField] private bool isTwoDimensionalSwitch = false;
		[HideInInspector,SerializeField] private bool isThreeDimensionalSwitch = false;
		[HideInInspector,SerializeField] private bool isAnalogSwitch = false;
		[HideInInspector,SerializeField] private bool isDigitalSwitch = false;
		[HideInInspector,SerializeField] private bool isCustomLineSwitch = false;
		[HideInInspector,SerializeField] private GameObject bigLine2D = null;
		[HideInInspector,SerializeField] private GameObject bigLine3DSpriteRenderer = null;
		[HideInInspector,SerializeField] private GameObject bigLine3DMeshRenderer = null;
		[HideInInspector,SerializeField] private GameObject smallLine2D = null;
		[HideInInspector,SerializeField] private GameObject smallLine3DSpriteRenderer = null;
		[HideInInspector,SerializeField] private GameObject smallLine3DMeshRenderer = null;
		[HideInInspector,SerializeField] private GameObject needle2D = null;
		[HideInInspector,SerializeField] private GameObject needle3DSpriteRenderer = null;
		[HideInInspector,SerializeField] private GameObject needle3DMeshRenderer = null;
		[HideInInspector,SerializeField] private GameObject bigLineTracker = null;
		[HideInInspector,SerializeField] private GameObject smallLineTracker = null;
		[HideInInspector,SerializeField] private GameObject needleTracker = null;
		[HideInInspector] private Transform[] lineTransforms = new Transform[0];
		[HideInInspector] private Transform needleTransform = null;
		[HideInInspector] private bool clearLines = false;
		[HideInInspector] private bool linesIsUpdating = false;
		[HideInInspector] private bool needleIsUpdating = false;
		[HideInInspector] private bool textIsUpdating = false;
		#if UNITY_EDITOR
		[MenuItem("Advanced Assets/Utility/Gauge System/Gauge System",false,29)]
		private static void AddComponent ()
		{
			for(int a = 0,A = Selection.gameObjects.Length; a < A; a++)
				Undo.AddComponent(Selection.gameObjects[a],typeof(GaugeSystem));
		}
		[MenuItem("Advanced Assets/Utility/Gauge System/Gauge System",true)]
		private static bool AddComponentCheck () {return Selection.gameObjects.Length > 0;}
		#endif
		private void Awake ()
		{
			if(!Application.isPlaying || linesUpdateMode != UpdateMode.OnAwake || needleUpdateMode != UpdateMode.OnAwake || textUpdateMode != UpdateMode.OnAwake)return;
			if(linesUpdateMode == UpdateMode.OnAwake)linesIsUpdating = true;
			if(needleUpdateMode == UpdateMode.OnAwake)needleIsUpdating = true;
			if(textUpdateMode == UpdateMode.OnAwake)textIsUpdating = true;
			Update();
		}
		private void Update ()
		{
			EditorHandler();
			if(Application.isPlaying)
			{
				CheckHandler();
				ExecuteHandler();
			}
		}
		private void EditorHandler ()
		{
			SwitchHandler();
			lineCount = Mathf.Clamp(lineCount,0,50);
			#if UNITY_EDITOR
			valuesCount = (byte)Mathf.Clamp(valuesCount,2,100);
			#endif
			minimumAngle = Mathf.Clamp(minimumAngle,-360,360);
			maximumAngle = Mathf.Clamp(maximumAngle,-360,360);
			maximumValue = Mathf.Clamp(maximumValue,0.1f,float.MaxValue);
			lineHandler = TransformHandler(lineHandler,"Line Handler");
			if(clamp)value = Mathf.Clamp(value,minimumValue,maximumValue);
			if(useNeedle)needleHandler = TransformHandler(needleHandler,"Needle Handler");
			else
			{
				if(useNeedlePosition)useNeedlePosition = false;
				if(useNeedleSize)useNeedleSize = false;
				if(useNeedlePivotSize)useNeedlePivotSize = false;
				if(useNeedleColor)useNeedleColor = false;
			}
			if(gaugeType == GaugeType.Analog)
			{
				if(!useNeedle)
				{
					if(needle)needle = null;
					if(needleHandler)
					{
						DestroyImmediate(needleHandler.gameObject);
						needleHandler = null;
					}
				}
				if(text)
				{
					DestroyImmediate(text.gameObject);
					text = null;
				}
				if(useText)useText = false;
			}
			if(gaugeType == GaugeType.Digital)
			{
				if(needleTransform)
				{
					DestroyImmediate(needleTransform.gameObject);
					needleTransform = null;
				}
				if(needleHandler)
				{
					DestroyImmediate(needleHandler.gameObject);
					needleHandler = null;
				}
				if(!useText && text)text = null;
				if(needle)needle = null;
				if(useNeedle)useNeedle = false;
				if(useNeedlePosition)useNeedlePosition = false;
				if(useNeedleSize)useNeedleSize = false;
				if(useNeedlePivotSize)useNeedlePivotSize = false;
				if(useNeedleColor)useNeedleColor = false;
			}
			if(dimensionType == DimensionType.TwoDimensional)
			{
				if(!useCustomLine && bigLine != bigLine2D || bigLine == smallLine2D || bigLine == needle2D || bigLine == bigLine3DSpriteRenderer || bigLine == bigLine3DMeshRenderer || bigLine == smallLine3DSpriteRenderer || bigLine == smallLine3DMeshRenderer || bigLine == needle3DSpriteRenderer || bigLine == needle3DMeshRenderer || !bigLine)
					bigLine = bigLine2D;
				if(!useCustomLine && smallLine != smallLine2D || smallLine == bigLine2D || smallLine == needle2D || smallLine == bigLine3DSpriteRenderer || smallLine == bigLine3DMeshRenderer || smallLine == smallLine3DSpriteRenderer || smallLine == smallLine3DMeshRenderer || smallLine == needle3DSpriteRenderer || smallLine == needle3DMeshRenderer || !smallLine)
					smallLine = smallLine2D;
				if(gaugeType == GaugeType.Analog && useNeedle && (!useCustomLine && needle != needle2D || needle == bigLine2D || needle == smallLine2D || needle == bigLine3DSpriteRenderer || needle == bigLine3DMeshRenderer || needle == smallLine3DSpriteRenderer || needle == smallLine3DMeshRenderer || needle == needle3DSpriteRenderer || needle == needle3DMeshRenderer || !needle))
					needle = needle2D;
				if(primaryMaterial)
				{
					DestroyImmediate(primaryMaterial);
					primaryMaterial = null;
				}
				if(secondaryMaterial)
				{
					DestroyImmediate(secondaryMaterial);
					secondaryMaterial = null;
				}
				if(needleMaterial)
				{
					DestroyImmediate(needleMaterial);
					needleMaterial = null;
				}
			}
			if(dimensionType == DimensionType.ThreeDimensional)
			{
				if(!useCustomLine && bigLine != (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? bigLine3DSpriteRenderer : bigLine3DMeshRenderer) || threeDimensionalType == ThreeDimensionalType.SpriteRenderer && bigLine == bigLine3DMeshRenderer || threeDimensionalType == ThreeDimensionalType.MeshRenderer && bigLine == bigLine3DSpriteRenderer || bigLine == smallLine3DSpriteRenderer || bigLine == smallLine3DMeshRenderer || bigLine == needle3DSpriteRenderer || bigLine == needle3DMeshRenderer || bigLine == bigLine2D || bigLine == smallLine2D || bigLine == needle2D || !bigLine)
					bigLine = (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? bigLine3DSpriteRenderer : bigLine3DMeshRenderer);
				if(!useCustomLine && smallLine != (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? smallLine3DSpriteRenderer : smallLine3DMeshRenderer) || threeDimensionalType == ThreeDimensionalType.SpriteRenderer && smallLine == smallLine3DMeshRenderer || threeDimensionalType == ThreeDimensionalType.MeshRenderer && smallLine == smallLine3DSpriteRenderer || smallLine == bigLine3DSpriteRenderer || smallLine == bigLine3DMeshRenderer || smallLine == needle3DSpriteRenderer || smallLine == needle3DMeshRenderer || smallLine == bigLine2D || smallLine == smallLine2D || smallLine == needle2D || !smallLine)
					smallLine = (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? smallLine3DSpriteRenderer : smallLine3DMeshRenderer);
				if(gaugeType == GaugeType.Analog && useNeedle && (!useCustomLine && needle != (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? needle3DSpriteRenderer : needle3DMeshRenderer) || threeDimensionalType == ThreeDimensionalType.SpriteRenderer && needle == needle3DMeshRenderer || threeDimensionalType == ThreeDimensionalType.MeshRenderer && needle == needle3DSpriteRenderer || needle == bigLine3DSpriteRenderer || needle == bigLine3DMeshRenderer || needle == smallLine3DSpriteRenderer || needle == smallLine3DMeshRenderer || needle == bigLine2D || needle == smallLine2D || needle == needle2D || !needle))
					needle = (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? needle3DSpriteRenderer : needle3DMeshRenderer);
				if(threeDimensionalType == ThreeDimensionalType.MeshRenderer)
				{
					if(!primaryMaterial)
					{
						primaryMaterial = new Material(Shader.Find("Standard"));
						primaryMaterial.name = "Primary";
						primaryMaterial.SetFloat("_Glossiness",0);
						primaryMaterial.SetFloat("_Metallic",0);
					}
					if(!secondaryMaterial)
					{
						secondaryMaterial = new Material(Shader.Find("Standard"));
						secondaryMaterial.name = "Secondary";
						secondaryMaterial.SetFloat("_Glossiness",0);
						secondaryMaterial.SetFloat("_Metallic",0);
					}
					if(gaugeType == GaugeType.Analog)
					{
						if(!needleMaterial)
						{
							needleMaterial = new Material(Shader.Find("Standard"));
							needleMaterial.name = "Needle";
							needleMaterial.color = Color.red;
							needleMaterial.SetFloat("_Glossiness",0);
							needleMaterial.SetFloat("_Metallic",0);
						}
						if(useBigLineColor && primaryMaterial.HasProperty("_Color") && primaryMaterial.color != bigLineColor)
							primaryMaterial.color = bigLineColor;
						if(useSmallLineColor && secondaryMaterial.HasProperty("_Color") && secondaryMaterial.color != smallLineColor)
							secondaryMaterial.color = smallLineColor;
						if(useNeedleColor && needleMaterial.HasProperty("_Color") && needleMaterial.color != needleColor)
							needleMaterial.color = needleColor;
					}
					if(gaugeType == GaugeType.Digital)
					{
						if(needleMaterial)
						{
							DestroyImmediate(needleMaterial);
							needleMaterial = null;
						}
						if(digitalType == DigitalType.Activation)
						{
							if(useBigLineColor && primaryMaterial.HasProperty("_Color") && primaryMaterial.color != bigLineColor)
								primaryMaterial.color = bigLineColor;
							if(useSmallLineColor && secondaryMaterial.HasProperty("_Color") && secondaryMaterial.color != smallLineColor)
								secondaryMaterial.color = smallLineColor;
						}
						if(digitalType == DigitalType.Color)
						{
							if(primaryMaterial.HasProperty("_Color") && primaryMaterial.color != lineColorOn)
								primaryMaterial.color = lineColorOn;
							if(secondaryMaterial.HasProperty("_Color") && secondaryMaterial.color != lineColorOff)
								secondaryMaterial.color = lineColorOff;
						}
					}
				}
				else
				{
					if(primaryMaterial)
					{
						DestroyImmediate(primaryMaterial);
						primaryMaterial = null;
					}
					if(secondaryMaterial)
					{
						DestroyImmediate(secondaryMaterial);
						secondaryMaterial = null;
					}
					if(needleMaterial)
					{
						DestroyImmediate(needleMaterial);
						needleMaterial = null;
					}
				}
			}
			if(!Application.isPlaying)
			{
				if(bigLineTracker != bigLine)bigLineTracker = bigLine;
				if(smallLineTracker != smallLine)smallLineTracker = smallLine;
				if(needleTracker != needle)needleTracker = needle;
			}
		}
		private void SwitchHandler ()
		{
			if(onTwoDimensionalSwitch)onTwoDimensionalSwitch = false;
			if(onThreeDimensionalSwitch)onThreeDimensionalSwitch = false;
			if(onAnalogSwitch)onAnalogSwitch = false;
			if(onDigitalSwitch)onDigitalSwitch = false;
			if(onCustomLineSwitch)onCustomLineSwitch = false;
			if(isTwoDimensionalSwitch != (dimensionType == DimensionType.TwoDimensional))
			{
				isTwoDimensionalSwitch = dimensionType == DimensionType.TwoDimensional;
				if(dimensionType == DimensionType.TwoDimensional)onTwoDimensionalSwitch = true;
			}
			if(isThreeDimensionalSwitch != (dimensionType == DimensionType.ThreeDimensional))
			{
				isThreeDimensionalSwitch = dimensionType == DimensionType.ThreeDimensional;
				if(dimensionType == DimensionType.ThreeDimensional)onThreeDimensionalSwitch = true;
			}
			if(isAnalogSwitch != (gaugeType == GaugeType.Analog))
			{
				isAnalogSwitch = gaugeType == GaugeType.Analog;
				if(gaugeType == GaugeType.Analog)onAnalogSwitch = true;
			}
			if(isDigitalSwitch != (gaugeType == GaugeType.Digital))
			{
				isDigitalSwitch = gaugeType == GaugeType.Digital;
				if(gaugeType == GaugeType.Digital)onDigitalSwitch = true;
			}
			if(isCustomLineSwitch != useCustomLine)
			{
				isCustomLineSwitch = useCustomLine;
				onCustomLineSwitch = true;
			}
			if(onTwoDimensionalSwitch)
			{
				if(gaugeType == GaugeType.Analog)SwitchHandler(new Vector2(0,139),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(6,17.5f),new Vector2(5,12),new Vector2(0.375f,0.375f));
				if(gaugeType == GaugeType.Digital)SwitchHandler(new Vector2(0,141),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(23,12),new Vector2(23,12),new Vector2(0.375f,0.375f));
			}
			if(onThreeDimensionalSwitch)
			{
				if(gaugeType == GaugeType.Analog)SwitchHandler(new Vector2(0,1.475f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.0768f,0.192f),new Vector2(0.0512f,0.128f),new Vector2(0.02f,0.02f));
				if(gaugeType == GaugeType.Digital)SwitchHandler(new Vector2(0,1.5f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.256f,0.128f),new Vector2(0.256f,0.128f),new Vector2(0.02f,0.02f));
			}
			if(onAnalogSwitch)
			{
				if(dimensionType == DimensionType.TwoDimensional)SwitchHandler(new Vector2(0,139),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(6,17.5f),new Vector2(5,12),new Vector2(0.375f,0.375f));
				if(dimensionType == DimensionType.ThreeDimensional)SwitchHandler(new Vector2(0,1.475f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.0768f,0.192f),new Vector2(0.0512f,0.128f),new Vector2(0.02f,0.02f));
			}
			if(onDigitalSwitch)
			{
				if(dimensionType == DimensionType.TwoDimensional)SwitchHandler(new Vector2(0,141),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(23,12),new Vector2(23,12),new Vector2(0.375f,0.375f));
				if(dimensionType == DimensionType.ThreeDimensional)SwitchHandler(new Vector2(0,1.5f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.256f,0.128f),new Vector2(0.256f,0.128f),new Vector2(0.02f,0.02f));
			}
			if(onCustomLineSwitch && !useCustomLine)
			{
				if(dimensionType == DimensionType.TwoDimensional)
				{
					if(gaugeType == GaugeType.Analog)SwitchHandler(new Vector2(0,139),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(6,17.5f),new Vector2(5,12),new Vector2(0.375f,0.375f));
					if(gaugeType == GaugeType.Digital)SwitchHandler(new Vector2(0,141),new Vector2(0,141),new Vector2(0,105.75f),new Vector2(23,12),new Vector2(23,12),new Vector2(0.375f,0.375f));
				}
				if(dimensionType == DimensionType.ThreeDimensional)
				{
					if(gaugeType == GaugeType.Analog)SwitchHandler(new Vector2(0,1.475f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.0768f,0.192f),new Vector2(0.0512f,0.128f),new Vector2(0.02f,0.02f));
					if(gaugeType == GaugeType.Digital)SwitchHandler(new Vector2(0,1.5f),new Vector2(0,1.5f),new Vector2(0,1.125f),new Vector2(0.256f,0.128f),new Vector2(0.256f,0.128f),new Vector2(0.02f,0.02f));
				}
			}
		}
		private void SwitchHandler (Vector2 bigLinePosition,Vector2 smallLinePosition,Vector2 lineTextPosition,Vector2 bigLineSize,Vector2 smallLineSize,Vector2 lineTextSize)
		{
			if(this.bigLinePosition != bigLinePosition)
				this.bigLinePosition = bigLinePosition;
			if(this.smallLinePosition != smallLinePosition)
				this.smallLinePosition = smallLinePosition;
			if(this.lineTextPosition != lineTextPosition)
				this.lineTextPosition = lineTextPosition;
			if(this.bigLineSize != bigLineSize)
				this.bigLineSize = bigLineSize;
			if(this.smallLineSize != smallLineSize)
				this.smallLineSize = smallLineSize;
			if(this.lineTextSize != lineTextSize)
				this.lineTextSize = lineTextSize;
		}
		private void CheckHandler ()
		{
			if(bigLineTracker != bigLine)
			{
				bigLineTracker = bigLine;
				if(dimensionType == DimensionType.TwoDimensional && bigLineTracker == bigLine2D)
				{
					if(gaugeType == GaugeType.Analog && bigLineSize != new Vector2(6,17.5f))
						bigLineSize = new Vector2(6,17.5f);
					if(gaugeType == GaugeType.Digital && bigLineSize != new Vector2(23,12))
						bigLineSize = new Vector2(23,12);
				}
				if(dimensionType == DimensionType.ThreeDimensional && bigLineTracker == (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? bigLine3DSpriteRenderer : bigLine3DMeshRenderer))
				{
					if(gaugeType == GaugeType.Analog && bigLineSize != new Vector2(0.0768f,0.192f))
						bigLineSize = new Vector2(0.0768f,0.192f);
					if(gaugeType == GaugeType.Digital && bigLineSize != new Vector2(0.256f,0.128f))
						bigLineSize = new Vector2(0.256f,0.128f);
				}
				if(!clearLines)clearLines = true;
			}
			if(smallLineTracker != smallLine)
			{
				smallLineTracker = smallLine;
				if(dimensionType == DimensionType.TwoDimensional && smallLineTracker == smallLine2D)
				{
					if(gaugeType == GaugeType.Analog && smallLineSize != new Vector2(5,12))
						smallLineSize = new Vector2(5,12);
					if(gaugeType == GaugeType.Digital && smallLineSize != new Vector2(23,12))
						smallLineSize = new Vector2(23,12);
				}
				if(dimensionType == DimensionType.ThreeDimensional && smallLineTracker == (threeDimensionalType == ThreeDimensionalType.SpriteRenderer ? smallLine3DSpriteRenderer : smallLine3DMeshRenderer))
				{
					if(gaugeType == GaugeType.Analog && smallLineSize != new Vector2(0.0512f,0.128f))
						smallLineSize = new Vector2(0.0512f,0.128f);
					if(gaugeType == GaugeType.Digital && smallLineSize != new Vector2(0.256f,0.128f))
						smallLineSize = new Vector2(0.256f,0.128f);
				}
				if(!clearLines)clearLines = true;
			}
			if(needleTracker != needle)
			{
				if(needleTransform)
				{
					Destroy(needleTransform.gameObject);
					needleTransform = null;
				}
				needleTracker = needle;
			}
			if(values.Count > 0 && lineTransforms.Length != values.Count * lineCount + (values.Count - lineCount) || onTwoDimensionalSwitch || onThreeDimensionalSwitch || onAnalogSwitch || onDigitalSwitch || clearLines)
			{
				for(int a = 0,A = lineTransforms.Length; a < A; a++)if(lineTransforms[a])
					Destroy(lineTransforms[a].gameObject);
				if(clearLines)clearLines = false;
				lineTransforms = new Transform[values.Count * lineCount + (values.Count - lineCount)];
			}
		}
		private void ExecuteHandler ()
		{
			if(linesUpdateMode == UpdateMode.EveryFrame || (linesUpdateMode == UpdateMode.OnAwake || linesUpdateMode == UpdateMode.ViaScripting) && linesIsUpdating)for(int a = 0,A = lineTransforms.Length,textIndex = 0; a < A; a++)
			{
				if(linesIsUpdating)linesIsUpdating = false;
				int index = lineCount > 0 ? (a % (lineCount + 1)) : (values.Count > 0 ? 0 : -1);
				if(index != -1)
				{
					bool isZero = index == 0;
					if(!lineTransforms[a])
					{
						if(isZero && bigLine)
							lineTransforms[a] = Instantiate(bigLine).transform;
						if(!isZero && smallLine)
							lineTransforms[a] = Instantiate(smallLine).transform;
						if(lineTransforms[a])
						{
							if(dimensionType == DimensionType.ThreeDimensional && threeDimensionalType == ThreeDimensionalType.MeshRenderer)
							{
								Transform transform = lineTransforms[a].Find("Meshes");
								if(transform)
								{
									MeshRenderer[] meshRendererObjects = transform.GetComponentsInChildren<MeshRenderer>();
									for(int b = 0,B = meshRendererObjects.Length; b < B; b++)if(meshRendererObjects[b].name == "Mesh")
										ColorHandler(meshRendererObjects[b],(isZero ? primaryMaterial : secondaryMaterial));
								}
							}
							lineTransforms[a].name = isZero ? "Big Line" : "Small Line";
							lineTransforms[a].SetParent(lineHandler,false);
						}
					}
					if(lineTransforms[a])
					{
						Transform transform = lineTransforms[a].Find("Meshes");
						GameObject gameObject = transform ? transform.gameObject : null;
						if(dimensionType == DimensionType.TwoDimensional)
						{
							if(isZero)
							{
								Transform textTransform = lineTransforms[a].Find("Text");
								Text textObject = textTransform ? textTransform.GetComponent<Text>() : null;
								PositionHandler(transform,bigLinePosition,true);
								ScaleHandler(transform,bigLineSize,true);
								if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useBigLineColor && transform)
								{
									Image[] imageObjects = transform.GetComponentsInChildren<Image>();
									for(int b = 0,B = imageObjects.Length; b < B; b++)if(imageObjects[b].name == "Mesh")
										ColorHandler(imageObjects[b],bigLineColor);
								}
								if(useLineText)
								{
									if(a > 0)textIndex = textIndex + 1;
									if(textObject)
									{
										if(!textObject.gameObject.activeSelf)
											textObject.gameObject.SetActive(true);
										if(textObject.text != values[textIndex].ToString())
											textObject.text = values[textIndex].ToString();
										PositionHandler(textTransform,lineTextPosition,true);
										ScaleHandler(textTransform,lineTextSize,true);
										ColorHandler(textObject,lineTextColor,useLineTextColor);
										if(useWorldTextRotation)RotationHandler(textTransform,new Vector3(textTransform.localEulerAngles.x,textTransform.localEulerAngles.y,-lineTransforms[a].localEulerAngles.z));
										else RotationHandler(textTransform,Vector3.zero);
									}
								}
								else if(textObject && textObject.gameObject.activeSelf)
									textObject.gameObject.SetActive(false);
							}
							else
							{
								PositionHandler(transform,smallLinePosition,true);
								ScaleHandler(transform,smallLineSize,true);
								if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useSmallLineColor && transform)
								{
									Image[] imageObjects = transform.GetComponentsInChildren<Image>();
									for(int b = 0,B = imageObjects.Length; b < B; b++)if(imageObjects[b].name == "Mesh")
										ColorHandler(imageObjects[b],smallLineColor);
								}
							}
							if(gaugeType == GaugeType.Analog && gameObject && !gameObject.activeSelf)gameObject.SetActive(true);
							if(gaugeType == GaugeType.Digital)
							{
								float result = maximumValue / (A - 1);
								result = a == 0 ? 0 : ((result / 2) + (result * (a - 1)));
								if(digitalType == DigitalType.Activation && gameObject)
								{
									if(value >= result && !gameObject.activeSelf)gameObject.SetActive(true);
									if(value < result && gameObject.activeSelf)gameObject.SetActive(false);
								}
								if(digitalType == DigitalType.Color && transform)
								{
									if(!gameObject.activeSelf)gameObject.SetActive(true);
									if(value >= result)
									{
										Image[] imageObjects = transform.GetComponentsInChildren<Image>();
										for(int b = 0,B = imageObjects.Length; b < B; b++)if(imageObjects[b].name == "Mesh")
											if(imageObjects[b].color != lineColorOn)imageObjects[b].color = lineColorOn;
									}
									if(value < result)
									{
										Image[] imageObjects = transform.GetComponentsInChildren<Image>();
										for(int b = 0,B = imageObjects.Length; b < B; b++)if(imageObjects[b].name == "Mesh")
											if(imageObjects[b].color != lineColorOff)imageObjects[b].color = lineColorOff;
									}
								}
							}
						}
						if(dimensionType == DimensionType.ThreeDimensional)
						{
							if(threeDimensionalType == ThreeDimensionalType.SpriteRenderer)
							{
								if(isZero)
								{
									Transform textMeshTransform = lineTransforms[a].Find("Text");
									TextMesh textMeshObject = textMeshTransform ? textMeshTransform.GetComponent<TextMesh>() : null;
									PositionHandler(transform,bigLinePosition,true);
									ScaleHandler(transform,bigLineSize,true);
									if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useBigLineColor && transform)
									{
										SpriteRenderer[] spriteRendererObjects = transform.GetComponentsInChildren<SpriteRenderer>();
										for(int b = 0,B = spriteRendererObjects.Length; b < B; b++)if(spriteRendererObjects[b].name == "Mesh")
											ColorHandler(spriteRendererObjects[b],bigLineColor);
									}
									if(useLineText)
									{
										if(a > 0)textIndex = textIndex + 1;
										if(textMeshObject)
										{
											if(!textMeshObject.gameObject.activeSelf)
												textMeshObject.gameObject.SetActive(true);
											if(textMeshObject.text != values[textIndex].ToString())
												textMeshObject.text = values[textIndex].ToString();
											PositionHandler(textMeshObject.transform,lineTextPosition,true);
											ScaleHandler(textMeshObject.transform,lineTextSize,true);
											ColorHandler(textMeshObject,lineTextColor,useLineTextColor);
											if(useWorldTextRotation)RotationHandler(textMeshObject.transform,new Vector3(textMeshObject.transform.localEulerAngles.x,textMeshObject.transform.localEulerAngles.y,-lineTransforms[a].localEulerAngles.z));
											else RotationHandler(textMeshObject.transform,Vector3.zero);
										}
									}
									else if(textMeshObject && textMeshObject.gameObject.activeSelf)
										textMeshObject.gameObject.SetActive(false);
								}
								else
								{
									PositionHandler(transform,smallLinePosition,true);
									ScaleHandler(transform,smallLineSize,true);
									if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useSmallLineColor && transform)
									{
										SpriteRenderer[] spriteRendererObjects = transform.GetComponentsInChildren<SpriteRenderer>();
										for(int b = 0,B = spriteRendererObjects.Length; b < B; b++)if(spriteRendererObjects[b].name == "Mesh")
											ColorHandler(spriteRendererObjects[b],smallLineColor);
									}
								}
								if(gaugeType == GaugeType.Analog && gameObject && !gameObject.activeSelf)gameObject.SetActive(true);
								if(gaugeType == GaugeType.Digital)
								{
									float result = maximumValue / (A - 1);
									result = a == 0 ? 0 : ((result / 2) + (result * (a - 1)));
									if(digitalType == DigitalType.Activation && gameObject)
									{
										if(value >= result && !gameObject.activeSelf)gameObject.SetActive(true);
										if(value < result && gameObject.activeSelf)gameObject.SetActive(false);
									}
									if(digitalType == DigitalType.Color && transform)
									{
										SpriteRenderer[] spriteRendererObjects = transform.GetComponentsInChildren<SpriteRenderer>();
										if(!gameObject.activeSelf)gameObject.SetActive(true);
										for(int b = 0,B = spriteRendererObjects.Length; b < B; b++)if(spriteRendererObjects[b].name == "Mesh")
										{
											if(value >= result && spriteRendererObjects[b].color != lineColorOn)spriteRendererObjects[b].color = lineColorOn;
											if(value < result && spriteRendererObjects[b].color != lineColorOff)spriteRendererObjects[b].color = lineColorOff;
										}
									}
								}
							}
							if(threeDimensionalType == ThreeDimensionalType.MeshRenderer)
							{
								if(isZero)
								{
									Transform textMeshTransform = lineTransforms[a].Find("Text");
									TextMesh textMeshObject = textMeshTransform ? textMeshTransform.GetComponent<TextMesh>() : null;
									PositionHandler(transform,bigLinePosition,true);
									ScaleHandler(transform,bigLineSize,true);
									if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useBigLineColor && transform)
									{
										MeshRenderer[] meshRendererObjects = transform.GetComponentsInChildren<MeshRenderer>();
										for(int b = 0,B = meshRendererObjects.Length; b < B; b++)if(meshRendererObjects[b].name == "Mesh")
											ColorHandler(meshRendererObjects[b],primaryMaterial);
									}
									if(useLineText)
									{
										if(a > 0)textIndex = textIndex + 1;
										if(textMeshObject)
										{
											if(!textMeshObject.gameObject.activeSelf)
												textMeshObject.gameObject.SetActive(true);
											if(textMeshObject.text != values[textIndex].ToString())
												textMeshObject.text = values[textIndex].ToString();
											PositionHandler(textMeshObject.transform,lineTextPosition,true);
											ScaleHandler(textMeshObject.transform,lineTextSize,true);
											ColorHandler(textMeshObject,lineTextColor,useLineTextColor);
											if(useWorldTextRotation)RotationHandler(textMeshObject.transform,new Vector3(textMeshObject.transform.localEulerAngles.x,textMeshObject.transform.localEulerAngles.y,-lineTransforms[a].localEulerAngles.z));
											else RotationHandler(textMeshObject.transform,Vector3.zero);
										}
									}
									else if(textMeshObject && textMeshObject.gameObject.activeSelf)
										textMeshObject.gameObject.SetActive(false);
								}
								else
								{
									PositionHandler(transform,smallLinePosition,true);
									ScaleHandler(transform,smallLineSize,true);
									if((gaugeType == GaugeType.Analog || gaugeType == GaugeType.Digital && digitalType == DigitalType.Activation) && useSmallLineColor && transform)
									{
										MeshRenderer[] meshRendererObjects = transform.GetComponentsInChildren<MeshRenderer>();
										for(int b = 0,B = meshRendererObjects.Length; b < B; b++)if(meshRendererObjects[b].name == "Mesh")
											ColorHandler(meshRendererObjects[b],secondaryMaterial);
									}
								}
								if(gaugeType == GaugeType.Analog && gameObject && !gameObject.activeSelf)gameObject.SetActive(true);
								if(gaugeType == GaugeType.Digital)
								{
									float result = maximumValue / (A - 1);
									result = a == 0 ? 0 : ((result / 2) + (result * (a - 1)));
									if(digitalType == DigitalType.Activation && gameObject)
									{
										if(value >= result && !gameObject.activeSelf)gameObject.SetActive(true);
										if(value < result && gameObject.activeSelf)gameObject.SetActive(false);
									}
									if(digitalType == DigitalType.Color && transform)
									{
										MeshRenderer[] meshRendererObjects = transform.GetComponentsInChildren<MeshRenderer>();
										if(!gameObject.activeSelf)gameObject.SetActive(true);
										for(int b = 0,B = meshRendererObjects.Length; b < B; b++)if(meshRendererObjects[b].name == "Mesh")
										{
											if(value >= result)ColorHandler(meshRendererObjects[b],primaryMaterial);
											if(value < result)ColorHandler(meshRendererObjects[b],secondaryMaterial);
										}
									}
								}
							}
						}
						RotationHandler(lineTransforms[a],new Vector3(0,0,SegmentAngles(minimumAngle,maximumAngle,A,a)));
					}
				}
			}
			if(gaugeType == GaugeType.Analog && useNeedle && (needleUpdateMode == UpdateMode.EveryFrame || (needleUpdateMode == UpdateMode.OnAwake || needleUpdateMode == UpdateMode.ViaScripting) && needleIsUpdating))
			{
				if(needleIsUpdating)needleIsUpdating = false;
				if(!needleTransform)
				{
					if(needle)needleTransform = Instantiate(needle).transform;
					if(needleTransform)
					{
						needleTransform.name = "Needle";
						needleTransform.SetParent(needleHandler,false);
					}
				}
				if(needleTransform)
				{
					Transform transform = needleTransform.Find("Meshes");
					Transform pivot = needleTransform.Find("Pivot");
					PositionHandler(needleTransform,needlePosition,useNeedlePosition);
					RotationHandler(needleTransform,new Vector3(0,0,RangeConversion(value,0,maximumValue,minimumAngle,maximumAngle)));
					ScaleHandler(transform,needleSize,useNeedleSize);
					ScaleHandler(pivot,needlePivotSize,useNeedlePivotSize);
					if(useNeedleColor)
					{
						if(dimensionType == DimensionType.TwoDimensional)
						{
							Image[] imageObjects = needleTransform.GetComponentsInChildren<Image>();
							for(int b = 0,B = imageObjects.Length; b < B; b++)if(imageObjects[b].name == "Mesh")
								ColorHandler(imageObjects[b],needleColor);
						}
						if(dimensionType == DimensionType.ThreeDimensional && threeDimensionalType == ThreeDimensionalType.SpriteRenderer)
						{
							SpriteRenderer[] spriteRendererObjects = needleTransform.GetComponentsInChildren<SpriteRenderer>();
							for(int b = 0,B = spriteRendererObjects.Length; b < B; b++)if(spriteRendererObjects[b].name == "Mesh")
								ColorHandler(spriteRendererObjects[b],needleColor);
						}
					}
					if(dimensionType == DimensionType.ThreeDimensional && threeDimensionalType == ThreeDimensionalType.MeshRenderer)
					{
						MeshRenderer[] meshRendererObjects = needleTransform.GetComponentsInChildren<MeshRenderer>();
						for(int b = 0,B = meshRendererObjects.Length; b < B; b++)if(meshRendererObjects[b].name == "Mesh")
							ColorHandler(meshRendererObjects[b],needleMaterial);
					}
				}
			}
			if(gaugeType == GaugeType.Digital && useText && (textUpdateMode == UpdateMode.EveryFrame || (textUpdateMode == UpdateMode.OnAwake || textUpdateMode == UpdateMode.ViaScripting) && textIsUpdating))
			{
				if(textIsUpdating)textIsUpdating = false;
				if(!text)
				{
					if(!createTextAutomatically && textName != string.Empty)
					{
						Transform textTransform = transform.Find(textName);
						if(textTransform)text = textTransform;
					}
					if(createTextAutomatically)CreateTextAutomatically();
				}
				if(text)
				{
					string value = (absolute ? Mathf.Abs(Mathf.RoundToInt(this.value)) : Mathf.RoundToInt(this.value)).ToString();
					Text textObject = text.GetComponent<Text>();
					TextMesh textMeshObject = text.GetComponent<TextMesh>();
					if(textObject && textObject.text != value)
						textObject.text = value;
					if(textMeshObject && textMeshObject.text != value)
						textMeshObject.text = value;
					if(!createTextAutomatically && textName != string.Empty)
					{
						Transform textTransform = transform.Find(textName);
						if(text != textTransform)text = null;
					}
				}
			}
		}
		private void PositionHandler (Transform transform,Vector2 position,bool enabled)
		{
			if(enabled && transform && transform.localPosition != new Vector3(position.x,position.y,0))
				transform.localPosition = new Vector3(position.x,position.y,0);
		}
		private void RotationHandler (Transform transform,Vector3 rotation)
		{
			if(transform && transform.localRotation != Quaternion.Euler(rotation))
				transform.localRotation = Quaternion.Euler(rotation);
		}
		private void ScaleHandler (Transform transform,Vector2 scale,bool enabled)
		{
			if(enabled && transform && transform.localScale != new Vector3(scale.x,scale.y,1))
				transform.localScale = new Vector3(scale.x,scale.y,1);
		}
		private void ColorHandler (Image image,Color color)
		{
			if(image && image.color != color)
				image.color = color;
		}
		private void ColorHandler (SpriteRenderer spriteRenderer,Color color)
		{
			if(spriteRenderer && spriteRenderer.color != color)
				spriteRenderer.color = color;
		}
		private void ColorHandler (MeshRenderer meshRenderer,Material material)
		{
			if(meshRenderer && meshRenderer.material != material)
				meshRenderer.material = material;
		}
		private void ColorHandler (Text text,Color color,bool enabled)
		{
			if(enabled && text && text.color != color)
				text.color = color;
		}
		private void ColorHandler (TextMesh textMesh,Color color,bool enabled)
		{
			if(enabled && textMesh && textMesh.color != color)
				textMesh.color = color;
		}
		private Transform TransformHandler (Transform handler,string name)
		{
			if(!handler)
			{
				if(transform.Find(name))
					handler = transform.Find(name);
				else
				{
					if(GetComponent<RectTransform>())
					{
						handler = new GameObject(name,typeof(RectTransform)).transform;
						handler.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
					}
					else handler = new GameObject(name).transform;
					handler.SetParent(transform,false);
				}
			}
			else if(handler != transform.Find(name))
				handler = null;
			return handler;
		}
		private float SegmentAngles (float minimumAngle,float maximumAngle,int segments,int index) {return segments > 1 ? (minimumAngle + ((maximumAngle - minimumAngle) / (segments - 1) * index)) : minimumAngle;}
		private float RangeConversion (float value,float minimumValue,float maximumValue,float minimum,float maximum) {return minimum + (value - minimumValue) / (maximumValue - minimumValue) * (maximum - minimum);}
		public void SetDimensionType (DimensionType value) {if(dimensionType != value)dimensionType = value;}
		public void SetDimensionType (int value)
		{
			DimensionType convertedValue = (DimensionType)value;
			if(dimensionType != convertedValue)dimensionType = convertedValue;
		}
		public void SetGaugeType (GaugeType value) {if(gaugeType != value)gaugeType = value;}
		public void SetGaugeType (int value)
		{
			GaugeType convertedValue = (GaugeType)value;
			if(gaugeType != convertedValue)gaugeType = convertedValue;
		}
		public void SetThreeDimensionType (ThreeDimensionalType value) {if(threeDimensionalType != value)threeDimensionalType = value;}
		public void SetThreeDimensionType (int value)
		{
			ThreeDimensionalType convertedValue = (ThreeDimensionalType)value;
			if(threeDimensionalType != convertedValue)threeDimensionalType = convertedValue;
		}
		public void SetDigitalType (DigitalType value) {if(digitalType != value)digitalType = value;}
		public void SetDigitalType (int value)
		{
			DigitalType convertedValue = (DigitalType)value;
			if(digitalType != convertedValue)digitalType = convertedValue;
		}
		public void SetPrimaryMaterial (Material value) {if(primaryMaterial != value)primaryMaterial = value;}
		public void SetSecondaryMaterial (Material value) {if(secondaryMaterial != value)secondaryMaterial = value;}
		public void SetNeedleMaterial (Material value) {if(needleMaterial != value)needleMaterial = value;}
		public void SetLinesUpdateMode (UpdateMode value) {if(linesUpdateMode != value)linesUpdateMode = value;}
		public void SetLinesUpdateMode (int value)
		{
			UpdateMode convertedValue = (UpdateMode)value;
			if(linesUpdateMode != convertedValue)linesUpdateMode = convertedValue;
		}
		public void SetLineHandler (Transform value) {if(lineHandler != value)lineHandler = value;}
		public void SetLineHandler (GameObject value) {if(lineHandler != value.transform)lineHandler = value.transform;}
		public void SetLineCount (int value) {if(lineCount != value)lineCount = value;}
		public void UseCustomLine (bool value) {if(useCustomLine != value)useCustomLine = value;}
		public void SetBigLine (GameObject value) {if(bigLine != value)bigLine = value;}
		public void SetBigLine (Transform value) {if(bigLine != value.gameObject)bigLine = value.gameObject;}
		public void SetSmallLine (GameObject value) {if(smallLine != value)smallLine = value;}
		public void SetSmallLine (Transform value) {if(smallLine != value.gameObject)smallLine = value.gameObject;}
		public void SetMinimumAngle (float value) {if(minimumAngle != value)minimumAngle = value;}
		public void SetMaximumAngle (float value) {if(maximumAngle != value)maximumAngle = value;}
		public void SetValues (List<float> value) {if(values != value)values = value;}
		public void SetValues (float[] value)
		{
			List<float> convertedValue = new List<float>(value);
			if(values != convertedValue)values = convertedValue;
		}
		public void SetMinimumValue (float value) {if(minimumValue != value)minimumValue = value;}
		public void SetValue (float value) {if(this.value != value)this.value = value;}
		public void SetMaximumValue (float value) {if(maximumValue != value)maximumValue = value;}
		public void Clamp (bool value) {if(clamp != value)clamp = value;}
		public void UseNeedle (bool value) {if(useNeedle != value)useNeedle = value;}
		public void SetNeedleUpdateMode (UpdateMode value) {if(needleUpdateMode != value)needleUpdateMode = value;}
		public void SetNeedleUpdateMode (int value)
		{
			UpdateMode convertedValue = (UpdateMode)value;
			if(needleUpdateMode != convertedValue)needleUpdateMode = convertedValue;
		}
		public void SetNeedleHandler (Transform value) {if(needleHandler != value)needleHandler = value;}
		public void SetNeedleHandler (GameObject value) {if(needleHandler != value.transform)needleHandler = value.transform;}
		public void UseCustomNeedle (bool value) {if(useCustomNeedle != value)useCustomNeedle = value;}
		public void SetNeedle (GameObject value) {if(needle != value)needle = value;}
		public void SetNeedle (Transform value) {if(needle != value.gameObject)needle = value.gameObject;}
		public void UseText (bool value) {if(useText != value)useText = value;}
		public void SetTextUpdateMode (UpdateMode value) {if(textUpdateMode != value)textUpdateMode = value;}
		public void SetTextUpdateMode (int value)
		{
			UpdateMode convertedValue = (UpdateMode)value;
			if(textUpdateMode != convertedValue)textUpdateMode = convertedValue;
		}
		public void CreateAutomatically (bool value) {if(createTextAutomatically != value)createTextAutomatically = value;}
		public void Absolute (bool value) {if(absolute != value)absolute = value;}
		public void SetTextOffset (Vector2 value) {if(textOffset != value)textOffset = value;}
		public void SetTextOffset (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(textOffset != convertedValue)textOffset = convertedValue;
		}
		public void SetTextOffset (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(textOffset != convertedValue)textOffset = convertedValue;
		}
		public void SetTextSize (Vector2 value) {if(textSize != value)textSize = value;}
		public void SetTextSize (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(textSize != convertedValue)textSize = convertedValue;
		}
		public void SetTextSize (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(textSize != convertedValue)textSize = convertedValue;
		}
		public void SetTextFont (Font value) {if(textFont != value)textFont = value;}
		public void SetTextColor (Color value) {if(textColor != value)textColor = value;}
		public void SetTextName (string value) {if(textName != value)textName = value;}
		public void SetText (Transform value) {if(text != value)text = value;}
		public void SetText (GameObject value) {if(text != value.transform)text = value.transform;}
		public void SetBigLinePosition (Vector2 value) {if(bigLinePosition != value)bigLinePosition = value;}
		public void SetBigLinePosition (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(bigLinePosition != convertedValue)bigLinePosition = convertedValue;
		}
		public void SetBigLinePosition (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(bigLinePosition != convertedValue)bigLinePosition = convertedValue;
		}
		public void SetSmallLinePosition (Vector2 value) {if(smallLinePosition != value)smallLinePosition = value;}
		public void SetSmallLinePosition (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(smallLinePosition != convertedValue)smallLinePosition = convertedValue;
		}
		public void SetSmallLinePosition (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(smallLinePosition != convertedValue)smallLinePosition = convertedValue;
		}
		public void SetLineTextPosition (Vector2 value) {if(lineTextPosition != value)lineTextPosition = value;}
		public void SetLineTextPosition (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(lineTextPosition != convertedValue)lineTextPosition = convertedValue;
		}
		public void SetLineTextPosition (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(lineTextPosition != convertedValue)lineTextPosition = convertedValue;
		}
		public void UseNeedlePosition (bool value) {if(useNeedlePosition != value)useNeedlePosition = value;}
		public void SetNeedlePosition (Vector2 value) {if(needlePosition != value)needlePosition = value;}
		public void SetNeedlePosition (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(needlePosition != convertedValue)needlePosition = convertedValue;
		}
		public void SetNeedlePosition (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(needlePosition != convertedValue)needlePosition = convertedValue;
		}
		public void SetBigLineSize (Vector2 value) {if(bigLineSize != value)bigLineSize = value;}
		public void SetBigLineSize (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(bigLineSize != convertedValue)bigLineSize = convertedValue;
		}
		public void SetBigLineSize (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(bigLineSize != convertedValue)bigLineSize = convertedValue;
		}
		public void SetSmallLineSize (Vector2 value) {if(smallLineSize != value)smallLineSize = value;}
		public void SetSmallLineSize (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(smallLineSize != convertedValue)smallLineSize = convertedValue;
		}
		public void SetSmallLineSize (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(smallLineSize != convertedValue)smallLineSize = convertedValue;
		}
		public void SetLineTextSize (Vector2 value) {if(lineTextSize != value)lineTextSize = value;}
		public void SetLineTextSize (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(lineTextSize != convertedValue)lineTextSize = convertedValue;
		}
		public void SetLineTextSize (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(lineTextSize != convertedValue)lineTextSize = convertedValue;
		}
		public void UseNeedleSize (bool value) {if(useNeedleSize != value)useNeedleSize = value;}
		public void SetNeedleSize (Vector2 value) {if(needleSize != value)needleSize = value;}
		public void SetNeedleSize (Vector3 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(needleSize != convertedValue)needleSize = convertedValue;
		}
		public void SetNeedleSize (Vector4 value)
		{
			Vector2 convertedValue = new Vector2(value.x,value.y);
			if(needleSize != convertedValue)needleSize = convertedValue;
		}
		public void UseBigLineColor (bool value) {if(useBigLineColor != value)useBigLineColor = value;}
		public void SetBigLineColor (Color value) {if(bigLineColor != value)bigLineColor = value;}
		public void UseSmallLineColor (bool value) {if(useSmallLineColor != value)useSmallLineColor = value;}
		public void SetSmallLineColor (Color value) {if(smallLineColor != value)smallLineColor = value;}
		public void SetLineColorOn (Color value) {if(lineColorOn != value)lineColorOn = value;}
		public void SetLineColorOff (Color value) {if(lineColorOff != value)lineColorOff = value;}
		public void UseNeedleColor (bool value) {if(useNeedleColor != value)useNeedleColor = value;}
		public void SetNeedleColor (Color value) {if(needleColor != value)needleColor = value;}
		public void UseLineTextColor (bool value) {if(useLineTextColor != value)useLineTextColor = value;}
		public void SetLineTextColor (Color value) {if(lineTextColor != value)lineTextColor = value;}
		public void UseLineText (bool value) {if(useLineText != value)useLineText = value;}
		public void UseWorldTextRotation (bool value) {if(useWorldTextRotation != value)useWorldTextRotation = value;}
		public void CreateTextAutomatically ()
		{
			if(dimensionType == DimensionType.TwoDimensional)
			{
				#if UNITY_EDITOR
				Undo.RecordObject(this,"Inspector");
				text = new GameObject("Text",typeof(RectTransform),typeof(CanvasRenderer),typeof(Text)).transform;
				Undo.RegisterCreatedObjectUndo(text.gameObject,"Inspector");
				#else
				text = new GameObject("Text",typeof(RectTransform),typeof(CanvasRenderer),typeof(Text)).transform;
				#endif
				text.SetParent(transform,false);
				if(textOffset != Vector2.zero)text.localPosition = new Vector3(textOffset.x,textOffset.y,0);
				text.localScale = new Vector3(textSize.x * 0.06f,textSize.y * 0.06f,1);
				Text textObject = text.GetComponent<Text>();
				textObject.font = textFont;
				textObject.fontSize = 100;
				textObject.alignment = TextAnchor.MiddleCenter;
				textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(350,150);
				textObject.color = textColor;
			}
			if(dimensionType == DimensionType.ThreeDimensional)
			{
				#if UNITY_EDITOR
				Undo.RecordObject(this,"Inspector");
				text = new GameObject("Text",typeof(MeshRenderer),typeof(TextMesh)).transform;
				Undo.RegisterCreatedObjectUndo(text.gameObject,"Inspector");
				#else
				text = new GameObject("Text",typeof(MeshRenderer),typeof(TextMesh)).transform;
				#endif
				text.SetParent(transform,false);
				if(textOffset != Vector2.zero)text.localPosition = new Vector3(textOffset.x,textOffset.y,0);
				text.localScale = new Vector3(textSize.x * 0.06f,textSize.y * 0.06f,1);
				TextMesh textMeshObject = text.GetComponent<TextMesh>();
				textMeshObject.font = textFont;
				textMeshObject.fontSize = 100;
				textMeshObject.anchor = TextAnchor.MiddleCenter;
				textMeshObject.alignment = TextAlignment.Center;
				textMeshObject.color = textColor;
			}
		}
		public void ApplyValuesAutomatically (byte count)
		{
			if(values.Count != count + 1)
				values = new List<float>(new float[count + 1]);
			for(int a = 0,A = values.Count; a < A; a++)if(values[a] != maximumValue / count * a)
				values[a] = maximumValue / count * a;
		}
		public void UpdateAll ()
		{
			UpdateLines();
			UpdateNeedle();
			UpdateText();
		}
		public void UpdateLines ()
		{
			if((linesUpdateMode == UpdateMode.OnAwake || linesUpdateMode == UpdateMode.ViaScripting) && !linesIsUpdating)
				linesIsUpdating = true;
		}
		public void UpdateNeedle ()
		{
			if((needleUpdateMode == UpdateMode.OnAwake || needleUpdateMode == UpdateMode.ViaScripting) && !needleIsUpdating)
				needleIsUpdating = true;
		}
		public void UpdateText ()
		{
			if((textUpdateMode == UpdateMode.OnAwake || textUpdateMode == UpdateMode.ViaScripting) && !textIsUpdating)
				textIsUpdating = true;
		}
	}
	#if UNITY_EDITOR
	[CustomEditor(typeof(GaugeSystem)),CanEditMultipleObjects]
	internal class GaugeSystemEditor : Editor
	{
		private GaugeSystem[] gaugeSystems
		{
			get
			{
				GaugeSystem[] gaugeSystems = new GaugeSystem[targets.Length];
				for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < targets.Length; gaugeSystemsIndex++)
					gaugeSystems[gaugeSystemsIndex] = (GaugeSystem)targets[gaugeSystemsIndex];
				return gaugeSystems;
			}
		}
		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			MainSection();
			LinesSection();
			AnglesSection();
			ValuesSection();
			ConfigureSection();
			if(GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
				for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)
					EditorUtility.SetDirty(gaugeSystems[gaugeSystemsIndex]);
			}
		}
		private void MainSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Main","BoldLabel");
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.PropertyField(serializedObject.FindProperty("dimensionType"),GUIContent.none,true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("gaugeType"),GUIContent.none,true);
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].dimensionType == GaugeSystem.DimensionType.ThreeDimensional || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital)
				{
					if(gaugeSystems[0].dimensionType == GaugeSystem.DimensionType.ThreeDimensional || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital)
					{
						EditorGUILayout.BeginHorizontal();
						{
							if(gaugeSystems[0].dimensionType == GaugeSystem.DimensionType.ThreeDimensional)
								EditorGUILayout.PropertyField(serializedObject.FindProperty("threeDimensionalType"),GUIContent.none,true);
							if(gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital)
								EditorGUILayout.PropertyField(serializedObject.FindProperty("digitalType"),GUIContent.none,true);
						}
						EditorGUILayout.EndHorizontal();
					}
					if(gaugeSystems[0].dimensionType == GaugeSystem.DimensionType.ThreeDimensional && gaugeSystems[0].threeDimensionalType == GaugeSystem.ThreeDimensionalType.MeshRenderer)
					{
						EditorGUILayout.BeginHorizontal();
						{
							EditorGUILayout.BeginVertical("Box");
							{
								EditorGUILayout.BeginHorizontal();
								{
									GUILayout.FlexibleSpace();
									GUILayout.Label("Primary Material");
									GUILayout.FlexibleSpace();
								}
								EditorGUILayout.EndHorizontal();
								EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryMaterial"),GUIContent.none,true);
							}
							EditorGUILayout.EndVertical();
							EditorGUILayout.BeginVertical("Box");
							{
								EditorGUILayout.BeginHorizontal();
								{
									GUILayout.FlexibleSpace();
									GUILayout.Label("Secondary Material");
									GUILayout.FlexibleSpace();
								}
								EditorGUILayout.EndHorizontal();
								EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryMaterial"),GUIContent.none,true);
							}
							EditorGUILayout.EndVertical();
						}
						EditorGUILayout.EndHorizontal();
						if(gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog)
						{
							EditorGUILayout.BeginVertical("Box");
							{
								EditorGUILayout.BeginHorizontal();
								{
									GUILayout.FlexibleSpace();
									GUILayout.Label("Needle Material");
									GUILayout.FlexibleSpace();
								}
								EditorGUILayout.EndHorizontal();
								EditorGUILayout.PropertyField(serializedObject.FindProperty("needleMaterial"),GUIContent.none,true);
							}
							EditorGUILayout.EndVertical();
						}
					}
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void LinesSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Lines","BoldLabel");
				EditorGUILayout.PropertyField(serializedObject.FindProperty("linesUpdateMode"),true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("lineHandler"),true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("lineCount"),true);
				EditorGUILayout.BeginVertical("Box");
				{
					GUI.backgroundColor = gaugeSystems[0].applyValuesAutomatically ? Color.green : Color.red;
					if(GUILayout.Button("Apply Automatically"))
					{
						Undo.RecordObjects(targets,"Inspector");
						gaugeSystems[0].applyValuesAutomatically = !gaugeSystems[0].applyValuesAutomatically;
						for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].applyValuesAutomatically != gaugeSystems[0].applyValuesAutomatically)
							gaugeSystems[gaugeSystemsIndex].applyValuesAutomatically = gaugeSystems[0].applyValuesAutomatically;
						GUI.FocusControl(null);
					}
					GUI.backgroundColor = Color.white;
					if(gaugeSystems[0].applyValuesAutomatically)
					{
						EditorGUILayout.BeginHorizontal();
						{
							EditorGUILayout.BeginHorizontal();
							{
								EditorGUILayout.BeginVertical("Box");
								{
									EditorGUILayout.BeginHorizontal();
									{
										GUILayout.FlexibleSpace();
										GUILayout.Label("Maximum Value");
										GUILayout.FlexibleSpace();
									}
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"),GUIContent.none,true);
								}
								EditorGUILayout.EndVertical();
								EditorGUILayout.BeginVertical("Box");
								{
									EditorGUILayout.BeginHorizontal();
									{
										GUILayout.FlexibleSpace();
										GUILayout.Label("Count");
										GUILayout.FlexibleSpace();
									}
									EditorGUILayout.EndHorizontal();
									EditorGUILayout.PropertyField(serializedObject.FindProperty("valuesCount"),GUIContent.none,true);
								}
								EditorGUILayout.EndVertical();
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.BeginVertical(GUILayout.MaxWidth(130));
							{
								float result = gaugeSystems[0].maximumValue / gaugeSystems[0].valuesCount;
								GUIStyle style = new GUIStyle() {fontSize = 13};
								EditorGUILayout.BeginHorizontal();
								{
									GUILayout.Label("=",style);
									GUILayout.Space(3);
									if(result >= 0 && result < 10)GUILayout.Label(result.ToString("0.000"),style);
									if(result >= 10 && result < 100)GUILayout.Label(result.ToString("00.00"),style);
									if(result >= 100 && result < 1000)GUILayout.Label(result.ToString("000.0"),style);
									if(result >= 1000 && result < 1000000)GUILayout.Label((result / 1000).ToString("000") + "K",style);
									if(result >= 1000000 && result < 1000000000)GUILayout.Label((result / 1000000).ToString("000") + "M",style);
									GUILayout.FlexibleSpace();
								}
								EditorGUILayout.EndHorizontal();
								GUILayout.Label("Per Line",style);
								GUILayout.Label("(Value)",style);
							}
							EditorGUILayout.EndVertical();
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
						{
							if(GUILayout.Button("Apply"))
							{
								Undo.RecordObjects(targets,"Inspector");
								for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)
									gaugeSystems[gaugeSystemsIndex].ApplyValuesAutomatically(gaugeSystems[gaugeSystemsIndex].valuesCount);
								GUI.FocusControl(null);
							}
							GUI.enabled = gaugeSystems[0].values.Count != 0;
							if(GUILayout.Button("Reset"))
							{
								Undo.RecordObjects(targets,"Inspector");
								for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)
									gaugeSystems[gaugeSystemsIndex].values.Clear();
								GUI.FocusControl(null);
							}
							GUI.enabled = true;
						}
						EditorGUILayout.EndHorizontal();
					}
					else
					{
						if(!serializedObject.isEditingMultipleObjects)LinesSectionValuesContainer();
						else
						{
							GUI.enabled = false;
							EditorGUILayout.BeginHorizontal("Box");
							GUILayout.Box("Values",GUILayout.ExpandWidth(true));
							EditorGUILayout.EndHorizontal();
							GUI.enabled = true;
						}
					}
				}
				EditorGUILayout.EndVertical();
				LinesSectionCustomLineContainer();
			}
			EditorGUILayout.EndVertical();
		}
		private void LinesSectionValuesContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					if(GUILayout.Button("Values","Box",GUILayout.ExpandWidth(true)))
					{
						gaugeSystems[0].valuesIsExpanded = !gaugeSystems[0].valuesIsExpanded;
						GUI.FocusControl(null);
					}
					GUI.enabled = gaugeSystems[0].values.Count != 0;
					if(GUILayout.Button("X",GUILayout.Width(20),GUILayout.Height(20)))
					{
						Undo.RecordObject(target,"Inspector");
						gaugeSystems[0].values.Clear();
						GUI.FocusControl(null);
					}
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].valuesIsExpanded)
				{
					if(gaugeSystems[0].values.Count >= 5)gaugeSystems[0].valuesScrollView = EditorGUILayout.BeginScrollView(gaugeSystems[0].valuesScrollView,GUILayout.Height(101));
					else
					{
						if(gaugeSystems[0].valuesScrollView != Vector2.zero)
							gaugeSystems[0].valuesScrollView = Vector2.zero;
						if(gaugeSystems[0].valuesScrollViewIndex != 0)
							gaugeSystems[0].valuesScrollViewIndex = 0;
					}
					if(gaugeSystems[0].valuesScrollViewIndex > 0)GUILayout.Space(gaugeSystems[0].valuesScrollViewIndex * 26);
					for(int a = gaugeSystems[0].valuesScrollViewIndex; a <= Mathf.Clamp(gaugeSystems[0].valuesScrollViewIndex + 4,0,gaugeSystems[0].values.Count - 1); a++)
					{
						EditorGUILayout.BeginHorizontal("Box");
						{
							EditorGUILayout.BeginHorizontal("Box");
							GUILayout.Box(a.ToString("000"),new GUIStyle() {fontSize = 8},GUILayout.ExpandWidth(false));
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("values").GetArrayElementAtIndex(a),GUIContent.none,true);
							GUI.enabled = a != 0;
							if(GUILayout.Button("▲",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								float current = gaugeSystems[0].values[a];
								float previous = gaugeSystems[0].values[a - 1];
								gaugeSystems[0].values[a - 1] = current;
								gaugeSystems[0].values[a] = previous;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = a != gaugeSystems[0].values.Count - 1;
							if(GUILayout.Button("▼",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								float current = gaugeSystems[0].values[a];
								float next = gaugeSystems[0].values[a + 1];
								gaugeSystems[0].values[a + 1] = current;
								gaugeSystems[0].values[a] = next;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = true;
							if(GUILayout.Button("-",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								gaugeSystems[0].values.RemoveAt(a);
								GUI.FocusControl(null);
								break;
							}
						}
						EditorGUILayout.EndHorizontal();
					}
					if(gaugeSystems[0].valuesScrollViewIndex + 5 < gaugeSystems[0].values.Count)
						GUILayout.Space((gaugeSystems[0].values.Count - (gaugeSystems[0].valuesScrollViewIndex + 5)) * 26);
					if(gaugeSystems[0].values.Count >= 5)
					{
						if(gaugeSystems[0].valuesScrollViewIndex != gaugeSystems[0].valuesScrollView.y / 26 && (Event.current.type == EventType.Repaint && Event.current.type == EventType.ScrollWheel || Event.current.type != EventType.Layout && Event.current.type != EventType.ScrollWheel))
							gaugeSystems[0].valuesScrollViewIndex = (int)gaugeSystems[0].valuesScrollView.y / 26;
						EditorGUILayout.EndScrollView();
					}
					EditorGUILayout.BeginHorizontal("Box");
					{
						EditorGUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label("Add a new Value?");
							GUILayout.FlexibleSpace();
						}
						EditorGUILayout.EndHorizontal();
						if(GUILayout.Button("+",GUILayout.Width(20),GUILayout.Height(20)))
						{
							Undo.RecordObject(target,"Inspector");
							gaugeSystems[0].values.Add(gaugeSystems[0].values.Count);
							GUI.FocusControl(null);
						}
					}
					EditorGUILayout.EndHorizontal();
				}
				if(Application.isPlaying && gaugeSystems[0].values.Count == 0)
					EditorGUILayout.HelpBox("Values should not be left empty",MessageType.Error);
			}
			EditorGUILayout.EndVertical();
		}
		private void LinesSectionCustomLineContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useCustomLine"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					EditorGUILayout.LabelField("Custom Line",EditorStyles.boldLabel);
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].useCustomLine)
				{
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.FlexibleSpace();
								GUILayout.Label("Big Line");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("bigLine"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.FlexibleSpace();
								GUILayout.Label("Small Line");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("smallLine"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void AnglesSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Angles","BoldLabel");
				EditorGUILayout.BeginVertical("Box");
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						GUILayout.Label("Minimum Angle");
						GUILayout.FlexibleSpace();
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumAngle"),GUIContent.none,true);
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.BeginVertical("Box");
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						GUILayout.Label("Maximum Angle");
						GUILayout.FlexibleSpace();
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumAngle"),GUIContent.none,true);
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndVertical();
		}
		private void ValuesSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Values","BoldLabel");
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].clamp;
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label("Minimum Value",new GUIStyle() {fontSize = 8});
							GUILayout.FlexibleSpace();
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.PropertyField(serializedObject.FindProperty("minimumValue"),GUIContent.none,true);
					}
					EditorGUILayout.EndVertical();
					GUI.enabled = true;
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label("Value",new GUIStyle() {fontSize = 9});
							GUILayout.FlexibleSpace();
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.PropertyField(serializedObject.FindProperty("value"),GUIContent.none,true);
					}
					EditorGUILayout.EndVertical();
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.BeginHorizontal();
						{
							GUILayout.FlexibleSpace();
							GUILayout.Label("Maximum Value",new GUIStyle() {fontSize = 8});
							GUILayout.FlexibleSpace();
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"),GUIContent.none,true);
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndHorizontal();
				GUI.backgroundColor = gaugeSystems[0].clamp ? Color.green : Color.red;
				if(GUILayout.Button("Clamp"))
				{
					Undo.RecordObjects(targets,"Inspector");
					gaugeSystems[0].clamp = !gaugeSystems[0].clamp;
					for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].clamp != gaugeSystems[0].clamp)
						gaugeSystems[gaugeSystemsIndex].clamp = gaugeSystems[0].clamp;
					GUI.FocusControl(null);
				}
				GUI.backgroundColor = Color.white;
				if(gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog)ValuesSectionNeedleContainer();
				if(gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital)ValuesSectionTextContainer();
			}
			EditorGUILayout.EndVertical();
		}
		private void ValuesSectionNeedleContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useNeedle"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					EditorGUILayout.LabelField("Needle",EditorStyles.boldLabel);
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].useNeedle)
				{
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needleUpdateMode"),true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needleHandler"),true);
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.BeginHorizontal();
						{
							EditorGUIUtility.labelWidth = 1;
							EditorGUILayout.PropertyField(serializedObject.FindProperty("useCustomNeedle"),GUIContent.none,true);
							EditorGUIUtility.labelWidth = 0;
							EditorGUILayout.LabelField("Custom Needle",EditorStyles.boldLabel);
							GUILayout.FlexibleSpace();
						}
						EditorGUILayout.EndHorizontal();
						if(gaugeSystems[0].useCustomNeedle)EditorGUILayout.PropertyField(serializedObject.FindProperty("needle"),GUIContent.none,true);
					}
					EditorGUILayout.EndVertical();
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void ValuesSectionTextContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useText"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					EditorGUILayout.LabelField("Text",EditorStyles.boldLabel);
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].useText)
				{
					EditorGUILayout.PropertyField(serializedObject.FindProperty("textUpdateMode"),true);
					GUI.enabled = !gaugeSystems[0].text;
					EditorGUILayout.BeginHorizontal();
					{
						GUI.backgroundColor = gaugeSystems[0].createTextAutomatically ? Color.green : Color.red;
						if(GUILayout.Button("Create Automatically"))
						{
							Undo.RecordObjects(targets,"Inspector");
							gaugeSystems[0].createTextAutomatically = !gaugeSystems[0].createTextAutomatically;
							for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].createTextAutomatically != gaugeSystems[0].createTextAutomatically)
								gaugeSystems[gaugeSystemsIndex].createTextAutomatically = gaugeSystems[0].createTextAutomatically;
							GUI.FocusControl(null);
						}
						GUI.enabled = true;
						GUI.backgroundColor = gaugeSystems[0].absolute ? Color.green : Color.red;
						if(GUILayout.Button("Absolute"))
						{
							Undo.RecordObjects(targets,"Inspector");
							gaugeSystems[0].absolute = !gaugeSystems[0].absolute;
							for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].absolute != gaugeSystems[0].absolute)
								gaugeSystems[gaugeSystemsIndex].absolute = gaugeSystems[0].absolute;
							GUI.FocusControl(null);
						}
						GUI.backgroundColor = Color.white;
					}
					EditorGUILayout.EndHorizontal();
					if(gaugeSystems[0].createTextAutomatically)
					{
						EditorGUILayout.PropertyField(serializedObject.FindProperty("textOffset"),true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("textSize"),true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("textFont"),true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("textColor"),true);
						EditorGUILayout.BeginHorizontal();
						{
							GUI.enabled = !gaugeSystems[0].text;
							if(GUILayout.Button("Create"))
							{
								for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)
									gaugeSystems[gaugeSystemsIndex].CreateTextAutomatically();
								GUI.FocusControl(null);
							}
							GUI.enabled = gaugeSystems[0].text;
							if(GUILayout.Button("Delete"))
							{
								Undo.RecordObjects(targets,"Inspector");
								for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)
									if(gaugeSystems[gaugeSystemsIndex].text)Undo.DestroyObjectImmediate(gaugeSystems[gaugeSystemsIndex].text.gameObject);
								GUI.FocusControl(null);
							}
							GUI.enabled = true;
						}
						EditorGUILayout.EndHorizontal();
					}
					else
					{
						EditorGUILayout.PropertyField(serializedObject.FindProperty("textName"),true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("text"),true);
					}
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void ConfigureSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Configuration","BoldLabel");
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("bigLinePosition"),true);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("smallLinePosition"),true);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					GUI.enabled = gaugeSystems[0].useLineText;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("lineTextPosition"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].useNeedle;
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useNeedlePosition"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUI.enabled = gaugeSystems[0].useNeedle && gaugeSystems[0].useNeedlePosition;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needlePosition"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("bigLineSize"),true);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("smallLineSize"),true);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label(GUIContent.none);
					GUILayout.Space(11);
					GUI.enabled = gaugeSystems[0].useLineText;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("lineTextSize"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].useNeedle;
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useNeedleSize"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUI.enabled = gaugeSystems[0].useNeedle && gaugeSystems[0].useNeedleSize;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needleSize"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].useNeedle;
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useNeedlePivotSize"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUI.enabled = gaugeSystems[0].useNeedle && gaugeSystems[0].useNeedlePivotSize;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needlePivotSize"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital && gaugeSystems[0].digitalType == GaugeSystem.DigitalType.Color)
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label(GUIContent.none);
						GUILayout.Space(11);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("lineColorOn"),true);
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label(GUIContent.none);
						GUILayout.Space(11);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("lineColorOff"),true);
					}
					EditorGUILayout.EndHorizontal();
				}
				else
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUI.enabled = gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital && gaugeSystems[0].digitalType == GaugeSystem.DigitalType.Activation;
						EditorGUIUtility.labelWidth = 1;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("useBigLineColor"),GUIContent.none,true);
						EditorGUIUtility.labelWidth = 0;
						GUI.enabled = (gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital && gaugeSystems[0].digitalType == GaugeSystem.DigitalType.Activation) && gaugeSystems[0].useBigLineColor;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("bigLineColor"),true);
						GUI.enabled = true;
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					{
						GUI.enabled = gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital && gaugeSystems[0].digitalType == GaugeSystem.DigitalType.Activation;
						EditorGUIUtility.labelWidth = 1;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("useSmallLineColor"),GUIContent.none,true);
						EditorGUIUtility.labelWidth = 0;
						GUI.enabled = (gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Analog || gaugeSystems[0].gaugeType == GaugeSystem.GaugeType.Digital && gaugeSystems[0].digitalType == GaugeSystem.DigitalType.Activation) && gaugeSystems[0].useSmallLineColor;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("smallLineColor"),true);
						GUI.enabled = true;
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].useNeedle;
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useNeedleColor"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUI.enabled = gaugeSystems[0].useNeedle && gaugeSystems[0].useNeedleColor;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("needleColor"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUI.enabled = gaugeSystems[0].useLineText;
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("useLineTextColor"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUI.enabled = gaugeSystems[0].useLineText && gaugeSystems[0].useLineTextColor;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("lineTextColor"),true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				{
					GUI.backgroundColor = gaugeSystems[0].useLineText ? Color.green : Color.red;
					if(GUILayout.Button("Line Text"))
					{
						Undo.RecordObjects(targets,"Inspector");
						gaugeSystems[0].useLineText = !gaugeSystems[0].useLineText;
						for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].useLineText != gaugeSystems[0].useLineText)
							gaugeSystems[gaugeSystemsIndex].useLineText = gaugeSystems[0].useLineText;
						GUI.FocusControl(null);
					}
					GUI.enabled = gaugeSystems[0].useLineText;
					GUI.backgroundColor = gaugeSystems[0].useWorldTextRotation ? Color.green : Color.red;
					if(GUILayout.Button("World Text Rotation"))
					{
						Undo.RecordObjects(targets,"Inspector");
						gaugeSystems[0].useWorldTextRotation = !gaugeSystems[0].useWorldTextRotation;
						for(int gaugeSystemsIndex = 0; gaugeSystemsIndex < gaugeSystems.Length; gaugeSystemsIndex++)if(gaugeSystems[gaugeSystemsIndex].useWorldTextRotation != gaugeSystems[0].useWorldTextRotation)
							gaugeSystems[gaugeSystemsIndex].useWorldTextRotation = gaugeSystems[0].useWorldTextRotation;
						GUI.FocusControl(null);
					}
					GUI.backgroundColor = Color.white;
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
	}
	#endif
}