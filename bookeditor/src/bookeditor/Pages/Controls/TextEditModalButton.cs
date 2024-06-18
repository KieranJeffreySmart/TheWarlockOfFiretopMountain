using System.ComponentModel;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace bookeditor.Controls;

public class TextEditModalButton : CompositeControl
{
    public static DotvvmControl GetContents(
        [DefaultValue("text")] ValueOrBinding<string> editedText,
        [DefaultValue(null)] ValueOrBinding<string?> modalIdRef,
        [DefaultValue(null)] ValueOrBinding<string?> modalId,
        ICommandBinding? changed = null)
    {
        var editButton = new Button()
        .SetProperty(c => c.ButtonTagName, ButtonTagName.button)
        .AddAttribute("data-toggle", "modal")
        .SetAttribute("data-target", modalIdRef)
        .AddCssClasses("btn")
        .SetProperty(c => c.Text, "Edit");

        

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
                        .AddAttribute("type", "button")
                        .AddAttribute("data-dismiss", "modal")
                        .AddAttribute("aria-label", "Close")
                        .AppendChildren(
                            new HtmlGenericControl("span") { InnerText = "x" }
                            .AddAttribute("aria-hidden", "true")
                        )
                    ),
                    new HtmlGenericControl("div")
                    .AddCssClass("modal-body")
                    .AppendChildren(
                        new TextBox()
                        .SetProperty(b => b.Changed, changed)
                        .SetProperty(b => b.CssStyles["width"], "450px")
                        .SetProperty(b => b.CssStyles["height"], "600px")
                        .SetProperty(b => b.Type, TextBoxType.MultiLine)
                        .SetProperty(b => b.Text, editedText)
                    )
                )
            )
        );


        

// <div class="modal" id="exampleModalLong" tabindex="-1" role="dialog">
//     <div class="modal-dialog" role="document">
//       <div class="modal-content">
//         <div class="modal-header">
//           <h5 class="modal-title">Modal title</h5>
//           <button type="button" class="close" data-dismiss="modal" aria-label="Close">
//             <span aria-hidden="true">&times;</span>
//           </button>
//         </div>
//         <div class="modal-body">
//           <p>Modal body text goes here.</p>
//         </div>
//         <div class="modal-footer">
//           <button type="button" class="btn btn-primary">Save changes</button>
//           <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
//         </div>
//       </div>
//     </div>
//   </div>
        
        return new HtmlGenericControl("div")
        .AddCssClass("mod")
        // .AddAttribute("role", "dialog")
        .AppendChildren(editButton, modalcontrol);
    }
}