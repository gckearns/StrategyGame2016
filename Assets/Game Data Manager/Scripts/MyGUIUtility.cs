using UnityEngine;
using UnityEditor;

namespace MyNamespace
{
	public class MyGUILayout {

		public static Texture2D GetTexture(Color color) {
			Texture2D tex = new Texture2D(4, 4);
			for (int y = 0; y < tex.height; y++) {
				for (int x = 0; x < tex.width; x++) {
					tex.SetPixel(x, y, color);
				}	
			}
			tex.Apply();
			return tex;
		}

		static GUIStyle GetStyle(int index, int length) {
			GUIStyle leftButton = new GUIStyle("TL tab left");
			GUIStyle middleButton = new GUIStyle("TL tab mid");
			GUIStyle rightButton = new GUIStyle("TL tab right");
			if (length == 1) {
				return middleButton;
			}
			if (index == 0) {
				return leftButton;
			}
			if (index == (length - 1)) {
				return rightButton;
			}
			return middleButton;
		}

		public static int ToggleBar(int selected, string[] strings, params GUILayoutOption[] options){
			int count = strings.Length;
			bool[] values = new bool[count];
			if (selected >= 0) {
				values[selected] = true;	
			} else {
				selected = -1;
			}
			bool changed;
			EditorGUILayout.BeginHorizontal(options);
			for (int i = 0; i < count; i++) {
				EditorGUI.BeginChangeCheck();
				GUILayout.Toggle(values[i], strings[i], GetStyle(i,count));
				changed = EditorGUI.EndChangeCheck();
				if (changed) {
					selected = i;
				}
			}
			EditorGUILayout.EndHorizontal();
			return selected;
		}

		public static int ToggleBar(int selected, string[] strings, GUIStyle style, params GUILayoutOption[] options){
			int count = strings.Length;
			bool[] values = new bool[count];
			if (selected >= 0) {
				values[selected] = true;	
			} else {
				selected = -1;
			}
			bool changed;
			EditorGUILayout.BeginHorizontal(style, options);
			for (int i = 0; i < count; i++) {
				EditorGUI.BeginChangeCheck();
				GUILayout.Toggle(values[i], strings[i], GetStyle(i,count));
				changed = EditorGUI.EndChangeCheck();
				if (changed) {
					selected = i;
				}
			}
			EditorGUILayout.EndHorizontal();
			return selected;
		}

		public static int MenuBar(int selected, string[] strings, params GUILayoutOption[] options){
			GUIStyle myButton = new GUIStyle(GUI.skin.button);
			myButton.margin = new RectOffset(0, 0, 0, 0);
			myButton.normal.background = null;
			int count = strings.Length;
			bool[] values = new bool[count];
			if (selected >= 0) {
				values[selected] = true;	
			} else {
				selected = -1;
			}
			bool changed;
			EditorGUILayout.BeginHorizontal(options);
			for (int i = 0; i < count; i++) {
				EditorGUI.BeginChangeCheck();
				GUILayout.Toggle(values[i], strings[i], myButton);
				changed = EditorGUI.EndChangeCheck();
				if (changed) {
					if (i == selected) {
						selected = -1;
					} else {
						selected = i;	
					}
				}
			}
			EditorGUILayout.EndHorizontal();
			return selected;
		}

		public static int MenuBar(int selected, string[] strings, GUIStyle style, params GUILayoutOption[] options) {
			GUIStyle myButton = new GUIStyle(GUI.skin.button);
			myButton.margin = new RectOffset(0, 0, 0, 0);
			myButton.normal.background = null;
			int count = strings.Length;
			bool[] values = new bool[count];
			if (selected >= 0) {
				values[selected] = true;
			} else {
				selected = -1;
			}
			bool changed;
			EditorGUILayout.BeginHorizontal(style, options);
			for (int i = 0; i < count; i++) {
				EditorGUI.BeginChangeCheck();
				GUILayout.Toggle(values[i], strings[i], myButton);
				changed = EditorGUI.EndChangeCheck();
				if (changed) {
					if (i == selected) {
						selected = -1;
					} else {
						selected = i;
					}
				}
			}
			EditorGUILayout.EndHorizontal();
			return selected;
		}

		//		mySkin.box = defaultSkin.box;
		//		mySkin.button = defaultSkin.button;
		//		mySkin.customStyles = defaultSkin.customStyles;
		//		mySkin.font= defaultSkin.font;
		//		mySkin.horizontalScrollbar = defaultSkin.horizontalScrollbar;
		//		mySkin.horizontalScrollbarLeftButton = defaultSkin.horizontalScrollbarLeftButton;
		//		mySkin.horizontalScrollbarRightButton = defaultSkin.horizontalScrollbarRightButton;
		//		mySkin.horizontalScrollbarThumb = defaultSkin.horizontalScrollbarThumb;
		//		mySkin.horizontalSlider = defaultSkin.horizontalSlider;
		//		mySkin.horizontalSliderThumb = defaultSkin.horizontalSliderThumb;
		//		mySkin.label = defaultSkin.label;
		//		mySkin.scrollView = defaultSkin.scrollView;
		//		mySkin.settings.cursorColor = defaultSkin.settings.cursorColor;
		//		mySkin.settings.cursorFlashSpeed = defaultSkin.settings.cursorFlashSpeed;
		//		mySkin.settings.doubleClickSelectsWord = defaultSkin.settings.doubleClickSelectsWord;
		//		mySkin.settings.selectionColor = defaultSkin.settings.selectionColor;
		//		mySkin.settings.tripleClickSelectsLine = defaultSkin.settings.tripleClickSelectsLine;
		//		mySkin.textArea = defaultSkin.textArea;
		//		mySkin.textField = defaultSkin.textField;
		//		mySkin.toggle = defaultSkin.toggle;
		//		mySkin.verticalScrollbar = defaultSkin.verticalScrollbar;
		//		mySkin.verticalScrollbarDownButton = defaultSkin.verticalScrollbarDownButton;
		//		mySkin.verticalScrollbarUpButton = defaultSkin.verticalScrollbarUpButton;
		//		mySkin.verticalScrollbarThumb = defaultSkin.verticalScrollbarThumb;
		//		mySkin.verticalSlider = defaultSkin.verticalSlider;
		//		mySkin.verticalSliderThumb = defaultSkin.verticalSliderThumb;
		//		mySkin.window = defaultSkin.window;
	}


}

