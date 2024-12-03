namespace TestTaskKovalska.Controls;

public interface ITreeViewNode
{
    string Name { get; set; }
    object Value { get; set; }
    bool IsExtended { get; set; }
    
    bool CanExtend { get; }
    
    Action AdvancedAction { get; set; }
}