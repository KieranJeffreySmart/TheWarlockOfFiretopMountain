using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;

namespace bookeditor.Controls;

public class TextEditModalButton : CompositeControl
{
    public DotvvmControl GetContents(
        [DefaultValue("text")] ValueOrBinding<string> editedText,
        [DefaultValue(null)] ValueOrBinding<string?> modalId,
        [DefaultValue(null)] ValueOrBinding<string?> modalRef,
        ICommandBinding? textChanged = null)
    {
        var editButton = new Button()
        .SetProperty(c => c.ButtonTagName, ButtonTagName.button)
        .AddAttribute("data-toggle", "modal")
        .SetAttribute("data-target", modalRef)
        .SetAttribute("generated-id", modalId)
        .AddCssClasses("btn")
        .AppendChildren(
            new HtmlGenericControl("img")
            .SetAttribute("src", "~/res/bootstrap-icons/pencil.svg")
        );


        var newTextBox = new TextBox()
        .SetProperty(b => b.CssStyles["width"], "450px")
        .SetProperty(b => b.CssStyles["height"], "600px")
        .SetProperty(b => b.Type, TextBoxType.MultiLine)
        .SetProperty(b => b.Text, editedText)
        .SetProperty(b => b.Changed, textChanged);

        

        var modalcontrol = new HtmlGenericControl("div")
        .AddCssClass("modal")
        .AddAttribute("role", "dialog")
        .SetProperty(c => c.ClientIDMode, ClientIDMode.Static)
        .SetProperty(c => c.ID, modalId)
        .AppendChildren(
            new HtmlGenericControl("div")
            .AddCssClass("modal-dialog")
            .AddAttribute("role", "document")
            .AppendChildren(
                new HtmlGenericControl("div")
                .AddCssClass("modal-content")
                .AppendChildren(
                    new HtmlGenericControl("div")
                    .AddCssClass("modal-header")
                    .AppendChildren(
                        new HtmlGenericControl("button")
                        .AddCssClass("close")
                        .AddCssClass("btn")
                        .AddAttribute("type", "button")
                        .AddAttribute("data-dismiss", "modal")
                        .AddAttribute("aria-label", "Close")
                        .AppendChildren(
                            new HtmlGenericControl("span") { InnerText = "x" }
                            .AddAttribute("aria-hidden", "true")
                        )                        
                    ),
                    new HtmlGenericControl("span")
                    .AddCssClass("modal-body")
                    .AppendChildren(newTextBox)
                )
            )
        );        
        
        var container = new HtmlGenericControl("div")
        .AppendChildren(editButton, modalcontrol);

        return container;
    }

}