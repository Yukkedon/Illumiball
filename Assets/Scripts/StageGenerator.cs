using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character;
    public GameObject[] stageChips; // あらかじめ設定していたステージチップ
    public int startChipIndex;      // 自動生成スタートチップインデックス
    public int preInstantiate;      // 先読みする数
    public List<GameObject> generatedStageList = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        // publicで設定するときに1から始められるよう直感的にするために-1をしている
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        // 現在位置の取得
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // 現在の位置に自動生成する分を足したものが現在いるチップインデックスよりも大きい場合
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }

    // indexを渡すことで「設定したindex分のステージを作る」という仕様
    // どんどんindexを増やしそのindexをStageSizeにかけてあげることで自動生成を実現している
    public void UpdateStage(int toChipIndex)
    {
        // 作りたい個数(引数 toChipIndex)とプレイヤーの現在地インデックスを比較して
        // 
        if (toChipIndex <= currentChipIndex) return;

        // 現在地indexから作りたい個数分GanarateStageをする
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            generatedStageList.Add(stageObject);
        }

        // 作りたい個数＋2を超えてステージを作ったら古い順番にステージを削除する
        // ifでやらないのは万が一複数個出現されて１つしか消えなかった場合に削除してほしいからかも
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();
        // 現在のステージチップindexを作りたいindexに設定する
        currentChipIndex = toChipIndex;
    }

    // 設定したstageChipsからランダムで取り出し、　
    // ステージのゲームオブジェクトを返す
    GameObject GenerateStage(int chipIndex)
    {

        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity);
        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldstage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldstage);
    }
}
