namespace TestTaskKovalska.Controls;

public interface ILazyLoadTreeViewNode : IHasChildrenTreeViewNode
{
    Func<ITreeViewNode, IEnumerable<IHasChildrenTreeViewNode>> GetChildren { get; }
}