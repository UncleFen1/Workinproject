using Inputs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SwithImageProperties4 : SwithImageProperties3
    {

        [Header("RoulRune1Image")]//new
        [SerializeField] protected Image roulRune1Image;//new

        [Header("RoulModRune1Image")]//new
        [SerializeField] protected Image roulModRune1Image;//new
        //
        protected override void MousePoint(InputMouseData _data)
        {
            if (_data.HitObject != null)
            {
                tempGameObject = _data.HitObject;

                if (roulPersImage.gameObject == tempGameObject)
                {
                    if (roulPersImage.sprite != null)
                    {
                        TextLabelImg($"{roulPersImage.gameObject.name} {roulPersImage.sprite.name}");
                    }
                }

                if (roulModPersImage.gameObject == tempGameObject)
                {
                    if (roulModPersImage.sprite != null)
                    {
                        TextLabelImg($"{roulModPersImage.gameObject.name} {roulModPersImage.sprite.name}");
                    }
                }

                if (roulEnemyImage.gameObject == tempGameObject)
                {
                    if (roulEnemyImage.sprite != null)
                    {
                        TextLabelImg($"{roulEnemyImage.gameObject.name} {roulEnemyImage.sprite.name}");
                    }
                }

                if (roulModEnemyImage.gameObject == tempGameObject)
                {
                    if (roulModEnemyImage.sprite != null)
                    {
                        TextLabelImg($"{roulModEnemyImage.gameObject.name} {roulModEnemyImage.sprite.name}");
                    }
                }

                if (roulMirImage.gameObject == tempGameObject)
                {
                    if (roulMirImage.sprite != null)
                    {
                        TextLabelImg($"{roulMirImage.gameObject.name} {roulMirImage.sprite.name}");
                    }
                }

                if (roulModMirImage.gameObject == tempGameObject)
                {
                    if (roulModMirImage.sprite != null)
                    {
                        TextLabelImg($"{roulModMirImage.gameObject.name} {roulModMirImage.sprite.name}");
                    }
                }
                //new
                if (roulRune1Image.gameObject == tempGameObject)
                {
                    if (roulRune1Image.sprite != null)
                    {
                        TextLabelImg($"{roulRune1Image.gameObject.name} {roulRune1Image.sprite.name}");
                    }
                }

                if (roulModRune1Image.gameObject == tempGameObject)
                {
                    if (roulModRune1Image.sprite != null)
                    {
                        TextLabelImg($"{roulModRune1Image.gameObject.name} {roulModRune1Image.sprite.name}");
                    }
                }
            }
            else { tempGameObject = null; TextLabelImg($""); }
        }
        protected override void MousePress(InputMouseData _data)
        {
            if (tempGameObject != null & _data.MouseLeftButton != 0)
            {
                if (roulPersImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesPers.Length);
                    roulPersImage.sprite = imagePropertiesPers[nomer];
                    TextLabelImg($"{roulPersImage.gameObject.name} {imagePropertiesPers[nomer].name}");
                }

                if (roulModPersImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModPersImage.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModPersImage.gameObject.name} {imageMods[nomer].name}");
                }

                if (roulEnemyImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesEnemy.Length);
                    roulEnemyImage.sprite = imagePropertiesEnemy[nomer];
                    TextLabelImg($"{roulEnemyImage.gameObject.name} {imagePropertiesEnemy[nomer].name}");
                }

                if (roulModEnemyImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModEnemyImage.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModEnemyImage.gameObject.name} {imageMods[nomer].name}");
                }

                if (roulMirImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesMir.Length);
                    roulMirImage.sprite = imagePropertiesMir[nomer];
                    TextLabelImg($"{roulMirImage.gameObject.name} {imagePropertiesMir[nomer].name}");
                }

                if (roulModMirImage.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModMirImage.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModMirImage.gameObject.name} {imageMods[nomer].name}");
                }
                //new

                if (roulRune1Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesSumm.Length);
                    roulRune1Image.sprite = imagePropertiesSumm[nomer];
                    TextLabelImg($"{roulRune1Image.gameObject.name} {imagePropertiesSumm[nomer].name}");
                }

                if (roulModRune1Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModRune1Image.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModRune1Image.gameObject.name} {imageMods[nomer].name}");
                }
                //Переход в сцену
                // StartLvl();
            }

        }
        // private void StartLvl()
        // {
        //     if (roulPersImage.sprite != null & roulModPersImage.sprite != null &
        //     roulEnemyImage.sprite != null & roulModEnemyImage.sprite != null &
        //     roulMirImage.sprite != null & roulModMirImage.sprite != null &
        //     roulRune1Image.sprite != null & roulModRune1Image.sprite != null)
        //     {
        //         scenes.OpenScenID(idLvlScene);
        //     }
        // }
    }
}

