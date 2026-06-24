using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelUPManager : MonoBehaviour
{
    //LevelUpイベント参照先
    [SerializeField]
    private ExPContainer _container;

    //クリックが吸われないようにする
    [SerializeField]
    private GameObject _system;

    //スキルを格納
    [SerializeField]
    private List<SkillData> _skills = new List<SkillData>();

    //スキル格納庫
    [SerializeField]
    private List<SkillHolder> _skillHolders = new List<SkillHolder>();

    //抽選回数
    private const int _lotteryCount = 3;

    private void OnEnable()
    {
        _container.OnNextLevel += CreateSkills;//本採用
    }
    private void CreateSkills()
    {
        _system.SetActive(false);
        for (int i = 0; i < _lotteryCount; i++)
        {
            //全要素からランダムで一つを抽出
            int randomIndex = UnityEngine.Random.Range(0, _skills.Count);

            //スキルホルダーにランダムなスキルをセット
            _skillHolders[i].SetSkillData(_skills[randomIndex]);
        }
    }
    public void EnableSystemObject()
    {
        _system.SetActive(true);
    }
}
