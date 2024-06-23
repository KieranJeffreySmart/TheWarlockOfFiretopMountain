using System;
using System.Linq;
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

    public Option? SelectedOption { get; set; }
    public bool IsSaveEnabled { get; private set; } = false;
    
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
        this.SelectedOption = null;
        this.CacheChanges();
    }

    public void UpdateSelectedPage()
    {
        this.SelectedOption = null;
        this.CacheChanges();
    }

    public void SaveToFile()
    {
        if (library == null || this.SelectedBook == null)
            // [rgR] should implement custom exceptions
            throw new Exception("No Book Selected");

        library.WriteBookToLibrarySync(this.SelectedBook);
    }

    public void ApplyChanges()
    {
        if (this.SelectedBook == null || this.SelectedPage == null)
            // [rgR] should implement custom exceptions
            throw new Exception("No Page Selected");

        var bookPageIdx = this.SelectedBook.Pages.FindIndex(p => p.PageType == this.SelectedPage.PageType && p.Index == this.SelectedPage.Index);
        if (bookPageIdx > -1) 
        {
            this.SelectedBook.Pages[bookPageIdx] = this.SelectedPage;
        }

        this.IsSaveEnabled = true;
    }

    public void AppendSceneBlock()
    {
        if (this.SelectedPage == null)
            // [rgR] should implement custom exceptions
            throw new Exception("No Page Selected");

        this.SelectedPage.Scene ??= new Scene();
        this.AppendBlock(this.SelectedPage.Scene, new Block { BlockType = "text", StringValue = string.Empty });
    }

    public void AppendStoryBlock()
    {
        if (this.SelectedPage == null)
            // [rgR] should implement custom exceptions
            throw new Exception("No Page Selected");

        this.SelectedPage.Story ??= new Story();
        this.AppendBlock(this.SelectedPage.Story, new Block { BlockType = "text", StringValue = string.Empty });
    }

    private void AppendBlock(IBlockContainer container, Block block)
    {
        container.Blocks ??= [];
        var length = container.Blocks.Length;
        var newBlocks = new Block[length+1];

        container.Blocks.CopyTo(newBlocks, 0);
        container.Blocks = newBlocks;
        container.Blocks[length] = block;
    }

    public void InsertSceneBlockAfter(int index)
    {
        var maxInsertableIndex = this.SelectedPage?.Scene?.Blocks?.Length-2 ?? -1;
        if (this.SelectedPage?.Scene?.Blocks == null || index < 0 || index > maxInsertableIndex) 
        {
            this.AppendSceneBlock();
            return;
        }

        this.InsertBlock(this.SelectedPage.Scene, index, new Block() { BlockType = "text", StringValue = string.Empty });        
    }

    private void InsertBlock(IBlockContainer container, int index, Block block)
    {        
        container.Blocks ??= [];
        var newLength = container.Blocks.Length+1;
        var blocks = new Block[newLength];

        for (var i = 0; i < newLength; i++)
        {
            if (i <= index)
            {
                blocks[i] = container.Blocks[i];
                continue;
            }

            if (i > index+1)
            {
                blocks[i] = container.Blocks[i-1];
                continue;
            }
            
            blocks[i] = block;
        }

        container.Blocks = blocks;
    }

    public void InsertStoryBlockAfter(int index)
    {
        var maxInsertableIndex = this.SelectedPage?.Story?.Blocks?.Length-2 ?? -1;
        if (this.SelectedPage?.Story?.Blocks == null || index < 0 || index > maxInsertableIndex) 
        {
            this.AppendStoryBlock();
            return;
        }

        var newLength = this.SelectedPage.Story.Blocks.Length+1;
        var blocks = new Block[newLength];

        for (var i = 0; i < newLength; i++)
        {
            if (i <= index)
            {
                blocks[i] = this.SelectedPage.Story.Blocks[i];
                continue;
            }

            if (i > index+1)
            {
                blocks[i] = this.SelectedPage.Story.Blocks[i-1];
                continue;
            }
            
            blocks[i] = new Block() { BlockType = "text", StringValue = string.Empty };
        }

        this.SelectedPage.Story.Blocks = blocks;
    }
    
    public void DeleteSceneBlock(int index)
    {
        var blockCount = this.SelectedPage?.Scene?.Blocks?.Length;
        if (this.Books == null || this.SelectedPage?.Scene?.Blocks == null || blockCount == null || blockCount == 0 || index < 0 || index >= blockCount)
            // [rgR] should implement custom exceptions
            throw new Exception("Cannot delete something that doesnt exist");

        var blockList = this.SelectedPage.Scene.Blocks.ToList();
        blockList.RemoveAt(index);

        this.SelectedPage.Scene.Blocks = [.. blockList];
    }
    
    public void DeleteStoryBlock(int index)
    {
        var blockCount = this.SelectedPage?.Story?.Blocks?.Length;
        if (this.Books == null || this.SelectedPage?.Story?.Blocks == null || blockCount == null || blockCount == 0 || index < 0 || index >= blockCount)
            // [rgR] should implement custom exceptions
            throw new Exception("Cannot delete something that doesnt exist");

        var blockList = this.SelectedPage.Story.Blocks.ToList();
        blockList.RemoveAt(index);

        this.SelectedPage.Story.Blocks = [.. blockList];
    }

    public void AppendOptionByCommand(string? command = null)
    {
        if (this.SelectedBook == null || this.SelectedPage == null)
            // [rgR] should implement custom exceptions
            throw new Exception("No Page Selected");

        this.SelectedPage.Options ??= [];
        
        var length = this.SelectedPage.Options.Length;
        var newOptionss = new Option[length+1];
        this.SelectedPage.Options.CopyTo(newOptionss, 0);
        this.SelectedPage.Options = newOptionss;

        if (string.IsNullOrWhiteSpace(command))
        {
            this.SelectedPage.Options[length] = new Option();
            return;
        }

        if (command == "START_GAME")
        {
            this.SelectedPage.Options[length] = new Option { Command = command, Key="s", Label="Start game" };
            return;
        }

        if (command == "QUIT_GAME")
        {
            this.SelectedPage.Options[length] = new Option { Command = command, Key="q", Label="Quit game" };
            return;
        }
        
        if (command == "NEXT_PAGE")
        {
            this.SelectedPage.Options[length] = new Option { Command = command, Key="n", Label="Next page" };
            return;
        }
        
        if (command == "PREVIOUS_PAGE")
        {
            this.SelectedPage.Options[length] = new Option { Command = command, Key="p", Label="Previous page" };
            return;
        }
        
        if (command == "GOTO_PAGE")
        {
            this.SelectedPage.Options[length] = new Option 
            { 
                Command = command, Key="g", 
                Label="Go to page",
                Arguments = [ new OptionArgument { Key = "page", Value = "1" } ]
            };

            return;
        }

        this.SelectedPage.Options[length] = new Option { Command = command, Key="", Label=command };
    }

    public void DeleteOption(int index)
    {
        var optionCount = this.SelectedPage?.Options?.Length;
        if (this.Books == null || this.SelectedPage?.Options == null || optionCount == null || optionCount == 0 || index < 0 || index >= optionCount)
            // [rgR] should implement custom exceptions
            throw new Exception("Cannot delete something that doesnt exist");

        var optionList = this.SelectedPage.Options.ToList();
        optionList.RemoveAt(index);

        this.SelectedPage.Options = [.. optionList];
    }
}
