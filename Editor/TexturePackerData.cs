using UnityEngine;

namespace UnityTextureRgbPacker.Editor
{
    public class TexturePackerData : ScriptableObject
    {
        #region PRIVATE SERIALZIED FIELDS
        [SerializeField] private ProcessTypeCategories processType;
        #endregion
        
        #region PUBLIC PROPERTIES

        #region PUBLIC FIELDS 
        public enum ProcessTypeCategories
        {
            Pack,
            Unpack,
            Swap
        }
        #endregion
        
        public ProcessTypeCategories ProcessType
        {
            get => processType;
            set => processType = value;
        }
        #endregion
        
        

    }
}