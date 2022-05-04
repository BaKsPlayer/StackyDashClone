using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject _playModeMenu;
    [SerializeField] private RewardMenu _rewardMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.OnPlayerStopped += PlayerStopped;
        }
    }

    private void PlayerStopped()
    {
        _playModeMenu.SetActive(false);
        _rewardMenu.gameObject.SetActive(true);

        //TODO: Убрать комментарий со строчки ниже (закоментировал, чтобы не увеличивался лвл, так как нет кроме нулевого)
        //PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);

        _rewardMenu.Fill();
    }
}
