using UnityEngine;

namespace UnityTextureRgbPacker.Editor
{
    public class TexturePackerData : ScriptableObject
    {
        #region PRIVATE SERIALZIED FIELDS
        [SerializeField] private ProcessTypeCategories processType;
        [SerializeField] private bool isPackSingle;
        [SerializeField] private bool isPackBatch;
        #endregion
        
        #region PUBLIC FIELDS 
        public enum ProcessTypeCategories
        {
            Pack,
            // Unpack,
            // Swap
        }
        #endregion
        
        #region PUBLIC PROPERTIES
        public ProcessTypeCategories ProcessType
        {
            get => processType;
            set => processType = value;
        }
        public bool IsPackSingle
        {
            get => isPackSingle;
            set => isPackSingle = value;
        }
        public bool IsPackBatch
        {
            get => isPackBatch;
            set => isPackBatch = value;
        }
        #endregion

        #region PRIVATE DEFAULT VALUES
        private const ProcessTypeCategories ProcessTypeDefault = ProcessTypeCategories.Pack;
        private const bool IsPackSingleDefault = true;
        private const bool IsPackBatchDefault = false;
        #endregion


        public TexturePackerData()
        {
            Reset();
        }

        public void Reset()
        {
            ProcessType = ProcessTypeDefault;
            isPackSingle = IsPackSingleDefault;
            isPackBatch = IsPackBatchDefault;
        }

    }
}