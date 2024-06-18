using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Registrator
{
    public class Registrator : MonoBehaviour
    {
        public int thisHash;
        public int tempChildrenHash;
        private int[] childrenHash;
        private bool isStopClass = false, isRun = false;

        private IRegistrator registrator;
        [Inject]
        public void Init(IRegistrator _registrator)
        {
            registrator = _registrator;
        }
        private void OnEnable()
        {

        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            thisHash = gameObject.GetHashCode();
            Construction element = new Construction
            {
                Hash = thisHash,
                ChildrenHash = GetChildren(),
                Object = gameObject,
            };
            registrator.SetData(element);
            if (!isRun)
            {
                isRun = true;
            }
        }
        private int[] GetChildren()
        {
            Collider[] childrens = GetComponentsInChildren<Collider>();
            childrenHash = new int[childrens.Length];

            if (childrens != null)
            {
                for (int i = 0; i < childrens.Length; i++)
                {
                    tempChildrenHash = childrens[i].gameObject.GetHashCode();
                    if (tempChildrenHash != thisHash)
                    {
                        childrenHash[i] = tempChildrenHash;
                    }
                }
            }
            return childrenHash;
        }
        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        void Update()
        {

        }
        private void RunUpdate()
        {

        }
        private void OnDisable()
        {

        }
    }
}

