# YoiBlazor
The simpliest way to build the component.

### Build component
```csharp
[HtmlTag]
public class Container : BlazorComponentBase, IHasChildContent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    
    [Parameter][CssClass("container-fluid")][FalseCssClass("container")] public bool? Fluid { get; set; }
}
```
### Use component
```html
<Container>...</Container>
<Container Fluid="true">...</Container>
```
### Check HTML
```html
<div class="container">...</div>
<div class="container-fluid">...</div>
```


## Installation
> Install-Package YoiBlazor


### [See Document](https://github.com/AchievedOwner/YoiBlazor/wiki)

# Changelogs

### 2.2
* [new]The extension method `BuildCascadingValueComponent` of `RenderTreeBuilder`
* [update]Change the method `AddStyleAttribute` & `AddCssClassAttribute` to be the extension of `IBlazorComponent`
* [update]Downgrade to `.NET Standard 2.0`
* [fix]Missing auto element reference
* [fix]Error summaries and comments.