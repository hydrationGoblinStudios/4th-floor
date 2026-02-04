using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

// Tool para multi-edicao de transicoes de animacao. (pq diabos que nao tem na edicao normal nao sei)
public class BulkTransitionWindow : EditorWindow
{
    private static bool hasExitTime;
    private static float transitionDuration;

    [MenuItem("Tools/Bulk Transition Editor")]
    public static void ShowExample()
    {
        BulkTransitionWindow wnd = GetWindow<BulkTransitionWindow>();
        wnd.titleContent = new GUIContent("Bulk Transition Editor");

        hasExitTime = false;
    }

    public void OnGUI()
    {
        GUILayout.Label("Tool para ajustar todas as transições de animação selecionadas em bulk.");
        GUILayout.Label("== AVISO: UNDO NÃO IMPLEMENTADO ==");  // Nao vou fazer a nao ser que alguem começe a usar esses treco

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Has exit time:", EditorStyles.label);
        hasExitTime = GUILayout.Toggle(hasExitTime, "");
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Transition duration:", EditorStyles.label);
        transitionDuration = EditorGUILayout.FloatField(transitionDuration);
        GUILayout.EndHorizontal();
        
        GUILayout.Space(5);

        if (GUILayout.Button("Apply"))
        {
            foreach (Object obj in Selection.objects)
            {
                //Debug.Log("Selected: " + obj.name + "—— Type: " + obj.GetType());
                if(obj is AnimatorStateTransition)
                {
                    AnimatorStateTransition transition = obj as AnimatorStateTransition;
                    transition.hasExitTime = hasExitTime;
                    transition.duration = transitionDuration;
                }
            }
        }

    }
}
