using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character;
    public GameObject[] stageChips; // ���炩���ߐݒ肵�Ă����X�e�[�W�`�b�v
    public int startChipIndex;      // ���������X�^�[�g�`�b�v�C���f�b�N�X
    public int preInstantiate;      // ��ǂ݂��鐔
    public List<GameObject> generatedStageList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // public�Őݒ肷��Ƃ���1����n�߂���悤�����I�ɂ��邽�߂�-1�����Ă���
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        // ���݈ʒu�̎擾
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // ���݂̈ʒu�Ɏ����������镪�𑫂������̂����݂���`�b�v�C���f�b�N�X�����傫���ꍇ
        if (charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }

    // �X�e�[�W�̍X�V
    public void UpdateStage(int toChipIndex)
    {
        
        if (toChipIndex <= currentChipIndex) return;

        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            generatedStageList.Add(stageObject);
        }

        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;
    }

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
