namespace AdvancedAssets.Utility.GaugeSystem
{
	using UnityEngine;
	#if UNITY_EDITOR
	using UnityEditor;
	#endif
	using System.Collections.Generic;
	[AddComponentMenu("Advanced Assets/Utility/Gauge System/Gauge Target",30),ExecuteInEditMode]
	public class GaugeTarget : MonoBehaviour
	{
		public enum UpdateMode {OnAwake,EveryFrame,ViaScripting}
		public UpdateMode updateMode = UpdateMode.EveryFrame;
		#if UNITY_EDITOR
		public string targetsName = "Untitled";
		#endif
		public List<Target> targets = new List<Target>();
		#if UNITY_EDITOR
		[HideInInspector] public bool targetsIsExpanded = true;
		#endif
		[System.Serializable] public class Target
		{
			public string name = string.Empty;
			public GaugeTargetProfile profile = null;
			public GaugeSystem gauge = null;
			public Component source = null;
			#if UNITY_EDITOR
			[HideInInspector] public bool applyValuesAutomatically = false;
			[HideInInspector] public byte valuesCount = 5;
			#endif
			public bool overrideValues = true;
			public List<float> values = new List<float>();
			#if UNITY_5_3_OR_NEWER
			[Delayed] public string valueName = string.Empty;
			[Delayed] public string maximumValueName = string.Empty;
			#else
			public string valueName = string.Empty;
			public string maximumValueName = string.Empty;
			#endif
			public bool overrideValue = true;
			public float value = 0;
			public bool overrideMaximumValue = true;
			public float maximumValue = 100;
			public bool absolute = true;
			[HideInInspector] public bool isUpdating = false;
			#if UNITY_EDITOR
			[HideInInspector] public int valueIndex = 0;
			[HideInInspector] public int maximumValueIndex = 0;
			[HideInInspector] public int valuesScrollViewIndex = 0;
			[HideInInspector] public bool isExpanded = false;
			[HideInInspector] public bool valuesIsExpanded = true;
			[HideInInspector] public Vector2 valuesScrollView = Vector2.zero;
			#endif
			public void Update (bool run)
			{
				EditorHandler();
				if(run)
				{
					if(isUpdating)isUpdating = false;
					if(overrideValue && valueName != string.Empty)
						GetVariableValue(valueName,ref value);
					if(overrideMaximumValue && maximumValueName != string.Empty)
						GetVariableValue(maximumValueName,ref maximumValue);
					if(gauge)
					{
						if(overrideValues)gauge.SetValues(values);
						if(overrideValue)gauge.SetValue(value);
						if(overrideMaximumValue)gauge.SetMaximumValue(maximumValue);
					}
				}
			}
			private void EditorHandler ()
			{
				if(Application.isPlaying && !this.gauge)
				{
					GaugeSystem gauge = (GaugeSystem)GameObject.FindObjectOfType(typeof(GaugeSystem));
					if(gauge)this.gauge = gauge;
				}
				#if UNITY_EDITOR
				if(!overrideValues && applyValuesAutomatically)
					applyValuesAutomatically = false;
				valuesCount = (byte)Mathf.Clamp(valuesCount,2,100);
				#endif
				maximumValue = Mathf.Clamp(maximumValue,0.1f,float.MaxValue);
				if(profile)
				{
					#if UNITY_EDITOR
					if(applyValuesAutomatically)applyValuesAutomatically = false;
					#endif
					if(profile.overrideName)SetName(profile.name);
					if(profile.overrideValues)SetValues(profile.values);
					if(profile.overrideValue && profile.valueName == string.Empty)
						SetValue(profile.value);
					if(profile.overrideMaximumValue && profile.maximumValueName == string.Empty)
						SetMaximumValue(profile.maximumValue);
					if(profile.valueName != string.Empty)
						SetValueName(profile.valueName);
					if(profile.maximumValueName != string.Empty)
						SetMaximumValueName(profile.maximumValueName);
				}
				else
				{
					FilterHandler(ref valueName);
					FilterHandler(ref maximumValueName);
				}
			}
			private void FilterHandler (ref string name)
			{
				if(name == string.Empty)return;
				char[] chars = name.ToCharArray();
				for(int a = 0,count = 0; a < chars.Length; a++)
				{
					if(a > 0 && chars[a] == '.')
					{
						if(count > 0)
						{
							name = name.Remove(a,1);
							chars = name.ToCharArray();
							a = a - 1;
						}
						count = count + 1;
					}
					if(count > 0 && chars[a] == '.' && a + 1 < chars.Length && chars[a + 1] != 'x' && chars[a + 1] != 'y' && chars[a + 1] != 'z')
					{
						name = name.Remove(a + 1,1);
						chars = name.ToCharArray();
						count = count - 1;
						a = a - 1;
					}
					if(count > 0 && a + 1 < chars.Length && (chars[a + 1] == 'x' || chars[a + 1] == 'y' || chars[a + 1] == 'z') && a + 2 < chars.Length)
					{
						name = name.Remove(a + 2,1);
						chars = name.ToCharArray();
						count = count - 1;
						a = a - 1;
					}
					if(chars[a] != 'a' && chars[a] != 'b' && chars[a] != 'c' && chars[a] != 'd' && chars[a] != 'e' && chars[a] != 'f' && chars[a] != 'g' && chars[a] != 'h' && chars[a] != 'i' && chars[a] != 'j' && chars[a] != 'k' && chars[a] != 'l' && chars[a] != 'm' && chars[a] != 'n' && chars[a] != 'o' && chars[a] != 'p' && chars[a] != 'q' && chars[a] != 'r' && chars[a] != 's' && chars[a] != 't' && chars[a] != 'u' && chars[a] != 'v' && chars[a] != 'w' && chars[a] != 'x' && chars[a] != 'y' && chars[a] != 'z')
					{
						if(chars[a] != 'A' && chars[a] != 'B' && chars[a] != 'C' && chars[a] != 'D' && chars[a] != 'E' && chars[a] != 'F' && chars[a] != 'G' && chars[a] != 'H' && chars[a] != 'I' && chars[a] != 'J' && chars[a] != 'K' && chars[a] != 'L' && chars[a] != 'M' && chars[a] != 'N' && chars[a] != 'O' && chars[a] != 'P' && chars[a] != 'Q' && chars[a] != 'R' && chars[a] != 'S' && chars[a] != 'T' && chars[a] != 'U' && chars[a] != 'V' && chars[a] != 'W' && chars[a] != 'X' && chars[a] != 'Y' && chars[a] != 'Z')
						{
							if((chars[a] != '0' && chars[a] != '1' && chars[a] != '2' && chars[a] != '3' && chars[a] != '4' && chars[a] != '5' && chars[a] != '6' && chars[a] != '7' && chars[a] != '8' && chars[a] != '9') || (a == 0 && (chars[a] == '0' || chars[a] == '1' || chars[a] == '2' || chars[a] == '3' || chars[a] == '4' || chars[a] == '5' || chars[a] == '6' || chars[a] == '7' || chars[a] == '8' || chars[a] == '9')))
							{
								if((chars[a] != '.' || a == 0 && chars[a] == '.') && chars[a] != '_')
								{
									name = name.Remove(a,1);
									chars = name.ToCharArray();
									a = a - 1;
								}
							}
						}
					}
				}
			}
			private void GetVariableValue (string variableName,ref float variableValue)
			{
				if(!source)return;
				string name = variableName.Replace(".x",string.Empty).Replace(".y",string.Empty).Replace(".z",string.Empty);
				if(source.GetType().BaseType == typeof(MonoBehaviour))
				{
					System.Reflection.FieldInfo fieldInfo = (name != string.Empty ? source.GetType().GetField(name,System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic) : null);
					if(fieldInfo != null)
					{
						if(fieldInfo.FieldType == typeof(float))
							variableValue = (float)fieldInfo.GetValue(source);
						if(fieldInfo.FieldType == typeof(int))
							variableValue = (int)fieldInfo.GetValue(source);
						if(fieldInfo.FieldType == typeof(Vector2))
						{
							if(variableName.Contains(".x"))variableValue = ((Vector2)fieldInfo.GetValue(source)).x;
							if(variableName.Contains(".y"))variableValue = ((Vector2)fieldInfo.GetValue(source)).y;
						}
						if(fieldInfo.FieldType == typeof(Vector3))
						{
							if(variableName.Contains(".x"))variableValue = ((Vector3)fieldInfo.GetValue(source)).x;
							if(variableName.Contains(".y"))variableValue = ((Vector3)fieldInfo.GetValue(source)).y;
							if(variableName.Contains(".z"))variableValue = ((Vector3)fieldInfo.GetValue(source)).z;
						}
					}
				}
				else if(source.GetType() != typeof(Transform))
				{
					System.Reflection.PropertyInfo propertyInfo = (name != string.Empty ? source.GetType().GetProperty(name) : null);
					if(propertyInfo != null)
					{
						if(propertyInfo.PropertyType == typeof(float))
							variableValue = (float)propertyInfo.GetValue(source,null);
						if(propertyInfo.PropertyType == typeof(int))
							variableValue = (int)propertyInfo.GetValue(source,null);
						if(propertyInfo.PropertyType == typeof(Vector2))
						{
							if(variableName.Contains(".x"))variableValue = ((Vector2)propertyInfo.GetValue(source,null)).x;
							if(variableName.Contains(".y"))variableValue = ((Vector2)propertyInfo.GetValue(source,null)).y;
						}
						if(propertyInfo.PropertyType == typeof(Vector3))
						{
							if(variableName.Contains(".x"))variableValue = ((Vector3)propertyInfo.GetValue(source,null)).x;
							if(variableName.Contains(".y"))variableValue = ((Vector3)propertyInfo.GetValue(source,null)).y;
							if(variableName.Contains(".z"))variableValue = ((Vector3)propertyInfo.GetValue(source,null)).z;
						}
					}
				}
				if(absolute)variableValue = Mathf.Abs(variableValue);
			}
			public void SetName (string value) {if(name != value)name = value;}
			public void SetProfile (GaugeTargetProfile value) {if(profile != value)profile = value;}
			public void SetGauge (GaugeSystem value) {if(gauge != value)gauge = value;}
			public void SetSource (Component value) {if(source != value)source = value;}
			public void OverrideValues (bool value) {if(overrideValues != value)overrideValues = value;}
			public void SetValues (List<float> value) {if(values != value)values = value;}
			public void SetValues (float[] value)
			{
				List<float> convertedValue = new List<float>(value);
				if(values != convertedValue)values = convertedValue;
			}
			public void SetValueName (string value) {if(valueName != value)valueName = value;}
			public void SetMaximumValueName (string value) {if(maximumValueName != value)maximumValueName = value;}
			public void OverrideValue (bool value) {if(overrideValue != value)overrideValue = value;}
			public void SetValue (float value) {if(this.value != value)this.value = value;}
			public void OverrideMaximumValue (bool value) {if(overrideMaximumValue != value)overrideMaximumValue = value;}
			public void SetMaximumValue (float value) {if(maximumValue != value)maximumValue = value;}
			public void Absolute (bool value) {if(absolute != value)absolute = value;}
			public void ApplyValuesAutomatically (byte count)
			{
				if(values.Count != count + 1)
					values = new List<float>(new float[count + 1]);
				for(int a = 0,A = values.Count; a < A; a++)if(values[a] != maximumValue / count * a)
					values[a] = maximumValue / count * a;
			}
		}
		#if UNITY_EDITOR
		[MenuItem("Advanced Assets/Utility/Gauge System/Gauge Target",false,30)]
		private static void AddComponent ()
		{
			for(int a = 0,A = Selection.gameObjects.Length; a < A; a++)
				Undo.AddComponent(Selection.gameObjects[a],typeof(GaugeTarget));
		}
		[MenuItem("Advanced Assets/Utility/Gauge System/Gauge Target",true)]
		private static bool AddComponentCheck () {return Selection.gameObjects.Length > 0;}
		#endif
		private void Awake ()
		{
			if(!Application.isPlaying || updateMode != UpdateMode.OnAwake)return;
			for(int a = 0,A = targets.Count; a < A; a++)
				targets[a].Update(true);
		}
		private void Update ()
		{
			for(int a = 0,A = targets.Count; a < A; a++)
				targets[a].Update(Application.isPlaying && (updateMode == UpdateMode.EveryFrame || (updateMode == UpdateMode.OnAwake || updateMode == UpdateMode.ViaScripting) && targets[a].isUpdating));
		}
		public void SetUpdateMode (UpdateMode value) {if(updateMode != value)updateMode = value;}
		public void SetUpdateMode (int value)
		{
			UpdateMode convertedValue = (UpdateMode)value;
			if(updateMode != convertedValue)updateMode = convertedValue;
		}
		public void SetTargets (List<Target> value) {if(targets != value)targets = value;}
		public void SetTargets (Target[] value)
		{
			List<Target> convertedValue = new List<Target>(value);
			if(targets != convertedValue)targets = convertedValue;
		}
		public void UpdateTargetAtIndex (int value)
		{
			if((updateMode == UpdateMode.OnAwake || updateMode == UpdateMode.ViaScripting) && value >= 0 && value < targets.Count && !targets[value].isUpdating)
				targets[value].isUpdating = true;
		}
		public void UpdateAllTargets ()
		{
			if(updateMode == UpdateMode.OnAwake || updateMode == UpdateMode.ViaScripting)for(int a = 0,A = targets.Count; a < A; a++)if(!targets[a].isUpdating)
				targets[a].isUpdating = true;
		}
	}
	#if UNITY_EDITOR
	[CustomEditor(typeof(GaugeTarget)),CanEditMultipleObjects]
	internal class GaugeTargetEditor : Editor
	{
		private GaugeTarget[] gaugeTargets
		{
			get
			{
				GaugeTarget[] gaugeTargets = new GaugeTarget[targets.Length];
				for(int gaugeTargetsIndex = 0; gaugeTargetsIndex < targets.Length; gaugeTargetsIndex++)
					gaugeTargets[gaugeTargetsIndex] = (GaugeTarget)targets[gaugeTargetsIndex];
				return gaugeTargets;
			}
		}
		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			MainSection();
			if(GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
				for(int gaugeTargetsIndex = 0; gaugeTargetsIndex < gaugeTargets.Length; gaugeTargetsIndex++)
					EditorUtility.SetDirty(gaugeTargets[gaugeTargetsIndex]);
			}
		}
		private void MainSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Main","BoldLabel");
				EditorGUILayout.PropertyField(serializedObject.FindProperty("updateMode"),true);
				if(!serializedObject.isEditingMultipleObjects)MainSectionTargetsContainer();
				else
				{
					GUI.enabled = false;
					EditorGUILayout.BeginHorizontal("Box");
					GUILayout.Box("Targets",GUILayout.ExpandWidth(true));
					EditorGUILayout.EndHorizontal();
					GUI.enabled = true;
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void MainSectionTargetsContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					if(GUILayout.Button("Targets","Box",GUILayout.ExpandWidth(true)))
					{
						gaugeTargets[0].targetsIsExpanded = !gaugeTargets[0].targetsIsExpanded;
						GUI.FocusControl(null);
					}
					GUI.enabled = gaugeTargets[0].targets.Count != 0;
					if(GUILayout.Button("X",GUILayout.Width(20),GUILayout.Height(20)))
					{
						Undo.RecordObject(target,"Inspector");
						gaugeTargets[0].targets.Clear();
						GUI.FocusControl(null);
					}
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeTargets[0].targetsIsExpanded)
				{
					for(int a = 0; a < gaugeTargets[0].targets.Count; a++)
					{
						GaugeTarget.Target currentTarget = gaugeTargets[0].targets[a];
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.Box(a.ToString("000"));
								if(GUILayout.Button(gaugeTargets[0].targets[a].name,"Box",GUILayout.ExpandWidth(true)))
								{
									gaugeTargets[0].targets[a].isExpanded = !gaugeTargets[0].targets[a].isExpanded;
									GUI.FocusControl(null);
								}
								GUI.enabled = a != 0;
								if(GUILayout.Button("▲",GUILayout.Width(20),GUILayout.Height(20)))
								{
									Undo.RecordObject(target,"Inspector");
									GaugeTarget.Target previous = gaugeTargets[0].targets[a - 1];
									gaugeTargets[0].targets[a - 1] = currentTarget;
									gaugeTargets[0].targets[a] = previous;
									GUI.FocusControl(null);
									break;
								}
								GUI.enabled = a != gaugeTargets[0].targets.Count - 1;
								if(GUILayout.Button("▼",GUILayout.Width(20),GUILayout.Height(20)))
								{
									Undo.RecordObject(target,"Inspector");
									GaugeTarget.Target next = gaugeTargets[0].targets[a + 1];
									gaugeTargets[0].targets[a + 1] = currentTarget;
									gaugeTargets[0].targets[a] = next;
									GUI.FocusControl(null);
									break;
								}
								GUI.enabled = true;
								if(GUILayout.Button("-",GUILayout.Width(20),GUILayout.Height(20)))
								{
									Undo.RecordObject(target,"Inspector");
									gaugeTargets[0].targets.RemoveAt(a);
									GUI.FocusControl(null);
									break;
								}
							}
							EditorGUILayout.EndHorizontal();
							if(currentTarget.isExpanded)
							{
								SerializedProperty currentTargetProperty = serializedObject.FindProperty("targets").GetArrayElementAtIndex(a);
								EditorGUILayout.BeginHorizontal();
								{
									GUILayout.Space(20);
									EditorGUILayout.BeginVertical();
									{
										EditorGUILayout.BeginVertical("Box");
										{
											EditorGUIUtility.labelWidth = 100;
											EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("profile"),true);
											EditorGUILayout.BeginHorizontal();
											{
												if(GUILayout.Button(new GUIContent() {text = "Import",tooltip = "Load from an asset file"}))
												{
													string path = EditorUtility.OpenFilePanel("Load Asset",string.Empty,"asset");
													if(path != string.Empty)
													{
														if(path.StartsWith(Application.dataPath))
														{
															GaugeTargetProfile profile = (GaugeTargetProfile)AssetDatabase.LoadAssetAtPath(path.Replace(Application.dataPath,"Assets"),typeof(GaugeTargetProfile));
															if(profile)
															{
																Undo.RecordObject(target,"Inspector");
																if(profile.overrideName)currentTarget.SetName(profile.name);
																if(profile.overrideValues)currentTarget.SetValues(profile.values);
																if(profile.valueName != string.Empty)
																	currentTarget.SetValueName(profile.valueName);
																if(profile.maximumValueName != string.Empty)
																	currentTarget.SetMaximumValueName(profile.maximumValueName);
																currentTarget.SetProfile(profile);
															}
															else Debug.LogError("The selected asset is not a Gauge Target Profile asset",gaugeTargets[0]);
														}
														else Debug.LogError("The selected asset is not in the '" + Application.productName + "/Assets' folder",gaugeTargets[0]);
													}
													GUI.FocusControl(null);
												}
												if(GUILayout.Button(new GUIContent() {text = "Export",tooltip = "Save to an asset file"}))
												{
													string path = EditorUtility.SaveFilePanelInProject("Save Asset",currentTarget.name != string.Empty ? currentTarget.name : "New Gauge Target Profile","asset",string.Empty);
													if(path != string.Empty)
													{
														GaugeTargetProfile profile = ScriptableObject.CreateInstance<GaugeTargetProfile>();
														profile.SetName(currentTarget.name);
														profile.SetValues(currentTarget.values);
														profile.SetValueName(currentTarget.valueName);
														profile.SetMaximumValueName(currentTarget.maximumValueName);
														currentTarget.SetProfile(profile);
														AssetDatabase.CreateAsset(profile,path);
													}
													GUI.FocusControl(null);
												}
											}
											EditorGUILayout.EndHorizontal();
										}
										EditorGUILayout.EndVertical();
										EditorGUILayout.BeginVertical("Box");
										{
											bool reset = false;
											GUI.enabled = !currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideName;
											EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("name"),true);
											GUI.enabled = true;
											EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("gauge"),true);
											if(!currentTarget.source || currentTarget.source && currentTarget.source.GetType() != typeof(Transform))
												EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("source"),true);
											if(currentTarget.source && currentTarget.source.GetType() == typeof(Transform))
												MainSectionTargetsContainerComponentSelectionContainer(currentTarget,currentTargetProperty,ref reset);
											EditorGUILayout.BeginVertical("Box");
											{
												GUI.enabled = currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
												GUI.backgroundColor = currentTarget.applyValuesAutomatically ? Color.green : Color.red;
												if(GUILayout.Button("Apply EveryFrame"))
												{
													Undo.RecordObject(target,"Inspector");
													currentTarget.applyValuesAutomatically = !currentTarget.applyValuesAutomatically;
													GUI.FocusControl(null);
												}
												GUI.enabled = true;
												GUI.backgroundColor = Color.white;
												if(currentTarget.applyValuesAutomatically)
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
																	GUILayout.Label("Maximum",new GUIStyle() {fontSize = 11});
																	GUILayout.FlexibleSpace();
																}
																EditorGUILayout.EndHorizontal();
																EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("maximumValue"),GUIContent.none,true);
															}
															EditorGUILayout.EndVertical();
															EditorGUILayout.BeginVertical("Box");
															{
																EditorGUILayout.BeginHorizontal();
																{
																	GUILayout.FlexibleSpace();
																	GUILayout.Label("Count",new GUIStyle() {fontSize = 11});
																	GUILayout.FlexibleSpace();
																}
																EditorGUILayout.EndHorizontal();
																EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("valuesCount"),GUIContent.none,true);
															}
															EditorGUILayout.EndVertical();
														}
														EditorGUILayout.EndHorizontal();
														EditorGUILayout.BeginVertical(GUILayout.MaxWidth(130));
														{
															float result = currentTarget.maximumValue / currentTarget.valuesCount;
															GUIStyle style = new GUIStyle() {fontSize = 11};
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
															Undo.RecordObject(target,"Inspector");
															currentTarget.ApplyValuesAutomatically(currentTarget.valuesCount);
															GUI.FocusControl(null);
														}
														GUI.enabled = currentTarget.values.Count != 0;
														if(GUILayout.Button("Reset"))
														{
															Undo.RecordObject(target,"Inspector");
															currentTarget.values.Clear();
															GUI.FocusControl(null);
														}
														GUI.enabled = true;
													}
													EditorGUILayout.EndHorizontal();
												}
												else MainSectionTargetsContainerValuesContainer(currentTarget,currentTargetProperty);
											}
											EditorGUILayout.EndVertical();
											MainSectionTargetsContainerVariableNamesContainer(currentTarget,currentTargetProperty,reset);
											EditorGUILayout.BeginHorizontal();
											{
												EditorGUILayout.BeginVertical("Box");
												{
													EditorGUILayout.BeginHorizontal();
													{
														EditorGUIUtility.labelWidth = 1;
														EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("overrideValue"),GUIContent.none,true);
														EditorGUIUtility.labelWidth = 0;
														GUILayout.Label("Value");
														GUILayout.FlexibleSpace();
													}
													EditorGUILayout.EndHorizontal();
													GUI.enabled = currentTarget.overrideValue && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValue) && currentTarget.valueIndex == 0;
													EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("value"),GUIContent.none,true);
													GUI.enabled = true;
												}
												EditorGUILayout.EndVertical();
												EditorGUILayout.BeginVertical("Box");
												{
													EditorGUILayout.BeginHorizontal();
													{
														EditorGUIUtility.labelWidth = 1;
														EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("overrideMaximumValue"),GUIContent.none,true);
														EditorGUIUtility.labelWidth = 0;
														GUILayout.Label("Maximum Value");
														GUILayout.FlexibleSpace();
													}
													EditorGUILayout.EndHorizontal();
													GUI.enabled = currentTarget.overrideMaximumValue && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideMaximumValue) && currentTarget.maximumValueIndex == 0;
													EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("maximumValue"),GUIContent.none,true);
													GUI.enabled = true;
												}
												EditorGUILayout.EndVertical();
											}
											EditorGUILayout.EndHorizontal();
											GUI.backgroundColor = currentTarget.absolute ? Color.green : Color.red;
											if(GUILayout.Button("Absolute"))
											{
												Undo.RecordObject(target,"Inspector");
												currentTarget.absolute = !currentTarget.absolute;
												GUI.FocusControl(null);
											}
											GUI.backgroundColor = Color.white;
										}
										EditorGUILayout.EndVertical();
									}
									EditorGUILayout.EndVertical();
								}
								EditorGUILayout.EndHorizontal();
							}
						}
						EditorGUILayout.EndVertical();
					}
					EditorGUILayout.BeginHorizontal("Box");
					{
						EditorGUIUtility.labelWidth = 40;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("targetsName"),new GUIContent("Name"),true);
						if(GUILayout.Button("+",GUILayout.Width(20),GUILayout.Height(20)))
						{
							Undo.RecordObject(target,"Inspector");
							gaugeTargets[0].targets.Add(new GaugeTarget.Target() {name = gaugeTargets[0].targetsName});
							GUI.FocusControl(null);
						}
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void MainSectionTargetsContainerComponentSelectionContainer (GaugeTarget.Target currentTarget,SerializedProperty currentTargetProperty,ref bool reset)
		{
			Component[] components = currentTarget.source.GetComponents<Component>();
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("source"),true);
				if(currentTarget.valueName != string.Empty)
					currentTarget.valueName = string.Empty;
				if(currentTarget.maximumValueName != string.Empty)
					currentTarget.maximumValueName = string.Empty;
				for(int a = 1; a < components.Length; a++)
				{
					string name = components[a].GetType().Name;
					char[] chars = name.ToCharArray();
					EditorGUILayout.BeginHorizontal("Box");
					{
						for(int b = 1; b < chars.Length - 1; b++)
						{
							if(char.IsLower(chars[b]) && char.IsUpper(chars[b + 1]))
							{
								name = name.Insert(b + 1," ");
								chars = name.ToCharArray();
								b = b + 1;
							}
						}
						if(GUILayout.Button(EditorGUIUtility.ObjectContent(components[a],components[a].GetType()).image,"Label",GUILayout.Width(16),GUILayout.Height(16)) || GUILayout.Button(name,"Label"))
						{
							Undo.RecordObject(target,"Inspector");
							currentTarget.source = components[a];
							reset = true;
							GUI.FocusControl(null);
						}
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.BeginHorizontal("Box");
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label("Component Selection","BoldLabel");
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
		private void MainSectionTargetsContainerValuesContainer (GaugeTarget.Target currentTarget,SerializedProperty currentTargetProperty)
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal("Box");
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("overrideValues"),true,GUILayout.Width(15));
					EditorGUIUtility.labelWidth = 0;
					if(GUILayout.Button("Values","Label",GUILayout.ExpandWidth(true)))
					{
						currentTarget.valuesIsExpanded = !currentTarget.valuesIsExpanded;
						GUI.FocusControl(null);
					}
					GUI.enabled = currentTarget.values.Count != 0 && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
					if(GUILayout.Button("X",GUILayout.Width(16),GUILayout.Height(16)))
					{
						Undo.RecordObject(target,"Inspector");
						currentTarget.values.Clear();
						GUI.FocusControl(null);
					}
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				if(currentTarget.valuesIsExpanded)
				{
					if(currentTarget.values.Count >= 5)currentTarget.valuesScrollView = EditorGUILayout.BeginScrollView(currentTarget.valuesScrollView,GUILayout.Height(100));
					else
					{
						if(currentTarget.valuesScrollView != Vector2.zero)
							currentTarget.valuesScrollView = Vector2.zero;
						if(currentTarget.valuesScrollViewIndex != 0)
							currentTarget.valuesScrollViewIndex = 0;
					}
					if(currentTarget.valuesScrollViewIndex > 0)GUILayout.Space(currentTarget.valuesScrollViewIndex * 26);
					GUI.enabled = currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
					for(int a = currentTarget.valuesScrollViewIndex; a <= Mathf.Clamp(currentTarget.valuesScrollViewIndex + 4,0,currentTarget.values.Count - 1); a++)
					{
						EditorGUILayout.BeginHorizontal("Box");
						{
							EditorGUILayout.BeginHorizontal("Box");
							GUILayout.Box(a.ToString("000"),new GUIStyle() {fontSize = 8},GUILayout.ExpandWidth(false));
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("values").GetArrayElementAtIndex(a),GUIContent.none,true);
							GUI.enabled = a != 0 && currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
							if(GUILayout.Button("▲",GUILayout.Width(16),GUILayout.Height(16)))
							{
								float current = currentTarget.values[a];
								float previous = currentTarget.values[a - 1];
								Undo.RecordObject(target,"Inspector");
								currentTarget.values[a - 1] = current;
								currentTarget.values[a] = previous;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = a != currentTarget.values.Count - 1 && currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
							if(GUILayout.Button("▼",GUILayout.Width(16),GUILayout.Height(16)))
							{
								float current = currentTarget.values[a];
								float next = currentTarget.values[a + 1];
								Undo.RecordObject(target,"Inspector");
								currentTarget.values[a + 1] = current;
								currentTarget.values[a] = next;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
							if(GUILayout.Button("-",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								currentTarget.values.RemoveAt(a);
								GUI.FocusControl(null);
								break;
							}
						}
						EditorGUILayout.EndHorizontal();
					}
					GUI.enabled = true;
					if(currentTarget.valuesScrollViewIndex + 5 < currentTarget.values.Count)
						GUILayout.Space((currentTarget.values.Count - (currentTarget.valuesScrollViewIndex + 5)) * 26);
					if(currentTarget.values.Count >= 5)
					{
						if(currentTarget.valuesScrollViewIndex != currentTarget.valuesScrollView.y / 26 && (Event.current.type == EventType.Repaint && Event.current.type == EventType.ScrollWheel || Event.current.type != EventType.Layout && Event.current.type != EventType.ScrollWheel))
							currentTarget.valuesScrollViewIndex = (int)currentTarget.valuesScrollView.y / 26;
						EditorGUILayout.EndScrollView();
					}
					GUI.enabled = currentTarget.overrideValues && (!currentTarget.profile || currentTarget.profile && !currentTarget.profile.overrideValues);
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
							currentTarget.values.Add(currentTarget.values.Count);
							GUI.FocusControl(null);
						}
					}
					EditorGUILayout.EndHorizontal();
					GUI.enabled = true;
				}
			}
			EditorGUILayout.EndVertical();
		}
		private void MainSectionTargetsContainerVariableNamesContainer (GaugeTarget.Target currentTarget,SerializedProperty currentTargetProperty,bool reset)
		{
			List<string> names = new List<string>();
			string valueName = string.Empty;
			string maximumValueName = string.Empty;
			bool valueNameExists = false;
			bool maximumValueNameExists = false;
			Color color = GUI.color;
			names.Add("Not Specified");
			if(currentTarget.source)
			{
				if(currentTarget.source.GetType().BaseType == typeof(MonoBehaviour))
				{
					System.Reflection.FieldInfo[] fieldInfos = currentTarget.source.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
					for(int a = 0; a < fieldInfos.Length; a++)if(fieldInfos[a].IsPublic)
					{
						if(fieldInfos[a].FieldType == typeof(float))
						{
							names.Add("[Public] [Float] " + fieldInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && (names[b - 1].Contains("[Int]") || names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(int))
						{
							names.Add("[Public] [Int] " + fieldInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && (names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(Vector2))
						{
							names.Add("[Public] [Vector2] " + fieldInfos[a].Name + ".x");
							names.Add("[Public] [Vector2] " + fieldInfos[a].Name + ".y");
							int b = names.Count - 1;
							while(b > 0 && (names[b - 2].Contains("[Vector3]")))
							{
								string previousX = names[b - 4];
								string previousY = names[b - 3];
								string previousZ = names[b - 2];
								string currentX = names[b - 1];
								string currentY = names[b];
								names[b - 4] = currentX;
								names[b - 3] = currentY;
								names[b - 2] = previousX;
								names[b - 1] = previousY;
								names[b] = previousZ;
								b = b - 3;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(Vector3))
						{
							names.Add("[Public] [Vector3] " + fieldInfos[a].Name + ".x");
							names.Add("[Public] [Vector3] " + fieldInfos[a].Name + ".y");
							names.Add("[Public] [Vector3] " + fieldInfos[a].Name + ".z");
							continue;
						}
					}
					for(int a = 0; a < fieldInfos.Length; a++)if(fieldInfos[a].IsPrivate)
					{
						if(fieldInfos[a].FieldType == typeof(float))
						{
							names.Add("[Private] [Float] " + fieldInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && names[b - 1].Contains("[Private]") && (names[b - 1].Contains("[Int]") || names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(int))
						{
							names.Add("[Private] [Int] " + fieldInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && names[b - 1].Contains("[Private]") && (names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(Vector2))
						{
							names.Add("[Private] [Vector2] " + fieldInfos[a].Name + ".x");
							names.Add("[Private] [Vector2] " + fieldInfos[a].Name + ".y");
							int b = names.Count - 1;
							while(b > 0 && names[b - 1].Contains("[Private]") && (names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(fieldInfos[a].FieldType == typeof(Vector3))
						{
							names.Add("[Private] [Vector3] " + fieldInfos[a].Name + ".x");
							names.Add("[Private] [Vector3] " + fieldInfos[a].Name + ".y");
							names.Add("[Private] [Vector3] " + fieldInfos[a].Name + ".z");
							continue;
						}
					}
				}
				else if(currentTarget.source.GetType() != typeof(Transform))
				{
					System.Reflection.PropertyInfo[] propertyInfos = currentTarget.source.GetType().GetProperties();
					for(int a = 0; a < propertyInfos.Length; a++)
					{
						if(propertyInfos[a].PropertyType == typeof(float))
						{
							names.Add("[Float] " + propertyInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && (names[b - 1].Contains("[Int]") || names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(propertyInfos[a].PropertyType == typeof(int))
						{
							names.Add("[Int] " + propertyInfos[a].Name);
							int b = names.Count - 1;
							while(b > 0 && (names[b - 1].Contains("[Vector2]") || names[b - 1].Contains("[Vector3]")))
							{
								string current = names[b];
								string previous = names[b - 1];
								names[b] = previous;
								names[b - 1] = current;
								b = b - 1;
							}
							continue;
						}
						if(propertyInfos[a].PropertyType == typeof(Vector2))
						{
							names.Add("[Vector2] " + propertyInfos[a].Name + ".x");
							names.Add("[Vector2] " + propertyInfos[a].Name + ".y");
							int b = names.Count - 1;
							while(b > 0 && (names[b - 2].Contains("[Vector3]")))
							{
								string previousX = names[b - 4];
								string previousY = names[b - 3];
								string previousZ = names[b - 2];
								string currentX = names[b - 1];
								string currentY = names[b];
								names[b - 4] = currentX;
								names[b - 3] = currentY;
								names[b - 2] = previousX;
								names[b - 1] = previousY;
								names[b] = previousZ;
								b = b - 3;
							}
							continue;
						}
						if(propertyInfos[a].PropertyType == typeof(Vector3))
						{
							names.Add("[Vector3] " + propertyInfos[a].Name + ".x");
							names.Add("[Vector3] " + propertyInfos[a].Name + ".y");
							names.Add("[Vector3] " + propertyInfos[a].Name + ".z");
							continue;
						}
					}
				}
				if(currentTarget.overrideValue || currentTarget.overrideMaximumValue)
				{
					if(currentTarget.source.GetType().BaseType == typeof(MonoBehaviour))
					{
						System.Reflection.FieldInfo[] fieldInfos = currentTarget.source.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
						for(int a = 0; a < fieldInfos.Length; a++)
						{
							if(fieldInfos[a].FieldType == typeof(float) || fieldInfos[a].FieldType == typeof(int))
							{
								if(currentTarget.valueName == fieldInfos[a].Name)
								{
									if(currentTarget.valueIndex == 0)
										valueName = fieldInfos[a].Name;
									valueNameExists = true;
								}
								if(currentTarget.maximumValueName == fieldInfos[a].Name)
								{
									if(currentTarget.maximumValueIndex == 0)
										maximumValueName = fieldInfos[a].Name;
									maximumValueNameExists = true;
								}
								if(valueNameExists && maximumValueNameExists)break;
							}
							if(fieldInfos[a].FieldType == typeof(Vector2) || fieldInfos[a].FieldType == typeof(Vector3))
							{
								if(fieldInfos[a].FieldType == typeof(Vector2) && (currentTarget.valueName == fieldInfos[a].Name + ".x" || currentTarget.valueName == fieldInfos[a].Name + ".y") || fieldInfos[a].FieldType == typeof(Vector3) && (currentTarget.valueName == fieldInfos[a].Name + ".x" || currentTarget.valueName == fieldInfos[a].Name + ".y" || currentTarget.valueName == fieldInfos[a].Name + ".z"))
								{
									if(currentTarget.valueIndex == 0)
									{
										if(currentTarget.valueName.Contains(".x"))
											valueName = fieldInfos[a].Name + ".x";
										if(currentTarget.valueName.Contains(".y"))
											valueName = fieldInfos[a].Name + ".y";
										if(currentTarget.valueName.Contains(".z"))
											valueName = fieldInfos[a].Name + ".z";
									}
									valueNameExists = true;
								}
								if(fieldInfos[a].FieldType == typeof(Vector2) && (currentTarget.maximumValueName == fieldInfos[a].Name + ".x" || currentTarget.maximumValueName == fieldInfos[a].Name + ".y") || fieldInfos[a].FieldType == typeof(Vector3) && (currentTarget.maximumValueName == fieldInfos[a].Name + ".x" || currentTarget.maximumValueName == fieldInfos[a].Name + ".y" || currentTarget.maximumValueName == fieldInfos[a].Name + ".z"))
								{
									if(currentTarget.maximumValueIndex == 0)
									{
										if(currentTarget.maximumValueName.Contains(".x"))
											maximumValueName = fieldInfos[a].Name + ".x";
										if(currentTarget.maximumValueName.Contains(".y"))
											maximumValueName = fieldInfos[a].Name + ".y";
										if(currentTarget.maximumValueName.Contains(".z"))
											maximumValueName = fieldInfos[a].Name + ".z";
									}
									maximumValueNameExists = true;
								}
								if(valueNameExists && maximumValueNameExists)break;
							}
						}
					}
					else if(currentTarget.source.GetType() != typeof(Transform))
					{
						System.Reflection.PropertyInfo[] propertyInfos = currentTarget.source.GetType().GetProperties();
						for(int a = 0; a < propertyInfos.Length; a++)
						{
							if(propertyInfos[a].PropertyType == typeof(float) || propertyInfos[a].PropertyType == typeof(int))
							{
								if(currentTarget.valueName == propertyInfos[a].Name)
								{
									if(currentTarget.valueIndex == 0)
										valueName = propertyInfos[a].Name;
									valueNameExists = true;
								}
								if(currentTarget.maximumValueName == propertyInfos[a].Name)
								{
									if(currentTarget.maximumValueIndex == 0)
										maximumValueName = propertyInfos[a].Name;
									maximumValueNameExists = true;
								}
								if(valueNameExists && maximumValueNameExists)break;
							}
							if(propertyInfos[a].PropertyType == typeof(Vector2) || propertyInfos[a].PropertyType == typeof(Vector3))
							{
								if(propertyInfos[a].PropertyType == typeof(Vector2) && (currentTarget.valueName == propertyInfos[a].Name + ".x" || currentTarget.valueName == propertyInfos[a].Name + ".y") || propertyInfos[a].PropertyType == typeof(Vector3) && (currentTarget.valueName == propertyInfos[a].Name + ".x" || currentTarget.valueName == propertyInfos[a].Name + ".y" || currentTarget.valueName == propertyInfos[a].Name + ".z"))
								{
									if(currentTarget.valueIndex == 0)
									{
										if(currentTarget.valueName.Contains(".x"))
											valueName = propertyInfos[a].Name + ".x";
										if(currentTarget.valueName.Contains(".y"))
											valueName = propertyInfos[a].Name + ".y";
										if(currentTarget.valueName.Contains(".z"))
											valueName = propertyInfos[a].Name + ".z";
									}
									valueNameExists = true;
								}
								if(propertyInfos[a].PropertyType == typeof(Vector2) && (currentTarget.maximumValueName == propertyInfos[a].Name + ".x" || currentTarget.maximumValueName == propertyInfos[a].Name + ".y") || propertyInfos[a].PropertyType == typeof(Vector3) && (currentTarget.maximumValueName == propertyInfos[a].Name + ".x" || currentTarget.maximumValueName == propertyInfos[a].Name + ".y" || currentTarget.maximumValueName == propertyInfos[a].Name + ".z"))
								{
									if(currentTarget.maximumValueIndex == 0)
									{
										if(currentTarget.maximumValueName.Contains(".x"))
											maximumValueName = propertyInfos[a].Name + ".x";
										if(currentTarget.maximumValueName.Contains(".y"))
											maximumValueName = propertyInfos[a].Name + ".y";
										if(currentTarget.maximumValueName.Contains(".z"))
											maximumValueName = propertyInfos[a].Name + ".z";
									}
									maximumValueNameExists = true;
								}
								if(valueNameExists && maximumValueNameExists)break;
							}
						}
					}
				}
			}
			GUI.enabled = currentTarget.overrideValue && (!currentTarget.profile || currentTarget.profile && currentTarget.profile.valueName == string.Empty);
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					if(currentTarget.overrideValue && currentTarget.valueIndex > 0 && !valueNameExists)
						currentTarget.valueIndex = 0;
					GUI.color = currentTarget.overrideValue ? (currentTarget.valueIndex > 0 && valueNameExists ? Color.green : Color.red) : Color.yellow;
					GUILayout.Box(GUIContent.none,GUILayout.Width(12),GUILayout.Height(12));
					GUI.color = color;
					GUILayout.Label("Value Name");
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
				if(currentTarget.source && valueName != string.Empty)for(int a = 1; a < names.Count; a++)if(valueName == names[a].Replace("[Public] ",string.Empty).Replace("[Private] ",string.Empty).Replace("[Float] ",string.Empty).Replace("[Int] ",string.Empty).Replace("[Vector2] ",string.Empty).Replace("[Vector3] ",string.Empty))
					currentTarget.valueIndex = a;
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("valueName"),GUIContent.none,true);
					GUI.enabled = currentTarget.overrideValue && (!currentTarget.profile || currentTarget.profile && currentTarget.profile.valueName == string.Empty) && currentTarget.source && currentTarget.source.GetType() != typeof(Transform);
					EditorGUI.BeginChangeCheck();
					int index = EditorGUILayout.Popup(currentTarget.valueIndex,names.ToArray());
					if(index >= 0 && (EditorGUI.EndChangeCheck() || reset))
					{
						if(reset)
						{
							index = 1;
							currentTarget.valueIndex = 1;
						}
						Undo.RecordObject(target,"Inspector");
						currentTarget.valueName = index < names.Count ? names[index].Replace("Not Specified",string.Empty).Replace("[Public] ",string.Empty).Replace("[Private] ",string.Empty).Replace("[Float] ",string.Empty).Replace("[Int] ",string.Empty).Replace("[Vector2] ",string.Empty).Replace("[Vector3] ",string.Empty) : string.Empty;
						currentTarget.valueIndex = index;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
			GUI.enabled = currentTarget.overrideMaximumValue && (!currentTarget.profile || currentTarget.profile && currentTarget.profile.maximumValueName == string.Empty);
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal();
				{
					if(currentTarget.overrideMaximumValue && currentTarget.maximumValueIndex > 0 && !maximumValueNameExists)
						currentTarget.maximumValueIndex = 0;
					GUI.color = currentTarget.overrideMaximumValue ? (currentTarget.maximumValueIndex > 0 && maximumValueNameExists ? Color.green : Color.red) : Color.yellow;
					GUILayout.Box(GUIContent.none,GUILayout.Width(12),GUILayout.Height(12));
					GUI.color = color;
					GUILayout.Label("Maximum Value Name");
					GUILayout.FlexibleSpace();
				}
				EditorGUILayout.EndHorizontal();
				if(currentTarget.source && maximumValueName != string.Empty)for(int a = 1; a < names.Count; a++)if(maximumValueName == names[a].Replace("[Public] ",string.Empty).Replace("[Private] ",string.Empty).Replace("[Float] ",string.Empty).Replace("[Int] ",string.Empty).Replace("[Vector2] ",string.Empty).Replace("[Vector3] ",string.Empty))
					currentTarget.maximumValueIndex = a;
				EditorGUILayout.BeginHorizontal();
				{
					EditorGUILayout.PropertyField(currentTargetProperty.FindPropertyRelative("maximumValueName"),GUIContent.none,true);
					GUI.enabled = currentTarget.overrideMaximumValue && (!currentTarget.profile || currentTarget.profile && currentTarget.profile.maximumValueName == string.Empty) && currentTarget.source && currentTarget.source.GetType() != typeof(Transform);
					EditorGUI.BeginChangeCheck();
					int index = EditorGUILayout.Popup(currentTarget.maximumValueIndex,names.ToArray());
					if(index >= 0 && (EditorGUI.EndChangeCheck() || reset))
					{
						if(reset)
						{
							index = 1;
							currentTarget.maximumValueIndex = 1;
						}
						Undo.RecordObject(target,"Inspector");
						currentTarget.maximumValueName = index < names.Count ? names[index].Replace("Not Specified",string.Empty).Replace("[Public] ",string.Empty).Replace("[Private] ",string.Empty).Replace("[Float] ",string.Empty).Replace("[Int] ",string.Empty).Replace("[Vector2] ",string.Empty).Replace("[Vector3] ",string.Empty) : string.Empty;
						currentTarget.maximumValueIndex = index;
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
			GUI.enabled = true;
		}
	}
	#endif
}