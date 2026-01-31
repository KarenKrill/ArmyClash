using KarenKrill.UniCore.UI.Views.Abstractions;

namespace ArmyClash.UI.Views.Abstractions
{
    public interface IDiagnosticsView : IView
    {
        public string FpsText { set; }
    }
}