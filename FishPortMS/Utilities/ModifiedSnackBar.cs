using MudBlazor;

namespace FishPortMS.Utilities
{
    public class ModifiedSnackBar
    {
        private readonly ISnackbar Snackbar;
        public ModifiedSnackBar(ISnackbar snackbar)
        {
            Snackbar = snackbar;
        }
        public void ShowMessage(string message, Severity severity, SnackbarConfiguration configuration = null)
        {
            Snackbar.Add(
                message,
                severity,
                config =>
                {
                    config.ShowTransitionDuration = 200;
                    config.HideTransitionDuration = 400;
                    config.VisibleStateDuration = 2500;
                });
        }
    }
}
