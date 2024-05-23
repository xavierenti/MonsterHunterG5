using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunt : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public int MaxRarity;
    [SerializeField] public int MinRarity;
    [SerializeField] private Button _button;

    private const float RewardsTimeToLife = 5f;
    private const float VoidTimeToLife = 2f;

    private Monster _monster;
    private SystemManager _sysInstance;
    private MonsterViewController _viewController;

    private List<Item> _itemsDropped = new();

    private void Start()
    {
        _sysInstance = SystemManager.Instace;
        _viewController = MonsterViewController.Instance;
        _monster = MonsterViewController.Instance.Monster;
    }

    public void HuntMonster()
    {
        // Limipiamos la lista de recompensas
        _itemsDropped.Clear();

        GetRewards();
        StartCoroutine(BlockHuntButton());
    }

    private void GetRewards()
    {
        if (_monster.rewards.Count != 0)
        {
            for (int i = 0; i < _monster.rewards.Count; i++)
            {
                RewardDrop(_monster.rewards[i].item);
            }
        }

        _viewController.ItemsRewards = _itemsDropped;
        _viewController.LoadView(ViewsName.MONSTERREAWRDPOPUP, false);
    }

    private void RewardDrop(Monster.Reward.Item item)
    {
        int drop = Random.Range(MinRarity, MaxRarity + 1);

        if (drop < item.rarity)
        {
            return;
        }

        Item inventoryItem = item;
        _sysInstance.AddToInventory(inventoryItem);
        _itemsDropped.Add(inventoryItem);
    }

    IEnumerator BlockHuntButton()
    {
        _button.interactable = false;

        float delta = 0f;

        float timeToLife;
        if (_viewController.ItemsRewards.Count > 0)
        {
            timeToLife = RewardsTimeToLife;
        }
        else
        {
            timeToLife = VoidTimeToLife;
        }

        while (delta < timeToLife)
        {
            delta += Time.deltaTime;
            yield return null;
        }

        _button.interactable = true;
    }
}
