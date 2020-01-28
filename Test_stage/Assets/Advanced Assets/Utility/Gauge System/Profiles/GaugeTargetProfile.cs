namespace AdvancedAssets.Utility.GaugeSystem
{
	using UnityEngine;
	#if UNITY_EDITOR
	using UnityEditor;
	#endif
	using System.Collections.Generic;
	public class GaugeTargetProfile : ScriptableObject
	{
		public bool overrideName = true;
		public new string name = string.Empty;
		#if UNITY_EDITOR
		public bool applyValuesAutomatically = false;
		public byte valuesCount = 5;
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
		#if UNITY_EDITOR
		[HideInInspector] public bool valuesIsExpanded = true;
		[HideInInspector] public int valuesScrollViewIndex = 0;
		[HideInInspector] public Vector2 valuesScrollView = Vector2.zero;
		#endif
		private void OnValidate ()
		{
			#if UNITY_EDITOR
			valuesCount = (byte)Mathf.Clamp(valuesCount,2,100);
			#endif
			maximumValue = Mathf.Clamp(maximumValue,0.1f,float.MaxValue);
			FilterHandler(ref valueName);
			FilterHandler(ref maximumValueName);
			if(valueName != string.Empty && overrideValue)
				overrideValue = false;
			if(maximumValueName != string.Empty && overrideMaximumValue)
				overrideMaximumValue = false;
			if(!overrideValue && value != 0)
				value = 0;
			if(!overrideMaximumValue && maximumValue != 0.1f)
				maximumValue = 0.1f;
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
		public void OverrideName (bool value) {if(overrideName != value)overrideName = value;}
		public void SetName (string value) {if(name != value)name = value;}
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
		public void ApplyValuesAutomatically (byte count)
		{
			if(values.Count != count + 1)
				values = new List<float>(new float[count + 1]);
			for(int a = 0,A = values.Count; a < A; a++)if(values[a] != maximumValue / count * a)
				values[a] = maximumValue / count * a;
		}
	}
	#if UNITY_EDITOR
	internal class StartGaugeTargetProfile
	{
		[MenuItem("Assets/Create/Advanced Assets/Gauge Target Profile",false,11)]
		private static void Action () {ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,ScriptableObject.CreateInstance<EndGaugeTargetProfile>(),"New Gauge Target Profile.asset",EditorGUIUtility.FindTexture("ScriptableObject Icon"),string.Empty);}
	}
	internal class EndGaugeTargetProfile : UnityEditor.ProjectWindowCallback.EndNameEditAction
	{
		public override void Action (int instanceId,string pathName,string resourceFile)
		{
			GaugeTargetProfile gaugeTargetProfile = ScriptableObject.CreateInstance<GaugeTargetProfile>();
			gaugeTargetProfile.name = System.IO.Path.GetFileName(pathName.Replace(".asset",string.Empty));
			AssetDatabase.CreateAsset(gaugeTargetProfile,pathName);
			ProjectWindowUtil.ShowCreatedAsset(gaugeTargetProfile);
		}
	}
	[CustomEditor(typeof(GaugeTargetProfile)),CanEditMultipleObjects]
	internal class GaugeTargetProfileEditor : Editor
	{
		private GaugeTargetProfile[] gaugeTargetProfiles
		{
			get
			{
				GaugeTargetProfile[] gaugeTargetProfiles = new GaugeTargetProfile[targets.Length];
				for(int gaugeTargetProfilesIndex = 0; gaugeTargetProfilesIndex < targets.Length; gaugeTargetProfilesIndex++)
					gaugeTargetProfiles[gaugeTargetProfilesIndex] = (GaugeTargetProfile)targets[gaugeTargetProfilesIndex];
				return gaugeTargetProfiles;
			}
		}
		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			ProfileSection();
			if(GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
				for(int gaugeTargetProfilesIndex = 0; gaugeTargetProfilesIndex < gaugeTargetProfiles.Length; gaugeTargetProfilesIndex++)
					EditorUtility.SetDirty(gaugeTargetProfiles[gaugeTargetProfilesIndex]);
			}
		}
		private void ProfileSection ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				GUILayout.Label("Profile","BoldLabel");
				EditorGUILayout.BeginHorizontal("Box");
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideName"),GUIContent.none,true);
					EditorGUIUtility.labelWidth = 0;
					GUILayout.Label("Name");
					GUILayout.FlexibleSpace();
					GUI.enabled = gaugeTargetProfiles[0].overrideName;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("name"),GUIContent.none,true);
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginVertical("Box");
				{
					GUI.backgroundColor = gaugeTargetProfiles[0].applyValuesAutomatically ? Color.green : Color.red;
					if(GUILayout.Button("Apply Automatically"))
					{
						Undo.RecordObjects(targets,"Inspector");
						gaugeTargetProfiles[0].applyValuesAutomatically = !gaugeTargetProfiles[0].applyValuesAutomatically;
						for(int gaugeTargetProfilesIndex = 0; gaugeTargetProfilesIndex < gaugeTargetProfiles.Length; gaugeTargetProfilesIndex++)if(gaugeTargetProfiles[gaugeTargetProfilesIndex].applyValuesAutomatically != gaugeTargetProfiles[0].applyValuesAutomatically)
							gaugeTargetProfiles[gaugeTargetProfilesIndex].applyValuesAutomatically = gaugeTargetProfiles[0].applyValuesAutomatically;
						GUI.FocusControl(null);
					}
					GUI.backgroundColor = Color.white;
					if(gaugeTargetProfiles[0].applyValuesAutomatically)
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
								float result = gaugeTargetProfiles[0].maximumValue / gaugeTargetProfiles[0].valuesCount;
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
								for(int gaugeTargetProfilesIndex = 0; gaugeTargetProfilesIndex < gaugeTargetProfiles.Length; gaugeTargetProfilesIndex++)
									gaugeTargetProfiles[gaugeTargetProfilesIndex].ApplyValuesAutomatically(gaugeTargetProfiles[gaugeTargetProfilesIndex].valuesCount);
								GUI.FocusControl(null);
							}
							GUI.enabled = gaugeTargetProfiles[0].values.Count != 0;
							if(GUILayout.Button("Reset"))
							{
								Undo.RecordObjects(targets,"Inspector");
								for(int gaugeTargetProfilesIndex = 0; gaugeTargetProfilesIndex < gaugeTargetProfiles.Length; gaugeTargetProfilesIndex++)
									gaugeTargetProfiles[gaugeTargetProfilesIndex].values.Clear();
								GUI.FocusControl(null);
							}
							GUI.enabled = true;
						}
						EditorGUILayout.EndHorizontal();
					}
					else
					{
						if(!serializedObject.isEditingMultipleObjects)ProfileSectionValuesContainer();
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
				EditorGUILayout.BeginVertical("Box");
				{
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.FlexibleSpace();
								GUILayout.Label("Value Name");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("valueName"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								GUILayout.FlexibleSpace();
								GUILayout.Label("Maximum Value Name");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValueName"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					{
						GUI.enabled = gaugeTargetProfiles[0].valueName == string.Empty;
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								EditorGUIUtility.labelWidth = 1;
								EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideValue"),GUIContent.none,true);
								EditorGUIUtility.labelWidth = 0;
								GUILayout.Label("Value");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							GUI.enabled = gaugeTargetProfiles[0].overrideValue;
							EditorGUILayout.PropertyField(serializedObject.FindProperty("value"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
						GUI.enabled = gaugeTargetProfiles[0].maximumValueName == string.Empty;
						EditorGUILayout.BeginVertical("Box");
						{
							EditorGUILayout.BeginHorizontal();
							{
								EditorGUIUtility.labelWidth = 1;
								EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideMaximumValue"),GUIContent.none,true);
								EditorGUIUtility.labelWidth = 0;
								GUILayout.Label("Maximum Value");
								GUILayout.FlexibleSpace();
							}
							EditorGUILayout.EndHorizontal();
							GUI.enabled = gaugeTargetProfiles[0].overrideMaximumValue;
							EditorGUILayout.PropertyField(serializedObject.FindProperty("maximumValue"),GUIContent.none,true);
						}
						EditorGUILayout.EndVertical();
						GUI.enabled = true;
					}
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndVertical();
		}
		private void ProfileSectionValuesContainer ()
		{
			EditorGUILayout.BeginVertical("Box");
			{
				EditorGUILayout.BeginHorizontal("Box");
				{
					EditorGUIUtility.labelWidth = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty("overrideValues"),true,GUILayout.Width(15));
					EditorGUIUtility.labelWidth = 0;
					if(GUILayout.Button("Values","Label",GUILayout.ExpandWidth(true)))
					{
						gaugeTargetProfiles[0].valuesIsExpanded = !gaugeTargetProfiles[0].valuesIsExpanded;
						GUI.FocusControl(null);
					}
					GUI.enabled = gaugeTargetProfiles[0].values.Count != 0;
					if(GUILayout.Button("X",GUILayout.Width(16),GUILayout.Height(16)))
					{
						Undo.RecordObject(target,"Inspector");
						gaugeTargetProfiles[0].values.Clear();
						GUI.FocusControl(null);
					}
					GUI.enabled = true;
				}
				EditorGUILayout.EndHorizontal();
				if(gaugeTargetProfiles[0].valuesIsExpanded)
				{
					if(gaugeTargetProfiles[0].values.Count >= 5)gaugeTargetProfiles[0].valuesScrollView = EditorGUILayout.BeginScrollView(gaugeTargetProfiles[0].valuesScrollView,GUILayout.Height(100));
					else
					{
						if(gaugeTargetProfiles[0].valuesScrollView != Vector2.zero)
							gaugeTargetProfiles[0].valuesScrollView = Vector2.zero;
						if(gaugeTargetProfiles[0].valuesScrollViewIndex != 0)
							gaugeTargetProfiles[0].valuesScrollViewIndex = 0;
					}
					if(gaugeTargetProfiles[0].valuesScrollViewIndex > 0)GUILayout.Space(gaugeTargetProfiles[0].valuesScrollViewIndex * 26);
					GUI.enabled = gaugeTargetProfiles[0].overrideValues;
					for(int a = gaugeTargetProfiles[0].valuesScrollViewIndex; a <= Mathf.Clamp(gaugeTargetProfiles[0].valuesScrollViewIndex + 4,0,gaugeTargetProfiles[0].values.Count - 1); a++)
					{
						EditorGUILayout.BeginHorizontal("Box");
						{
							EditorGUILayout.BeginHorizontal("Box");
							GUILayout.Box(a.ToString("000"),new GUIStyle() {fontSize = 8},GUILayout.ExpandWidth(false));
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.PropertyField(serializedObject.FindProperty("values").GetArrayElementAtIndex(a),GUIContent.none,true);
							GUI.enabled = a != 0 && gaugeTargetProfiles[0].overrideValues;
							if(GUILayout.Button("▲",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								float current = gaugeTargetProfiles[0].values[a];
								float previous = gaugeTargetProfiles[0].values[a - 1];
								gaugeTargetProfiles[0].values[a - 1] = current;
								gaugeTargetProfiles[0].values[a] = previous;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = a != gaugeTargetProfiles[0].values.Count - 1 && gaugeTargetProfiles[0].overrideValues;
							if(GUILayout.Button("▼",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								float current = gaugeTargetProfiles[0].values[a];
								float next = gaugeTargetProfiles[0].values[a + 1];
								gaugeTargetProfiles[0].values[a + 1] = current;
								gaugeTargetProfiles[0].values[a] = next;
								GUI.FocusControl(null);
								break;
							}
							GUI.enabled = gaugeTargetProfiles[0].overrideValues;
							if(GUILayout.Button("-",GUILayout.Width(16),GUILayout.Height(16)))
							{
								Undo.RecordObject(target,"Inspector");
								gaugeTargetProfiles[0].values.RemoveAt(a);
								GUI.FocusControl(null);
								break;
							}
						}
						EditorGUILayout.EndHorizontal();
					}
					GUI.enabled = true;
					if(gaugeTargetProfiles[0].valuesScrollViewIndex + 5 < gaugeTargetProfiles[0].values.Count)
						GUILayout.Space((gaugeTargetProfiles[0].values.Count - (gaugeTargetProfiles[0].valuesScrollViewIndex + 5)) * 26);
					if(gaugeTargetProfiles[0].values.Count >= 5)
					{
						if(gaugeTargetProfiles[0].valuesScrollViewIndex != gaugeTargetProfiles[0].valuesScrollView.y / 26 && (Event.current.type == EventType.Repaint && Event.current.type == EventType.ScrollWheel || Event.current.type != EventType.Layout && Event.current.type != EventType.ScrollWheel))
							gaugeTargetProfiles[0].valuesScrollViewIndex = (int)gaugeTargetProfiles[0].valuesScrollView.y / 26;
						EditorGUILayout.EndScrollView();
					}
					GUI.enabled = gaugeTargetProfiles[0].overrideValues;
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
							gaugeTargetProfiles[0].values.Add(gaugeTargetProfiles[0].values.Count);
							GUI.FocusControl(null);
						}
					}
					EditorGUILayout.EndHorizontal();
					GUI.enabled = true;
				}
			}
			EditorGUILayout.EndVertical();
		}
	}
	#endif
}