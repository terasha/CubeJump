using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Generic Mono singleton.
/// </summary>
/// 

/****************************************
* MonoSingleton 이용 설명
****************************************
* 1. 유니티 이벤트 사용지양 하며
* 만약 사용이 불가피 할 경우
* Base 이벤트를 호출을 권장합니다.
* 
* 2. 초기화는 가급적 OnInit,
* OnInitComplete의 활용을 권장합니다.
* 
* 3. 초기화, 씬전환, 삭제, 종료 등등의
* 시점에서 특정 Singletone에 접근할 경우
* BeInstanced의 확인후 적절한
* 예외 처리를 권장합니다.
****************************************/

public class MonoSinglton<T> : MonoBehaviour
where T : MonoSinglton<T>
{
    public Action OnCompleteInitEvent;
    public bool IsInitComplete { get; private set; }

    [SerializeField]
    private bool IsDontDestroyOnLoad = true;

    private static T m_Instance;
    private static object m_Syncroot = new object();
    private bool SameInstance { get { return (m_Instance == this); } }

    public static bool BeInstanced { get { return (m_Instance != null); } }
    public static T instnace
    {
        get
        {
            if (BeInstanced) return m_Instance;
            lock (m_Syncroot)
            {
                if (BeInstanced) return m_Instance;
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (BeInstanced) return m_Instance;
                m_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
                if (BeInstanced) return m_Instance;
            }
            return m_Instance;
        }
        private set { m_Instance = value; }
    }

    #region Unity Func
    void Awake()
    {
        #region Instance, DonDestroyOnLoad
        lock (m_Syncroot)
        {
            if(BeInstanced && !SameInstance)
            {
                Destroy(this.gameObject);
                return;
            }
            else if(BeInstanced && SameInstance)
            {
                if (IsDontDestroyOnLoad)
                    DontDestroyOnLoad(this.gameObject);
                return;
            }else
            {
                m_Instance = this as T;
                //if (IsDontDestroyOnLoad)
                //    DontDestroyOnLoad(this.gameObject);
                return;
            }
        }
        #endregion
    }
    #endregion
    
    // Use this for initialization
    protected virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnInit()
    {

    }

    protected virtual void OnInitComplete()
    {

        if (OnCompleteInitEvent != null)
            OnCompleteInitEvent();
    }

    public bool StartInit()
    {
        if (IsInitComplete)
        {
            Debug.LogWarning(this.name + " : 이미 초기화된 항목입니다.");
            return IsInitComplete;
        }

        try
        {
            OnInit();
            IsInitComplete = true;
            OnInitComplete();

        }catch(Exception error)
        {
            Debug.LogError(this.name + " : StartInit - " + error.Message);
            IsInitComplete = false;
        }
        return IsInitComplete;
    }

    protected virtual void OnDestroy()
    {
        IsInitComplete = false;
    }

    protected virtual void OnApplicationQuit()
    {
        IsInitComplete = false;
    }

    
}
