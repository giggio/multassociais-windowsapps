using System.ComponentModel;

namespace MultasSociais.WinPhone8App
{
    internal class DesignMode
    {

        public static bool DesignModeEnabled
        {
            get
            {
                return DesignerProperties.IsInDesignTool;
            }
        }
    }
}
