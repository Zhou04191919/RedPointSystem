using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RedpointTree
{
    private RedPointNode root;
    public Dictionary<string, string> NodeNames = new Dictionary<string, string>
    {
        { "Root","Root"},
        { "ModelA","Root|ModelA"},
        { "ModelA_Sub_1 ","Root|ModelA|ModelA_Sub_1"},
        { "ModelA_Sub_2 ","Root|ModelA|ModelA_Sub_2"},
        { "ModelB ", "Root|ModelB"},
        { "ModelB_Sub_1 ","Root|ModelB|ModelB_Sub_1"},
        { "ModelB_Sub_2 ","Root|ModelB|ModelB_Sub_2"}
    };

    public RedpointTree()
    {
        root = new RedPointNode("Root");
    }

    public void Init()
    {
        this.root =new RedPointNode("Root");
        ////构建前缀树
        foreach (var name in NodeNames.Values)
        {
            this.InsertNode(name);
        }
        ChangeRedPointCnt(NodeNames["ModelA_Sub_1"], 1);
        ChangeRedPointCnt(NodeNames["ModelA_Sub_2"], 1);
        ChangeRedPointCnt(NodeNames["ModelB_Sub_1"], 1);
        ChangeRedPointCnt(NodeNames["ModelB_Sub_2"], 1);
    }

    public void InsertNode(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }
        if (SearchNode(name) != null)
        {
            Debug.Log("你已经插入了此节点" + name);
            return;
        }

        RedPointNode node = root;
        node.passCnt += 1;
        string[] pathList = name.Split("|");
        foreach (string path in pathList)
        {
            if (!node.children.ContainsKey(path))
            {
                node.children.Add(path, RedPointNode.New(path));
            }
            node = node.children[path];
            node.passCnt = node.passCnt + 1;
        }
        node.endCnt = node.endCnt + 1;
    }
    /// <summary>
    /// 查询节点是否在树中并返回节点
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public RedPointNode SearchNode(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        RedPointNode node = this.root;
        string[] pathList = name.Split("|");
        foreach (string path in pathList)
        {
            if (!node.children.ContainsKey(path))
            {
                return null;
            }
            node = node.children[path];
        }
        if (node.endCnt > 0)
        {
            return node;
        }
        return null;
    }
    /// <summary>
    /// 删除某个节点
    /// </summary>
    /// <param name="name"></param>
    public void DeleteNode(string name)
    {
        if (SearchNode(name) == null)
        {
            return;
        }
        RedPointNode node = this.root;
        node.passCnt = node.passCnt - 1;
        string[] pathList = name.Split("|");
        foreach (string path in pathList)
        {
            RedPointNode childNode = node.children[path];
            childNode.passCnt = childNode.passCnt - 1;
            if (childNode.passCnt == 0)
            {
                // 如果该节点没有任何孩子，则直接删除
                node.children.Remove(path);
                return;
            }
            node = childNode;
        }
        node.endCnt = node.endCnt - 1;
    }
    /// <summary>
    /// 修改节点的红点数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="delta"></param>

    public void ChangeRedPointCnt(string name,int delta)
    {
        RedPointNode targetNode = SearchNode(name);
        if(targetNode==null)
        {
            return;
        }
        if(delta<0&& targetNode.redpointCnt+delta<0)
        {
            delta = -targetNode.redpointCnt;
        }
        RedPointNode node = this.root;
        string[] pathList = name.Split("|");
        foreach (string path in pathList)
        {
            RedPointNode childNode = node.children[path];
            childNode.redpointCnt = childNode.redpointCnt + delta;
            node = childNode;
            foreach (Action<int> cb in node.updateCb.Values)
            {
                cb?.Invoke(node.redpointCnt);
            }
        }
    }

    /// <summary>
    /// 设置红点更新回调函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    /// <param name="cb"></param>
    public void SetCallBack(string name,string key,Action<int>cb)
    {
        RedPointNode node = SearchNode(name);
        if (node == null)
        {
            return;
        }
        node.updateCb.Add(key, cb);

    }
    /// <summary>
    /// 查询节点的红点数
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetRedPointCnt(string name)
    {
        RedPointNode node = SearchNode(name);
        if (node == null)
        {
            return 0;
        }
        return node.redpointCnt;
    }
}

public class NodeNames
{
    public const string Root = "Root";

    public const string ModelA = "Root|ModelA";
    public const string ModelA_Sub_1 = "Root|ModelA|ModelA_Sub_1";
    public const string ModelA_Sub_2 = "Root|ModelA|ModelA_Sub_2";

    public const string ModelB = "Root|ModelB";
    public const string ModelB_Sub_1 = "Root|ModelB|ModelB_Sub_1";
    public const string ModelB_Sub_2 = "Root|ModelB|ModelB_Sub_2";

    public static List<string> NodeList = new List<string>() {
        Root,ModelA,ModelA_Sub_1,ModelA_Sub_2,ModelB,ModelB_Sub_1,ModelB_Sub_2
    };
}
