using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace MultasSociais.WinPhone8App
{
    public abstract class BaseScreen : Screen
    {
        public override void NotifyOfPropertyChange([CallerMemberName] string propertyName = "")
        {
            base.NotifyOfPropertyChange(propertyName);
        }
    }
}