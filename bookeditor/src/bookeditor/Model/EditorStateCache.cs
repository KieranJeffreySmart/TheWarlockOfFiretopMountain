namespace bookeditor;

public class EditorStateCache
{
    public EditorState? CurrentState { get; set; } = null;
}

public class EditorState
{
    public string? SelectedBookTitle { get; set; }
    public int? SelectedPageNumber { get; set; }
}