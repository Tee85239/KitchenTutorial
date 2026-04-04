using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{

    public AudioClip[] chop;
    public AudioClip[] deliveryfail;
    public AudioClip[] deliverysuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stove;
    public AudioClip[] trash;
    public AudioClip[] warning;

}
