using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceBatchSetter : MonoBehaviour
{
    // Adds a menu item in Unity under "Tools" to batch-set all AudioSources' output to the default mixer group
    [MenuItem("Tools/Set Default Audio Mixer Group")]
    static void SetAudioOutputGroup()
    {
        // Load the target AudioMixerGroup from Assets folder
        var group = AssetDatabase.LoadAssetAtPath<AudioMixerGroup>("Assets/MainMixer.mixer");

        if (group == null)
        {
            Debug.LogWarning("Mixer group not found!");
            return;
        }

        // Find all AudioSource components in the current scene
        var allAudioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (var src in allAudioSources)
        {
            // Register undo step for each audio source
            Undo.RecordObject(src, "Set Mixer Group");

            // Assign the loaded mixer group
            src.outputAudioMixerGroup = group;

            // Mark object as modified
            EditorUtility.SetDirty(src);
        }

        Debug.Log($"Set {allAudioSources.Length} AudioSources to {group.name}");
    }
}
