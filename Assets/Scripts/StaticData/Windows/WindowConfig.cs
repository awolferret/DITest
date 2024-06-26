using System;
using GameInfasrtucture.UI;
using GameInfasrtucture.UI.Services.Windows;

namespace StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}