using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Roulettes
{
    public class RouletteUISetup : MonoBehaviour
    {
        public Text playerText;
        public Text enemyText;
        public Text environmentText;

        private PlayerRoulette playerRoulette;
        private EnemyRoulette enemyRoulette;
        private EnvironmentRoulette environmentRoulette;
        [Inject]
        private void InitBindings(PlayerRoulette pr, EnemyRoulette er, EnvironmentRoulette envR)
        {
            playerRoulette = pr;
            enemyRoulette = er;
            environmentRoulette = envR;
        }

        void Start()
        {
            playerText.text = $"{playerRoulette.currentEntity.kind}: {playerRoulette.currentEntity.modifier}";
            enemyText.text = $"{enemyRoulette.currentEntity.kind}: {enemyRoulette.currentEntity.modifier}";
            environmentText.text = $"{environmentRoulette.currentEntity.kind}: {environmentRoulette.currentEntity.modifier}";
        }
    }
}
