using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameHallPanel : MonoBehaviour
{
    [SerializeField] Text txtRoot;
    [SerializeField] Text txtModelA;
    [SerializeField] Text txtModelA_1;
    [SerializeField] Text txtModelA_2;
    [SerializeField] Text txtModelB;
    [SerializeField] Text txtModelB_1;
    [SerializeField] Text txtModelB_2;

    [SerializeField] Button txtRootBtn;
    [SerializeField] Button txtModelABtn;
    [SerializeField] Button txtModelA_1Btn;
    [SerializeField] Button txtModelA_2Btn;
    [SerializeField] Button txtModelBBtn;
    [SerializeField] Button txtModelB_1Btn;
    [SerializeField] Button txtModelB_2Btn;
    [SerializeField] Button ReBtn;

    [SerializeField] GameObject op;
    [SerializeField] GameObject tp;
    [SerializeField] GameObject ap;
    [SerializeField] GameObject bp;
    

    RedpointTree redpointTree;

    void Start()
    {
        //redpointTree= new RedpointTree();
        //redpointTree.Init();
        //redpointTree.SetCallBack(redpointTree.NodeNames["Root"], "Root", redpointCnt =>
        //{
        //    UpdateRedPoint(redpointCnt);
        //});
        op.SetActive(true);
        tp.SetActive(false);
        RegisterClickEvents();
        CreateTree();
        SetCallBack();
        AddDate();
    }

    private void CreateTree()
    {
        redpointTree = new RedpointTree();
        for (int i = 0; i < NodeNames.NodeList.Count; i++)
        {
            redpointTree.InsertNode(NodeNames.NodeList[i]);
        }
    }
    private void RegisterClickEvents()
    {
        txtRootBtn.onClick.AddListener(()=> { op.SetActive(false);tp.SetActive(true); }); ;
        ReBtn.onClick.AddListener(() => { op.SetActive(true); tp.SetActive(false); });
        txtModelABtn.onClick.AddListener(() => { bp.SetActive(false); ap.SetActive(true); }) ;
        txtModelA_1Btn.onClick.AddListener(()=> { redpointTree.ChangeRedPointCnt(NodeNames.ModelA_Sub_1, -1); });
        txtModelA_2Btn.onClick.AddListener(() => { redpointTree.ChangeRedPointCnt(NodeNames.ModelA_Sub_2, -1); });
        txtModelBBtn.onClick.AddListener(() => { bp.SetActive(true); ap.SetActive(false); }) ;
        txtModelB_1Btn.onClick.AddListener(() => { redpointTree.ChangeRedPointCnt(NodeNames.ModelB_Sub_1, -1); });
        txtModelB_2Btn.onClick.AddListener(() => { redpointTree.ChangeRedPointCnt(NodeNames.ModelB_Sub_2, -1); });
    }
    private void SetCallBack()
    {
        redpointTree.SetCallBack(NodeNames.Root, "Root", (redpointCnt) =>
        {
            ShowText(txtRoot, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelA, "ModelA", (redpointCnt) =>
        {
            ShowText(txtModelA, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelA_Sub_1, "ModelA_Sub_1", (redpointCnt) =>
        {
            ShowText(txtModelA_1, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelA_Sub_2, "ModelA_Sub_2", (redpointCnt) =>
        {
            ShowText(txtModelA_2, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelB, "ModelB", (redpointCnt) =>
        {
            ShowText(txtModelB, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelB_Sub_1, "ModelB_Sub_1", (redpointCnt) =>
        {
            ShowText(txtModelB_1, redpointCnt);
        });
        redpointTree.SetCallBack(NodeNames.ModelB_Sub_2, "ModelB_Sub_2", (redpointCnt) =>
        {
            ShowText(txtModelB_2, redpointCnt);
        });
    }    
    private void ShowText( Text text, int redpointCnt)
    {
        text.text = redpointCnt.ToString();
        text.transform.parent.gameObject.SetActive(redpointCnt > 0);
    }
    private void AddDate()
    {
        redpointTree.ChangeRedPointCnt(NodeNames.ModelA_Sub_1, 1);
        redpointTree.ChangeRedPointCnt(NodeNames.ModelA_Sub_2, 1);
        redpointTree.ChangeRedPointCnt(NodeNames.ModelB_Sub_1, 1);
        redpointTree.ChangeRedPointCnt(NodeNames.ModelB_Sub_2, 1);
    }
}
