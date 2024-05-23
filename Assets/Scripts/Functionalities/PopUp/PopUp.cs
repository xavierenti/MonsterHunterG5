using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private TextMeshProUGUI _rewardsText;
    [SerializeField] private string _rewardMessageText;
    [SerializeField] private string _noRewardMessageText;

    private const float RewardsTimeToLife = 5f;
    private const float VoidTimeToLife = 2f;

    private float _timeToLife = 0f;
    private List<Item> _itemList = new();

    private void Start()
    {
        SetTimeToLife();
        Setup();
        StartCoroutine(PopUpLife());
    }

    private void SetTimeToLife()
    {
        _itemList = MonsterViewController.Instance.ItemsRewards;

        if (_itemList.Count == 0)
        {
            _timeToLife = VoidTimeToLife;
            return;
        }

        _timeToLife = RewardsTimeToLife;
    }

    private void Setup()
    {
        if (_itemList.Count == 0)
        {
            _rewardsText.text = _noRewardMessageText;
            return;
        }

        string msg = $"{_rewardMessageText}\n\n";
        foreach (Item item in _itemList)
        {
            msg += $"{item.name}\n";
        }
        _rewardsText.text = msg;
    }

    private void DestroyPopUp()
    {
        WindowManager.Instace.DestroyView(this.gameObject);
    }

    IEnumerator PopUpLife()
    {
        float delta = 0f;

        while (delta < _timeToLife)
        {
            delta += Time.deltaTime;
            yield return null;
        }

        DestroyPopUp();
    }
}
