using UnityEditor;
using UnityEngine;


namespace OccaSoftware.StylizedOutlines.Editor
{
    public class StylizedOutlinesGUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material mat = materialEditor.target as Material;
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Basic Configuration", EditorStyles.boldLabel);
            Color outlineColor = EditorGUILayout.ColorField(new GUIContent("Outline Color"), mat.GetColor(ShaderParams.outlineColor), false, false, true);
            float outlineThickness = EditorGUILayout.FloatField("Outline Thickness", mat.GetFloat(ShaderParams.outlineThickness));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Vertex Color Configuration", EditorStyles.boldLabel);
            bool useVertexColors = EditorGUILayout.Toggle("Use Vertex Color (R) to Attenuate Outline Thickness", mat.IsKeywordEnabled(ShaderParams.useVertexColors));


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Distance-based Attenuation", EditorStyles.boldLabel);
            bool attenuateByDistance = EditorGUILayout.Toggle("Attenuate By Distance", mat.IsKeywordEnabled(ShaderParams.attenuateByDistance));
            float completeFalloffDistance = mat.GetFloat(ShaderParams.completeFalloffDistance);
            if (attenuateByDistance)
            {
                completeFalloffDistance = EditorGUILayout.FloatField("Complete Falloff Distance", completeFalloffDistance);
            }


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Noise", EditorStyles.boldLabel);
            Texture2D noiseTexture = (Texture2D)EditorGUILayout.ObjectField("Noise Texture", mat.GetTexture(ShaderParams.noiseTexture), typeof(Texture2D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            bool randomOffset = EditorGUILayout.Toggle("Randomly Offset Sample Position", mat.IsKeywordEnabled(ShaderParams.randomOffset));

            float noiseFrequency = mat.GetFloat(ShaderParams.noiseFrequency);
            float noiseFramerate = mat.GetFloat(ShaderParams.noiseFramerate);
            if (noiseTexture != null)
            {
                noiseFrequency = EditorGUILayout.FloatField("Noise Frequency", noiseFrequency);
                noiseFramerate = EditorGUILayout.FloatField("Noise Framerate", noiseFramerate);
            }


            if (EditorGUI.EndChangeCheck())
            {
                mat.SetColor(ShaderParams.outlineColor, outlineColor);
                mat.SetFloat(ShaderParams.outlineThickness, outlineThickness);
                mat.SetFloat(ShaderParams.completeFalloffDistance, completeFalloffDistance);
                mat.SetTexture(ShaderParams.noiseTexture, noiseTexture);
                mat.SetFloat(ShaderParams.noiseFrequency, noiseFrequency);
                mat.SetFloat(ShaderParams.noiseFramerate, noiseFramerate);

                SetKeyword(mat, ShaderParams.useVertexColors, useVertexColors);
                SetKeyword(mat, ShaderParams.attenuateByDistance, attenuateByDistance);
                SetKeyword(mat, ShaderParams.randomOffset, randomOffset);
            }
        }

        private static class ShaderParams
        {
            public static int outlineColor = Shader.PropertyToID("_OutlineColor");
            public static int outlineThickness = Shader.PropertyToID("_OutlineThickness");
            public static int completeFalloffDistance = Shader.PropertyToID("_CompleteFalloffDistance");
            public static int noiseTexture = Shader.PropertyToID("_NoiseTexture");
            public static int noiseFrequency = Shader.PropertyToID("_NoiseFrequency");
            public static int noiseFramerate = Shader.PropertyToID("_NoiseFramerate");

            public static string useVertexColors = "USE_VERTEX_COLOR_ENABLED";
            public static string attenuateByDistance = "ATTENUATE_BY_DISTANCE_ENABLED";
            public static string randomOffset = "RANDOM_OFFSETS_ENABLED";
        }

        void SetKeyword(Material m, string k, bool s)
        {
            if (s) 
                m.EnableKeyword(k);
            else
                m.DisableKeyword(k);
        }
    }
}