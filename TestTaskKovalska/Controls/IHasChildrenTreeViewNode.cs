namespace TestTaskKovalska.Controls;

public interface IHasChildrenTreeViewNode : ITreeViewNode
{
    IList<IHasChildrenTreeViewNode> Children { get; }
    bool IsLeaf { get; set; }
}