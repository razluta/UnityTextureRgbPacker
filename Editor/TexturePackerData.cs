using NUnit.Framework.Constraints;
using UnityEngine;

namespace UnityTextureRgbPacker.Editor
{
    public class TexturePackerData : ScriptableObject
    {
        #region PRIVATE SERIALZIED FIELDS
        [SerializeField] private ProcessTypeCategories processType;
        [SerializeField] private bool isPackSingle;
        [SerializeField] private bool isPackBatch;
        // Red Channel Input
        [SerializeField] private bool isRedChannelActive;
        [SerializeField] private Texture2D redChannelTexture;
        [SerializeField] private bool isRedChannelTextureUseAllChannels;
        [SerializeField] private bool isRedChannelTextureUseRedChannel;
        [SerializeField] private bool isRedChannelTextureUseGreenChannel;
        [SerializeField] private bool isRedChannelTextureUseBlueChannel;
        [SerializeField] private bool isRedChannelTextureUseAlphaChannel;
        // Green Channel Input
        [SerializeField] private bool isGreenChannelActive;
        [SerializeField] private Texture2D greenChannelTexture;
        [SerializeField] private bool isGreenChannelTextureUseAllChannels;
        [SerializeField] private bool isGreenChannelTextureUseRedChannel;
        [SerializeField] private bool isGreenChannelTextureUseGreenChannel;
        [SerializeField] private bool isGreenChannelTextureUseBlueChannel;
        [SerializeField] private bool isGreenChannelTextureUseAlphaChannel;
        // Blue Channel Input
        [SerializeField] private bool isBlueChannelActive;
        [SerializeField] private Texture2D blueChannelTexture;
        [SerializeField] private bool isBlueChannelTextureUseAllChannels;
        [SerializeField] private bool isBlueChannelTextureUseRedChannel;
        [SerializeField] private bool isBlueChannelTextureUseGreenChannel;
        [SerializeField] private bool isBlueChannelTextureUseBlueChannel;
        [SerializeField] private bool isBlueChannelTextureUseAlphaChannel;
        // Alpha Channel Input
        [SerializeField] private bool isAlphaChannelActive;
        [SerializeField] private Texture2D alphaChannelTexture;
        [SerializeField] private bool isAlphaChannelTextureUseAllChannels;
        [SerializeField] private bool isAlphaChannelTextureUseRedChannel;
        [SerializeField] private bool isAlphaChannelTextureUseGreenChannel;
        [SerializeField] private bool isAlphaChannelTextureUseBlueChannel;
        [SerializeField] private bool isAlphaChannelTextureUseAlphaChannel;
        // Export Type
        [SerializeField] private TextureUtilities.TextureUtilitiesFormats textureFormat;
        // Export Size
        [SerializeField] private SizingCriteriaCategories sizingCriteria;
        [SerializeField] private int textureWidth;
        [SerializeField] private int textureHeight;
        // Export Names
        [SerializeField] private bool deriveRootFromInputs;
        [SerializeField] private string root;
        [SerializeField] private string nameIdentifier;
        #endregion
        
        #region PUBLIC FIELDS 
        public enum ProcessTypeCategories
        {
            Pack,
            // Unpack,
            // Swap
        }
        public enum SizingCriteriaCategories
        {
            ScaleToSmallestInput,
            SpecifySize
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
        // Red Channel Input
        public bool IsRedChannelActive
        {
            get => isRedChannelActive;
            set => isRedChannelActive = value;
        }
        public Texture2D RedChannelTexture
        {
            get => redChannelTexture;
            set => redChannelTexture = value;
        }
        public bool IsRedChannelTextureUseAllChannels
        {
            get => isRedChannelTextureUseAllChannels;
            set => isRedChannelTextureUseAllChannels = value;
        }
        public bool IsRedChannelTextureUseRedChannel
        {
            get => isRedChannelTextureUseRedChannel;
            set => isRedChannelTextureUseRedChannel = value;
        }
        public bool IsRedChannelTextureUseGreenChannel
        {
            get => isRedChannelTextureUseGreenChannel;
            set => isRedChannelTextureUseGreenChannel = value;
        }
        public bool IsRedChannelTextureUseBlueChannel
        {
            get => isRedChannelTextureUseBlueChannel;
            set => isRedChannelTextureUseBlueChannel = value;
        }
        public bool IsRedChannelTextureUseAlphaChannel
        {
            get => isRedChannelTextureUseAlphaChannel;
            set => isRedChannelTextureUseAlphaChannel = value;
        }
        // Green Channel Input
        public bool IsGreenChannelActive
        {
            get => isGreenChannelActive;
            set => isGreenChannelActive = value;
        }
        public Texture2D GreenChannelTexture
        {
            get => greenChannelTexture;
            set => greenChannelTexture = value;
        }
        public bool IsGreenChannelTextureUseAllChannels
        {
            get => isGreenChannelTextureUseAllChannels;
            set => isGreenChannelTextureUseAllChannels = value;
        }
        public bool IsGreenChannelTextureUseRedChannel
        {
            get => isGreenChannelTextureUseRedChannel;
            set => isGreenChannelTextureUseRedChannel = value;
        }
        public bool IsGreenChannelTextureUseGreenChannel
        {
            get => isGreenChannelTextureUseGreenChannel;
            set => isGreenChannelTextureUseGreenChannel = value;
        }
        public bool IsGreenChannelTextureUseBlueChannel
        {
            get => isGreenChannelTextureUseBlueChannel;
            set => isGreenChannelTextureUseBlueChannel = value;
        }
        public bool IsGreenChannelTextureUseAlphaChannel
        {
            get => isGreenChannelTextureUseAlphaChannel;
            set => isGreenChannelTextureUseAlphaChannel = value;
        }
        // Blue Channel Input
        public bool IsBlueChannelActive
        {
            get => isBlueChannelActive;
            set => isBlueChannelActive = value;
        }
        public Texture2D BlueChannelTexture
        {
            get => blueChannelTexture;
            set => blueChannelTexture = value;
        }
        public bool IsBlueChannelTextureUseAllChannels
        {
            get => isBlueChannelTextureUseAllChannels;
            set => isBlueChannelTextureUseAllChannels = value;
        }
        public bool IsBlueChannelTextureUseRedChannel
        {
            get => isBlueChannelTextureUseRedChannel;
            set => isBlueChannelTextureUseRedChannel = value;
        }
        public bool IsBlueChannelTextureUseGreenChannel
        {
            get => isBlueChannelTextureUseGreenChannel;
            set => isBlueChannelTextureUseGreenChannel = value;
        }
        public bool IsBlueChannelTextureUseBlueChannel
        {
            get => isBlueChannelTextureUseBlueChannel;
            set => isBlueChannelTextureUseBlueChannel = value;
        }
        public bool IsBlueChannelTextureUseAlphaChannel
        {
            get => isBlueChannelTextureUseAlphaChannel;
            set => isBlueChannelTextureUseAlphaChannel = value;
        }
        // Alpha Channel Input
        public bool IsAlphaChannelActive
        {
            get => isAlphaChannelActive;
            set => isAlphaChannelActive = value;
        }
        public Texture2D AlphaChannelTexture
        {
            get => alphaChannelTexture;
            set => alphaChannelTexture = value;
        }
        public bool IsAlphaChannelTextureUseAllChannels
        {
            get => isAlphaChannelTextureUseAllChannels;
            set => isAlphaChannelTextureUseAllChannels = value;
        }
        public bool IsAlphaChannelTextureUseRedChannel
        {
            get => isAlphaChannelTextureUseRedChannel;
            set => isAlphaChannelTextureUseRedChannel = value;
        }
        public bool IsAlphaChannelTextureUseGreenChannel
        {
            get => isAlphaChannelTextureUseGreenChannel;
            set => isAlphaChannelTextureUseGreenChannel = value;
        }
        public bool IsAlphaChannelTextureUseBlueChannel
        {
            get => isAlphaChannelTextureUseBlueChannel;
            set => isAlphaChannelTextureUseBlueChannel = value;
        }
        public bool IsAlphaChannelTextureUseAlphaChannel
        {
            get => isAlphaChannelTextureUseAlphaChannel;
            set => isAlphaChannelTextureUseAlphaChannel = value;
        }
        // Export Type
        public TextureUtilities.TextureUtilitiesFormats TextureFormat
        {
            get => textureFormat;
            set => textureFormat = value;
        }
        // Export Size
        public SizingCriteriaCategories SizingCriteria
        {
            get => sizingCriteria;
            set => sizingCriteria = value;
        }
        public int TextureWidth
        {
            get => textureWidth;
            set => textureWidth = value;
        }
        public int TextureHeight
        {
            get => textureHeight;
            set => textureHeight = value;
        }
        // Export Names
        public bool DeriveRootFromInputs
        {
            get => deriveRootFromInputs;
            set => deriveRootFromInputs = value;
        }
        public string Root
        {
            get => root;
            set => root = value;
        }
        public string NameIdentifier
        {
            get => nameIdentifier;
            set => nameIdentifier = value;
        }
        #endregion

        #region PRIVATE DEFAULT VALUES
        private const ProcessTypeCategories ProcessTypeDefault = ProcessTypeCategories.Pack;
        private const bool IsPackSingleDefault = true;
        private const bool IsPackBatchDefault = false;
        // Red Channel Input
        private const bool IsRedChannelActiveDefault = false;
        private const Texture2D RedChannelTextureDefault = null;
        private const bool IsRedChannelTextureUseAllChannelsDefault = true;
        private const bool IsRedChannelTextureUseRedChannelDefault = false;
        private const bool IsRedChannelTextureUseGreenChannelDefault = false;
        private const bool IsRedChannelTextureUseBlueChannelDefault = false;
        private const bool IsRedChannelTextureUseAlphaChannelDefault = false;
        // Green Channel Input
        private const bool IsGreenChannelActiveDefault = false;
        private const Texture2D GreenChannelTextureDefault = null;
        private const bool IsGreenChannelTextureUseAllChannelsDefault = true;
        private const bool IsGreenChannelTextureUseRedChannelDefault = false;
        private const bool IsGreenChannelTextureUseGreenChannelDefault = false;
        private const bool IsGreenChannelTextureUseBlueChannelDefault = false;
        private const bool IsGreenChannelTextureUseAlphaChannelDefault = false;
        // Blue Channel Input
        private const bool IsBlueChannelActiveDefault = false;
        private const Texture2D BlueChannelTextureDefault = null;
        private const bool IsBlueChannelTextureUseAllChannelsDefault = true;
        private const bool IsBlueChannelTextureUseRedChannelDefault = false;
        private const bool IsBlueChannelTextureUseGreenChannelDefault = false;
        private const bool IsBlueChannelTextureUseBlueChannelDefault = false;
        private const bool IsBlueChannelTextureUseAlphaChannelDefault = false;
        // Alpha Channel Input
        private const bool IsAlphaChannelActiveDefault = false;
        private const Texture2D AlphaChannelTextureDefault = null;
        private const bool IsAlphaChannelTextureUseAllChannelsDefault = true;
        private const bool IsAlphaChannelTextureUseRedChannelDefault = false;
        private const bool IsAlphaChannelTextureUseGreenChannelDefault = false;
        private const bool IsAlphaChannelTextureUseBlueChannelDefault = false;
        private const bool IsAlphaChannelTextureUseAlphaChannelDefault = false;
        // Export Type
        private const TextureUtilities.TextureUtilitiesFormats TextureFormatDefault =
            TextureUtilities.TextureUtilitiesFormats.Tga;
        // Export Size
        private const int TextureWidthDefault = 512;
        private const int TextureHeightDefault = 512;
        // Export Names
        private const bool DeriveRootFromInputsDefault = true;
        private const string RootDefault = "";
        private const string NameIdentifierDefault = "Composite";
        #endregion

        public TexturePackerData()
        {
            Reset();
        }

        public void ClearInputs()
        {
            // Red Channel Input
            IsRedChannelActive = IsRedChannelActiveDefault;
            RedChannelTexture = RedChannelTextureDefault;
            IsRedChannelTextureUseAllChannels = IsRedChannelTextureUseAllChannelsDefault;
            IsRedChannelTextureUseRedChannel = IsRedChannelTextureUseRedChannelDefault;
            IsRedChannelTextureUseGreenChannel = IsRedChannelTextureUseGreenChannelDefault;
            IsRedChannelTextureUseBlueChannel = IsRedChannelTextureUseBlueChannelDefault;
            IsRedChannelTextureUseAlphaChannel = IsRedChannelTextureUseAlphaChannelDefault;
            // Green Channel Input
            IsGreenChannelActive = IsGreenChannelActiveDefault;
            GreenChannelTexture = GreenChannelTextureDefault;
            IsGreenChannelTextureUseAllChannels = IsGreenChannelTextureUseAllChannelsDefault;
            IsGreenChannelTextureUseRedChannel = IsGreenChannelTextureUseRedChannelDefault;
            IsGreenChannelTextureUseGreenChannel = IsGreenChannelTextureUseGreenChannelDefault;
            IsGreenChannelTextureUseBlueChannel = IsGreenChannelTextureUseBlueChannelDefault;
            IsGreenChannelTextureUseAlphaChannel = IsGreenChannelTextureUseAlphaChannelDefault;
            // Blue Channel Input
            IsBlueChannelActive = IsBlueChannelActiveDefault;
            BlueChannelTexture = BlueChannelTextureDefault;
            IsBlueChannelTextureUseAllChannels = IsBlueChannelTextureUseAllChannelsDefault;
            IsBlueChannelTextureUseRedChannel = IsBlueChannelTextureUseRedChannelDefault;
            IsBlueChannelTextureUseGreenChannel = IsBlueChannelTextureUseGreenChannelDefault;
            IsBlueChannelTextureUseBlueChannel = IsBlueChannelTextureUseBlueChannelDefault;
            IsBlueChannelTextureUseAlphaChannel = IsBlueChannelTextureUseAlphaChannelDefault;
            // Alpha Channel Input
            IsAlphaChannelActive= IsAlphaChannelActiveDefault;
            AlphaChannelTexture = AlphaChannelTextureDefault;
            IsAlphaChannelTextureUseAllChannels = IsAlphaChannelTextureUseAllChannelsDefault;
            IsAlphaChannelTextureUseRedChannel = IsAlphaChannelTextureUseRedChannelDefault;
            IsAlphaChannelTextureUseGreenChannel = IsAlphaChannelTextureUseGreenChannelDefault;
            IsAlphaChannelTextureUseBlueChannel = IsAlphaChannelTextureUseBlueChannelDefault;
            IsAlphaChannelTextureUseAlphaChannel = IsAlphaChannelTextureUseAlphaChannelDefault;
        }
        
        public void Reset()
        {
            ProcessType = ProcessTypeDefault;
            IsPackSingle = IsPackSingleDefault;
            IsPackBatch = IsPackBatchDefault;
            
            ClearInputs();

            TextureFormat = TextureFormatDefault;
            TextureWidth = TextureWidthDefault;
            TextureHeight = TextureHeightDefault;
            DeriveRootFromInputs = DeriveRootFromInputsDefault;
            Root = RootDefault;
            NameIdentifier = NameIdentifierDefault;
        }

    }
}