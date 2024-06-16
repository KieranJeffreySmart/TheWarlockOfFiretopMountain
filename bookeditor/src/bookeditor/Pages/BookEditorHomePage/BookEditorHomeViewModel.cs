using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Utils;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class BookEditorHomeViewModel : DotvvmViewModelBase
{
    public string? LibraryPath { get {return this.library?.RootPath; } set { if (this.library != null) {this.library.RootPath = value; } } }

    private readonly XmlLibrary? library;

    private readonly EditorStateCache stateCache;

    public BookEditorHomeViewModel(XmlLibrary library, EditorStateCache stateCache)
    {
        this.library = library;
        this.stateCache = stateCache;
        this.SetStateFromCache();
    }

    private void SetStateFromCache()
    {
        if (this.stateCache.CurrentState != null)
        {
            var state = this.stateCache.CurrentState;
            if (!string.IsNullOrWhiteSpace(state.SelectedBookTitle)) 
            {
                this.SelectedBook = Books?.FirstOrDefault(b => b.Title == state.SelectedBookTitle);

                if (state.SelectedPageNumber.HasValue && state.SelectedPageNumber.Value >= 0) 
                {
                    // [rgR] using the pagenumber to identify array index is dangerous, need to find a better way
                    this.SelectedPage = this.SelectedBook?.Pages[state.SelectedPageNumber.Value];
                }
            }

            this.SelectedBookDetails.Book = this.SelectedBook;
            this.SelectedPageDetails.Page = this.SelectedPage;
        }
    }

    [Bind(Direction.ServerToClientFirstRequest)]
    public Book[]? Books => library?.Books;

    public Book? SelectedBook { get; set; }

    public BookDetailViewModel SelectedBookDetails { get; } = new BookDetailViewModel();

    private void CacheChanges()
    {
            this.stateCache.CurrentState ??= new EditorState();

            this.stateCache.CurrentState.SelectedBookTitle = this.SelectedBook?.Title;

            // [rgR] using the pagenumber to identify array index is dangerous, need to find a better way
            this.stateCache.CurrentState.SelectedPageNumber = this.SelectedBook?.Pages?.FindIndex(p => p.PageType == SelectedPage?.PageType && p.Index == SelectedPage?.Index);
    }


    public void UpdateSelectedBook()
    {
        this.SelectedBookDetails.Book = this.SelectedBook;
        this.SelectedPage = null;
        this.SelectedPageDetails.Page = this.SelectedPage;
        this.CacheChanges();
    }
    public void UpdateSelectedPage()
    {
        this.SelectedPageDetails.Page = this.SelectedPage;
        this.CacheChanges();
    }

    public Page? SelectedPage { get; set; }

    public PageDetailViewModel SelectedPageDetails { get; } = new PageDetailViewModel();
}
