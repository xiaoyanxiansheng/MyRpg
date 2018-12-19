using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UICore : MonoBehaviour {

    #region enum
    // 控件类型
    public enum ComponentType
    {
        Transform,
        GameObject,
        Panel,
        Label,
        Input,
        Button,
        Texture,
        Sprite,
        Progressbar,
        Toggle,
        BoxCollider,
        ScrollView,
        UICore,
        UIGrid,
        UIWidget,
        UIPlayTween,
        TweenScale,
        UITable,
        UISlider,
    }

    public enum EventType
    {
        Null,
        Click,
        Press,
    }
    #endregion

    #region class
    [System.Serializable]
    public class ParamEvent
    {
        public string EventCallBack;
        public EventType eventType = EventType.Null;
    }

    [System.Serializable]
    public class Param
    {
        public string name;
        public Transform transform;
        public ComponentType componentType = ComponentType.Transform;
        public List<ParamEvent> events = new List<ParamEvent>();
    }

    [System.Serializable]
    public class ParamArray
    {
        public Param parent;
        public ParamArrayEle first;
    }

    [System.Serializable]
    public class ParamArrayEle
    {
        public Param root;
        public List<Param> childs;
    }

    #endregion

    #region menber
    public List<Param> param = new List<Param>();
    public List<ParamArray> paramArray = new List<ParamArray>();

    public List<Param> cacheParam = new List<Param>();
    #endregion

    #region method
    public void BindAllWidgets()
    {
        cacheParam.Clear();

        foreach(Param v in param)
        {
            cacheParam.Add(v);
        }
    }
    #endregion
}
