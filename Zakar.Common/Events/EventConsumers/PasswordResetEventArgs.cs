using Zakar.ViewModels;

namespace Zakar.Common.Events.EventConsumers
{
    public class PasswordResetEventArgs : System.EventArgs
    {
        private readonly LocalPasswordModel _model;

        public PasswordResetEventArgs(LocalPasswordModel model)
        {
            this._model = model;
        }

        public LocalPasswordModel PasswordModel
        {
            get
            {
                return this._model;
            }
        }
    }
}