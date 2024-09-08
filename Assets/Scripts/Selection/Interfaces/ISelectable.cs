public interface ISelectable
{
    void OnHover();
    void OnNotHover();
    void SelectionUpdate();
    bool IsSelected();
}
