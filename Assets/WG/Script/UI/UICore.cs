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

        public Param Clone()
        {
            return (Param)MemberwiseClone();
        }
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

        foreach(ParamArray v in paramArray)
        {
            Param frist = v.first.root;
            Transform parent = v.parent.transform;
            if (parent  != null)
            {
                cacheParam.Add(v.parent);
            }
            int count = parent.childCount;
            for(int i = 0; i < count; i++)
            {
                string rootName = frist.name + (i + 1);
                string index = "00" + (i + 1);
                Transform rooTrans = parent.FindChild(index);
                cacheParam.Add(BindAllWidgetsHelper(frist, rootName, rooTrans));
                for(int j = 0; j < v.first.childs.Count; j++)
                {
                    Param param = v.first.childs[j];
                    string childName = rootName + "_" + param.name;
                    Transform childTrans = rooTrans.FindChild(param.transform.name);
                    cacheParam.Add(BindAllWidgetsHelper(param, childName, childTrans));
                }
            }
        }
    }

    public Param BindAllWidgetsHelper(Param clone,string name,Transform trans,bool isClone = true)
    {
        Param param = null;
        if (isClone)
        {
            param = clone.Clone();
        }
        else
        {
            param = clone;
        }
        param.name = name;
        param.transform = trans;
        return param;
    }
    #endregion
}
