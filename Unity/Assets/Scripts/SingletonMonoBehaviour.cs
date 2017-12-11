using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singletonクラス
/// </summary>
/// <remarks>
/// 他のManagerクラスで継承
/// 参考:http://qiita.com/okuhiiro/items/3d69c602b8538c04a479
/// </remarks>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));

				if (instance == null)
				{
					Debug.LogError(typeof(T) + "is nothing!!");
				}

			}
			return instance;
		}
	}
}
