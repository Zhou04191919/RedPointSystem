using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPointNode 
{
    /// <summary>
    /// �ڵ���
    /// </summary>
    public  string name;
    /// <summary>
    /// �ڵ㱻�����Ĵ���
    /// </summary>
    public int passCnt=0;
    /// <summary>
    /// �ڵ���Ϊβ�ڵ�Ĵ���
    /// </summary>
    public int endCnt = 0;
    /// <summary>
    /// 
    /// </summary>
    public int redpointCnt = 0;
    public Dictionary<string, RedPointNode> children = new Dictionary<string, RedPointNode>();
    public Dictionary<string, Action<int>> updateCb = new Dictionary<string, Action<int>>();

    public RedPointNode(string name)
    {
        this.name = name;
        this.passCnt = 0;
        this.endCnt = 0;
        this.redpointCnt = 0;
        this.children = new Dictionary<string, RedPointNode>();
        this.updateCb = new Dictionary<string, Action<int>>();
    }
    public static RedPointNode New(string name)
    {
        return new RedPointNode(name);
    }
}
