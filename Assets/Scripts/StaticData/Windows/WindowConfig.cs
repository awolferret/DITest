using System;
using GameInfrastructure.UI;
using GameInfrastructure.UI.Services.Windows;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}