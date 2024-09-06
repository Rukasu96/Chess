public interface ISelectable
{
    void OnHover();
    void OnNotHover();
    void Selected();
    void Deselect();
    bool IsSelected();
}
