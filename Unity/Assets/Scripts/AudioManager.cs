using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プ：東　音量調整クラス
/// </summary>
/// <remarks>
/// AudioManagerクラスで使用
/// </remarks>
[System.Serializable]
public class SoundVolume
{
	public float bgm = 1.0f;
	public float se = 1.0f;

	public bool mute = false;

	public void Reset()
	{
		bgm = 1.0f;
		se = 1.0f;
		mute = false;
	}
}

/// <summary>
///　プ：東　Audio管理クラス
/// </summary>
/// <remarks>
/// SingletonMonoBehaviourを継承
/// </remarks>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	public SoundVolume volume = new SoundVolume();

	private AudioClip[] bgmClips;
	private AudioClip[] seClips;

	public AudioSource bgmSource;

	private const int SE_MAX = 8;
	public AudioSource[] seSources = new AudioSource[SE_MAX];

	private Dictionary<string, int> bgmDictionary = new Dictionary<string, int>();
	private Dictionary<string, int> seDictionary = new Dictionary<string, int>();

	Queue<int> seRequestQueue = new Queue<int>();

	private void Awake()
	{
		//AudioManagerを登録
		if (this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		bgmSource = gameObject.AddComponent<AudioSource>();

		for (int i = 0; i < SE_MAX; i++)
		{
			seSources[i] = gameObject.AddComponent<AudioSource>();
		}

		bgmClips = Resources.LoadAll<AudioClip>("Audio/BGM");
		seClips = Resources.LoadAll<AudioClip>("Audio/SE");

		//dictionaryに名前登録
		for (int i = 0; i < bgmClips.Length; i++)
		{
			bgmDictionary[bgmClips[i].name] = i;
		}

		for (int i = 0; i < seClips.Length; i++)
		{
			seDictionary[seClips[i].name] = i;
		}
	}

	private void Update()
	{
		bgmSource.mute = volume.mute;
		foreach (var source in seSources)
		{
			source.mute = volume.mute;
		}

		int req_count = seRequestQueue.Count;

		if (req_count != 0)
		{
			int sound_Index = seRequestQueue.Dequeue();
			PlaySeImpl(sound_Index);
		}
	}

    /// <summary>
    ///　再生が終了したAudioClipを空にする
    /// </summary>
    private void PlaySeImpl(int number)
	{
		if (0 > number || seClips.Length <= number)
		{
			return;
		}

		foreach (AudioSource Source in seSources)
		{
			if (Source.isPlaying == false)
			{
				Source.clip = seClips[number];
				Source.Play();
				return;
			}
		}
	}

    /// <summary>
    ///　BGMの再生をファイル名で指定
    /// </summary>
    public void PlayBGM(string name,float volume,bool loop)
	{
		PlayBGM(bgmDictionary[name],volume,loop);
	}

    /// <summary>
    ///　BGMの再生を読み込み順の番号で指定
    /// </summary>
    public void PlayBGM(int number,float volume,bool loop)
	{
		if (0 > number || bgmClips.Length <= number || bgmSource.clip == bgmClips[number])
		{
			return;
		}

		bgmSource.Stop();
		bgmSource.clip = bgmClips[number];
		bgmSource.volume = volume;
		bgmSource.loop = loop;
		bgmSource.Play();
	}

    /// <summary>
    ///　SEの再生をファイル名で指定
    /// </summary>
    public void PlaySE(string name)
	{
		PlaySE(seDictionary[name]);
	}

    /// <summary>
    ///　SEの再生を読み込み順の番号で指定
    /// </summary>
    public void PlaySE(int number)
	{
		if (!seRequestQueue.Contains(number))
		{
			seRequestQueue.Enqueue(number);
		}
	}

    /// <summary>
    ///　BGMを停止
    /// </summary>
    public void StopBGM()
	{
		bgmSource.Stop();
		bgmSource.clip = null;
	}

    /// <summary>
    ///　SEを停止
    /// </summary>
    public void StopSE()
	{
		AllStopSERequest();
		foreach (AudioSource source in seSources)
		{
			source.Stop();
			source.clip = null;
		}
	}

    /// <summary>
    ///　コレクション内のSEをクリア
    /// </summary>
    private void AllStopSERequest()
	{
		seRequestQueue.Clear();
	}
}
