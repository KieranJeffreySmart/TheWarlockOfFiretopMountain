﻿using System;
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

    public Book[]? Books => library?.Books;

    public Book? SelectedBook { get; set; }

    public Page? SelectedPage { get; set; }

    public PagePreviewViewModel SelectedPagePreview { get; } = new PagePreviewViewModel();

    public Option? SelectedOption { get; set; }
    public bool EnableSaving { get; private set; } = false;

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

            this.SelectedOption = null;
        }
    }

    public void SelectBook(Book? book)
    {
        this.SelectedBook = book;
        UpdateSelectedBook();
    }
    
    public void SelectPage(Page? page)
    {
        this.SelectedPage = page;
        UpdateSelectedPage();
    }

    public void SelectOption(Option? option)
    {
        this.SelectedOption = option;
    }

    private void CacheChanges()
    {
            this.stateCache.CurrentState ??= new EditorState();

            this.stateCache.CurrentState.SelectedBookTitle = this.SelectedBook?.Title;

            // [rgR] using the pagenumber to identify array index is dangerous, need to find a better way
            this.stateCache.CurrentState.SelectedPageNumber = this.SelectedBook?.Pages?.FindIndex(p => p.PageType == SelectedPage?.PageType && p.Index == SelectedPage?.Index);
    }

    public void UpdateSelectedBook()
    {
        this.SelectedPage = null;
        this.CacheChanges();
    }

    public void UpdateSelectedPage()
    {
        this.SelectedPagePreview.Page = this.SelectedPage;
        this.SelectedOption = null;
        this.CacheChanges();
    }

    public async Task SaveToFile()
    {
        if (library == null || this.SelectedBook == null)
            throw new Exception("No Book Selected");

        await library.WriteBookToLibrary(this.SelectedBook);
    }

    public void ComitChange()
    {
        if (this.SelectedBook == null || this.SelectedPage == null)
            throw new Exception("No Page Selected");

        var bookPageIdx = this.SelectedBook.Pages.FindIndex(p => p.PageType == this.SelectedPage.PageType && p.Index == this.SelectedPage.Index);
        if (bookPageIdx > -1) 
        {
            this.SelectedBook.Pages[bookPageIdx] = this.SelectedPage;
        }

        this.EnableSaving = true;
    }

    public void AppendSceneCaret()
    {
        if (this.SelectedPage == null)
            throw new Exception("No Page Selected");

        this.SelectedPage.Scene ??= new Scene();
        this.AppendCaret(this.SelectedPage.Scene, new Caret { CaretType = "text", StringValue = string.Empty });
    }

    public void AppendStoryCaret()
    {
        if (this.SelectedPage == null)
            throw new Exception("No Page Selected");

        this.SelectedPage.Story ??= new Story();
        this.AppendCaret(this.SelectedPage.Story, new Caret { CaretType = "text", StringValue = string.Empty });
    }

    private void AppendCaret(ICaretContainer scene, Caret caret)
    {
        scene.Carets ??= [];
        var length = scene.Carets.Length;
        var newCarets = new Caret[length+1];

        scene.Carets.CopyTo(newCarets, 0);
        scene.Carets = newCarets;
        scene.Carets[length] = caret;
    }
    
    public void InsertSceneCaretAfter(int index)
    {
        
    }
    
    public void InsertStoryCaretAfter(int index)
    {
        var maxInsertableIndex = this.SelectedPage?.Story?.Carets?.Length-2;
        if (maxInsertableIndex == null || index < 0 || index > maxInsertableIndex) this.AppendStoryCaret();



    }
    
    public void DeleteSceneCaret(int index)
    {

    }
    
    public void DeleteStoryCaret(int index)
    {

    }
}
