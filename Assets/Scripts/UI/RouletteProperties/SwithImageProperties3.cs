using System;
using System.Linq;
using Inputs;
using Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SwithImageProperties3 : MonoBehaviour
    {
        [Header("Кнопка LabelRoulElementButton")]
        [SerializeField] protected Text labelRoulElementButton;

        [Header("RoulPersImage")]
        [SerializeField] protected Image roulPersImage;

        [Header("RoulModPersImage")]
        [SerializeField] protected Image roulModPersImage;

        [Header("RoulEnemyImage")]
        [SerializeField] protected Image roulEnemyImage;

        [Header("RoulModEnemyImage")]
        [SerializeField] protected Image roulModEnemyImage;

        [Header("RoulMirImage")]
        [SerializeField] protected Image roulMirImage;

        [Header("RoulModMirImage")]
        [SerializeField] protected Image roulModMirImage;

        //
        [Header("ImagePropertiesPers")]
        [SerializeField] protected Sprite[] imagePropertiesPers;

        [Header("ImagePropertiesEnemy")]
        [SerializeField] protected Sprite[] imagePropertiesEnemy;

        [Header("ImagePropertiesMir")]
        [SerializeField] protected Sprite[] imagePropertiesMir;

        [Header("ImageMods")]
        [SerializeField] protected Sprite[] imageMods;

        [Header("Указать ID загружаемой сцены")]
        [SerializeField] protected int idLvlScene = 0;

        [Header("Кнопка ReternRoulProper3")]
        [SerializeField] private CustomButton followLvl3Button;
        protected Sprite[] imagePropertiesSumm;
        protected GameObject tempGameObject;
        protected int nomer;
        protected bool isStopClass = false, isRun = false;
        //
        protected IInputPlayerExecutor inputs;
        protected IUIGameExecutor uiGame;
        protected ISceneExecutor scenes;
        [Inject]
        public void Init(IUIGameExecutor _uiGame, IInputPlayerExecutor _inputs, ISceneExecutor _scenes)
        {
            uiGame = _uiGame;
            inputs = _inputs;
            scenes = _scenes;
        }
        private void OnEnable()
        {
            inputs.Enable();
            inputs.OnMousePoint += MousePoint;
            inputs.OnStartPressMouse += MousePress;
            //
            followLvl3Button.onClick.AddListener(() => OpenLvl());
        }
        protected virtual void MousePoint(InputMouseData _data)
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
            }
            else { tempGameObject = null; TextLabelImg($""); }
        }
        protected virtual void StartRandom()
        {
            nomer = RandomImg(imagePropertiesPers.Length);
            roulPersImage.sprite = imagePropertiesPers[nomer];

            nomer = RandomImg(imageMods.Length);
            roulModPersImage.sprite = imageMods[nomer];

            nomer = RandomImg(imagePropertiesEnemy.Length);
            roulEnemyImage.sprite = imagePropertiesEnemy[nomer];

            nomer = RandomImg(imageMods.Length);
            roulModEnemyImage.sprite = imageMods[nomer];

            nomer = RandomImg(imagePropertiesMir.Length);
            roulMirImage.sprite = imagePropertiesMir[nomer];

            nomer = RandomImg(imageMods.Length);
            roulModMirImage.sprite = imageMods[nomer];

            //Переход в сцену
            //StartLvl();
        }
        private void OpenLvl()
        {
            scenes.OpenScenID(idLvlScene);
        }
        protected virtual void MousePress(InputMouseData _data)
        {
            // if (tempGameObject != null & _data.MouseLeftButton != 0)
            // {
            //     if (roulPersImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imagePropertiesPers.Length);
            //         roulPersImage.sprite = imagePropertiesPers[nomer];
            //         TextLabelImg($"{roulPersImage.gameObject.name} {imagePropertiesPers[nomer].name}");
            //     }

            //     if (roulModPersImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imageMods.Length);
            //         roulModPersImage.sprite = imageMods[nomer];
            //         TextLabelImg($"{roulModPersImage.gameObject.name} {imageMods[nomer].name}");
            //     }

            //     if (roulEnemyImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imagePropertiesEnemy.Length);
            //         roulEnemyImage.sprite = imagePropertiesEnemy[nomer];
            //         TextLabelImg($"{roulEnemyImage.gameObject.name} {imagePropertiesEnemy[nomer].name}");
            //     }

            //     if (roulModEnemyImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imageMods.Length);
            //         roulModEnemyImage.sprite = imageMods[nomer];
            //         TextLabelImg($"{roulModEnemyImage.gameObject.name} {imageMods[nomer].name}");
            //     }

            //     if (roulMirImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imagePropertiesMir.Length);
            //         roulMirImage.sprite = imagePropertiesMir[nomer];
            //         TextLabelImg($"{roulMirImage.gameObject.name} {imagePropertiesMir[nomer].name}");
            //     }

            //     if (roulModMirImage.gameObject == tempGameObject)
            //     {
            //         nomer = RandomImg(imageMods.Length);
            //         roulModMirImage.sprite = imageMods[nomer];
            //         TextLabelImg($"{roulModMirImage.gameObject.name} {imageMods[nomer].name}");
            //     }
            //     //Переход в сцену
            //     StartLvl();
            // }

        }
        // private void StartLvl()
        // {
        //     if (roulPersImage.sprite != null & roulModPersImage.sprite != null &
        //     roulEnemyImage.sprite != null & roulModEnemyImage.sprite != null &
        //     roulMirImage.sprite != null & roulModMirImage.sprite != null)
        //     {
        //         scenes.OpenScenID(idLvlScene);
        //     }
        // }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            for (int i = 0; i < imagePropertiesPers.Length; i++)
            {
                imagePropertiesSumm = Creat(imagePropertiesPers[i], imagePropertiesSumm);
            }

            for (int i = 0; i < imagePropertiesEnemy.Length; i++)
            {
                imagePropertiesSumm = Creat(imagePropertiesEnemy[i], imagePropertiesSumm);
            }

            for (int i = 0; i < imagePropertiesMir.Length; i++)
            {
                imagePropertiesSumm = Creat(imagePropertiesMir[i], imagePropertiesSumm);
            }

            StartRandom();

            if (!isRun)
            {
                isRun = true;
            }
        }
        public Sprite[] Creat(Sprite intObject, Sprite[] massivObject)
        {
            bool isStop = false;
            if (massivObject != null)
            {
                if (!isStop)
                {
                    int newLength = massivObject.Length + 1;
                    Array.Resize(ref massivObject, newLength);
                    massivObject[newLength - 1] = intObject;
                    return massivObject;
                }
            }
            else
            {
                massivObject = new Sprite[] { intObject };
                return massivObject;
            }
            return massivObject;
        }
        protected int RandomImg(int _maxRange)
        {
            if (_maxRange > 0) { nomer = UnityEngine.Random.Range(0, _maxRange); }
            else { nomer = 0; }
            return nomer;
        }
        protected void TextLabelImg(string _txtLabel)
        {
            labelRoulElementButton.text = _txtLabel;
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

