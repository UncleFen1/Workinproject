using Inputs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SwithImageProperties8 : SwithImageProperties4
    {

        [Header("RoulRune2Image")]//new
        [SerializeField] protected Image roulRune2Image;

        [Header("RoulModRune2Image")]//new
        [SerializeField] protected Image roulModRune2Image;

        [Header("RoulRune3Image")]//new
        [SerializeField] protected Image roulRune3Image;

        [Header("RoulModRune3Image")]//new
        [SerializeField] protected Image roulModRune3Image;

        [Header("RoulRune4Image")]//new
        [SerializeField] protected Image roulRune4Image;

        [Header("RoulModRune4Image")]//new
        [SerializeField] protected Image roulModRune4Image;

        [Header("RoulRune5Image")]//new
        [SerializeField] protected Image roulRune5Image;

        [Header("RoulModRune5Image")]//new
        [SerializeField] protected Image roulModRune5Image;
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
                //new+
                if (roulRune2Image.gameObject == tempGameObject)
                {
                    if (roulRune2Image.sprite != null)
                    {
                        TextLabelImg($"{roulRune2Image.gameObject.name} {roulRune2Image.sprite.name}");
                    }
                }

                if (roulModRune2Image.gameObject == tempGameObject)
                {
                    if (roulModRune2Image.sprite != null)
                    {
                        TextLabelImg($"{roulModRune2Image.gameObject.name} {roulModRune2Image.sprite.name}");
                    }
                }

                if (roulRune3Image.gameObject == tempGameObject)
                {
                    if (roulRune3Image.sprite != null)
                    {
                        TextLabelImg($"{roulRune3Image.gameObject.name} {roulRune3Image.sprite.name}");
                    }
                }

                if (roulModRune3Image.gameObject == tempGameObject)
                {
                    if (roulModRune3Image.sprite != null)
                    {
                        TextLabelImg($"{roulModRune3Image.gameObject.name} {roulModRune3Image.sprite.name}");
                    }
                }

                if (roulRune4Image.gameObject == tempGameObject)
                {
                    if (roulRune4Image.sprite != null)
                    {
                        TextLabelImg($"{roulRune4Image.gameObject.name} {roulRune4Image.sprite.name}");
                    }
                }

                if (roulModRune4Image.gameObject == tempGameObject)
                {
                    if (roulModRune4Image.sprite != null)
                    {
                        TextLabelImg($"{roulModRune4Image.gameObject.name} {roulModRune4Image.sprite.name}");
                    }
                }

                if (roulRune5Image.gameObject == tempGameObject)
                {
                    if (roulRune5Image.sprite != null)
                    {
                        TextLabelImg($"{roulRune5Image.gameObject.name} {roulRune5Image.sprite.name}");
                    }
                }

                if (roulModRune5Image.gameObject == tempGameObject)
                {
                    if (roulModRune5Image.sprite != null)
                    {
                        TextLabelImg($"{roulModRune5Image.gameObject.name} {roulModRune5Image.sprite.name}");
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
                //new+

                if (roulRune2Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesSumm.Length);
                    roulRune2Image.sprite = imagePropertiesSumm[nomer];
                    TextLabelImg($"{roulRune2Image.gameObject.name} {imagePropertiesSumm[nomer].name}");
                }

                if (roulModRune2Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModRune2Image.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModRune2Image.gameObject.name} {imageMods[nomer].name}");
                }

                if (roulRune3Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesSumm.Length);
                    roulRune3Image.sprite = imagePropertiesSumm[nomer];
                    TextLabelImg($"{roulRune3Image.gameObject.name} {imagePropertiesSumm[nomer].name}");
                }

                if (roulModRune3Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModRune3Image.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModRune3Image.gameObject.name} {imageMods[nomer].name}");
                }

                if (roulRune4Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesSumm.Length);
                    roulRune4Image.sprite = imagePropertiesSumm[nomer];
                    TextLabelImg($"{roulRune4Image.gameObject.name} {imagePropertiesSumm[nomer].name}");
                }

                if (roulModRune4Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModRune4Image.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModRune4Image.gameObject.name} {imageMods[nomer].name}");
                }

                if (roulRune5Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imagePropertiesSumm.Length);
                    roulRune5Image.sprite = imagePropertiesSumm[nomer];
                    TextLabelImg($"{roulRune5Image.gameObject.name} {imagePropertiesSumm[nomer].name}");
                }

                if (roulModRune5Image.gameObject == tempGameObject)
                {
                    nomer = RandomImg(imageMods.Length);
                    roulModRune5Image.sprite = imageMods[nomer];
                    TextLabelImg($"{roulModRune5Image.gameObject.name} {imageMods[nomer].name}");
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
        //     roulRune1Image.sprite != null & roulModRune1Image.sprite != null &
        //     roulRune2Image.sprite != null & roulModRune2Image.sprite != null &
        //     roulRune3Image.sprite != null & roulModRune3Image.sprite != null &
        //     roulRune4Image.sprite != null & roulModRune4Image.sprite != null &
        //     roulRune5Image.sprite != null & roulModRune5Image.sprite != null)
        //     {
        //         scenes.OpenScenID(idLvlScene);
        //     }
        // }

    }
}

