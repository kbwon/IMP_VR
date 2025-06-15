using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceBatchSetter : MonoBehaviour
{
    [MenuItem("Tools/Set Default Audio Mixer Group")]
    static void SetAudioOutputGroup()
    {
        var group = AssetDatabase.LoadAssetAtPath<AudioMixerGroup>("Assets/MainMixer.mixer"); // 경로 수정

        if (group == null)
        {
            Debug.LogWarning("Mixer group not found!");
            return;
        }

        var allAudioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (var src in allAudioSources)
        {
            Undo.RecordObject(src, "Set Mixer Group");
            src.outputAudioMixerGroup = group;
            EditorUtility.SetDirty(src);
        }

        Debug.Log($"Set {allAudioSources.Length} AudioSources to {group.name}");
    }
}
